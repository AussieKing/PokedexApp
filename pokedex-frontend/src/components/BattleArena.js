import React, { useState } from 'react';
import './BattleArena.css';

const BattleArena = ({ pokemons }) => {
    const [battlePokemon1, setBattlePokemon1] = useState(null);
    const [battlePokemon2, setBattlePokemon2] = useState(null);
    const [battleLog, setBattleLog] = useState('');

    const handleBattle = () => {
        if (!battlePokemon1 || !battlePokemon2) {
            setBattleLog('Please select two Pokémon to battle.');
            return;
        }

        if (battlePokemon1.id === battlePokemon2.id) {
            setBattleLog('Please select two different Pokémon.');
            return;
        }

        setBattleLog('');
        simulateBattle(battlePokemon1, battlePokemon2);
    };

    const simulateBattle = (pokemon1, pokemon2) => {
        let log = '';
        let hp1 = pokemon1.stats.find(stat => stat.stat.name === 'hp').baseStat;
        let hp2 = pokemon2.stats.find(stat => stat.stat.name === 'hp').baseStat;

        while (hp1 > 0 && hp2 > 0) {
            const damage1 = Math.floor(Math.random() * 20) + 10;
            const damage2 = Math.floor(Math.random() * 20) + 10;
            hp2 -= damage1;
            log += `${pokemon1.name} attacks ${pokemon2.name} for ${damage1} damage.\n`;
            if (hp2 <= 0) break;
            hp1 -= damage2;
            log += `${pokemon2.name} attacks ${pokemon1.name} for ${damage2} damage.\n`;
        }

        const winner = hp1 > 0 ? pokemon1.name : pokemon2.name;
        log += `${winner} wins!`;
        setBattleLog(log);
    };

    return (
        <div className="battle-arena">
            <div className="background-box">
                <h2>Battle Arena</h2>
            </div>
            <div className="controls">
                <select onChange={(e) => setBattlePokemon1(pokemons.find(p => p.id == e.target.value))}>
                    <option>Select Battle Pokemon 1</option>
                    {pokemons.map(p => (
                        <option key={p.id} value={p.id}>
                            {p.name}
                        </option>
                    ))}
                </select>
                <select onChange={(e) => setBattlePokemon2(pokemons.find(p => p.id == e.target.value))}>
                    <option>Select Battle Pokemon 2</option>
                    {pokemons.map(p => (
                        <option key={p.id} value={p.id}>
                            {p.name}
                        </option>
                    ))}
                </select>
                <button onClick={handleBattle}>Battle Pokemons</button>
            </div>
            <div className="battle-log">
                <pre>{battleLog}</pre>
            </div>
        </div>
    );
};

export default BattleArena;