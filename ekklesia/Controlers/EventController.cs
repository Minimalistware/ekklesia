using ekklesia.Models.EventModel;
using ekklesia.Models.MemberModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
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
            List<SelectListItem> members = GetAllMembers();
            ViewBag.reunionViewModel = new CreateReunionViewModel(members);
            ViewBag.schoolViewModel = new CreateSundaySchoolViewModel(members);
            return View();
        }


        [HttpPost]
        public IActionResult CreateCult(CreateCultViewModel model)
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
        public IActionResult CreateReunion(CreateReunionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reunion = new Reunion(model);
                foreach (var id in model.SelectedMembers)
                {
                    var member = memberRepository.GetMember(int.Parse(id));
                    if (member != null)
                    {
                        try
                        {
                            reunion.AddMember(member);

                        }
                        catch (System.Exception ex)
                        {
                            throw new System.Exception("Erro ao adicionar membros presentes.", ex);
                        }
                    }

                }

                var speaker = memberRepository.GetMember(int.Parse(model.TeacherId));
                if (speaker != null)
                {
                    try
                    {
                        reunion.Speaker = speaker;

                    }
                    catch (System.Exception ex)
                    {
                        throw new System.Exception("Erro ao adicionar pregador.", ex);
                    }

                }

                try
                {
                    repository.Add(reunion);
                }
                catch (System.Exception ex)
                {
                    throw new System.Exception("Erro ao salvar Escola Dominical", ex);
                }

                return RedirectToAction("list", "event");
            }

            List<SelectListItem> members = GetAllMembers();
            ViewBag.reunionViewModel = new CreateReunionViewModel(members);
            ViewBag.schoolViewModel = new CreateSundaySchoolViewModel(members);
            return View("Create");
        }

        [HttpPost]
        public IActionResult CreateSundaySchool(CreateSundaySchoolViewModel model)
        {
            if (ModelState.IsValid)
            {
                //if (model.SelectedMembers == null || model.SelectedMembers.Contains("0"))

                //if (model.TeacherId != null || model.TeacherId != "0")

                var sundaySchool = new SundaySchool(model);
                foreach (var id in model.SelectedMembers)
                {
                    var member = memberRepository.GetMember(int.Parse(id));
                    if (member != null)
                    {
                        try
                        {
                            sundaySchool.AddMember(member);

                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Erro ao adicionar membros presentes.", ex);
                        }
                    }

                }

                var teacher = memberRepository.GetMember(int.Parse(model.TeacherId));
                if (teacher != null)
                {
                    try
                    {
                        sundaySchool.Teacher = teacher;

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao adicionar pregador.", ex);
                    }

                }

                try
                {
                    repository.Add(sundaySchool);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao salvar Escola Dominical", ex);
                }

                return RedirectToAction("list", "event");
            }

            List<SelectListItem> members = GetAllMembers();
            ViewBag.reunionViewModel = new CreateReunionViewModel(members);
            ViewBag.schoolViewModel = new CreateSundaySchoolViewModel(members);
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
                    var school = occasion as SundaySchool;
                    var membersList = SetMemberAtOccasion(
                        memberRepository.GetMembersInEvent(school.Id));
                    var model = new EditSundaySchoolViewModel(school, membersList);
                    return View("editSundaySchool", model);
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

        [HttpPost]
        public IActionResult EditSundaySchool(EditSundaySchoolViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sundaySchool = (SundaySchool) repository.GetEvent(model.Id);
                sundaySchool.Date = model.Date;
                sundaySchool.NumberOfBibles = model.NumberOfBibles;
                sundaySchool.Theme = model.Theme;
                sundaySchool.Verse = model.Verse;

                foreach (var id in model.SelectedMembers)
                {
                    var member = memberRepository.GetMember(int.Parse(id));
                    if (member != null)
                    {
                        try
                        {
                            sundaySchool.AddMember(member);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Erro ao adicionar membros presentes.", ex);
                        }
                    }

                }

                var teacher = memberRepository.GetMember(int.Parse(model.TeacherId));
                if (teacher != null)
                {
                    try
                    {
                        sundaySchool.Teacher = teacher;

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao adicionar pregador.", ex);
                    }

                }

                try
                {
                    repository.Update(sundaySchool);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao salvar Escola Dominical", ex);
                }

                return RedirectToAction("list", "event");
            }
            return View("EditSundaySchool", model);           
        }



        public ViewResult Detail(int? id)
        {
            var occasion = repository.GetEvent(id.Value);
            if (occasion == null)
            {
                Response.StatusCode = 404;
                return View("EventNotFound", id.Value);
            }
            CultDetailViewModel model = new CultDetailViewModel((Cult)occasion);
            return View("CultDetail", model);
        }

        private List<SelectListItem> GetAllMembers()
        {
            var memberList = memberRepository
                            .GetMembers()
                            .OrderBy(m => m.Name)
                            .ToList();



            List<SelectListItem> members = new List<SelectListItem>();
            foreach (var item in memberList)
            {
                members.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name

                });
            }

            return members;
        }

        private List<SelectListItem> SetMemberAtOccasion(IEnumerable<int> membersPresents)
        {
            var memberList = memberRepository
                            .GetMembers()
                            .ToList();

            List<SelectListItem> members = new List<SelectListItem>();
            foreach (var member in memberList)
            {
                var item = new SelectListItem
                {
                    Value = member.Id.ToString(),
                    Text = member.Name
                };

                if (membersPresents.Contains(member.Id))
                {
                    item.Selected = true;
                }

                members.Add(item);
            }

            return members;
        }
    }
}