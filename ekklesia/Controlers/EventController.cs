using ekklesia.Models.EventModel;
using ekklesia.Models.MemberModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ekklesia.Controlers
{
    public class EventController : Controller
    {

        private readonly IEventRepository repository;
        private readonly IMemberRepository memberRepository;

        public EventController(IEventRepository repository, IMemberRepository memberRepository)
        {
            this.repository = repository;
            this.memberRepository = memberRepository;
        }

        public ViewResult List()
        {
            var events = repository.GetEvents();
            return View(events);
        }


        [HttpGet]
        public ViewResult Create()
        {
            var memberList = new List<Member>();
            memberList = (from member in memberRepository.GetMembers() select member).ToList();
            ViewBag.MemberList = memberList;
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
        public ViewResult Edit(int Id)
        {
            var occasion = repository.GetEvent(Id);
            switch (occasion.EventType)
            {
                case EventType.Culto:
                    var editCulViewModel = new EditCultViewModel(occasion as Cult);
                    return View("editcult", editCulViewModel);
                case EventType.Escola_Dominical:
                    var editSundaySchoolViewModel = new EditSundaySchoolViewModel(occasion as SundaySchool);
                    return View("editSundaySchool", editSundaySchoolViewModel);
                default:
                    return View();
            }

        }

        [HttpPost]
        public IActionResult EditCult(EditCultViewModel model)
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