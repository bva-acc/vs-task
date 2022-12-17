using System;

namespace VSporte.Task.Solution.Models;

public class Fingerprint : ICloneable
{
    public PlayerClubItem[] PlayerClubs { get; set; }
    public PlayerItem[] Players { get; set; }
    public ClubItem[] Clubs { get; set; }

    public object Clone()
    {
        var fingerprint = (Fingerprint)MemberwiseClone();

        fingerprint.Players = Players.Select(p => (PlayerItem)p.Clone()).ToArray();
        fingerprint.Clubs = Clubs.Select(c => (ClubItem)c.Clone()).ToArray();

        return fingerprint;
    }
}