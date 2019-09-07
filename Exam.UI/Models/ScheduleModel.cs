using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.UI.Models
{
    public class ScheduleModel
    {
        public ScheduleModel() { }

        public ScheduleModel(string idExam, string name, float value, DateTime dateSchedule, int clientId)
        {
            IdExam = idExam;
            Name = name;
            DateSchedule = dateSchedule;
            ClientId = clientId;
            Value = value;
        }

        public string IdExam { get; set; }

        public string Name { get; set; }

        public float Value { get; set; }

        public DateTime DateSchedule { get; set; }

        public int ClientId { get; set; }

    }

    public class ScheduleModelPut
    {
        public ScheduleModelPut() { }

        public ScheduleModelPut(int idSchedule, DateTime dateSchedule)
        {
            IdSchedule = idSchedule;
            DateSchedule = dateSchedule;
        }

        public int IdSchedule { get; set; }

        public DateTime DateSchedule { get; set; }

    }
}
