using Application.Dtos.Request;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Mappers
{
    public class DtoToDomainMapping : Profile
    {
        public DtoToDomainMapping()
        {
            MappingArticle();
            MappingAuthor();
            MappingVideo();
            MappingUser();
        }

        private void MappingVideo()
        {
            CreateMap<CreateVideoRequestDto, Video>();
            CreateMap<UpdateVideoRequestDto, Video>();
        }

        private void MappingArticle()
        {
            CreateMap<CreateArticleRequestDto, Article>();
            CreateMap<UpdateArticleRequestDto, Article>();
        }

        private void MappingAuthor()
        {
            CreateMap<CreateAuthorRequestDto, Author>();
            CreateMap<UpdateAuthorRequestDto, Author>();
        }


        private void MappingUser()
        {
            CreateMap<UserSignUpRequestDto,IdentityUser>();
            CreateMap<UserLoginRequestDto,IdentityUser> ();
        }
    }
}
