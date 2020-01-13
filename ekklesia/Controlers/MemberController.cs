using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekklesia.Models.Member;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ekklesia.Controlers
{
    public class MemberController : Controller
    {
        private readonly IMemberRepository repository;

        public MemberController(IMemberRepository repository)
        {
            this.repository = repository;
        }

        
        public IActionResult List()
        {
            var members = repository.GetMembers();
            return View(members);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MemberCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Member member = new Member
                {
                    Name = model.Name,
                    Phone = model.Phone,
                    Position = model.Position
                };
                repository.Add(member);
                return RedirectToAction("list");
            }

            return View();
        }


    }
}