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

        [HttpPost("addById/{id}")]
        public async Task<IActionResult> AddById(int id)
        {
            await _pokemonService.AddPokemonByIdAsync(id);
            return CreatedAtAction(nameof(GetById), new { id = id }, null);
        }

        [HttpPost("addByName/{name}")]
        public async Task<IActionResult> AddByName(string name)
        {
            await _pokemonService.AddPokemonByNameAsync(name);
            return CreatedAtAction(nameof(GetByName), new { name = name }, null);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Pokemon pokemon)
        {
            await _pokemonService.UpdatePokemonAsync(pokemon);
            return Ok("The Pokemon has been updated in your Pokedex");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _pokemonService.DeletePokemonAsync(id);
            return Ok("The Pokemon has been deleted from your Pokedex");
        }
    }
}

// add a different controller to perform UPDATE functions like ADD and EDIT
