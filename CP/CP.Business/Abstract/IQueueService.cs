using System;
using System.Threading.Tasks;
using CP.Data.ModelTransport;

namespace CP.Business.Abstract
{
    public interface IQueueService
    {
       Task Send(Uri uri, TransportImageModel image);
    }
}
