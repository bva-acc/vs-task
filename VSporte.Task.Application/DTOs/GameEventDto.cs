using VSporte.Task.API.Entities;

namespace VSporte.Task.API.DTOs
{
    public class GameEventDto
    {
        public int Id { get; set; }
        public int? PlayerId { get; set; }
        public int ClubId { get; set; }
        public string Type { get; set; }
        public string MomentTime { get; set; }

        public void UpdateEntity(GameEventEntity entity)
        {
            entity.PlayerId = PlayerId;
            entity.ClubId = ClubId;
            entity.Type = Type;
            entity.MomentTime = MomentTime;
        }
    }
}
