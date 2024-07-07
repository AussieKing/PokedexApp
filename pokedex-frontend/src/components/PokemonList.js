import React, { useEffect, useState } from 'react';
import axios from 'axios';
import PokemonCard from './PokemonCard'; 
import '../App.css'; 

const PokemonList = () => {
    const [pokemons, setPokemons] = useState([]);
    const [searchType, setSearchType] = useState('all');
    const [searchInput, setSearchInput] = useState('');
    const [error, setError] = useState('');

    const fetchPokemons = async () => {
        try {
            setError('');
            let response;
            const externalBaseUrl = 'http://localhost:5037/api/pokemon'; // External API
            const internalBaseUrl = 'http://localhost:5037/api/pokemoncommand'; // Internal API

            switch (searchType) {
                case 'all':
                    response = await axios.get(`${externalBaseUrl}`);
                    break;
                case 'id':
                    if (searchInput) {
                        response = await axios.get(`${externalBaseUrl}/${searchInput}`);
                    } else {
                        setError('Please enter a valid ID');
                        return;
                    }
                    break;
                case 'name':
                    if (searchInput) {
                        response = await axios.get(`${externalBaseUrl}/name/${searchInput}`);
                    } else {
                        setError('Please enter a valid name');
                        return;
                    }
                    break;
                case 'pokedex':
                    response = await axios.get(`${internalBaseUrl}`);
                    break;
                default:
                    response = { data: [] };
                    break;
            }
            setPokemons(Array.isArray(response.data) ? response.data : [response.data]);
        } catch (error) {
            console.error('Error fetching pokemons:', error);
            setError('Error fetching pokemons. Please try again later.');
        }
    };

    useEffect(() => {
        fetchPokemons();
    }, [searchType, searchInput]);

    const handleSearch = (type) => {
        setSearchType(type);
    };

    return (
        <div className="container">
            <h1>Pokédex</h1>
            <div className="controls">
                <button onClick={() => handleSearch('all')}>Fetch All Pokémon</button>
                <button onClick={() => handleSearch('pokedex')}>Fetch from Pokedex</button>
                <input
                    type="text"
                    placeholder="Search by ID"
                    value={searchInput}
                    onChange={(e) => setSearchInput(e.target.value)}
                />
                <button onClick={() => handleSearch('id')}>Search by ID</button>
                <input
                    type="text"
                    placeholder="Search by Name"
                    value={searchInput}
                    onChange={(e) => setSearchInput(e.target.value)}
                />
                <button onClick={() => handleSearch('name')}>Search by Name</button>
            </div>
            {error && <p className="error">{error}</p>}
            <div className="pokemon-list">
                {pokemons.map((pokemon) => (
                    <PokemonCard key={pokemon.id} pokemon={pokemon} refreshPokemons={fetchPokemons} />
                ))}
            </div>
        </div>
    );
};

export default PokemonList;
