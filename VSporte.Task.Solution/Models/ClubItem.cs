namespace VSporte.Task.Solution.Models;

public class ClubItem : ICloneable
{
    public Guid ClubId { get; set; }
    public string FullName { get; set; }
    public string? City { get; set; }

    public object Clone() => MemberwiseClone();
}