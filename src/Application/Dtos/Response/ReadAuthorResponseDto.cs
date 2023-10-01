namespace Application.Dtos.Response
{
    public class ReadAuthorResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Slug { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
