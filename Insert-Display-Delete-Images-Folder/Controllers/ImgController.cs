using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insert_Display_Delete_Images_Folder.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Insert_Display_Delete_Images_Folder.Controllers
{
    public class ImgController : Controller
    {
        private readonly IWebHostEnvironment _iweb;
        private object fileInfo;

        public ImgController(IWebHostEnvironment iweb)
        {
            _iweb = iweb;
        }
        public IActionResult Index()
        {
            ImageClass ic = new ImageClass();
            var displayimg = Path.Combine(_iweb.WebRootPath, "Images");
            DirectoryInfo di = new DirectoryInfo(displayimg);
            FileInfo[] fileInfos = di.GetFiles();
            ic.Fileimage = fileInfo;
            return View(ic);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile imgFile)
        {
            string ext = Path.GetExtension(imgFile.FileName);
            if(ext==".jpg" || ext==".gif")
            {
                var imgsave = Path.Combine(_iweb.WebRootPath, "Images", imgFile.FileName);
                var stream = new FileStream(imgsave, FileMode.Create);
                await imgFile.CopyToAsync(stream);
                stream.Close();
            }
            return RedirectToAction("Index");
        }
    }
}
