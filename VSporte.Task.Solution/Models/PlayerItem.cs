namespace VSporte.Task.Solution.Models;

public class PlayerItem : ICloneable
{
    public Guid PlayerId { get; set; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }

    public object Clone() => MemberwiseClone();
}