using ExamOne.DAL;
using ExamOne.ExtensionMethods;
using ExamOne.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExamOne.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> sliders = _context.sliders.ToList();
            return View(sliders);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();
            bool isExist = _context.sliders.Any(s => s.Title.ToLower().Trim() == slider.Title.ToLower().Trim());
            if (isExist)
            {
                return View(slider);
            }

            string folderName = Path.Combine(_env.WebRootPath, "images");
            slider.Image = slider.ImageFile.SaveImage(folderName);

            _context.sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Slider sliderDb = _context.sliders.Find(id);
            if(sliderDb == null)
            {
                return NotFound();
            }
            _context.sliders.Remove(sliderDb);
            _context.SaveChanges();
            sliderDb.Image.DeleteFile(Path.Combine(_env.WebRootPath, "images"));
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Slider sliderDb = _context.sliders.Find(id);
            if (sliderDb == null)
            {
                return NotFound();
            }

            return View(sliderDb);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Slider slider)
        {
            if (!ModelState.IsValid) return View();
            Slider sliderDb = _context.sliders.Find(id);
            if(sliderDb == null)
            {
                return NotFound();
            }
            sliderDb.Image.DeleteFile(Path.Combine(_env.WebRootPath, "images"));


            string folderName = Path.Combine(_env.WebRootPath, "images");
            slider.Image = slider.ImageFile.SaveImage(folderName);

            sliderDb.Title = slider.Title;
            sliderDb.Position = slider.Position;
            sliderDb.Image = slider.Image;
            sliderDb.FullName = slider.FullName;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }


    
}
