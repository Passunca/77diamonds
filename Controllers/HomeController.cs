using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using developerBackEndTest.Models;

namespace developerBackEndTest.Controllers
{
    public class HomeController : Controller
    {
        diamondEntities db = new diamondEntities();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View(db);
        }


        [HttpGet]
        public ActionResult Create(int id)
        {
            ViewBag.Title = "AddImg Page";

            ItemPhoto img = db.ItemPhotos.Find(id);

            if (img == null)
            {
                return HttpNotFound();
            }

            return View(img);
        }

        [HttpPost]
        public ActionResult Create(ItemPhoto img)
        {
            if (img != null)
            {
                string path = uploadedImg(img.imageFile);

                if(path == string.Empty)
                {

                }
                else
                {
                    ItemPhoto newPhoto = new ItemPhoto()
                    {
                        FileName = img.imageFile.FileName,
                        Type = img.Type,
                        TypeId = img.TypeId,
                        IsActive = img.IsActive,
                        Item = img.Item,
                        ItemId = img.ItemId,
                        Position = img.Position,
                        CreatedAt = DateTime.Now
                    };


                    using (db)
                    {
                        db.ItemPhotos.Add(newPhoto);


                        List<ItemPhotoPropertySet> photoPropertySets =  db.ItemPhotoPropertySets.Where(x => x.ItemPhotoId == img.Id).ToList();

                        foreach(var photoPropertySet in photoPropertySets)
                        {
                            ItemPhotoPropertySet newPopertySet = new ItemPhotoPropertySet()
                            {
                                ItemPhotoId = newPhoto.Id,
                                PropertyId = photoPropertySet.PropertyId,
                                Value = photoPropertySet.Value
                            };
                        

                            db.ItemPhotoPropertySets.Add(newPopertySet);
                        }

                        db.SaveChanges();
                       
                    }
                    ModelState.Clear();

                }

            }
            return View();
        }

        public string uploadedImg(HttpPostedFileBase uploadedImg)
        {
            string path = "";

            if (uploadedImg != null && uploadedImg.ContentLength > 0)
            {
                string fileExt = Path.GetExtension(uploadedImg.FileName);

                if (fileExt == ".jpg")
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName(uploadedImg.FileName));
                        uploadedImg.SaveAs(path);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            return path;
        }
    }
}
