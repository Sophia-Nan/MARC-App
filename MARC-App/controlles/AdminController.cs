using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MARC_App.repository;
using Microsoft.AspNetCore.Mvc;

namespace MARC_App.controlles
{
    public class AdminController : Controller
    {
        private readonly IAdminRep rep;

        public AdminController(IAdminRep rep)
        {
            this.rep = rep;
        }

        public IActionResult GetAllUsers()
        {
            ViewBag.a = rep.GetAllUsers();
            return View();
        }

        public IActionResult GetAllCategories()
        {
            ViewBag.b = rep.GetAllCategories();
            return View();
        }

        public IActionResult GetAllProjects()
        {
            ViewBag.c = rep.GetAllProjects();
            return View();
        }

        public IActionResult GetAllSpecifications()
        {
            ViewBag.d = rep.GetAllSpecifications();
            return View();
        }

        public IActionResult GetAllInstruments()
        {
            ViewBag.e = rep.GetAllInstruments();
            return View();
        }

        public IActionResult GetAllBookings()
        {
            ViewBag.f = rep.GetAllBookings();
            return View();
        }
    }
}
