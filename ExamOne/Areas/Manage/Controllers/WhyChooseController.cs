using ExamOne.DAL;
using ExamOne.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ExamOne.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class WhyChooseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public WhyChooseController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<WhyChoose> chooseItems = _context.whychooses.ToList();
            return View(chooseItems);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(WhyChoose chooseItem)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExist = _context.whychooses.Any(s => s.Title.ToLower().Trim() == chooseItem.Title.ToLower().Trim());
            if (isExist)
            {
                return View(chooseItem);
            }
            _context.whychooses.Add(chooseItem);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            WhyChoose chooseItem = _context.whychooses.Find(id);
            if (chooseItem == null)
            {
                return NotFound();
            }
            _context.whychooses.Remove(chooseItem);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            WhyChoose chooseItem = _context.whychooses.Find(id);
            if (chooseItem == null)
            {
                return NotFound();
            }
            return View(chooseItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,WhyChoose chooseItem)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            WhyChoose chooseItemDb = _context.whychooses.Find(id);
            if (chooseItem == null)
            {
                return NotFound();
            }
            chooseItemDb.Icon = chooseItem.Icon;
            chooseItemDb.Title = chooseItem.Title;
            chooseItemDb.Description = chooseItem.Description;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
