using VSporte.Task.API.Entities;

namespace VSporte.Task.API.DTOs
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public int Number { get; set; }

        public void UpdateEntity(PlayerEntity entity)
        {
            entity.Name = Name;
            entity.Surname = Surname;
            entity.Patronymic = Patronymic;
            entity.Number = Number;
        }
    }
}
