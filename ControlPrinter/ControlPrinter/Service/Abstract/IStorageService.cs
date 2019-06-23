using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlPrinter.Data;
using MessageBus.Models;

namespace ControlPrinter.Service.Abstract
{
    public interface IStorageService
    {
        void SavePictures(Image pictures);

        Image LoadPicture();
    }
}
