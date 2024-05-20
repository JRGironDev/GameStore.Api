using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
    new (1, "Street Fighter II", "Fighting", 9.99M, new DateOnly(1991, 2, 6)),
    new (2, "The Legend of Zelda: Ocarina of Time", "Action-adventure", 49.99M, new DateOnly(1998, 11, 23)),
    new (3, "Super Mario 64", "Platform", 39.99M, new DateOnly(1996, 6, 23)),
    new (4, "Final Fantasy VII", "Role-playing", 49.99M, new DateOnly(1997, 1, 31)),
    new (5, "Metal Gear Solid", "Stealth", 29.99M, new DateOnly(1998, 10, 21)),
    new (6, "Half-Life", "First-person shooter", 9.99M, new DateOnly(1998, 11, 19)),
];

// GET /games
app.MapGet("games", () => games);

// GET /games/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id))
    .WithName(GetGameEndpointName);

// POST /games
app.MapPost("games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
        );

    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

app.Run();
