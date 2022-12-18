using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using VSporte.Task.Domain.Common;

namespace VSporte.Task.API.Entities
{
    [Index(nameof(Name), nameof(Surname), nameof(Number), IsUnique = true)]
    public class PlayerEntity : AutoIncrement
    {
        [Required(ErrorMessage = "Поле {0} обязательно")]
        [MaxLength(100, ErrorMessage = "Поле {0} должно быть не более {1} символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле {0} обязательно")]
        [MaxLength(100, ErrorMessage = "Поле {0} должно быть не более {1} символов")]
        public string Surname { get; set; }

        public string? Patronymic { get; set; }

        [Required(ErrorMessage = "Поле {0} обязательно")]
        public int Number { get; set; }

        public ICollection<PlayerClubEntity> PlayerClubs { get; set; }
    }
}
