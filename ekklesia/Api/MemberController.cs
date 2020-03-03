using ekklesia.Models.MemberModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ekklesia.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository repository;
        private readonly IHostingEnvironment hostingEnviroment;

        public MemberController(IMemberRepository repository, IHostingEnvironment hostingEnviroment)
        {
            this.repository = repository;
            this.hostingEnviroment = hostingEnviroment;
        }


        //[HttpGet]
        //public ActionResult Get()
        //{
        //    return repository.GetMembers();
        //}


        //[HttpGet("{id}")]
        //public IActionResult<Member> Get(int id)
        //{
        //    Member member = repository.GetMember(id);
        //    if (member == null)
        //    {
        //        return NotFound();
        //    }
        //    return member;

        //}

    }
}