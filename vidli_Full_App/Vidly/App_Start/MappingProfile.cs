using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Controllers.Api;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile :Profile 
    {

        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomersDto>();
            Mapper.CreateMap<CustomersDto, Customer>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<MembershipTypeDto, MembershipType>();
            Mapper.CreateMap<MoviesDto, Movie>();
            Mapper.CreateMap<Movie, MoviesDto>();
            Mapper.CreateMap<Genre, GenreDto>();
            Mapper.CreateMap<GenreDto, Genre>();
        }
    }
}