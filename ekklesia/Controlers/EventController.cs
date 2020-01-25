using ekklesia.Models.EventModel;
using ekklesia.Models.MemberModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var memberList = memberRepository
                .GetMembers()
                .OrderBy(m => m.Name)
                .ToList();

            // First, initialize the array to number of teams in playerTeams
            string[] memberListIds = new string[memberList.Count()];

            // Then, set the value of platerTeams.Count so the for loop doesn't need to work it out every iteration
            int length = memberListIds.Count();

            // Now loop over each of the playerTeams and store the Id in the playerTeamsId array
            for (int i = 0; i < length; i++)
            {
                // Note that we employ the ToString() method to convert the Guid to the string
                memberListIds[i] = memberList[i].Id.ToString();
            }

            ViewBag.MemberSelectList = new SelectList(memberList, "Id", "Name");
    
            var MemberMultiSelectList = new MultiSelectList(memberList, "Id", "Name", memberListIds);


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