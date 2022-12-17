using VSporte.Task.API.Entities;

namespace VSporte.Task.API.DTOs
{
    public class PlayerClubDto
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int ClubId { get; set; }

        public void UpdateEntity(PlayerClubEntity entity)
        {
            entity.PlayerId = PlayerId;
            entity.ClubId = ClubId;
        }
    }
}
