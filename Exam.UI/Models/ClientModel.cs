using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.UI.Models
{

    public class ClientModel
    {
        public ClientModel() { }

        public ClientModel(string name, string cpf, DateTime dateBirth, int id)
        {
            Name = name;
            Cpf = cpf;
            DateBirth = dateBirth;
            Id = id;
        }

        public string Name { get; set; }

        public string Cpf { get; set; }

        public DateTime DateBirth { get; set; }

        public int Id { get; set; }

    }
    public class ClientModelCreate
    {
        public ClientModelCreate() { }

        public ClientModelCreate(string name, string cpf, DateTime dateBirth)
        {
            Name = name;
            Cpf = cpf;
            DateBirth = dateBirth;
        }

        public string Name { get; set; }

        public string Cpf { get; set; }

        public DateTime DateBirth { get; set; }

    }

    public class ClientModelGet
    {
        public ClientModelGet() { }

        public ClientModelGet(string name, string cpf, DateTime dateBirth, int id)
        {
            Name = name;
            Cpf = cpf;
            DateBirth = dateBirth;
            Id = id;
        }

        public string Name { get; set; }

        public string Cpf { get; set; }

        public DateTime DateBirth { get; set; }

        public int Id { get; set; }

        public float TotalValue{get;set;}

        public List<ScheduleModel> ScheduleModels { get; set; }

    }
}
