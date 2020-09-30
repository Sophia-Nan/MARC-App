using MARC_App.Container;
using MARC_App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MARC_App.repository
{
    public interface IAdminRep
    {
        IEnumerable<User> GetAllUsers();
        IEnumerable<Category> GetAllCategories();
        IEnumerable<Project> GetAllProjects();
        IEnumerable<Specification> GetAllSpecifications();
        IEnumerable<Instrument> GetAllInstruments();
        IEnumerable<BookInstrument> GetAllBookings();
        //User GetById(int Id);
        IEnumerable<BookInstrument> GETClosed(string status);
        IEnumerable<BookInstrument> GEtApprove(string status);
        IEnumerable<BookInstrument> GEtPending(string status);
        public IEnumerable<User> gett_all_users();


        User AddUser(User obj);
        User EditUser(User obj);
        User DeleteUser(int Id);

        Category AddCategory(Category obj);
        Category EditCategory(Category obj);
        Category DeleteCategory(int Id);

        Project AddProject(Project obj);
        Project EditProject(Project obj);
        Project DeleteProject(int Id);

        Specification AddSpecification(Specification obj);
        Specification EditSpecification(Specification obj);
        Specification DeleteSpecification(int Id);

        Instrument AddInstrument(Instrument obj);
        Instrument EditInstrument(Instrument obj);
        Instrument DeleteInstrument(int Id);

        BookInstrument ApproveBooking(BookInstrument obj);
        BookInstrument CancelBooking(int id);
        BookInstrument UpdateBooking(BookInstrument obj);

    }
}
