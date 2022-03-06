using AutoMapper;

using AnimalackApi.Models.Users;
using AnimalackApi.Models.Pets;
using AnimalackApi.Entities;

namespace AnimalackApi.Helpers;

public class AutoMapper : Profile
{
  public AutoMapper()
  {
    CreateMap<RegisterRequest, User>();
    CreateMap<UpdateRequest, User>();
    CreateMap<User, UserResponse>();
    CreateMap<User, AuthResponse>();

    CreateMap<RegisterPetRequest, Pet>();
    CreateMap<PetResponse, Pet>();
    CreateMap<Pet, PetResponse>();
  }
}