using GICApp.ApplicationCore.Application.Services;
using GICApp.ApplicationCore.Domain.Entities;
using GICApp.Infrastructure.Persistence.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GIC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountService _bankAccountService;
        public BankAccountController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
             return    Ok( await _bankAccountService.GetAll());
        }

        [HttpGet("{id}")]
       
        public async Task<IActionResult> GetById(int id)
        {
             return Ok(await _bankAccountService.GetById(id));
        }

         [HttpPost] 
            public async Task<ActionResult<BankAccount>> Post([FromBody] BankAccount account)
        {
             
             _bankAccountService.Add(account); 
             return CreatedAtAction(nameof(GetById), new { id = account.Id }, account);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BankAccount account)
        {
            _bankAccountService.Update(account);
            return NoContent();
         }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
             _bankAccountService.Delete(id);
             return NoContent();
        }

         
    }
}
