using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Domain.Entities
{
    public class Schedule : Entity
    {
        public Schedule() { }

        public Schedule(string idExam, string name, DateTime dateBirth, int clientId)
        {
            IdExam = idExam;
            Name = name;
            ClientId = clientId;
        }

        public string IdExam { get; set; }

        public string Name { get; set; }

        public int ClientId { get; set; }
    }
}
