using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.Data.Entities;
using Secret_Santa_MVC.Tools;

namespace Secret_Santa_MVC.Components
{
    public class RoomInfo : ViewComponent
    {
        public IViewComponentResult Invoke(int id)
        {
            PlayTools play = new();
            return View(play.GetListMember(id));
        }
    }
}
