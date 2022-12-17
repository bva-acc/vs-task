using System.ComponentModel.DataAnnotations;
using VSporte.Task.API.Models;
using VSporte.Task.Domain.Common;

namespace VSporte.Task.API.Entities
{
    public class PlayerClubEntity : AutoIncrement
    {
        [Required(ErrorMessage = "Поле {0} обязательно")]
        public int PlayerId { get; set; }
        public PlayerEntity Player { get; set; }

        [Required(ErrorMessage = "Поле {0} обязательно")]
        public int ClubId { get; set; }
        public ClubEntity Club { get; set; }
    }
}
