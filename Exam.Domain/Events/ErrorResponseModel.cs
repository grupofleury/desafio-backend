using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Domain.Events
{
    public class ErrorResponseModel
    {
        public string Message { get; set; }

        public ErrorResponseModel(string message) => Message = message;
    }
}
