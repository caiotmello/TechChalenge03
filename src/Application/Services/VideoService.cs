using Application.Dtos.Request;
using Application.Dtos.Response;
using Application.Dtos.Validations;
using Application.Services.Interface;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Application.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;

        public VideoService(IVideoRepository videoRepository, IMapper mapper)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<ReadVideoResponseDto>> CreateAsync(CreateVideoRequestDto videoDto)
        {
            if (videoDto == null)
                return ResultService.Fail<ReadVideoResponseDto>("The Object is null!");

            var validation = new CreateVideoRequestDtoValidator().Validate(videoDto);
            if (!validation.IsValid)
                return ResultService.RequestError<ReadVideoResponseDto>("Validation Problem!", validation);

            var video = _mapper.Map<Video>(videoDto);
            var data = await _videoRepository.AddAsync(video);
            return ResultService.Ok<ReadVideoResponseDto>(_mapper.Map<ReadVideoResponseDto>(data));
        }

        public async Task<ResultService> DeleteAsync(int Id)
        {
            var video = await _videoRepository.GetAsync(Id);
            if (video == null)
                return ResultService.Fail("Video not found!");

            await _videoRepository.RemoveAsync(video);
            return ResultService.Ok($"Video from Id:{Id} was deleted!");
        }

        public async Task<ResultService<ICollection<ReadVideoResponseDto>>> GetAsync()
        {
            var videos = await _videoRepository.GetAllAsync();
            return ResultService.Ok<ICollection<ReadVideoResponseDto>>(_mapper.Map<ICollection<ReadVideoResponseDto>>(videos));
        }

        public async Task<ResultService<ReadVideoResponseDto>> GetByIdAsync(int Id)
        {
            var video = await _videoRepository.GetAsync(Id);
            if (video == null)
                return ResultService.Fail<ReadVideoResponseDto>("Video not found!");

            return ResultService.Ok<ReadVideoResponseDto>(_mapper.Map<ReadVideoResponseDto>(video));
        }

        public async Task<ResultService> UpdateAsync(UpdateVideoRequestDto videoDto)
        {
            if (videoDto == null)
                return ResultService.Fail<ReadVideoResponseDto>("The Object is null!");

            var validation = new UpdateVideoRequestDtoValidator().Validate(videoDto);
            if (!validation.IsValid)
                return ResultService.RequestError<ReadVideoResponseDto>("Validation Problem!", validation);

            var video = await _videoRepository.GetAsync(videoDto.Id);
            if (video == null)
                return ResultService.Fail("Video not found!");

            video = _mapper.Map<UpdateVideoRequestDto, Video>(videoDto, video);
            video.ModifiedDate = DateTime.Now;
            await _videoRepository.UpdateAsync(video);
            return ResultService.Ok("Video updated!");
        }
    }
}
