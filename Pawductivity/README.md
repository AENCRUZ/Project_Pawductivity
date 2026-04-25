# 🐾 Pawductivity
**A Digital Pet Productivity System** — CS 222 Advanced OOP Project  
Team LAVA · Section CS-2202 · Batangas State University

---

## 🚀 How to Run in VS Code

### Prerequisites
1. Install [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (Windows only — WinForms requires Windows)
2. Install VS Code extension: **C# Dev Kit** (by Microsoft)

### Steps
```bash
# 1. Open this folder in VS Code
#    File > Open Folder > select the Pawductivity folder

# 2. Run via terminal
dotnet run

# OR press F5 (uses .vscode/launch.json)
```

---

## 📁 Project Structure

```
Pawductivity/
├── Program.cs              ← Entry point
├── PawTheme.cs             ← All pink colors & fonts (change here to retheme!)
├── Models/
│   ├── Pet.cs              ← Abstract base class (Encapsulation + Inheritance)
│   ├── PetTypes.cs         ← CatPet & DogPet (Polymorphism)
│   ├── TaskItem.cs         ← Task data model
│   └── ShopItem.cs         ← Shop item model
├── Managers/
│   └── GameManager.cs      ← All game logic (Abstraction)
└── Forms/
    ├── LoginForm.cs         ← Start screen
    ├── DashboardForm.cs     ← Main screen (pet + tasks)
    ├── TaskEditForm.cs      ← Add/Edit task dialog
    ├── ShopForm.cs          ← Buy items for your pet
    └── StatsForm.cs         ← Productivity analytics
```

---

## 🎮 Features Implemented

| Feature | Status |
|---|---|
| Login with username & pet name | ✅ |
| Choose Cat 🐱 or Dog 🐶 | ✅ |
| Add / Edit / Delete tasks | ✅ |
| Complete tasks → pet gains XP & mood | ✅ |
| Pet levels up and evolves | ✅ |
| Overdue tasks → pet loses health/mood | ✅ |
| Shop system with coins | ✅ |
| Streak tracking | ✅ |
| Stats & analytics screen | ✅ |
| Pink kawaii theme throughout | ✅ |

---

## 🎨 OOP Principles Applied

- **Encapsulation** — `Pet` uses private fields with public properties (health, mood, XP)
- **Inheritance** — `CatPet` and `DogPet` extend abstract `Pet`
- **Polymorphism** — `ReactToTaskCompleted()`, `ReactToTaskMissed()`, `GetGreeting()` behave differently per pet type
- **Abstraction** — `GameManager` hides how data is managed; forms only call high-level methods

---

## 🌸 Theme Customization
All colors live in `PawTheme.cs`. Change `Primary`, `Background`, etc. to retheme the whole app instantly!
