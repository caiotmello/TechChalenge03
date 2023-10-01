using Application.Dtos.Response;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Mappers
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping()
        {
            MappingArticle();
            MappingAuthor();
            MappingVideo();
            MappingUser();
        }

        private void MappingVideo()
        {
            CreateMap<Video, ReadVideoResponseDto>();
        }

        private void MappingArticle()
        {
            CreateMap<Article, ReadArticleResponseDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(article => article.Author));
        }

        private void MappingAuthor()
        {
            CreateMap<Author, ReadAuthorResponseDto>();
        }

        private void MappingUser()
        {
            CreateMap<IdentityUser, UserLoginResponseDto>();
        }
    }
}
