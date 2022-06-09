using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U20494000_HW3_272FINAL.Models;

namespace U20494000_HW3_272FINAL.Controllers
{
    public class SystemController : Controller
    {
        // GET: System
        [HttpGet]
        public ActionResult Home()//Home view is returned via ActionResult()
        {
            return View();
        }

        //Retrieves chosen radio button option and sends uploaded file name to files view:
        [HttpPost]
        public ActionResult Home(HttpPostedFileBase file, string optradio, FormCollection collection)
        {
            //Recieving selected option:
            string option = Convert.ToString(collection["optradio"]);

            //if statement: for the chosen option and what type of file it is saved and passed as:
            if (option == "Document")
            {
                file.SaveAs(Path.Combine(Server.MapPath(" ~/Content/Media/Documents"), Path.GetFileName(file.FileName)));

            }
            else if (option == "Image")
            {
                file.SaveAs(Path.Combine(Server.MapPath(" ~/Content/Media/Images"), Path.GetFileName(file.FileName)));
            }
            else
            {
                file.SaveAs(Path.Combine(Server.MapPath(" ~/Content/Media/Videos"), Path.GetFileName(file.FileName)));
            }

            return RedirectToAction("Home");
        }

        public ActionResult Files()
        {
            //Files are stored as string values and chosen option and upload is retrieved and stored in FileModel list:
            List<FileModel> files = new List<FileModel>();
            string[] Documents = Directory.GetFiles(Server.MapPath("~/Content/Media/Documents"));
            string[] Images = Directory.GetFiles(Server.MapPath("~/Content/Media/Images"));
            string[] Videos = Directory.GetFiles(Server.MapPath("~/Content/Media/Videos"));

            //foreach loop is used to store chosen file into FileModel list:
            foreach (var file in Documents)
            {
                FileModel fileChosen = new FileModel();
                fileChosen.FileName = Path.GetFileName(file);
                fileChosen.FileOption = "document";
                files.Add(fileChosen);

            }


            foreach (var file in Images)
            {
                FileModel fileChosen = new FileModel();
                fileChosen.FileName = Path.GetFileName(file);
                fileChosen.FileOption = "image";
                files.Add(fileChosen);

            }

            foreach (var file in Videos)
            {
                FileModel fileChosen = new FileModel();
                fileChosen.FileName = Path.GetFileName(file);
                fileChosen.FileOption = "video";
                files.Add(fileChosen);

            }

            return View(files);
        }


        //The functioning of the download button and dowmloading of chosen file:

        public FileResult DownloadFile(string file, string fileOption)
        {
            //byte array to read and store uploaded information via the download button:
            byte[] Bytes = null;

            //Option of the file chosen to download if statement:
            if (fileOption == "document")
            {
                Bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/ Content / Media / Documents/") + file);
            }

            else if (fileOption == "image")
            {
                Bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/ Content / Media / Images/") + file);
            }

            else
            {
                Bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/ Content / Media / Videos/") + file);
            }

            return File(Bytes, "application/octet stream", file);


        }
        //The delete button function that removes the uploaded file from the bootstrap table on files view:
        public ActionResult DeleteFile(string file, string fileOption)
        {
            string chosenfile = null;
            //if statement for deleted option:
            if (fileOption == "document")
            {
                chosenfile = Server.MapPath("~/ Content / Media / Documents/") + file;
            }

            else if (fileOption == "image")
            {
                chosenfile = Server.MapPath("~/ Content / Media / Images/") + file;
            }
            else
            {
                chosenfile = Server.MapPath("~/ Content / Media / Videos/") + file;
            }

            //Deletes the chosen option and redirects to home view:
            System.IO.File.Delete(chosenfile);
            return RedirectToAction("Home");
        }

        //ActionResult function stores image data into an array thereby adding retrieved information into FileModel list:
        public ActionResult Images()
        {

            List<FileModel> Images = new List<FileModel>();
            string[] FileImage = Directory.GetFiles(Server.MapPath("~/Content/Media/Images"));
            foreach (var file in FileImage)
            {
                FileModel fileChosen = new FileModel();
                fileChosen.FileName = Path.GetFileName(file);
                fileChosen.FileOption = "image";
                Images.Add(fileChosen);

            }
            return View(Images);
        }
        //ActionResult function stores video data into an array thereby adding retrieved information into FileModel list:
        public ActionResult Videos()
        {
            List<FileModel> Videos = new List<FileModel>();
            string[] FileVideo = Directory.GetFiles(Server.MapPath("~/Content/Media/Videos"));
            foreach (var file in FileVideo)
            {
                FileModel fileChosen = new FileModel();
                fileChosen.FileName = Path.GetFileName(file);
                fileChosen.FileOption = "video";
                Videos.Add(fileChosen);
            }
            return View(Videos);
        }
        //Return About Me View:
        public ActionResult AboutMe()
        {
            return View();
        }




    }
}