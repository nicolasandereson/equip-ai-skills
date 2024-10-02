using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.SemanticKernel;

public class MusicLibraryPlugin
{
    
    /// <summary>
    /// Adds a song to the recently played list.
    /// </summary>
    /// <param name="artist">The name of the artist.</param>
    /// <param name="song">The title of the song.</param>
    /// <param name="genre">The song genre.</param>
    /// <returns>A message indicating that the song has been added to the recently played list.</returns>
   [KernelFunction, Description("Add a song to the recently played list")]
    public static string AddToRecentlyPlayed(
    [Description("The name of the artist")] string artist,
    [Description("The title of the song")] string song,
    [Description("The song genre")] string genre)
    {
        // Read the existing content from the file
        string filePath = "data/recentlyplayed.txt";
        string jsonContent = File.ReadAllText(filePath);
        var recentlyPlayed = (JsonArray)JsonNode.Parse(jsonContent);

        var newSong = new JsonObject
        {
            ["title"] = song,
            ["artist"] = artist,
            ["genre"] = genre
        };

        recentlyPlayed.Insert(0, newSong);
        File.WriteAllText(filePath, JsonSerializer.Serialize(recentlyPlayed,
            new JsonSerializerOptions { WriteIndented = true }));

        return $"Added '{song}' to recently played";
    }
}

