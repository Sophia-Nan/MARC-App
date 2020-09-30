using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using MARC_App.Container;
using MARC_App.Entities;
using MARC_App.repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace MARC_App.controlles
{

    public class UserController : Controller
    {

        private readonly dbContainer db;
        
        private readonly IuserRep rep;

        public UserController(IuserRep rep,dbContainer db)
        {
            this.rep = rep;
            this.db = db;
        }

        public IActionResult getinstruments()
        {
            Instrument i = rep.GetInstrument(6);
            
            return View(i);

        }
        public IActionResult get_projects()
        {
            ViewBag.y = rep.GetProjects();
            return View();

        }
        /*public IActionResult add_instrument(string name,string spec,string cat)
        {
            Instrument obj=new Instrument();
            Category cattt = new Category(); 
        
            obj.InstrumentName = name;
            obj.InstrumentDescription = spec;

            var list = db.Categories.Where(a => a.CategoryName == cat);
            cattt = (Category)list;
            obj.Category=cattt;
            
            rep.add_intrument(obj);
          
            return View();
        }*/
        public IActionResult getorders(int id)
        {

           var data = rep.get_orders(id);
            return View(data);
        }
    }
}
