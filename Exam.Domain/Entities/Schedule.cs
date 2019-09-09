using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Domain.Entities
{
    public class Schedule : Entity
    {
        public Schedule() { }

        public Schedule(string idExam, string name, float value, DateTime dateSchedule, int clientId)
        {
            IdExam = idExam;
            Name = name;
            DateSchedule = dateSchedule;
            ClientId = clientId;
            Value = value;
        }

        public string IdExam { get; set; }

        public string Name { get; set; }

        public DateTime DateSchedule { get; set; }

        public int ClientId { get; set; }

        public float Value { get; set; }

        public Client Client { get; set; }

    }
}
