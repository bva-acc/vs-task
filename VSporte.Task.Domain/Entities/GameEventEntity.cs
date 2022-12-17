using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VSporte.Task.Domain.Common;
using VSporte.Task.API.Models;

namespace VSporte.Task.API.Entities
{
    public class GameEventEntity : AutoIncrement
    {
        [Required(ErrorMessage = "Поле {0} обязательно")]
        [ForeignKey("Player")]
        public int? PlayerId { get; set; }
        public PlayerEntity Player { get; set; }

        [Required(ErrorMessage = "Поле {0} обязательно")]
        [ForeignKey("Club")]
        public int ClubId { get; set; }
        public ClubEntity Club { get; set; }

        [Required(ErrorMessage = "Поле {0} обязательно")]
        [MaxLength(200, ErrorMessage = "Поле {0} должно быть не более {1} символов")]
        public string Type { get; set; }

        // Время момента - произвольная строка, т.к. время может быть как 59я минута, 45 + 3 доп. минута (доп. минута 1го тайма) и др.
        // Возможно, имеет смыл сделать сделать поле с типом enum или отдельный класс
        [Required(ErrorMessage = "Поле {0} обязательно")]
        [MaxLength(10, ErrorMessage = "Поле {0} должно быть не более {1} символов")]
        public string MomentTime { get; set; }
    }
}
