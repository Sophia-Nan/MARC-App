using MARC_App.Container;
using MARC_App.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MARC_App.repository
{
    public class AdminRep : IAdminRep
    {
        private readonly dbContainer db;

        public AdminRep(dbContainer db)
        {
            this.db = db;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var data = db.Users.OrderBy(a => a.Name);
            return data;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var data = db.Categories.OrderBy(a => a.CategoryName);
            return data;
        }

        public IEnumerable<Project> GetAllProjects()
        {
            var data = db.Projects.OrderBy(a => a.ProjectName);
            return data;
        }
        public IEnumerable<Specification> GetAllSpecifications()
        {
            var data = db.Specifications.OrderBy(a => a.SpecificationName);
            return data;
        }

        public IEnumerable<Instrument> GetAllInstruments()
        {
            var data = db.Instruments.OrderBy(a => a.InstrumentName);
            return data;
        }

        public IEnumerable<BookInstrument> GetAllBookings()
        {
            DateTime now = DateTime.Now;

            //var data = db.BookInstruments.OrderBy(a => a.From).OrderBy(b => b.Project.Priority);
            //return data;
            var data = from BookInstrument in db.BookInstruments.OrderBy(a=>a.From).OrderBy(b => b.Project.Priority)
                      where BookInstrument.Approval== "pending"
                       select new BookInstrument
                       {

                           Id = BookInstrument.Id,
                           Instrument = db.Instruments.Where(a => a.Id == BookInstrument.Instrument.Id).SingleOrDefault(),
                           From = BookInstrument.From,
                           To = BookInstrument.To,
                           Project = db.Projects.Where(a => a.Id == BookInstrument.Project.Id).SingleOrDefault(),
                           ActualWorkingHours = BookInstrument.ActualWorkingHours,
                           Notes = BookInstrument.Notes,
                           AdditionalReq = BookInstrument.AdditionalReq,
                           Approval = BookInstrument.Approval,
                           User=db.Users.Where(a=>a.Id==BookInstrument.User.Id).SingleOrDefault()


                       };
            return data.ToList();
          

        }
        public IEnumerable<BookInstrument> GEtPending(string status)
        {
            status.ToLower();
            var data = db.BookInstruments.Where(a => a.Approval == status).ToList();
            return data;
        }
        public IEnumerable<BookInstrument> GEtApprove(string status)
        {
            status.ToLower();
            var data = db.BookInstruments.Where(a => a.Approval == status).ToList();
            return data;
        }
        public IEnumerable<BookInstrument> GETClosed(string status)
            
        {
            status.ToLower();
            var data = db.BookInstruments.Where(a => a.Approval == status).ToList();
            return data;
        }










        private BookInstrument InvalidOperationException(string v)
        {
            throw new NotImplementedException();
        }

        public BookInstrument ApproveBooking(BookInstrument obj)
        {
            obj.Approval.ToLower();
            obj.Instrument = db.Instruments.Find(obj.Instrument.Id);
            obj.User = db.Users.Find(obj.User.Id);
            obj.Project = db.Projects.Find(obj.Project.Id);
            int id = obj.Instrument.Id;
            InstrumentCondition IC = db.InstrumentConditions.Where(a=>a.Instrument.Id==id).FirstOrDefault();
            var olddata = db.BookInstruments.Find(obj.Id);

            var temp = from book in db.BookInstruments.Where(a => a.Instrument.Id == obj.Instrument.Id)

                       select new BookInstrument
                       {
                           From = book.From,
                           To = book.To,
                           Approval=book.Approval
                       };
            bool t1 = true;
            var temp2 = temp.ToList();
            foreach (var item in temp2)
            {
                if ((obj.From >= item.From && obj.From <= item.To) || (obj.To >= item.From && obj.To <= item.To))
                {
                    if (item.Approval == "approved")
                    { t1 = false;
                        break;
                    }
                }

            }
            if (t1 == true)
            {
                olddata.From = olddata.From;
                olddata.To = olddata.To;
                olddata.ActualWorkingHours = olddata.ActualWorkingHours;
                olddata.Approval = "approved";
                olddata.Notes = olddata.Notes;
                olddata.AdditionalReq = olddata.AdditionalReq;
                olddata.Instrument = obj.Instrument;
                olddata.User = olddata.User;
                olddata.Project = olddata.Project;
                //IC.Condition = false;
                db.SaveChanges();
                return obj;
            }
            else
            {
                BookInstrument i = new BookInstrument();    
                return i;
            }

        }

        public BookInstrument CancelBooking(int id)
        {

            var data=db.BookInstruments.Find(id);
            db.BookInstruments.Remove(data);
            db.SaveChanges();
            return data;
        }

        public BookInstrument UpdateBooking(BookInstrument obj)
        {
            obj.Approval.ToLower();
            obj.Instrument = db.Instruments.Find(obj.Instrument.Id);
            obj.User = db.Users.Find(obj.User.Id);
            obj.Project = db.Projects.Find(obj.Project.Id);
            int id = obj.Instrument.Id;
            InstrumentCondition IC = db.InstrumentConditions.Where(a => a.Instrument.Id == id).FirstOrDefault();

                var olddata = db.BookInstruments.Find(obj.Id);
            
                olddata.From = obj.From;
                olddata.To = obj.To;
                olddata.ActualWorkingHours = obj.ActualWorkingHours;
                olddata.Approval = obj.Approval;
                olddata.Notes = obj.Notes;
                olddata.AdditionalReq = obj.AdditionalReq;
                olddata.Instrument = obj.Instrument;
                olddata.User = olddata.User;
                olddata.Project = olddata.Project;

                db.SaveChanges();
                return obj;
            

        }







        public User AddUser(User obj)
        {
            var data = db.Users.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public User DeleteUser(int Id)
        {
            var olddata = db.Users.Find(Id);
            
            db.Users.Remove(olddata);
            db.SaveChanges();
            return olddata;

        }

        public User EditUser(User obj)
        {
            var olddata = db.Users.Find(obj.Id);

            olddata.Name = obj.Name;
            olddata.Password = obj.Password;
            olddata.UserType = db.Usertypes.Find(obj.UserType.Id);

            db.SaveChanges();
            return obj;

        }


        public IEnumerable<User> gett_all_users()
        {
            var data = from user in db.Users
                       join
                       type in db.Usertypes
                       on user.UserType.Id equals type.Id
                       select new User
                       {
                           Id = user.Id,
                           Name = user.Name,
                           Password = user.Password,
                           UserType = type
                       };
            return data.ToList();
        }




        public Project AddProject(Project obj)
        {
            var data = db.Projects.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public Project DeleteProject(int Id)
        {
            var olddata = db.Projects.Find(Id);

            db.Projects.Remove(olddata);
            db.SaveChanges();
            return olddata;

        }

        public Project EditProject(Project obj)
        {
            var olddata = db.Projects.Find(obj.Id);

            olddata.Code = obj.Code;
            olddata.ProjectName = obj.ProjectName;
            olddata.ActivePi = obj.ActivePi;
            olddata.DosageForm = obj.DosageForm;
            olddata.Concentration = obj.Concentration;
            olddata.Priority = obj.Priority;

            db.SaveChanges();
            return obj;

        }







        public Instrument AddInstrument(Instrument obj)
        {
            db.Instruments.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public Instrument DeleteInstrument(int Id)
        {
            var olddata = db.Instruments.Find(Id);

            db.Instruments.Remove(olddata);
            db.SaveChanges();
            return olddata;

        }

        public Instrument EditInstrument(Instrument obj)
        {
            var olddata = db.Instruments.Find(obj.Id);

            olddata.InstrumentName = obj.InstrumentName;
            olddata.InstrumentDescription = obj.InstrumentDescription;
            olddata.Category = db.Categories.Find(obj.Category.Id);

            db.SaveChanges();
            return obj;

        }





        //tmm m3ada del 

        public Specification AddSpecification(Specification obj)
        {
            var data = db.Specifications.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public Specification DeleteSpecification(int Id)
        {
            var olddata = db.Specifications.Find(Id);

            db.Specifications.Remove(olddata);
            db.SaveChanges();
            return olddata;

        }

        public Specification EditSpecification(Specification obj)
        {
            var olddata = db.Specifications.Find(obj.Id);

            olddata.SpecificationName = obj.SpecificationName;
            olddata.SpecificationDescription = obj.SpecificationDescription;

            db.SaveChanges();
            return obj;

        }







        public Category AddCategory(Category obj)
        {
            
            var data = db.Categories.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public Category DeleteCategory(int Id)
        {
            var olddata = db.Categories.Find(Id);

            db.Categories.Remove(olddata);
            db.SaveChanges();
            return olddata;

        }

        public Category EditCategory(Category obj)
        {
            var olddata = db.Categories.Find(obj.Id);

            olddata.CategoryName = obj.CategoryName;
            olddata.CategoryDescription = obj.CategoryDescription;

            db.SaveChanges();
            return obj;

        }
    }
}
