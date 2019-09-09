using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Domain.Entities
{
    public class ExamsList
    {
        public List<Exams> Exams { get; set; }
    }

    public class Exams
    {
        public string id { get; set; }

        public string name { get; set; }

        public float value { get; set; }
    }
}