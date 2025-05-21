# 🍽️ RecipeSharing App
 
A simple web app to share, browse, comment, and rate recipes. Built with **ASP.NET Core Web API** and **Angular**.
 
---
 
## 🔧 Tech Stack
 
- **Backend**: ASP.NET 8 Core, Entity Framework Core, JWT Auth
- **Frontend**: Angular, Angular Material
- **Database**: SQL Server (or SQLite)
 
---
 
## 🐞 Challenges Faced
 
- Handling JWT securely between frontend and backend
- Managing dynamic forms (ingredients, steps , instructions)
- Making all Dynamic components to Reuse (card,form-filed,table) in Shared Folder
- Taking time to Load application
- Uploading and previewing recipe images
- Separating business logic into services
 
---
 
## ▶️ How to Run
 
### Backend
 
```bash
cd RecipeSharing.API/Authentication
dotnet restore
dotnet ef database update
dotnet run


## ▶️ How to Run
 
### Angular
 
```bash
cd RecipeSharing.API/Authentication/AuthApi/ClientAPP
npm install
ng serve / npm start  // To run the application.
