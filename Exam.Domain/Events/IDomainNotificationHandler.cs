using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Domain.Events
{
    public interface IDomainNotificationHandler
    {
        void AddErrorMessages(List<ValidationFailure> errorMessage);

        List<ErrorResponseModel> GetErrors();

        bool HasError();

        void AddErrorMessage(string error);

    }
}
