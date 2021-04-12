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
    public class BankController : DefaultController
    {
        private readonly BankDBContext _context;

        public BankController(BankDBContext context, ILogger<DefaultController> logger) : base(logger)
        {
            _context = context;
        }

        // GET: api/<BankController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBanksAsync()
        {
            return await ActionAsync<IEnumerable<Bank>>(async () => await _context.Banks.ToListAsync());
        }

        // GET api/<BankController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bank>> GetBankAsync(int id) => await ActionAsync<Bank>(async () =>
        {
            var bank = await _context.Banks.FindAsync(id);
            return bank == null ? NotFound() : bank;
        });

        // POST api/<BankController>
        [HttpPost]
        public async Task<ActionResult<Bank>> CreateBankAsync(Bank bank) => await ActionAsync<Bank>(async () =>
        {
            _context.Banks.Add(bank);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBankAsync", new { id = bank.BankID }, bank);
        });

        // PUT api/<BankController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Bank>> UpdateBankAsync(int id, Bank bank)
        {
            return id != bank.BankID
                ? BadRequest()
                : await ActionAsync<Bank>(async () =>
            {
                _context.Entry(bank).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return bank;
            });
        }

        // DELETE api/<BankController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bank>> DeleteBankAsync(int id) => await ActionAsync<Bank>(async () =>
        {
            var bank = await _context.Banks.FindAsync(id);
            if (bank == null)
            {
                return NotFound();
            }

            _context.Banks.Remove(bank);
            await _context.SaveChangesAsync();
            return bank;
        });
    }
}
