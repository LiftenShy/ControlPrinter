﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services.Description;
using CP.Data;
using CP.Data.Models;
using  CP.Storage;
using CP.Web.Models;

namespace CP.Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "user,admin")]

        public ActionResult Index()
        {
            /*using (IRepository<Data.Models.Image> imageRepository = new EfRepository<Data.Models.Image>())
            {
                if (imageRepository.Table.Any())
                {
                    Data.Models.Image img = imageRepository.GetById(1);
                    return this.View("Index",new ImageModel { FileName = img.NameImage, FilePath = "~/Images/" + img.NameImage });
                }
            }*/
            return this.View();
        }

        [HttpPost]
        public ActionResult IndexPost(HttpPostedFileBase fileBase)
        {
            IFileManager fileManager = new LocalFileManager(this.Server.MapPath(Path.Combine("~/Images/")));
            fileManager.SaveFile(fileBase.FileName,fileBase.InputStream);
            ImageModel img = new ImageModel {FileName = fileBase.FileName, FilePath = this.Server.MapPath(Path.Combine("~/Images/", fileBase.FileName))};
            return this.View("Index",img);
        }

        [HttpPost]
        [ActionName("android")]
        public ActionResult AndroidPost(byte[] fileBase)
        {
            ///IFileManager fileManager = new LocalFileManager();
            //fileManager.SaveFile("pic",pic);
            //ImageModel img = new ImageModel {FileName = "pic", FilePath = pic};
            return this.View("Index"); //,img);
        }
    }
}