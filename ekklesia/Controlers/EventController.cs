using ekklesia.Models.EventModel;
using ekklesia.Models.MemberModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

                       

            List<SelectListItem> members = new List<SelectListItem>();
            foreach (var item in memberList)
            {
                members.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }
            
            ViewBag.reunionViewModel = new CreateReunionViewModel(members);
            ViewBag.schoolViewModel = new CreateSundaySchoolViewModel(members);

            return View();
        }

        [HttpPost]
        public IActionResult CreateSundaySchool(CreateSundaySchoolViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("list", "event");
            }
            var memberList = memberRepository
                .GetMembers()
                .OrderBy(m => m.Name)
                .ToList();



            List<SelectListItem> members = new List<SelectListItem>();
            foreach (var item in memberList)
            {
                members.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }

            ViewBag.reunionViewModel = new CreateReunionViewModel(members);
            ViewBag.schoolViewModel = new CreateSundaySchoolViewModel(members);

            return View("Create");
        }


        [HttpPost]
        public IActionResult Create(CreateCultViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cult = new Cult(model);
                repository.Add(cult);
                return RedirectToAction("list", "event");
            }
            

            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(CreateReunionViewModel model)
        {
            if (ModelState.IsValid)
            {                
                var reunion = new Reunion(model);
                repository.Add(reunion);
                return RedirectToAction("list", "event");
            }
            return View("Create");
        }
        [HttpPost]
        public IActionResult CreateSundaySchoolA(CreateSundaySchoolViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.SelectedMembers == null || model.SelectedMembers.Contains("0"))
                {
                    if (model.TeacherId != null || model.TeacherId != "0")
                    {
                        var sundaySchool = new SundaySchool(model);
                        foreach (var id in model.SelectedMembers)
                        {
                            try
                            {
                                var member = memberRepository.GetMember(id);
                                sundaySchool.AddMember(member);
                            }
                            catch (System.Exception)
                            {

                                //return View("Error", new HandleErrorInfo(ex, "Players", "Index"));
                            }

                        }
                        sundaySchool.Speaker = memberRepository.GetMember(model.TeacherId);
                        repository.Add(sundaySchool);
                        return RedirectToAction("list", "event");
                    }

                }

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
                //case EventType.Escola_Dominical:
                //    var editSundaySchoolViewModel = new EditSundaySchoolViewModel(occasion as SundaySchool);
                //    return View("editSundaySchool", editSundaySchoolViewModel);
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