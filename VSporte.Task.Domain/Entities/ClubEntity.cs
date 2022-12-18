using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using VSporte.Task.API.Entities;
using VSporte.Task.Domain.Common;

namespace VSporte.Task.API.Models
{
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(ShortName), IsUnique = true)]
    public class ClubEntity : AutoIncrement
    {
        [Required(ErrorMessage = "Поле {0} обязательно")]
        [MaxLength(100, ErrorMessage = "Поле {0} должно быть не более {1} символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле {0} обязательно")]
        [MaxLength(50, ErrorMessage = "Поле {0} должно быть не более {1} символов")]
        public string ShortName { get; set; }

        public ICollection<PlayerClubEntity> PlayerClubs { get; set; }
    }
}
