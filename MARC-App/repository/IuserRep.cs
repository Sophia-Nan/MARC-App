using MARC_App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MARC_App.repository
{
   public interface IuserRep
    {

        IEnumerable<Instrument> getall_Instrument();
        IEnumerable<Project> GetProjects();
        IEnumerable<BookInstrument> get_orders(int userid);
        IEnumerable<Specification> GetSpecifications();
        IEnumerable<Category> GetCategories();
        Instrument GetInstrument(int id);

        void bookinstrumet(BookInstrument obj);
        
         public User VlidateUser(string username, string password);
        public IEnumerable<BookInstrument> getordersbyid(int id, string staus);
        public void addreview(ReviewAndScore obj);



    }
}
