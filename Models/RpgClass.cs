using System.Text.Json.Serialization;

namespace _netCourse
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass 
    {
        Knight,
        Mage, 
        Cleric
    }
}