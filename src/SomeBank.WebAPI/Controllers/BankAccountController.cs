using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SomeBank.Core.Models;
using SomeBank.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SomeBank.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : DefaultController
    {
        private readonly BankDBContext _context;

        public BankAccountController(BankDBContext context, ILogger<DefaultController> logger) : base(logger)
        {
            _context = context;
        }

        // GET: api/<BankAccountController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccount>>> GetBankAccountsAsync()
        {
            return await ActionAsync<IEnumerable<BankAccount>>(async () => await _context.BankAccounts.ToListAsync());
        }

        // GET api/<BankAccountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccount>> GetBankAccountAsync(int id) => await ActionAsync<BankAccount>(async () =>
        {
            var bankAccount = await _context.BankAccounts.FindAsync(id);
            return bankAccount == null ? NotFound() : bankAccount;
        });

        // POST api/<BankAccountController>
        [HttpPost]
        public async Task<ActionResult<BankAccount>> CreateBankAccountAsync(BankAccount bankAccount) => await ActionAsync<BankAccount>(async () =>
        {
            _context.BankAccounts.Add(bankAccount);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBankAccount", new { id = bankAccount.BankAccountID }, bankAccount);
        });

        // PUT api/<BankAccountController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BankAccount>> UpdateBankAccount(int id, BankAccount bankAccount)
        {
            return id != bankAccount.BankAccountID
                ? BadRequest()
                : await ActionAsync<BankAccount>(async () =>
                {
                    _context.Entry(bankAccount).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return bankAccount;
                });
        }

        // DELETE api/<BankAccountController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BankAccount>> DeleteBankAccountAsync(int id) => await ActionAsync<BankAccount>(async () =>
        {
            var bankAccount = await _context.BankAccounts.FindAsync(id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            _context.BankAccounts.Remove(bankAccount);
            await _context.SaveChangesAsync();
            return bankAccount;
        });
    }
}
