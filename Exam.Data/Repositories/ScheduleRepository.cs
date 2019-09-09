using Exam.Domain.Entities;
using Exam.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exam.Data.Repositories
{
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(DbContext context) : base(context)
        {
        }
        public Schedule GetByIdExamDate(string idExam,DateTime date)
        {
            var dateTime = date.ToString("dd/MM/yyyy HH");
            return _dbSet.Where(x => x.IdExam == idExam && x.DateSchedule.ToString("dd/MM/yyyy HH") == dateTime).FirstOrDefault();
            
        }        
    }
}
