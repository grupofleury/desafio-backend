using Exam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Domain.Interfaces.Repositories
{
    public interface IExamsRepository
    {
        ExamsList GetExams();
    }
}