# GovaTransit – Blazor Server + Radzen + MongoDB
Plateforme de gestion des lignes d’autobus, tickets, transactions et administration.

## Description du Projet
GovaTransit est une application web construite avec Blazor Server (.NET 8), utilisant Radzen.Blazor pour l’interface et MongoDB pour la base de données.  
L’objectif est de fournir un système permettant de gérer les lignes d’autobus, acheter des tickets, suivre les transactions et administrer le système.

## Technologies utilisées
- Blazor Server (.NET 8)
- Radzen.Blazor
- MongoDB
- MongoDB.Driver
- Bootstrap 5
- C#
- .NET Dependency Injection

## Structure du Projet
/BlazorApp1
 ├── Components
 │    └── Layout
 │         ├── MainLayout.razor
 │         ├── NavLayout.razor
 │         └── Sidebar.razor
 ├── Models
 │    ├── Line.cs
 │    ├── Ticket.cs
 │    ├── User.cs
 │    └── Log.cs
 ├── Pages
 │    ├── Lines
 │    ├── Tickets
 │    ├── BuyTicket
 │    ├── Admin
 │    └── Dashboard
 ├── Services
 │    ├── LineService.cs
 │    ├── TicketService.cs
 │    ├── UserService.cs
 │    └── LogService.cs
 ├── App.razor
 └── Program.cs

## Fonctionnalités Actuelles
### Gestion des Lignes
- Création, modification, suppression
- Affichage via DataGrid Radzen
- Validation des données

### Achat de Tickets
- Formulaire interactif
- Dropdown dynamique basé sur les lignes disponibles
- Validation des champs
- Enregistrement dans MongoDB
- Journalisation des actions

### Interface Radzen
- Sidebar complète
- Navigation fonctionnelle
- Thème Material
- Icônes cohérentes

### Services MongoDB
Implémentent les opérations CRUD principales.

## Installation & Exécution
### Prérequis
- .NET 8
- MongoDB local ou Atlas
- Visual Studio 2022 ou VS Code

### Configuration MongoDB (appsettings.json)
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "GovaTransitDB"
  }
}

### Commandes
dotnet restore  
dotnet run  

L’application sera disponible sur : https://localhost:7107

## Scripts MongoDB

### Création des collections
db.createCollection("Lines");
db.createCollection("Tickets");
db.createCollection("Users");
db.createCollection("Logs");

### Exemples de documents
Lines :
{
  "Name": "Line A",
  "LineCode": "A01",
  "Description": "Centre-ville → South End"
}

Tickets :
{
  "LineCode": "A01",
  "Price": 3.25,
  "PurchaseDate": ISODate("2025-12-07T10:30:00Z")
}

Users :
{
  "UserId": "USR001",
  "Username": "guillaume",
  "Email": "guik@gmail.com",
  "Role": "Admin"
}

## Requêtes CRUD

### Lecture (READ)
db.Lines.find({ LineCode: "A01" })

### Mise à jour (UPDATE)
db.Lines.updateOne(
  { LineCode: "A01" },
  { $set: { Name: "Line A Express" } }
)

### Suppression (DELETE)
db.Tickets.deleteOne({ _id: ObjectId("656f1c42123abc9876543210") })

## Conclusion
Le projet présente une intégration complète entre Blazor Server, Radzen et MongoDB, avec une architecture claire et évolutive.

## Recommandations futures
- Ajouter l’authentification utilisateur
- Ajouter un tableau de bord statistique
- Gestion des chauffeurs
- Ajout de SignalR pour notifications en temps réel
- Export PDF des tickets ou rapports
