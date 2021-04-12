using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace SomeBank.WebAPI.Controllers
{
    public class DefaultController : ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;

        public DefaultController(ILogger<DefaultController> logger)
        {
            _logger = logger;
        }

        protected async Task<ActionResult<T>> ActionAsync<T>(Func<Task<ActionResult<T>>> func)
        {
            try
            {
                var result = await func().ConfigureAwait(false);
                return Ok(result.Value == null ? result.Result : result.Value);
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError("Please check the provided AWS Credentials.");
                return StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError("Please check the provided AWS Credentials.");
                return StatusCode(StatusCodes.Status304NotModified, e.Message);
            }
            catch (SqlException e)
            {
                _logger.LogError("Please check the provided AWS Credentials.");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError($"Unknown encountered on server. Message:'{ e.Message }'");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"{ AppDomain.CurrentDomain.FriendlyName } : { e.Message }");
            }
        }
    }
}
