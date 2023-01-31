using System.IO;
using Newtonsoft.Json;
using VSporte.Task.Solution.Models;

namespace VSporte.Task.Test.Providers;

public class FingerprintProvider
{
    public Fingerprint Get(int version, bool isCorrect = false) => new()
    {
        Clubs = ReadFromFile<ClubItem>("clubs", version, isCorrect),
        Players = ReadFromFile<PlayerItem>("players", version, isCorrect),
        PlayerClubs = ReadFromFile<PlayerClubItem>("player_clubs", version, isCorrect)
    };

    private static T[] ReadFromFile<T>(string name, int version, bool isCorrectVersion)
    {
        //var text = File.ReadAllText($"Source/{name}.json");

        var isCorrectText = isCorrectVersion ? "correct_" : string.Empty;
        var text = File.ReadAllText($"D:/Apps/VSporteTask/Fingerprint/{isCorrectText}{name}{version}.json");
        return JsonConvert.DeserializeObject<T[]>(text);
    }
}