using VSporte.Task.API.Models;

namespace VSporte.Task.API.DTOs
{
    public class ClubDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public void UpdateEntity(ClubEntity entity)
        {
            entity.Name = Name;
            entity.ShortName = ShortName;
        }
    }
}
