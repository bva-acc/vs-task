namespace VSporte.Task.Solution.Models;

public class PlayerClubItem
{
    public Guid PlayerId { get; set; }
    public Guid ClubId { get; set; }

    public object Clone() => MemberwiseClone();
}