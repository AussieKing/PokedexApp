using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using PokedexApp.Data;
using PokedexApp.Models;

namespace PokedexApp.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly HttpClient _httpClient;
        private readonly PokedexContext _context;
        private readonly ILogger<PokemonRepository> _logger;

        public PokemonRepository(
            HttpClient httpClient,
            PokedexContext context,
            ILogger<PokemonRepository> logger
        )
        {
            _httpClient = httpClient;
            _context = context;
            _logger = logger;
        }

        public async Task AddPokemonAsync(Pokemon pokemon)
        {
            _logger.LogInformation("Adding a new Pokemon to the database.");
            pokemon.Id = 0;
            _context.Pokemons.Add(pokemon);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Pokemon added to the database.");
        }

        public async Task DeletePokemonAsync(int id)
        {
            _logger.LogInformation("Deleting a Pokemon with ID {Id} from the database.", id);
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon != null)
            {
                _context.Pokemons.Remove(pokemon);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Pokemon deleted from the database.");
            }
            else
            {
                _logger.LogWarning("Pokemon with ID {Id} not found in the database.", id);
                throw new Exception("Pokemon not found.");
            }
        }

        public async Task<Pokemon> GetPokemonByIdAsync(int id)
        {
            _logger.LogInformation("Fetching Pokemon with ID {Id} from the database.", id);
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon != null)
            {
                _logger.LogInformation("Pokemon with ID {Id} found in the database.", id);
                return pokemon;
            }

            string url = $"https://pokeapi.co/api/v2/pokemon/{id}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            _logger.LogInformation("Fetching Pokemon with ID {Id} from the API.", id);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(data);
                pokemon = new Pokemon
                {
                    Id = json["id"].Value<int>(),
                    Name = json["name"].Value<string>(),
                    Height = json["height"].Value<int>(),
                    Weight = json["weight"].Value<int>(),
                    BaseExperience = json["base_experience"].Value<int>(),
                    ImageUrl = json["sprites"]
                        ["other"]["official-artwork"]["front_default"]
                        .Value<string>(),
                    Types = json["types"]
                        .Select(t => new PokemonType
                        {
                            Slot = t["slot"].Value<int>(),
                            Type = new TypeInfo { Name = t["type"]["name"].Value<string>() }
                        })
                        .ToList()
                };
                return pokemon;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException("Pokemon not found");
            }
            else
            {
                throw new Exception("Error fetching data from the API.");
            }
        }

        public async Task<Pokemon> GetPokemonByNameAsync(string name)
        {
            _logger.LogInformation("Fetching Pokemon with name {Name} from the database.", name);
            var pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.Name == name);
            if (pokemon != null)
            {
                _logger.LogInformation("Pokemon with name {Name} found in the database.", name);
                return pokemon;
            }

            string url = $"https://pokeapi.co/api/v2/pokemon/{name}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            _logger.LogInformation("Fetching Pokemon with name {Name} from the API.", name);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(data);
                pokemon = new Pokemon
                {
                    Id = json["id"].Value<int>(),
                    Name = json["name"].Value<string>(),
                    Height = json["height"].Value<int>(),
                    Weight = json["weight"].Value<int>(),
                    BaseExperience = json["base_experience"].Value<int>(),
                    ImageUrl = json["sprites"]
                        ["other"]["official-artwork"]["front_default"]
                        .Value<string>(),
                    Types = json["types"]
                        .Select(t => new PokemonType
                        {
                            Slot = t["slot"].Value<int>(),
                            Type = new TypeInfo { Name = t["type"]["name"].Value<string>() }
                        })
                        .ToList()
                };
                return pokemon;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException("Pokemon not found");
            }
            else
            {
                throw new Exception("Error fetching data from the API.");
            }
        }

        public async Task<IEnumerable<Pokemon>> GetPokemonsAsync()
        {
            _logger.LogInformation("Fetching the first 152 Pokemons from the database.");
            var pokemons = new List<Pokemon>();

            for (int i = 1; i <= 152; i++)
            {
                string url = $"https://pokeapi.co/api/v2/pokemon/{i}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(data);
                    var pokemon = new Pokemon
                    {
                        Id = json["id"].Value<int>(),
                        Name = json["name"].Value<string>(),
                        Height = json["height"].Value<int>(),
                        Weight = json["weight"].Value<int>(),
                        BaseExperience = json["base_experience"].Value<int>(),
                        ImageUrl = json["sprites"]
                            ["other"]["official-artwork"]["front_default"]
                            .Value<string>(),
                        Types = json["types"]
                            .Select(t => new PokemonType
                            {
                                Slot = t["slot"].Value<int>(),
                                Type = new TypeInfo { Name = t["type"]["name"].Value<string>() }
                            })
                            .ToList()
                    };
                    pokemons.Add(pokemon);
                }
            }

            _logger.LogInformation("Fetched {Count} Pokemons from the API.", pokemons.Count);
            return pokemons;
        }

        public async Task<IEnumerable<Pokemon>> GetPokemonsInPokedexAsync()
        {
            _logger.LogInformation("Fetching all Pokemons from the Pokedex database.");
            return await _context.Pokemons.ToListAsync();
        }

        public async Task UpdatePokemonAsync(Pokemon pokemon)
        {
            _logger.LogInformation("Updating a Pokemon with ID {Id} in the database.", pokemon.Id);
            var existingPokemon = await GetPokemonByIdAsync(pokemon.Id);
            if (existingPokemon != null)
            {
                existingPokemon.Name = pokemon.Name;
                existingPokemon.Height = pokemon.Height;
                existingPokemon.Weight = pokemon.Weight;
                existingPokemon.BaseExperience = pokemon.BaseExperience;
                existingPokemon.Types = pokemon.Types;
                _context.Pokemons.Update(existingPokemon);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Pokemon with ID {Id} updated successfully.", pokemon.Id);
            }
            else
            {
                _logger.LogWarning("Pokemon with ID {Id} not found in the database.", pokemon.Id);
                throw new Exception("Pokemon not found.");
            }
        }
    }
}
