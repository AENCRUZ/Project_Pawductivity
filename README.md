# 🐾 Pawductivity
**A Digital Pet Productivity System** — CS 222 Advanced OOP Project  
Team LAVA · Section CS-2202 · Batangas State University

---

## 🚀 How to Run in Visual Studio Community

### Prerequisites
1. Install [Visual Studio Community 2022](https://visualstudio.microsoft.com/vs/community/) (Windows only — WinForms requires Windows)
2. During installation, select the **.NET Desktop Development** workload

### Steps
1. Open Visual Studio Community
2. Click **Open a project or solution**
3. Navigate to the `Pawductivity` folder and open `Pawductivity.sln`
4. Press **F5** to build and run, or go to **Debug > Start Debugging**

> 💡 To run without the debugger (faster), press **Ctrl+F5** instead.

---

## 📁 Project Structure

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
        ├── LoginForm.cs        ← Start screen
        ├── DashboardForm.cs    ← Main screen (pet + tasks)
        ├── TaskEditForm.cs     ← Add/Edit task dialog
        ├── ShopForm.cs         ← Buy items for your pet
        └── StatsForm.cs        ← Productivity analytics

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
