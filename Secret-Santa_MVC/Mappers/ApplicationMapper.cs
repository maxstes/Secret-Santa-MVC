using Secret_Santa_MVC.Data.Entities;
using Secret_Santa_MVC.Models;

namespace Secret_Santa_MVC.Mappers
{
    public static class ApplicationMapper
    {
#warning Tut koment
        //public static Application ToEntity(this FullApplicationModel app)
        //{
        //    return new Application
        //    {
        //        SampleData = new SampleData
        //        {
        //            Anamnesis = ""
        //        }
        //    };
        //}
        public static FullApplicationModel ToFullApplication(this Application app)
        {
            return new FullApplicationModel();
        }
        public static ShortApplicationModel ToShortApplication(this Application app)
        {
            return new ShortApplicationModel();
        }
    }
}
