using Exam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Domain.Interfaces.Repositories
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        Schedule GetByIdExamDate(string idExam,DateTime date);
    }
}