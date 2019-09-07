using AutoMapper;
using Exam.UI.Models;
using Exam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.UI.AutoMapper
{
    public class ModelToDomain : Profile
    {
        public ModelToDomain()
        {

            CreateMap<ClientModel,Client>();
            CreateMap<ScheduleModel,Schedule>();

        }
    }
}