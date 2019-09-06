using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.UI.Models
{
    public class ClientModel
    {
        public ClientModel() { }

        public ClientModel(string name, string cpf, DateTime dateBirth)
        {
            Name = name;
            Cpf = cpf;
            DateBirth = dateBirth;
        }
        public string Name { get; set; }

        public string Cpf { get; set; }

        public DateTime DateBirth { get; set; }  

    }
}
