using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ekklesia.Models.MemberModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ekklesia.Controlers
{
    public class MemberController : Controller
    {
        private readonly IMemberRepository repository;
        private readonly IHostingEnvironment hostingEnviroment;

        public MemberController(IMemberRepository repository, IHostingEnvironment hostingEnviroment)
        {
            this.repository = repository;
            this.hostingEnviroment = hostingEnviroment;
        }

        
        public ViewResult List()
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
                string uniqueFileName = ProcessUploadedFile(model);
                Member member = new Member
                {
                    Name = model.Name,
                    Phone = model.Phone,
                    Position = model.Position,
                    PhotoPath = uniqueFileName
                };
                repository.Add(member);
                return RedirectToAction("list");
            }

            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Member member = repository.GetMember(id);
            var viewModel = new MemberEditViewModel()
            {
                Id = member.Id,
                Name = member.Name,
                Phone = member.Phone,
                Position = member.Position,
                ExistingPhotoPath = member.PhotoPath,
                PageTitle = "Editar detalhes do membro"
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(MemberEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Member member = repository.GetMember(model.Id);
                member.Name = model.Name;
                member.Phone = model.Phone;
                member.Position = model.Position;

                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        var filePath = Path.Combine(hostingEnviroment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    member.PhotoPath = ProcessUploadedFile(model);
                }


                repository.Update(member);
                return RedirectToAction("list");
            }

            return View();
        }

        private string ProcessUploadedFile(MemberCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                var uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }


    }
}