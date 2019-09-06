using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Domain.Events
{
    public class DomainNotificationHandler : IDomainNotificationHandler
    {
        private readonly List<ErrorResponseModel> errors;

        public DomainNotificationHandler() => errors = new List<ErrorResponseModel>();

        public void AddErrorMessage(string error) => errors.Add(new ErrorResponseModel(error));

        public void AddErrorMessages(List<ValidationFailure> errorMessages) =>
            errorMessages?.ForEach(error =>
            {
                errors.Add(new ErrorResponseModel(error.ErrorMessage));
            });

        public List<ErrorResponseModel> GetErrors() => errors;
        public bool HasError() => errors.Any();
    }
}
