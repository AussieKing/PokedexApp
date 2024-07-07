import React from 'react';
import axios from 'axios';

const PokemonCard = ({ pokemon, refreshPokemons }) => {
    const handleAddToPokedex = async () => {
        try {
            await axios.post(`http://localhost:5037/api/pokemoncommand/addById/${pokemon.id}`);
            refreshPokemons();
            showNotification('Pokemon added to Pokedex!', 'success');
        } catch (error) {
            console.error('Error adding to Pokedex:', error);
        }
    };

    const handleDeleteFromPokedex = async () => {
        try {
            await axios.delete(`http://localhost:5037/api/pokemoncommand/${pokemon.id}`);
            refreshPokemons();
            showNotification('Pokemon deleted from Pokedex.', 'error');
        } catch (error) {
            console.error('Error deleting from Pokedex:', error);
        }
    };

    const showNotification = (message, type) => {
        const notification = document.createElement('div');
        notification.className = `notification ${type}`;
        notification.innerText = message;
        document.body.appendChild(notification);
        setTimeout(() => {
            document.body.removeChild(notification);
        }, 3000);
    };

    const totalScore = pokemon.hp + pokemon.attack + pokemon.defense + pokemon.specialAttack + pokemon.specialDefense + pokemon.speed;

    return (
        <div className="pokemon-card">
            <h2>{pokemon.name}</h2>
            <img src={pokemon.imageUrl} alt={pokemon.name} onError={(e) => { e.target.onerror = null; e.target.src = "default-image-url.png" }} />
            <p>Type: {pokemon.types?.map(type => type.type.name).join(', ') || 'Unknown'}</p>
            <p>HP: {pokemon.hp}</p>
            <p>Attack: {pokemon.attack}</p>
            <p>Defense: {pokemon.defense}</p>
            <p>Special Attack: {pokemon.specialAttack}</p>
            <p>Special Defense: {pokemon.specialDefense}</p>
            <p>Speed: {pokemon.speed}</p>
            <p>Total Score: {totalScore}</p>
            <p>Height: {pokemon.height}</p>
            <p>Weight: {pokemon.weight}</p>
            <div className="pokemon-actions">
                <button onClick={handleAddToPokedex}>Add to Pokedex</button>
                <button onClick={handleDeleteFromPokedex}>Delete from Pokedex</button>
            </div>
        </div>
    );
};

export default PokemonCard;
