# 🐾 Pawductivity
**A Digital Pet Productivity System** — CS 222 Advanced OOP Project  
Team LAVA · Section CS-2202 · Batangas State University

---

## 🚀 How to Run in Visual Studio Community

### Prerequisites
1. Install [Visual Studio Community 2026](https://visualstudio.microsoft.com/vs/community/)  
   *(Windows only — WinForms requires Windows)*
2. During installation, select the **.NET Desktop Development** workload

---

### Steps
1. Open Visual Studio Community
2. Click **Open a project or solution**
3. Navigate to the `Pawductivity` folder
4. Open `Pawductivity.slnx` *(solution file)*
5. Press **F5** to build and run, or go to **Debug > Start Debugging**

> 💡 To run without the debugger (faster), press **Ctrl + F5**

---

## 📁 Project Structure

```
Pawductivity/
├── Pawductivity.slnx          ← Solution file
├── Pawductivity.csproj        ← Project file
├── Program.cs                 ← Entry point
├── PawTheme.cs                ← Theme settings (colors & fonts)
├── Models/
│   ├── Pet.cs                 ← Abstract base class (Encapsulation + Inheritance)
│   ├── PetTypes.cs            ← CatPet & DogPet (Polymorphism)
│   ├── TaskItem.cs            ← Task data model
│   └── ShopItem.cs            ← Shop item model
├── Managers/
│   └── GameManager.cs         ← Game logic (Abstraction)
└── Forms/
    ├── LoginForm.cs           ← Start screen
    ├── DashboardForm.cs       ← Main screen (pet + tasks)
    ├── TaskEditForm.cs        ← Add/Edit task dialog
    ├── ShopForm.cs            ← Shop system
    └── StatsForm.cs           ← Productivity analytics
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
- **Polymorphism** — Methods like `ReactToTaskCompleted()`, `ReactToTaskMissed()`, and `GetGreeting()` behave differently depending on pet type
- **Abstraction** — `GameManager` handles core logic while forms interact through high-level methods

---

## 🌸 Theme Customization
All UI colors are defined in `PawTheme.cs`. Modify values like `Primary` and `Background` to instantly retheme the application.
