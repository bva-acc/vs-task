namespace VSporte.Task.API.DTOs
{
    public class PlayerWithClubIdDto
    {
        public Guid PlayerId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public Guid ClubId { get; set; }
    }
}
