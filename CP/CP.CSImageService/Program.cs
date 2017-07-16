
using System;
using System.IO;
using CP.Business;
using CP.Business.Abstract;
using CP.CSImageService.Service;
using CP.CSImageService.Service.Abstract;
using CP.Data;
using System.Data.Entity;
using CP.Storage;
using GreenPipes;
using MassTransit;
using Microsoft.Practices.Unity;
using Serilog;
using Topshelf;

namespace CP.CSImageService
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterType<IImageService, ImageService>();
            container.RegisterType<IImageProcessesService, ImageProcessesService>();
            container.RegisterType<IFileManager, CloudinaryFileManager>();
            container.RegisterType(typeof(IRepository<>), typeof(EfRepository<>));
            container.RegisterType<DbContext, ControlPrinterDbContext>();


            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.LiterateConsole()
                .CreateLogger();
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            Log.Debug("Working directory: {@WorkingDirectory}", AppDomain.CurrentDomain.BaseDirectory);


            Uri uri = new Uri("rabbitmq://localhost");
            IBusControl bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(uri, h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                    h.Heartbeat(300);
                });

                sbc.ReceiveEndpoint(host,"mq", e =>
                {
                    e.Consumer<ImageConsumer>(container);
                    e.UseRetry(c =>
                    {
                        c.Incremental(5, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
                    });
                    e.UseConcurrencyLimit(5);
                    e.PrefetchCount = 2;
                });
            });

            bus.Start();

            HostFactory.Run(x =>
            {
                x.SetServiceName("ImgProc");
                x.Service<ImageServiceControl>();
                x.RunAsLocalSystem();
            });
        }
    }
}
