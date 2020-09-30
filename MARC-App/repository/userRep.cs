using MARC_App.Container;
using MARC_App.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MARC_App.repository
{
    public class userRep : IuserRep
    {
        private readonly dbContainer db;
        public userRep(dbContainer db)
        {
            this.db = db;
        }


        public IEnumerable<Instrument> getall_Instrument()
        {
            var data = from Instrument in db.Instruments
                       
                       join
                        category in db.Categories
                    on Instrument.Category.Id equals category.Id
                       select new Instrument
                       {
                           Id = Instrument.Id,
                           InstrumentName = Instrument.InstrumentName,
                           InstrumentDescription = Instrument.InstrumentDescription,
                           Category = category


                       };

            return data.ToList();
        }

        public Instrument GetInstrument(int id)
        {
            var data = from Instrument in db.Instruments
                       where Instrument.Id == id
                       join
                        category in db.Categories
                    on Instrument.Category.Id equals category.Id
                       select new Instrument
                       {
                           Id = Instrument.Id,
                           InstrumentName = Instrument.InstrumentName,
                           InstrumentDescription = Instrument.InstrumentDescription,
                           Category = category


                       };

            return data.First();
        }
        public IEnumerable<Project> GetProjects() {

            var projects = db.Projects;
            return projects;

        }

        public IEnumerable<BookInstrument> get_orders(int userid)
        {

            var data = from BookInstrument in db.BookInstruments
                       where BookInstrument.User.Id == userid
                        select new BookInstrument
                       {

                           Id = BookInstrument.Id,
                           Instrument = db.Instruments.Where(a=>a.Id==BookInstrument.Instrument.Id).SingleOrDefault(),
                           From = BookInstrument.From,
                           To = BookInstrument.To,
                           Project = db.Projects.Where(a=>a.Id==BookInstrument.Project.Id).SingleOrDefault(),
                           ActualWorkingHours = BookInstrument.ActualWorkingHours,
                           Notes = BookInstrument.Notes,
                           AdditionalReq = BookInstrument.AdditionalReq,
                           Approval = BookInstrument.Approval


                       };
            return data.ToList();
                      

        }
        public IEnumerable<Specification> GetSpecifications(){

            return db.Specifications;
        }
        public IEnumerable<Category> GetCategories() {

            return db.Categories;
        }

        public void bookinstrumet(BookInstrument obj)
        {
            obj.Approval.ToLower();
            db.BookInstruments.Add(obj);
            db.SaveChanges();
        }
        public User VlidateUser(string username, string password)
        {
            var data = from user in db.Users
                       where user.Name == username && user.Password == password
                       join usertype in db.Usertypes
                       on user.UserType.Id equals usertype.Id
                       select new User
                       {
                           Id = user.Id,
                           Name = user.Name,
                           Password=user.Password,
                           UserType = usertype

                       };
            return data.SingleOrDefault();
        }
        public IEnumerable<BookInstrument> getordersbyid(int id, string status)
        {
            var data = from book in db.BookInstruments
                       where book.User.Id == id && book.Approval == status
                       select new BookInstrument
                       {
                           Id = book.Id,
                           From =book.From,
                           To=book.To,
                           ActualWorkingHours=book.ActualWorkingHours,
                           Notes=book.Notes,
                           AdditionalReq=book.AdditionalReq,
                           Approval=book.Approval,
                           User=book.User,
                           Instrument=book.Instrument,
                           Project=book.Project
                            

                       };
            return data.ToList();
        }
        public void addreview(ReviewAndScore obj)
        {
            ReviewAndScore r = new ReviewAndScore();
            r.Score = obj.Score;
            r.Review = obj.Review;
            r.Instrument = db.Instruments.Find(obj.Instrument.Id);
            db.ReviewAndScores.Add(r);
            db.SaveChanges();
        }
    }
}
