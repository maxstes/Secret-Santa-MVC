using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.Models;
using Secret_Santa_MVC.DBClass;

namespace Secret_Santa_MVC.Controllers
{
    public class SantaController : Controller
    {
        //readonly SantaDatabase _database;
        readonly Commands _commands;
        public SantaController()
        {
            // _database = new SantaDatabase();
            _commands = new Commands();
        }

    }
}
