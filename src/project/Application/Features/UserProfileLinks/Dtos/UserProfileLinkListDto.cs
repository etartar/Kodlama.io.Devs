namespace Application.Features.UserProfileLinks.Dtos
{
    public class UserProfileLinkListDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserProfileLinkName { get; set; }
        public string Link { get; set; }
    }
}
