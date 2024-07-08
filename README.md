# Pokedex App

The Pokedex App is a web application that allows users to search for Pokemon, add them to a personal Pokedex, and even simulate battles between selected Pokemon. The app uses a combination of a .NET backend and a React frontend to provide a seamless user experience.

## Table of Contents

- [Features](#features)
- [Installation and Usage](#installation-and-usage)
- [Technologies Used](#technologies-used)
- [Screenshots](#screenshots)
- [Live URL and Repository](#live-url-and-repository)
- [Credits](#credits)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Search Pokemon**: Search for Pokemon by ID or name using the PokeAPI.
- **Add to Pokedex**: Add your favorite Pokemon to your personal Pokedex.
- **Delete from Pokedex**: Remove Pokemon from your Pokedex.
- **Battle Arena**: Select two Pokemon and simulate a battle with animations and random attack/defense mechanisms.

## Installation and Usage

### Backend 
1. **Clone the repository**:
   ```b
   git clone https://github.com/AussieKing/PokedexApp
   cd PokedexApp

2. Navigate to the backend directory:

```
cd PokedexApp.Api
```
3. Restore dependencies and build the project:

```
dotnet restore
dotnet build
```
4. Run the application:
```
dotnet run
```
The backend should now be running on http://localhost:5037.

### Frontend
1. Navigate to the frontend directory:
```
cd pokedex-frontend
```
2. Install dependencies:
```
npm install
```
3. Run the application:
```
npm start
```
The frontend should now be running on http://localhost:3000.

## Technologies Used
### Backend:
***.NET Core***

***Entity Framework Core***
						
***Microsoft SQL Server***
						
***ASP.NET Core MVC***

***PokeAPI (for fetching Pokemon data)***

### Frontend:
***React***

***Axios***

***CSS for styling***

## Screenshots
### Fetch 152 Pokemons:
![Fetch-152-Pokemons](https://github.com/AussieKing/PokedexApp/assets/126050763/3b725084-4b00-45a5-ae64-70feb20b2c34)

### Fetch Pokedex:
![Fetch-Pokedex](https://github.com/AussieKing/PokedexApp/assets/126050763/3001301f-e384-42ae-a0d7-89577f8b6e19)
![SQL-Pokedex](https://github.com/AussieKing/PokedexApp/assets/126050763/abd2fbd1-aeb6-4ad1-9d3a-03acf626bfa1)

### Add Pokeomon to Pokedex:
![Added-Pokemon](https://github.com/AussieKing/PokedexApp/assets/126050763/b6c44691-df91-45da-a779-23e70969bde7)

### Delete Pokemon from Pokedex:
![Delete-Pokemon](https://github.com/AussieKing/PokedexApp/assets/126050763/12ec2875-c191-4841-ac2e-533591cecf4f)

### Battle Pokemons:
![Battle](https://github.com/AussieKing/PokedexApp/assets/126050763/76f4282d-c993-4e73-b4c5-e20b7ba5da42)

### Battle Result:
![Battlefield](https://github.com/AussieKing/PokedexApp/assets/126050763/79b7575f-db06-4446-babe-c43a5f321623)


## Live URL and Repository
The repository can be accessed at the following URL: [Pokedex App](https://github.com/AussieKing/PokedexApp)

## Credits
This project was created by [AussieKing](https://github.com/AussieKing), thanks to the [PokeAPI](https://pokeapi.co/) for providing the Pokemon data.

## Contributing
Please fork the repository and create a pull request with your changes. For major changes, please open an issue first to discuss what you would like to change.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
