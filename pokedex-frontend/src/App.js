import React, { useState, useEffect } from 'react';
import axios from 'axios';
import PokemonCard from './components/PokemonCard';
import BattleArena from './components/BattleArena';
import './App.css';

const App = () => {
    const [pokemons, setPokemons] = useState([]);
    const [searchId, setSearchId] = useState('');
    const [searchName, setSearchName] = useState('');
    const [pokedexPokemons, setPokedexPokemons] = useState([]);

    useEffect(() => {
        fetchPokemons();
    }, []);

    const fetchPokemons = async () => {
        try {
            const response = await axios.get('http://localhost:5037/api/pokemon');
            setPokemons(response.data);
        } catch (error) {
            console.error('Error fetching pokemons:', error);
        }
    };

    const fetchPokemonById = async () => {
        try {
            const response = await axios.get(`http://localhost:5037/api/pokemon/${searchId}`);
            setPokemons([response.data]);
        } catch (error) {
            console.error('Error fetching pokemon by ID:', error);
        }
    };

    const fetchPokemonByName = async () => {
        try {
            const response = await axios.get(`http://localhost:5037/api/pokemon/name/${searchName}`);
            setPokemons([response.data]);
        } catch (error) {
            console.error('Error fetching pokemon by name:', error);
        }
    };

    const fetchFromPokedex = async () => {
        try {
            const response = await axios.get('http://localhost:5037/api/pokemoncommand');
            setPokedexPokemons(response.data);
            setPokemons(response.data); // Update the state to render the Pokedex Pokemons
        } catch (error) {
            console.error('Error fetching pokemons from Pokedex:', error);
        }
    };

    const addToPokedex = async (id) => {
        try {
            const pokemon = pokemons.find(p => p.id === id);
            if (pokemon) {
                await axios.post(`http://localhost:5037/api/pokemoncommand/addById/${id}`);
                alert('Pokemon added to Pokedex!');
            } else {
                alert('Pokemon not found.');
            }
        } catch (error) {
            console.error('Error adding to Pokedex:', error);
            alert('Failed to add Pokemon to Pokedex. Please try again.');
        }
    };

    const deleteFromPokedex = async (id) => {
        try {
            await axios.delete(`http://localhost:5037/api/pokemoncommand/${id}`);
            alert('Pokemon deleted successfully!');
            // Update the state to remove the deleted Pokemon
            setPokemons(pokemons.filter(pokemon => pokemon.id !== id));
            setPokedexPokemons(pokedexPokemons.filter(pokemon => pokemon.id !== id)); // Update pokedexPokemons state as well
        } catch (error) {
            console.error('Error deleting from Pokedex:', error);
            alert('Failed to delete Pokemon. Please try again.');
        }
    };

    return (
        <div className="container">
            <div className="background-box">
                <h1>Pokedex</h1>
            </div>
            <div className="controls">
                <button onClick={fetchPokemons}>Fetch All Pokemon</button>
                <button onClick={fetchFromPokedex}>Fetch from Pokedex</button>
                <input
                    type="text"
                    placeholder="Search by ID"
                    value={searchId}
                    onChange={(e) => setSearchId(e.target.value)}
                />
                <button onClick={fetchPokemonById}>Search by ID</button>
                <input
                    type="text"
                    placeholder="Search by Name"
                    value={searchName}
                    onChange={(e) => setSearchName(e.target.value)}
                />
                <button onClick={fetchPokemonByName}>Search by Name</button>
            </div>
            <div className="pokemon-list">
                {pokemons.map(pokemon => (
                    <PokemonCard
                        key={pokemon.id}
                        pokemon={pokemon}
                        addToPokedex={addToPokedex}
                        deleteFromPokedex={deleteFromPokedex}
                    />
                ))}
            </div>
            {pokemons.length > 1 && <BattleArena pokemons={pokemons} />}
        </div>
    );
};

export default App;