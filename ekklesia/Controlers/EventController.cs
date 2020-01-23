using ekklesia.Models.EventModel;
using ekklesia.Models.ViewModels;
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
            var events = repository.GetEvents();
            return View(events);
        }


        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCult(CompoundCreateEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cult = new Cult(model.CreateCultView);
                repository.Add(cult);
                return RedirectToAction("list", "event");
            }
            return View("Create");
        }
        [HttpPost]
        public IActionResult CreateReunion(CompoundCreateEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reunion = new Reunion(model.CreateReunionView);
                repository.Add(reunion);
                return RedirectToAction("list", "event");
            }
            return View("Create");
        }
        [HttpPost]
        public IActionResult CreateSundaySchool(CompoundCreateEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sundaySchool = new SundaySchool(model.CreateSundaySchoolView);
                repository.Add(sundaySchool);
                return RedirectToAction("list", "event");
            }
            return View("Create");
        }

        [HttpGet]
        public ViewResult EditCult(int Id)
        {
            var cult = (Cult)repository.GetEvent(Id);
            var editCulViewModel = new EditCulViewModel(cult);
            return View(editCulViewModel);
        }

        [HttpPost]
        public IActionResult EditCult(EditCulViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cult = (Cult)repository.GetEvent(model.Id);
                cult.Date = model.Date;
                cult.MainVerse = model.MainVerse;
                repository.Update(cult);
                return RedirectToAction("list", "event");
            }
            return View();

        }
    }
}