using MARC_App.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MARC_App.Container
{
    public class dbContainer:DbContext
    {
        public dbContainer(DbContextOptions<dbContainer> options) : base(options)
        {

        }
        public DbSet<UserType> Usertypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<InstrumentSpecification> InstrumentSpecifications { get; set; }
        public DbSet<BookInstrument> BookInstruments { get; set; }
        public DbSet<ReviewAndScore> ReviewAndScores { get; set; }
        public DbSet<InstrumentCondition> InstrumentConditions { get; set; }


    }
}
