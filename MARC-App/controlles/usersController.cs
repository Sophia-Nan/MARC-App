using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MARC_App.Container;
using MARC_App.Entities;
using MARC_App.repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Http;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System.Data.SqlTypes;

namespace MARC_App.controlles
{   //[RoutePrefix("api/user")]
    //[Route("api/instruments")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly dbContainer db;

        private readonly IuserRep rep;
        private readonly IAdminRep rep2;

        public usersController(IuserRep rep, dbContainer db, IAdminRep rep2)
        {
            this.rep2 = rep2;
            this.rep = rep;
            this.db = db;
        }

        [Route("api/getinstruments")]
        [HttpGet]
        public ActionResult getinstruments()
        {
            var x = rep.getall_Instrument();
            return new JsonResult(x);


        }

        [Route("api/getprojects")]
        [HttpGet]
        public ActionResult getprojects()
        {

            var x = rep.GetProjects();
            return Ok(x);
        }


        [Route("api/getbyid/{Id}")]
        [HttpGet("Id")]
        public ActionResult getbyid(int Id)
        {

            var data = rep.GetInstrument(Id);
            return Ok(data);
        }

        [Route("api/getcategories")]
        [HttpGet]
        public ActionResult getcategories()
        {

            var data = rep.GetCategories();
            return Ok(data);
        }

        [Route("api/GetSpecifications")]

        [HttpGet]
        public ActionResult GetSpecifications()
        {

            var data = rep.GetSpecifications();
            return Ok(data);
        }
        [Route("api/get_orders/{userid}")]
        [HttpGet("{userid}")]
        public ActionResult get_orders(int userid)
        {
            var data = rep.get_orders(userid);
            return Ok(data);
        }
        [Route("api/login")]
        [HttpPost]
        public ActionResult login([FromBody] User obj)
        {
            var data = rep.VlidateUser(obj.Name, obj.Password);
            return Ok(data);
        }
        [Route("api/book")]
        [HttpPost]
        public ActionResult book([FromBody] BookInstrument obj)
        {
            BookInstrument b2 = new BookInstrument();
            b2.From = obj.From;
            b2.To = obj.To;
            b2.ActualWorkingHours = obj.ActualWorkingHours;
            b2.Approval = obj.Approval;
            b2.Notes = obj.Notes;
            b2.AdditionalReq = obj.AdditionalReq;

            b2.Instrument = db.Instruments.Find(obj.Instrument.Id);

            b2.User = db.Users.Find(obj.User.Id);
            b2.Project = db.Projects.Find(obj.Project.Id);


            rep.bookinstrumet(b2);

            return Ok(b2);


        }
        [Route("api/GetAllBooking")]
        [HttpGet]
        public IActionResult GetAllBookings()
        {

            var data = rep2.GetAllBookings();
            return new JsonResult(data);
        }
        [Route("api/ApproveBooking")]
        [HttpPut]
        public IActionResult ApproveBooking([FromBody] BookInstrument obj)
        {

            var data = rep2.ApproveBooking(obj);
            if (data.Id !=0 )
                return Ok();
            else
            {   
                return NotFound();
            }
        }

        [Route("api/CancelBooking")]
        [HttpDelete]
        public IActionResult CancelBooking([frombody] BookInstrument obj)
        {
            int id = obj.Id;
            var data = rep2.CancelBooking(id);
            return Ok();
        }

        [Route("api/UpdateBooking")]
        [HttpPut]
        public IActionResult UpdateBooking([FromBody] BookInstrument obj)
        {
            var data = rep2.UpdateBooking(obj);
            return Ok();
        }
        [Route("api/Pending/{s}")]
        [HttpGet("{s}")]
        public IActionResult Pending(string s)
        {
            var data = rep2.GEtPending(s);
            return Ok(data);
        }
        [Route("api/Approved/{s}")]
        [HttpGet("{s}")]
        public IActionResult Approved(string s)
        {
            var data = rep2.GEtApprove(s);
            return Ok(data);
        }
        [Route("api/closed/{s}")]
        [HttpGet("{s}")]
        public IActionResult closed(string s)
        {
            var data = rep2.GETClosed(s);
            return Ok(data);
        }
        [Route("api/AddUser")]
        [HttpPost]
        public IActionResult AddUser([FromBody] User obj)
        {
            User temp = new User();
            temp.Name = obj.Name;
            temp.Password = obj.Password;
            temp.UserType = db.Usertypes.Find(obj.UserType.Id);
            var data = rep2.AddUser(temp);
            return Ok();
        }
        [Route("api/DeleteUser")]
        [HttpDelete]
        public IActionResult DeleteUser([FromBody] User obj)
        {
            int id = obj.Id;
            var data = rep2.DeleteUser(id);
            return Ok();
        }
        [Route("api/Edituser")]
        [HttpPut]
        public IActionResult Edituser([FromBody] User obj)
        {
            var data = rep2.EditUser(obj);
            return Ok();
        }
        [Route("api/AddProject")]
        [HttpPost]
        public IActionResult AddProject([FromBody] Project obj)
        {
            Project temp = new Project();
            temp.ProjectName = obj.ProjectName;
            temp.ActivePi = obj.ActivePi;
            temp.Priority = obj.Priority;
            temp.Code = obj.Code;
            temp.Concentration = obj.Concentration;
            temp.DosageForm = obj.DosageForm;
            var data = rep2.AddProject(temp);
            return Ok();
        }
        [Route("api/DeleteProject")]
        [HttpDelete]
        public IActionResult DeleteProject([FromBody]  Project obj)
        {
            int id = obj.Id;
            var data = rep2.DeleteProject(id);
            return Ok();
        }

