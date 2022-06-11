
using ExamOne.DAL;
using ExamOne.Models;
using ExamOne.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ExamOne.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context; 
        }
        public IActionResult Index()
        {
            List <Slider> sliders = _context.sliders.ToList();
            List <WhyChoose> chooseItems = _context.whychooses.ToList();
            _HomeVM HomeVM = new _HomeVM()
            {
                sliders = sliders,
                chooseItems = chooseItems
            };
            return View(HomeVM);
        }

       
    }
}
