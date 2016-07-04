using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CP.Data;
using CP.Data.Models;
using CP.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CP.Test
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void RepositoryTest()
        {
            using (var userRepository = new EfRepository<User>(new ControlPrinterDbContext()))
            {
                User user = new User {UserName = "TestName", Password = "test password", RoleId = 1};
                userRepository.Insert(user);
                Assert.IsTrue(user.Id > 0, "User.Id is not set.");

                string changeName = user.UserName;
                user.UserName = "name test";
                userRepository.Update(user);
                Assert.AreNotEqual(changeName,user.UserName,"Name is not change");

                User newUser = userRepository.Table.FirstOrDefault(u => u.Id == user.Id);
                Assert.IsNotNull(newUser, "User does not exist");
                Assert.AreEqual(newUser.Id, user.Id, 0.1, "User ids don't match");
                Assert.AreEqual(user.UserName, newUser.UserName, "Name not equal");
                Assert.AreEqual(user.RoleId, newUser.RoleId,"User role is not equal");

                User userOnId = userRepository.GetById(user.Id);
                Assert.IsNotNull(newUser, "Can't take data on id");

                userRepository.Delete(user);
                User newUserAfterDelete = userRepository.Table.FirstOrDefault(u => u.Id == user.Id);
                Assert.IsNull(newUserAfterDelete, "Delete method doesn't actually delete entity");
            }
        }

        [TestMethod]
        public async Task CloudinaryTest()
        {
            //TODO : Conflict in Web.config with key on cludinary
            var cloudStorage = new CloudinaryFileManager();
            using (var httpClient = new HttpClient())
            using (var imageRepository = new EfRepository<Image>(new ControlPrinterDbContext()))
            {
                var img = imageRepository
                            .Table
                            .OrderByDescending(i => i.DateLoad)
                            .FirstOrDefault();
                    
                Assert.IsNotNull(img,"Can't take first data");
                string uriFile = cloudStorage.GetURI(img.NameImage);
                Assert.IsNotNull(uriFile,"Can't take uri from fileName in Cloudinary storage");
                var result = await httpClient.GetByteArrayAsync(uriFile);
                Assert.IsNotNull(result,"Can't take from Cludinary storage");
                Assert.IsTrue(result.Length > 0,"byte array is empty");
            }
            
        }
    }
}