<div align="center">

# 🐾 Pawductivity

**A Digital Pet Productivity System**

*CS 222 · Advanced Object-Oriented Programming · Batangas State University*

![Team](https://img.shields.io/badge/Team-LAVA-ff69b4?style=for-the-badge)
![Section](https://img.shields.io/badge/Section-CS--2202-c084fc?style=for-the-badge)

![Platform](https://img.shields.io/badge/Windows-0078D4?style=flat-square&logo=windows&logoColor=white)
![Framework](https://img.shields.io/badge/.NET_WinForms-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![IDE](https://img.shields.io/badge/Visual_Studio-5C2D91?style=flat-square&logo=visualstudio&logoColor=white)
![Theme](https://img.shields.io/badge/Theme-Pink_Kawaii_🌸-ff69b4?style=flat-square)

> *Stay productive. Keep your pet happy. Don't let your tasks go overdue.*

</div>

---

## 📖 Overview

**Pawductivity** is a gamified productivity desktop app built with .NET WinForms. You adopt a virtual pet — a cat 🐱 or a dog 🐶 — and your tasks directly affect its health and happiness. Complete tasks on time and your pet thrives, levels up, and evolves. Let them go overdue, and your pet suffers the consequences.

It's a productivity tool with stakes.

---

## 🚀 Getting Started

### Prerequisites

| # | Requirement | Details |
|---|---|---|
| 1 | [Visual Studio Community](https://visualstudio.microsoft.com/vs/community/) | Windows only — WinForms requires Windows |
| 2 | **.NET Desktop Development** workload | Select this during VS installation |

### Running the App

1. Open **Visual Studio Community**
2. Click **Open a project or solution**
3. Navigate to the `Pawductivity/` folder
4. Open `Pawductivity.slnx`
5. Press **F5** to build and run

> 💡 **Tip:** Use `Ctrl + F5` to run without the debugger for a faster startup.

---

## 📁 Project Structure

```
Pawductivity/
├── Pawductivity.slnx          ← Solution file
├── Pawductivity.csproj        ← Project file
├── Program.cs                 ← Entry point
├── PawTheme.cs                ← Centralized theme (colors & fonts)
│
├── Models/
│   ├── Pet.cs                 ← Abstract base class (Encapsulation + Inheritance)
│   ├── PetTypes.cs            ← CatPet & DogPet (Polymorphism)
│   ├── TaskItem.cs            ← Task data model
│   └── ShopItem.cs            ← Shop item model
│
├── Managers/
│   └── GameManager.cs         ← Core game logic (Abstraction)
│
└── Forms/
    ├── LoginForm.cs           ← Start / welcome screen
    ├── DashboardForm.cs       ← Main screen: pet + task list
    ├── TaskEditForm.cs        ← Add & edit task dialog
    ├── ShopForm.cs            ← Coin shop
    └── StatsForm.cs           ← Productivity analytics
```

---

## 🎮 Features

| Feature | Status |
|---|:---:|
| Login with username & pet name | ✅ |
| Choose Cat 🐱 or Dog 🐶 | ✅ |
| Add, edit, and delete tasks | ✅ |
| Complete tasks → pet gains XP & mood | ✅ |
| Pet levels up and evolves | ✅ |
| Overdue tasks → pet loses health & mood | ✅ |
| Coin-based shop system | ✅ |
| Daily streak tracking | ✅ |
| Productivity stats & analytics screen | ✅ |
| Consistent pink kawaii theme | ✅ |

---

## 🎓 OOP Principles

Pawductivity is designed as a showcase of core Object-Oriented Programming concepts:

### 🔒 Encapsulation — `Pet.cs`
Pet stats (health, mood, XP) are stored in **private fields** and accessed only through controlled public properties. Nothing outside the class can corrupt the pet's internal state directly.

### 🧬 Inheritance — `PetTypes.cs`
`CatPet` and `DogPet` both **extend** the abstract `Pet` base class. Shared behaviors (leveling up, losing health) live in the parent; each subclass adds its own personality on top.

### 🔀 Polymorphism — `PetTypes.cs`
Methods like `ReactToTaskCompleted()`, `ReactToTaskMissed()`, and `GetGreeting()` are **overridden** in each subclass. The same method call behaves differently depending on whether the pet is a cat or a dog — no `if/else` type-checking needed.

### 🏗️ Abstraction — `GameManager.cs`
All core game logic lives in `GameManager`. The UI forms never touch raw data directly — they call clean, high-level methods like `CompleteTask()` or `BuyItem()` and let the manager handle the rest.

---

## 🌸 Theming

All UI colors and fonts are centralized in **`PawTheme.cs`**. To retheme the entire app, you only need to update values in one file — no digging through individual forms.

```csharp
// PawTheme.cs — change here, updates everywhere
public static Color Primary   = Color.FromArgb(255, 105, 180); // hot pink
public static Color Background = Color.FromArgb(255, 240, 245); // soft blush
```

---

## 👥 Team

**Team LAVA** · Section CS-2202 · Batangas State University

*Made with 💖 for CS 222 — Advanced Object-Oriented Programming*
