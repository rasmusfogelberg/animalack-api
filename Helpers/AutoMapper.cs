using AutoMapper;

using AnimalackApi.Models.Users;
using AnimalackApi.Entities;

namespace AnimalackApi.Helpers;

public class AutoMapper : Profile
{
  public AutoMapper() 
  {
    CreateMap<RegisterRequest, User>();
    CreateMap<UpdateRequest, User>();
    CreateMap<User, UserResponse>();
  }
}