        [Route("api/EditProject")]
        [HttpPut]
        public IActionResult EditProject([FromBody] Project obj)
        {
            var data = rep2.EditProject(obj);
            return Ok();
        }

        //lsa

        [Route("api/AddInstrument")]
        [HttpPost]
        public IActionResult AddInstrument([FromBody] Instrument obj)
        {
            Instrument temp = new Instrument();
            temp.InstrumentName = obj.InstrumentName;
            temp.InstrumentDescription = obj.InstrumentDescription;
            temp.Category = db.Categories.Find(obj.Category.Id);
            var data = rep2.AddInstrument(temp);
            return Ok();
        }

        [Route("api/DeleteInstrument")]
        [HttpDelete]
        public IActionResult DeleteInstrument([FromBody] Instrument obj)
        {
            int id = obj.Id;
            var data = rep2.DeleteInstrument(id);
            return Ok();
        }

        [Route("api/EditInstrument")]
        [HttpPut]
        public IActionResult EditInstrument([FromBody] Instrument obj)
        {
            var data = rep2.EditInstrument(obj);
            return Ok();
        }




        //tmm m3ada del 
        [Route("api/AddSpecification")]
        [HttpPost]
        public IActionResult AddSpecification([FromBody] Specification obj)
        {
            Specification temp = new Specification();
            temp.SpecificationDescription = obj.SpecificationDescription;
            temp.SpecificationName = obj.SpecificationName;
            var data = rep2.AddSpecification(temp);
            return Ok();
        }

        [Route("api/DeleteSpecification")]
        [HttpDelete]
        public IActionResult DeleteSpecification([FromBody] Specification obj)
        {
            int id = obj.Id;
            var data = rep2.DeleteSpecification(id);
            return Ok();
        }

        [Route("api/EditSpecification")]
        [HttpPut]
        public IActionResult EditSpecification([FromBody]Specification obj)
        {
           var data= rep2.EditSpecification(obj);
            return Ok();
        }





        //dol tmm m3ada del 

        [Route("api/AddCategory")]
        [HttpPost]
        public IActionResult AddCategory([FromBody]Category obj)
        {
            Category temp = new Category();
            temp.CategoryName = obj.CategoryName;
            temp.CategoryDescription = obj.CategoryDescription;
            var data=rep2.AddCategory(temp);
            return Ok();

        }

        [Route("api/DeleteCategory")]
        [HttpDelete]
        public IActionResult DeleteCategory([FromBody] Category obj)
        {
            int id = obj.Id;
           var data= rep2.DeleteCategory(id);
            return Ok();
        }

        [Route("api/EditCategory")]
        [HttpPut]
        public IActionResult EditCategory([FromBody]Category obj)
        {
            var data=rep2.EditCategory(obj);
            return Ok();
        }


        [Route("api/getordersbyid/{id}/{status}")]
        [HttpGet("{id}/{status}")]
        public IActionResult getordersbyid(int id,String status)
        {
            var data = rep.getordersbyid(id, status);
            return Ok(data);
        }
        [Route("api/gett_all_users")]
        [HttpGet]
        public IActionResult gett_all_users()
        {
            var data=rep2.gett_all_users();
            return Ok(data);
        }
        [Route("api/addreview")]
        [HttpPost]
        public IActionResult addreview([frombody] ReviewAndScore obj)
        {
            rep.addreview(obj);
            return Ok();
        }

    }
}