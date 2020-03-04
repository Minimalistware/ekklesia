using ekklesia.Models.MemberModel;
using ekklesia.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ekklesia.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository repository;
        private readonly IHostingEnvironment hostingEnviroment;

        public MemberController(IMemberRepository repository, IHostingEnvironment hostingEnviroment)
        {
            this.repository = repository;
            this.hostingEnviroment = hostingEnviroment;
        }

        [HttpGet]
        public async Task<IEnumerable<Member>> Browse()
        {
            return await repository.GetMembers();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(int id)
        {
            Member member = await repository.GetMember(id);
            if (member == null)
            {
                return NotFound(id);
            }
            return Ok(member);
        }

        [HttpPost]
        public IActionResult Add(MemberCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Member member = new Member
                {
                    Name = model.Name,
                    Phone = model.Phone,
                    Position = model.Position,
                };
                repository.Add(member);
                var url = Url.Action("Read", new { id = member.Id });
                return Created(url, member);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> Update(MemberEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Member member = await repository.GetMember(model.Id);
                if (member == null)
                {
                    return NotFound(model.Id);
                }
                member.Name = model.Name;
                member.Phone = model.Phone;
                member.Position = model.Position;
                await repository.Update(member);
                return Ok(member);
            }
            return BadRequest(ModelState);
        }

    }
}