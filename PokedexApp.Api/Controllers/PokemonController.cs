using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PokedexApp.Models;
using PokedexApp.Services;

namespace PokedexApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet]
        public async Task<IEnumerable<Pokemon>> Get()
        {
            return await _pokemonService.GetPokemonsAsync();
        }

        [HttpGet("{id}")]
        public async Task<Pokemon> GetById(int id)
        {
            return await _pokemonService.GetPokemonByIdAsync(id);
        }

        [HttpGet("name/{name}")]
        public async Task<Pokemon> GetByName(string name)
        {
            return await _pokemonService.GetPokemonByNameAsync(name);
        }
    }
}
