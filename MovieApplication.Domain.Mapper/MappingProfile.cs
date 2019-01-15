using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MovieApplication.Domain.Dto.Models;
using MovieApplication.Domain.ServiceModels;

namespace MovieApplication.Domain.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MovieOmdbResponse, Movie>();
            CreateMap<Movie, MovieOmdbResponse>();
        }

    }
}
