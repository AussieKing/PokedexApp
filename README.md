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


## Live URL and Repository
The repository can be accessed at the following URL: [Pokedex App](https://github.com/AussieKing/PokedexApp)

## Credits
This project was created by [AussieKing](https://github.com/AussieKing), thanks to the [PokeAPI](https://pokeapi.co/) for providing the Pokemon data.

## Contributing
Please fork the repository and create a pull request with your changes. For major changes, please open an issue first to discuss what you would like to change.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
