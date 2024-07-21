using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Storyline.WebApp;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StoryMediaType
{
    Movie,
    Game,
    GraphicNovel
}
