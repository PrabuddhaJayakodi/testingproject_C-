using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using employeeRegister.Models;



namespace employeeRegister.Controllers
{
    public class ImageController : Controller
    {
       [HttpGet]
        public ActionResult Addimage()
        {
            return View();
        }

        [HttpPost]
         public ActionResult Addimage(Image imageModel)
        {
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.ImagePath = "~/uploadImages/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/uploadImages"), fileName);
            imageModel.ImageFile.SaveAs(fileName);
            using(ImageModels db = new ImageModels())
            {

                db.Images.Add(imageModel);
                db.SaveChanges();
            }
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult Viewimage(int id)
        {
            Image imagemodelobj = new Image();
            using(ImageModels db = new ImageModels())
            {

                imagemodelobj = db.Images.Where(x => x.Id == id).FirstOrDefault();
            }

            return View(imagemodelobj);
        }
    }
}