using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Domain.Events;
using Exam.Domain.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exam.UI.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IDomainNotificationHandler _domainNotificationHandler;

        protected BaseController(IDomainNotificationHandler domainNotificationHandler) =>
            _domainNotificationHandler = domainNotificationHandler;

        protected IEnumerable<object> ModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                yield return new { message = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message };
            }
        }

        protected IActionResult ErrorResponse<T>(T result) where T : ErrorResponseModel =>
            BadRequest(result);

        protected IActionResult Response<T>(T result, bool isCreateOperation = false)
        {
            if (result == null)
                return NoContent();

            return isCreateOperation ? StatusCode(201, result) : Ok(result);
        }
    }
}
