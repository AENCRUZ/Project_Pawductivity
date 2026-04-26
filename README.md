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

## 🔄 Gameplay Loop

```
Login → Add Task → Complete Task → Pet Reacts → Earn Coins → Buy Items
           ↑                                                       |
           └───────────────────── loop ────────────────────────────┘
```

Every task you complete rewards you and your pet. Every task you miss costs you both. The shop gives you something to work toward, and the streak system keeps you coming back daily.

---

## 🌱 Pet Evolution

Your pet evolves through five stages as you level up. Each level costs `current_level × 50 XP` — so leveling gets progressively harder.

| Stage | Level | Cat 🐱 | Dog 🐶 |
|---|---|---|---|
| 🥚 **Egg** | 1 | `🥚` | `🥚` |
| 🐱 **Baby** | 2–3 | `🐱` | `🐶` |
| 🐈 **Junior** | 4–6 | `🐈‍⬛` | `🐕` |
| 🐈 **Adult** | 7–9 | `🐈` | `🦮` |
| ✨ **Legend** | 10+ | `✨🐈‍⬛✨` | `✨🐕‍🦺✨` |

**How XP works:** cats earn more XP per task but lose mood faster when they miss one. Dogs earn slightly less XP but are more forgiving on mood — though they take more health damage.

| | High priority | Medium priority | Low priority |
|---|---|---|---|
| 🐱 Cat XP | +30 | +20 | +10 |
| 🐶 Dog XP | +25 | +15 | +8 |

> Each pet starts with **Health 80 · Mood 70 · Level 1 · 0 coins**. All stats are clamped between 0–100.

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

## 🛍️ Shop Items

Coins are earned by completing tasks (`XP gained ÷ 2` per task). Spend them in the shop to restore your pet's health and mood.

| Item | Cost | Health | Mood |
|---|:---:|:---:|:---:|
| 🎀 Pink Ribbon | 10 | — | +15 |
| 🍪 Star Cookie | 15 | +20 | +10 |
| 🍓 Strawberry Milk | 20 | +30 | — |
| 🌸 Flower Crown | 25 | — | +30 |
| 🛏️ Cozy Blanket | 30 | +25 | +20 |
| 🌈 Rainbow Toy | 40 | — | +40 |

---

## 😺 Mood System

Your pet's mood is a 0–100 value that maps to one of four states:

| Mood value | State | Emoji | Effect |
|---|---|---|---|
| 70–100 | Happy | `🐾✨` | Positive greetings, full reactions |
| 40–69 | Neutral | `🐾` | Calm, waiting |
| 20–39 | Sad | `😿` / `🥺` | Withdrawn, needs attention |
| 0–19 | Sick | `🤒` | Urgent — complete your tasks! |

---

## 🎓 OOP Principles

Pawductivity is designed as a showcase of core Object-Oriented Programming concepts:

### 🔒 Encapsulation — `Pet.cs`
`_health`, `_mood`, `_xp`, `_level`, and `_coins` are private backing fields. Their public properties add controlled logic on set — for example, `Health` clamps its value between 0 and 100, `XP` automatically triggers `CheckLevelUp()`, and `Coins` can never go negative. No code outside `Pet` can break these invariants.

### 🧬 Inheritance — `Pet.cs`, `PetTypes.cs`
`CatPet` and `DogPet` both extend the `abstract` `Pet` base class. Level-up logic (`CheckLevelUp`), evolution progression (`Evolve`), and derived properties like `CurrentMood` and `MoodEmoji` are defined once in `Pet` and shared by both subtypes automatically.

### 🔀 Polymorphism — `PetTypes.cs`
`ReactToTaskCompleted()`, `ReactToTaskMissed()`, and `GetGreeting()` are declared `abstract` in `Pet` and implemented differently in each subclass. Cats gain more XP but lose mood faster (`-20` vs `-12`); dogs recover health faster and give warmer responses. `StageEmoji` is `virtual` so dogs can override the default cat emoji set with their own.

### 🏗️ Abstraction — `GameManager.cs`
All game state — task management, pet updates, streak tracking, shop purchases — is coordinated through `GameManager`. Forms call methods like `CompleteTask()` or `BuyItem()` without knowing how XP is calculated, how evolution is triggered, or how stats are clamped. The complexity is hidden behind a clean interface.

---

## 🌸 Theming

All UI colors and fonts are centralized in **`PawTheme.cs`**. To retheme the entire app, you only need to update values in one file — no digging through individual forms.

```csharp
// PawTheme.cs — change here, updates everywhere
public static Color Primary   = Color.FromArgb(255, 105, 180); // hot pink
public static Color Background = Color.FromArgb(255, 240, 245); // soft blush
```

---

<div align="center">

## 👥 Team

**Team LAVA** · Section CS-2202 · Batangas State University

*Made with 💖 for CS 222 — Advanced Object-Oriented Programming*

</div>
