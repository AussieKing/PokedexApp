using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PokedexApp.Data;
using PokedexApp.Models;

namespace PokedexApp.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly HttpClient _httpClient;
        private readonly PokedexContext _context;

        public PokemonRepository(HttpClient httpClient, PokedexContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task AddPokemonAsync(Pokemon pokemon)
        {
            pokemon.Id = 0;
            _context.Pokemons.Add(pokemon);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePokemonAsync(int id)
        {
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon != null)
            {
                _context.Pokemons.Remove(pokemon);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Pokemon not found.");
            }
        }

        public async Task<Pokemon> GetPokemonByIdAsync(int id)
        {
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon != null)
            {
                return pokemon;
            }

            string url = $"https://pokeapi.co/api/v2/pokemon/{id}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                pokemon = JsonConvert.DeserializeObject<Pokemon>(json);
                return pokemon;
            }
            else
            {
                throw new Exception("Error fetching data from the API.");
            }
        }

        public async Task<Pokemon> GetPokemonByNameAsync(string name)
        {
            var pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.Name == name);
            if (pokemon != null)
            {
                return pokemon;
            }

            string url = $"https://pokeapi.co/api/v2/pokemon/{name}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                pokemon = JsonConvert.DeserializeObject<Pokemon>(data);
                return pokemon;
            }
            else
            {
                throw new Exception("Error fetching data from the API.");
            }
        }

        public async Task<IEnumerable<Pokemon>> GetPokemonsAsync()
        {
            var pokemons = new List<Pokemon>();

            for (int i = 1; i <= 152; i++)
            {
                string url = $"https://pokeapi.co/api/v2/pokemon/{i}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var pokemon = JsonConvert.DeserializeObject<Pokemon>(json);
                    pokemons.Add(pokemon);
                }
            }

            return pokemons;
        }

        public async Task UpdatePokemonAsync(Pokemon pokemon)
        {
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
            }
            else
            {
                throw new Exception("Pokemon not found.");
            }
        }
    }
}
