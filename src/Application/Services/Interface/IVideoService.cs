using Application.Dtos.Request;
using Application.Dtos.Response;

namespace Application.Services.Interface
{
    public interface IVideoService
    {
        Task<ResultService<ReadVideoResponseDto>> CreateAsync(CreateVideoRequestDto videoDto);
        Task<ResultService<ICollection<ReadVideoResponseDto>>> GetAsync();
        Task<ResultService<ReadVideoResponseDto>> GetByIdAsync(int Id);
        Task<ResultService> UpdateAsync(UpdateVideoRequestDto videoDto);
        Task<ResultService> DeleteAsync(int Id);
    }
}
