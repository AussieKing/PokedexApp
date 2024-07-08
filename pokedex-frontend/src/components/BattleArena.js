import React, { useState } from 'react';
import './BattleArena.css';

const BattleArena = ({ pokemons }) => {
    const [battleLog, setBattleLog] = useState([]);
    const [selectedPokemon1, setSelectedPokemon1] = useState(null);
    const [selectedPokemon2, setSelectedPokemon2] = useState(null);

    const handleBattle = () => {
        if (!selectedPokemon1 || !selectedPokemon2) {
            setBattleLog(['Please select two Pokemons to battle.']);
            return;
        }

        const pokemon1 = pokemons.find(p => p.name === selectedPokemon1);
        const pokemon2 = pokemons.find(p => p.name === selectedPokemon2);

        const log = [];
        let hp1 = pokemon1.stats.find(stat => stat.stat.name === 'hp').baseStat;
        let hp2 = pokemon2.stats.find(stat => stat.stat.name === 'hp').baseStat;

        while (hp1 > 0 && hp2 > 0) {
            if (Math.random() > 0.5) {
                // Pokémon 1 attacks Pokémon 2
                const attack = calculateAttack(pokemon1);
                const block = calculateBlock(pokemon2);
                const damage = Math.max(0, attack - block);
                hp2 -= damage;
                log.push(`${pokemon1.name} attacks ${pokemon2.name} for ${damage} damage.`);
            } else {
                // Pokémon 2 attacks Pokémon 1
                const attack = calculateAttack(pokemon2);
                const block = calculateBlock(pokemon1);
                const damage = Math.max(0, attack - block);
                hp1 -= damage;
                log.push(`${pokemon2.name} attacks ${pokemon1.name} for ${damage} damage.`);
            }
        }

        if (hp1 <= 0) {
            log.push(`${pokemon2.name} wins!`);
        } else {
            log.push(`${pokemon1.name} wins!`);
        }

        setBattleLog(log);
    };

    const calculateAttack = (pokemon) => {
        const attackStats = ['attack', 'special-attack'];
        const stat = pokemon.stats.find(stat => attackStats.includes(stat.stat.name));
        return Math.floor(stat.baseStat * Math.random());
    };

    const calculateBlock = (pokemon) => {
        const defenseStats = ['defense', 'special-defense'];
        const stat = pokemon.stats.find(stat => defenseStats.includes(stat.stat.name));
        return Math.floor(stat.baseStat * Math.random());
    };

    return (
        <div className="battle-arena">
            <h2>Battle Arena</h2>
            <div>
                <select onChange={(e) => setSelectedPokemon1(e.target.value)}>
                    <option value="">Select Battle Pokemon 1</option>
                    {pokemons.map(pokemon => (
                        <option key={pokemon.id} value={pokemon.name}>
                            {pokemon.name}
                        </option>
                    ))}
                </select>
                <select onChange={(e) => setSelectedPokemon2(e.target.value)}>
                    <option value="">Select Battle Pokemon 2</option>
                    {pokemons.map(pokemon => (
                        <option key={pokemon.id} value={pokemon.name}>
                            {pokemon.name}
                        </option>
                    ))}
                </select>
                <button onClick={handleBattle}>Battle Pokemons</button>
            </div>
            <div className="battle-log">
                {battleLog.map((entry, index) => (
                    <p key={index}>{entry}</p>
                ))}
            </div>
        </div>
    );
};

export default BattleArena;
