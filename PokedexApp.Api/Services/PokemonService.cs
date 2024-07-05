using System.Collections.Generic;
using System.Threading.Tasks;
using PokedexApp.Models;
using PokedexApp.Repositories;

namespace PokedexApp.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonService(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public async Task<Pokemon> GetPokemonByIdAsync(int id)
        {
            return await _pokemonRepository.GetPokemonByIdAsync(id);
        }

        public async Task<Pokemon> GetPokemonByNameAsync(string name)
        {
            return await _pokemonRepository.GetPokemonByNameAsync(name);
        }

        public async Task<IEnumerable<Pokemon>> GetPokemonsAsync()
        {
            return await _pokemonRepository.GetPokemonsAsync();
        }

        public async Task AddPokemonAsync(Pokemon pokemon)
        {
            await _pokemonRepository.AddPokemonAsync(pokemon);
        }

        public async Task AddPokemonByIdAsync(int id)
        {
            var pokemon = await _pokemonRepository.GetPokemonByIdAsync(id);
            if (pokemon != null)
            {
                await _pokemonRepository.AddPokemonAsync(pokemon);
            }
        }

        public async Task AddPokemonByNameAsync(string name)
        {
            var pokemon = await _pokemonRepository.GetPokemonByNameAsync(name);
            if (pokemon != null)
            {
                await _pokemonRepository.AddPokemonAsync(pokemon);
            }
        }

        public async Task DeletePokemonAsync(int id)
        {
            await _pokemonRepository.DeletePokemonAsync(id);
        }

        public async Task UpdatePokemonAsync(Pokemon pokemon)
        {
            await _pokemonRepository.UpdatePokemonAsync(pokemon);
        }
    }
}
