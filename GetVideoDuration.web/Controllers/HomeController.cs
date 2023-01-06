using GetVideoDuration.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System.Diagnostics;

namespace GetVideoDuration.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDuration(IFormFile file)
        {
            if (file != null)
            {

                if (file.ContentType != "video/mp4")
                {
                    if (file.ContentType != "video/x-matroska")
                    {
                        ModelState.AddModelError(string.Empty, "Video Type Must Be Mp4 or mkv");
                        return View("Index");
                    }
                    else
                    {
                        string filename = file.Name + Path.GetExtension(file.FileName);
                        string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", filename);
                        using (var stream = new FileStream(filepath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        ViewData["Duration-file"] = Getduration.videoduration(filepath);
                        long size = file.Length / 1000000;
                        ViewData["File Size"] = size;
                        ViewData["File Format"] = file.ContentType;
                        System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", filename));
                        return View("Index");
                    }
                }
                else
                {
                    string filename = file.Name + Path.GetExtension(file.FileName);
                    string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", filename);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    ViewData["Duration-file"] = Getduration.videoduration(filepath);
                    long size = file.Length / 1000000;
                    ViewData["File Size"] = size;
                    ViewData["File Format"] = file.ContentType;
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", filename));
                    return View("Index");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "File Cant Be Null");
                return View("Index");
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}