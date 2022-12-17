using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VSporte.Task.Domain.Common
{
    public abstract class AutoIncrement
    {
        /// <summary>
        /// Автоувеличивающийся идентификатор
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }
    }
}
