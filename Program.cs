// Importations des espaces de noms nécessaires.
using BlazorApp1.Components;
using BlazorApp1.Services;
using Radzen;

// Crée un constructeur d'application web.
var builder = WebApplication.CreateBuilder(args);

// Ajoute les services au conteneur d'injection de dépendances.
// Configure les composants Razor pour un rendu interactif côté serveur.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Ajoute les services nécessaires pour la bibliothèque de composants Radzen.
builder.Services.AddRadzenComponents();

// Enregistrement des services personnalisés liés à la base de données MongoDB.
// AddScoped : une nouvelle instance est créée pour chaque requête client (par circuit utilisateur).
builder.Services.AddScoped<MongoDbContext>();      // Contexte de la base de données.
builder.Services.AddScoped<UserService>();         // Service pour la gestion des utilisateurs.
builder.Services.AddScoped<LineService>();         // Service pour la gestion des lignes.
builder.Services.AddScoped<StationService>();      // Service pour la gestion des stations.
builder.Services.AddScoped<TicketService>();       // Service pour la gestion des tickets.
builder.Services.AddScoped<TransactionService>(); // Service pour la gestion des transactions.
builder.Services.AddScoped<LogService>();          // Service pour la gestion des logs.
// AddSingleton : une seule instance est créée pour toute la durée de vie de l'application.
builder.Services.AddSingleton<RoleService>();      // Service pour la gestion des rôles (partagé par tous les utilisateurs).



// Construit l'application.
var app = builder.Build();

// Configure le pipeline de traitement des requêtes HTTP.
// En mode production, configure la gestion des erreurs.
if (!app.Environment.IsDevelopment())
{
    // Redirige vers une page d'erreur en cas d'exception non gérée.
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // Active HSTS (HTTP Strict Transport Security) pour forcer l'utilisation de HTTPS.
    app.UseHsts();
}

// Redirige les requêtes HTTP vers HTTPS.
app.UseHttpsRedirection();

// Permet de servir les fichiers statiques (CSS, JS, images) depuis le dossier wwwroot.
app.UseStaticFiles();

// Ajoute une protection contre les attaques de type Cross-Site Request Forgery (CSRF).
app.UseAntiforgery();

// Mappe le composant racine de l'application et active le rendu interactif côté serveur.
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Lance l'application.
app.Run();
