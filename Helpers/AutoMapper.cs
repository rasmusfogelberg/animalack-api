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
    CreateMap<User, SingleUserResponse>();
    CreateMap<User, AuthResponse>();

    CreateMap<RegisterPetRequest, Pet>()
      .ForMember(destination => destination.Gender, 
      options => options.MapFrom(source => (PetGender)source.Gender));

    CreateMap<UpdatePetRequest, Pet>();

    CreateMap<Pet, RegisterPetResponse>();
    CreateMap<Pet, PetResponse>();
    CreateMap<Pet, SinglePetResponse>();
  }
}