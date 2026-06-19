# Club de Football — Application Web complète

Application de gestion de club de football développée avec **ASP.NET Core** (backend, Clean Architecture) et **Angular** (frontend), avec **MySQL** comme base de données et **Dapper** comme micro-ORM.

L'application permet la consultation publique des joueurs, équipes, matchs et statistiques, ainsi qu'une administration complète (authentifiée par JWT) pour gérer joueurs, équipes, matchs, entraînements, blessures, staff et contrats.

---

## Sommaire

- [Prérequis](#prérequis)
- [Installation](#installation)
- [Création de la base de données](#création-de-la-base-de-données)
- [Lancement du Backend](#lancement-du-backend)
- [Lancement du Frontend](#lancement-du-frontend)
- [Comptes de test](#comptes-de-test)
- [Architecture du projet](#architecture-du-projet)
- [Fonctionnalités](#fonctionnalités)

---

## Prérequis

| Outil | Version utilisée |
|---|---|
| .NET SDK | 10.0 |
| Node.js | 20.x ou plus récent |
| Angular CLI | 19.x (syntaxe `@if` / `@for` / `@switch`) |
| SGBD | MySQL 8.0 |
| IDE recommandé | Visual Studio Code |

Vérifiez vos versions installées avec :

```bash
dotnet --version
node --version
ng version
mysql --version
```

---

## Installation

### 1. Cloner le dépôt

```bash
git clone https://github.com/<votre-utilisateur>/<nom-du-depot>.git
cd <nom-du-depot>
```

### 2. Structure du projet

```
club-foot/
├── Backend/
│   ├── Api/              → Point d'entrée HTTP (Endpoints, Middleware, Program.cs)
│   ├── Core/              → Logique métier pure (Models, IGateways, UseCases)
│   ├── Infrastructure/    → Accès aux données (Dapper, Repositories, Gateways)
│   ├── Api.slnx
│   └── club_foot3.sql     → Script de création de la base de données
└── Frontend/              → Application Angular
```

---

## Création de la base de données

### 1. Ouvrir MySQL Workbench (ou tout client MySQL)

### 2. Exécuter le script SQL fourni

Le fichier `Backend/club_foot3.sql` contient :
- La création de la base `club_foot3`
- Toutes les tables (club, equipe, poste, joueur, staff, contrat, match_foot, entrainement, presence_entrainement, statistique_match, blessure)
- Les index, triggers et la vue `vue_joueurs_equipes`
- Les données de test (clubs, équipes, joueurs, matchs, etc.)

```bash
mysql -u root -p < Backend/club_foot3.sql
```

Ou bien, dans MySQL Workbench : ouvrez le fichier `club_foot3.sql` et cliquez sur **Execute (⚡)**.

### 3. Créer la table d'administration

Cette table n'est pas incluse dans le script principal et doit être créée séparément pour activer l'authentification :

```sql
USE club_foot3;

CREATE TABLE admin (
    id_admin INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(100) NOT NULL,
    password VARCHAR(100) NOT NULL
);

INSERT INTO admin (username, password) VALUES ('admin', 'admin123');
```

### 4. Configurer la chaîne de connexion

Ouvrez `Backend/Api/appsettings.json` et adaptez si nécessaire :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=club_foot3;Uid=root;Pwd=;"
  },
  "Jwt": {
    "Key": "ClubFootSecretKey2024XYZ123456789",
    "Issuer": "ClubFootAPI",
    "Audience": "ClubFootClient"
  }
}
```

> Si votre installation MySQL possède un mot de passe root, remplacez `Pwd=;` par `Pwd=votreMotDePasse;`.

---

## Lancement du Backend

Dans un terminal, à la racine du dépôt :

```bash
cd Backend/Api
dotnet restore
dotnet build
dotnet run
```

L'API démarre sur :

```
http://localhost:5207
```

Vous pouvez vérifier que l'API fonctionne en ouvrant :

```
http://localhost:5207/api/joueurs
```

Vous devriez voir la liste des joueurs au format JSON.

### Documentation Swagger (environnement de développement)

```
http://localhost:5207/swagger
```

---

## Lancement du Frontend

Dans un **second terminal**, à la racine du dépôt :

```bash
cd Frontend
npm install
ng serve
```

L'application Angular démarre sur :

```
http://localhost:4200
```

> **Important** : le Backend et le Frontend doivent tourner **simultanément**, chacun dans son propre terminal.

---

## Comptes de test

| Rôle | Username | Mot de passe |
|---|---|---|
| Administrateur | `admin` | `admin123` |

Connectez-vous via la page `/login` pour accéder au tableau de bord d'administration (`/admin`).

---

## Architecture du projet

### Backend — Clean Architecture

```
Api            →  Endpoints HTTP, Middleware, configuration JWT/CORS
   ↓ dépend de
Core           →  Models, interfaces (IGateways), logique métier (UseCases)
   ↑ implémenté par
Infrastructure →  Repositories Dapper, Gateways, connexion MySQL
```

- **Core** ne possède aucune dépendance externe (pas de Dapper, pas de MySQL) : c'est le cœur métier pur de l'application, conforme au principe de **Dependency Inversion** (SOLID).
- **Infrastructure** implémente les interfaces définies dans `Core` et gère l'accès aux données via **Dapper** exclusivement (aucun Entity Framework).
- **Api** orchestre la requête HTTP entrante, l'injection de dépendances et la sécurité (JWT, CORS), sans jamais accéder directement à la base de données.

Flux d'une requête :

```
Angular Component → Angular Service → API Endpoint → UseCase (Core) → Gateway (Core) → Repository (Infrastructure) → Dapper → MySQL
```

Et la réponse remonte la chaîne inverse jusqu'à l'interface utilisateur.

### Frontend — Angular

```
src/app/
├── components/        → Composants réutilisables (header, navbar...)
├── pages/
│   ├── public/        → Pages accessibles sans authentification
│   └── admin/         → Pages protégées par AuthGuard
├── services/
│   ├── api/           → Services HttpClient + modèles TypeScript
│   ├── auth-state.service.ts
│   └── auth.interceptor.ts
├── guards/
│   └── auth.guard.ts
├── app.routes.ts
└── app.config.ts
```

- **Services Angular** : toute la gestion de l'état (liste des joueurs, équipes, authentification...) est centralisée dans des services injectables, sans bibliothèque externe de state management (pas de NgRx).
- **AuthGuard** : protège les routes `/admin/*` ; redirige vers `/login` si l'utilisateur n'est pas authentifié.
- **AuthInterceptor** : ajoute automatiquement le token JWT (`Authorization: Bearer ...`) à chaque requête HTTP sortante.
- **Syntaxe Angular moderne** : utilisation de `@if`, `@for` et `@switch` dans les templates, ainsi que des **Signals** pour la réactivité locale des composants.

---

## Fonctionnalités

### Partie publique
- Consultation des joueurs
- Consultation des équipes
- Consultation du calendrier des matchs et de leurs résultats (avec badge Victoire / Défaite / Nul via `@switch`)
- Consultation des statistiques par match

### Partie privée (authentification JWT)
- Connexion administrateur
- Tableau de bord avec accès à tous les modules de gestion

### Administration (CRUD complet)
- Gestion des joueurs
- Gestion des équipes
- Gestion des matchs
- Gestion des entraînements
- Gestion des blessures
- Gestion du staff
- Gestion des contrats

---

## Technologies utilisées

**Backend** : ASP.NET Core, C#, Dapper, MySql.Data, JWT Bearer Authentication, Swagger

**Frontend** : Angular (standalone components, Signals, nouvelle syntaxe de contrôle de flux), TypeScript, HttpClient, Angular Router

**Base de données** : MySQL 8.0