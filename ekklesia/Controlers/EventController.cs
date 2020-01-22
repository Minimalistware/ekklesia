using ekklesia.Models.EventModel;
using Microsoft.AspNetCore.Mvc;

namespace ekklesia.Controlers
{
    public class EventController : Controller
    {

        private readonly IEventRepository repository;

        public EventController(IEventRepository repository)
        {
            this.repository = repository;

        }

        public ViewResult List()
        {
            var members = repository.GetEvents();
            return View();
        }


        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
    }
}