using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Domain.Entities
{
    public class Client : Entity
    {
        public Client() { }

        public Client(string name, string cpf, DateTime dateBirth)
        {
            Name = name;
            Cpf = cpf;
            DateBirth = dateBirth;
        }
        public string Name { get; set; }

        public string Cpf { get; set; }

        public DateTime DateBirth { get; set; }        

        public List<Schedule> Schedules { get; set; }

    }
}
