using System;
using System.IO;
using System.Threading.Tasks;
using ekklesia.Models;
using ekklesia.Models.MemberModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

        [AllowAnonymous]
        public async Task<ViewResult> List(int pageNumber = 1)
        {
            var paginatedList = await PaginatedList<Member>.CreateAsync(repository.Members(), pageNumber, 10);
            return View(paginatedList);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MemberCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model.Photo);
                Member member = new Member
                {
                    Name = model.Name,
                    Phone = model.Phone,
                    Position = model.Position,
                    PhotoPath = uniqueFileName
                };
                await repository.Add(member);
                return RedirectToAction("list", "member");
            }

            return View();
        }

        [HttpGet]
        public async Task<ViewResult> Edit(int id)
        {
            Member member = await repository.GetMember(id);
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
        public async Task<IActionResult> Edit(MemberEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Member member = await repository.GetMember(model.Id);
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
                    member.PhotoPath = ProcessUploadedFile(model.Photo);
                }


                await repository.Update(member);
                return RedirectToAction("list");
            }

            return View();
        }

        [HttpGet]
        public async Task<ViewResult> Details(int id)
        {
            Member member = await repository.GetMember(id);
            var viewModel = new MemberDetailsViewModel()
            {
                Name = member.Name,
                Phone = member.Phone,
                Position = member.Position,
                ExistingPhotoPath = member.PhotoPath,
                PageTitle = "Detalhes do membro"
            };

            return View(viewModel);
        }



        [HttpGet]
        [AllowAnonymous]
        public ViewResult Search()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ViewResult> Search(MemberSreachViewModel model)
        {
            var members = repository.Search(model);
            var paginatedList = await PaginatedList<Member>.CreateAsync(members, 1, 5);
            return View("List", paginatedList);
        }


        //TODO MÉTODO DUPLICADO
        private string ProcessUploadedFile(IFormFile file)
        {
            string uniqueFileName = null;
            if (file != null)
            {
                var uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }


    }
}