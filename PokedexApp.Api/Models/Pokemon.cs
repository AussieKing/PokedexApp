using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PokedexApp.Models
{
    public class Pokemon
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        [JsonPropertyName("base_experience")]
        public int BaseExperience { get; set; }

        [JsonProperty("types")]
        public List<PokemonType> Types { get; set; }
    }

    public class PokemonType
    {
        public int Id { get; set; } // Primary key

        [JsonProperty("slot")]
        public int Slot { get; set; }

        public int PokemonId { get; set; } // Foreign key to Pokemon

        [JsonProperty("type")]
        public TypeInfo Type { get; set; }
    }

    public class TypeInfo
    {
        public int Id { get; set; } // Add a primary key

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
