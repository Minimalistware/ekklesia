using ekklesia.Models.EventModel;
using ekklesia.Models.MemberModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
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
            return ReloadDataAndReturnView();
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
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao adicionar pregador.", ex);
                    }

                }

                try
                {
                    repository.Add(reunion);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao salvar Escola Dominical", ex);
                }

                return RedirectToAction("list", "event");
            }

            return ReloadDataAndReturnView();
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

            return ReloadDataAndReturnView();
        }

        [HttpPost]
        public IActionResult CreateBaptism(BaptismCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var baptism = new Baptism(model);
                foreach (var id in model.BaptizedMembersIds)
                {
                    var member = memberRepository.GetMember(int.Parse(id));
                    if (member != null)
                    {
                        try
                        {
                            baptism.AddMember(member);

                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Erro ao adicionar membros batizados.", ex);
                        }
                    }

                }
                try
                {
                    repository.Add(baptism);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao salvar Escola Dominical", ex);
                }

                return RedirectToAction("list", "event");

            }
            return ReloadDataAndReturnView();
        }

        [HttpPost]
        public IActionResult CreateCell(CellCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cell = new Cell(model);
                try
                {
                    repository.Add(cell);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao salvar Célula", ex);
                }
                return RedirectToAction("list", "event");
            }
            return ReloadDataAndReturnView();
        }

        [HttpGet]
        public ViewResult Edit(int Id)
        {
            var occasion = repository.GetEvent(Id);
            if (occasion == null)
            {
                ViewBag.ErrorMessage = $"Evento com Id: {Id} não pode ser encontrado";
                return View("NotFound");
            }
            switch (occasion.EventType)
            {
                case EventType.CULTO:
                    var editCulViewModel = new EditCultViewModel(occasion as Cult);
                    return View("Editcult", editCulViewModel);
                case EventType.ESCOLA_DOMINICAL:
                    var schoolmodel = new EditSundaySchoolViewModel(occasion as SundaySchool);
                    schoolmodel = ConfigureLists(schoolmodel);
                    return View("EditSundaySchool", schoolmodel);
                case EventType.REUNIÃO:
                    var reunionmodel = new EditReunionViewModel(occasion as Reunion);
                    reunionmodel = ConfigureLists(reunionmodel);
                    return View("EditReunion", reunionmodel);
                case EventType.BATISMO:
                    var baptismmodel = new BaptismEditViewModel(occasion as Baptism);
                    baptismmodel = ConfigureLists(baptismmodel);
                    return View("EditBaptism", baptismmodel);

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
                var sundaySchool = (SundaySchool)repository.GetEvent(model.Id);
                sundaySchool.Date = model.Date;
                sundaySchool.NumberOfBibles = model.NumberOfBibles;
                sundaySchool.Theme = model.Theme;
                sundaySchool.Verse = model.Verse;

                var teacher = memberRepository.GetMember(int.Parse(model.TeacherId));
                if (teacher != null)
                {
                    sundaySchool.Teacher = teacher;

                }

                try
                {
                    repository.Update(sundaySchool);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Erro ao atualizar Escola Dominical", ex.Message);
                }

                return RedirectToAction("list", "event");
            }
            return View("EditSundaySchool", model);
        }

        [HttpPost]
        public IActionResult EditReunion(EditReunionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reunion = repository.GetEvent(model.Id) as Reunion;
                reunion.Date = model.Date;
                reunion.EndTime = model.EndTime;
                reunion.Topic = model.Topic;

                var speaker = memberRepository.GetMember(int.Parse(model.TeacherId));
                if (speaker != null)
                {
                    reunion.Speaker = speaker;
                }

                try
                {
                    repository.Update(reunion);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Erro ao atualizar Reunião", ex.Message);
                }

                return RedirectToAction("list", "event");
            }

            return View("EditReunion", model);
        }

        [HttpPost]
        public IActionResult EditBaptism(BaptismEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var baptism = repository.GetEvent(model.Id) as Baptism;
                baptism.Date = model.Date;
                baptism.Place = model.Place;
                baptism.BaptizerId = model.BaptizerId;

                try
                {
                    repository.Update(baptism);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Erro ao atualizar Batismo", ex.Message);
                }

                return RedirectToAction("list", "event");
            }
            return View("EditBaptism", model);
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



        [HttpGet]
        public IActionResult EditMembersInEvent(string eventId)
        {

            var occasion = repository.GetEvent(int.Parse(eventId));
            if (occasion == null)
            {
                ViewBag.ErrorMessage = $"Evento com Id: {eventId} não pode ser encontrado";
                return View("NotFound");
            }

            if (occasion.EventType == EventType.CULTO)
            {
                ViewBag.ErrorMessage = $"Evento com Id: {eventId} incompatível com ação";
                return View("EventNotFound");
            }
            else
            {
                ViewBag.EventId = eventId;
                var model = GetMemberAtOccasionModel(occasion.Id);
                return View(model);
            }

        }


        [HttpPost]
        public IActionResult EditMembersInEvent(List<MemberEventViewModel> model, string eventId)
        {
            var occasion = repository.GetSundaySchoolWithItsMembers(int.Parse(eventId));

            if (occasion == null)
            {
                ViewBag.ErrorMessage = $"Evento com Id: {eventId} não pode ser encontrado";
                return View("EventNotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var member = memberRepository.GetMember(model[i].MemberId);

                if (model[i].IsSelected && !(occasion.Contains(member)))
                {
                    occasion.AddMember(member);
                }
                else if (!model[i].IsSelected && occasion.Contains(member))
                {
                    occasion.Remove(member);
                }
                else
                {
                    continue;
                }

            }
            repository.Update(occasion);
            return RedirectToAction("edit", "event", occasion.Id);
        }

        [HttpPost]
        public IActionResult EditMembersInReunion(List<MemberEventViewModel> model, string eventId)
        {
            var occasion = repository.GetReunionWithItsMembers(int.Parse(eventId));

            if (occasion == null)
            {
                ViewBag.ErrorMessage = $"Evento com Id: {eventId} não pode ser encontrado";
                return View("EventNotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var member = memberRepository.GetMember(model[i].MemberId);

                if (model[i].IsSelected)
                {
                    occasion.AddMember(member);
                }
                else if (occasion.Contains(member))
                {
                    occasion.Remove(member);
                }

            }
            repository.Update(occasion);
            return RedirectToAction("edit", "event", occasion.Id);
        }


        private EditSundaySchoolViewModel ConfigureLists(EditSundaySchoolViewModel model)
        {
            var presentsMembers = memberRepository.GetMembersInEvent(model.Id);

            HashSet<SelectListItem> allMembersList = new HashSet<SelectListItem>();
            foreach (var member in memberRepository.GetMembers().ToList())
            {
                var item = new SelectListItem
                {
                    Value = member.Id.ToString(),
                    Text = member.Name
                };

                allMembersList.Add(item);
            }

            model.PresentMembers = presentsMembers;
            model.AllMembers = allMembersList;
            return model;
        }

        private EditReunionViewModel ConfigureLists(EditReunionViewModel model)
        {
            var presentsMembers = memberRepository.GetMembersInEvent(model.Id);

            HashSet<SelectListItem> allMembersList = new HashSet<SelectListItem>();
            foreach (var member in memberRepository.GetMembers().ToList())
            {
                var item = new SelectListItem
                {
                    Value = member.Id.ToString(),
                    Text = member.Name
                };

                allMembersList.Add(item);
            }

            model.PresentMembers = presentsMembers;
            model.AllMembers = allMembersList;
            return model;
        }

        private BaptismEditViewModel ConfigureLists(BaptismEditViewModel model)
        {

            foreach (var member in memberRepository.GetMembers().ToList())
            {
                var item = new SelectListItem
                {
                    Value = member.Id.ToString(),
                    Text = member.Name
                };
                model.AddMember(item);
            }
            model.BaptizedMembers = memberRepository.GetMembersInEvent(model.Id);

            return model;
        }

        private List<MemberEventViewModel> GetMemberAtOccasionModel(int Id)
        {
            var presentsMembers = memberRepository.GetMembersInEvent(Id);

            var model = new List<MemberEventViewModel>();
            foreach (var member in memberRepository.GetMembers().ToList())
            {
                var memberEventViewModel = new MemberEventViewModel
                {
                    MemberId = member.Id,
                    MemberName = member.Name
                };

                if (presentsMembers.Contains(member))
                {
                    memberEventViewModel.IsSelected = true;
                }

                model.Add(memberEventViewModel);
            }

            return model;
        }

        private HashSet<SelectListItem> GetAllMembers()
        {
            var memberList = memberRepository
                            .GetMembers()
                            .OrderBy(m => m.Name)
                            .ToList();



            HashSet<SelectListItem> members = new HashSet<SelectListItem>();
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

        private ViewResult ReloadDataAndReturnView()
        {
            HashSet<SelectListItem> members = GetAllMembers();
            ViewBag.reunionViewModel = new CreateReunionViewModel(members);
            ViewBag.schoolViewModel = new CreateSundaySchoolViewModel(members);
            ViewBag.BaptismViewModel = new BaptismCreateViewModel(members);
            return View("Create");
        }
    }
}