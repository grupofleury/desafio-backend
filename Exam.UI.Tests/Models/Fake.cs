using Bogus;
using Exam.UI.Models;
using Exam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.UI.Tests.Models
{
    public class Fake
    {
        public Faker<ClientModelCreate> ClientsCreate { get; set; }
        public Faker<Client> Client { get; set; }
        public Faker<ClientModelGet> ClientGet { get; set; }
        public Faker<ClientModel> ClientModel { get; set; }

        public Faker<Schedule> Schedule { get; set; }
        public Faker<ScheduleModel> Schedules { get; set; }
        public Faker<ScheduleModelPut> SchedulePut { get; set; }

        public Faker<Exams> Exams { get; set; }


        public Fake()
        {
            ClientsCreate = new Faker<ClientModelCreate>()
                .RuleFor(x => x.Cpf, f => f.Lorem.Word())
                .RuleFor(x => x.DateBirth, new DateTime())
                .RuleFor(x => x.Name, f => f.Lorem.Word());

            ClientModel = new Faker<ClientModel>()
                .RuleFor(x => x.Cpf, f => f.Lorem.Word())
                .RuleFor(x => x.DateBirth, new DateTime())
                .RuleFor(x => x.Name, f => f.Lorem.Word());

            Client = new Faker<Client>()
                .RuleFor(x => x.Id, f => f.Random.Int())
                .RuleFor(x => x.Cpf, f => f.Lorem.Word())
                .RuleFor(x => x.DateBirth, new DateTime())
                .RuleFor(x => x.Name, f => f.Lorem.Word());

            ClientGet = new Faker<ClientModelGet>()
                .RuleFor(x => x.Id, f => f.Random.Int())
                .RuleFor(x => x.Cpf, f => f.Lorem.Word())
                .RuleFor(x => x.DateBirth, new DateTime())
                .RuleFor(x => x.Name, f => f.Lorem.Word())
                .RuleFor(x => x.TotalValue, f => f.Random.Float());

            Schedule = new Faker<Schedule>()
                .RuleFor(x => x.ClientId, f => f.Random.Int())
                .RuleFor(x => x.DateSchedule, new DateTime())
                .RuleFor(x => x.IdExam, f => f.Lorem.Word())
                .RuleFor(x => x.Name, f => f.Lorem.Word())
                .RuleFor(x => x.Value, f => f.Random.Float());


            Schedules = new Faker<ScheduleModel>()
                .RuleFor(x => x.ClientId, f => f.Random.Int())
                .RuleFor(x => x.DateSchedule, new DateTime())
                .RuleFor(x => x.IdExam, f => f.Lorem.Word())
                .RuleFor(x => x.Name, f => f.Lorem.Word())
                .RuleFor(x => x.Value, f => f.Random.Float());

            SchedulePut = new Faker<ScheduleModelPut>()
                .RuleFor(x => x.IdSchedule, f => f.Random.Int())
                .RuleFor(x => x.DateSchedule, new DateTime());

            Exams = new Faker<Exams>()
                .RuleFor(x => x.id, f => f.Lorem.Word())
                .RuleFor(x => x.name, f => f.Lorem.Word())
                .RuleFor(x => x.value, f => f.Random.Float());
        }

        public Client GenerateScheduleWithClient()
        {
            Schedule schedule = Schedule.Generate();
            var client = Client.Generate();
            client.Schedules = new List<Schedule>();
            client.Schedules.Add(schedule);
            return client;
        }

        public ExamsList GenerateExamList()
        {
            var exam = new ExamsList();
            var exams = Exams.Generate();
            exam.Exams = new List<Exams>();
            exam.Exams.Add(exams);
            return exam;
        }

        public int GenerateFakeId()
        {
            var random = new Random();
            return random.Next(99999, 999999);
        }

    }
}
