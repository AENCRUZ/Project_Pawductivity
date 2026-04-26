<div align="center">

# 🐾 Pawductivity

**A Digital Pet Productivity System**

*CS 222 Advanced Object-Oriented Programming*

![Team](https://img.shields.io/badge/Team-LAVA-ff69b4?style=for-the-badge)
![Section](https://img.shields.io/badge/Section-CS--2202-c084fc?style=for-the-badge)
![University](https://img.shields.io/badge/Batangas_State_University-blue?style=for-the-badge)

![Platform](https://img.shields.io/badge/Platform-Windows-0078D4?style=flat-square&logo=windows)
![Framework](https://img.shields.io/badge/.NET-WinForms-512BD4?style=flat-square&logo=dotnet)
![IDE](https://img.shields.io/badge/IDE-Visual_Studio-5C2D91?style=flat-square&logo=visualstudio)
![Theme](https://img.shields.io/badge/Theme-Pink_Kawaii_🌸-ff69b4?style=flat-square)

 *Stay productive. Keep your pet happy. Don't let your tasks go overdue.* 🐱🐶

</div>

---

## 🚀 How to Run

### Prerequisites

| # | Requirement | Notes |
|---|---|---|
| 1 | [Visual Studio Community 2026](https://visualstudio.microsoft.com/vs/community/) | Windows only — WinForms requires Windows |
| 2 | **.NET Desktop Development** workload | Select during VS installation |

### Steps

1. Open **Visual Studio Community**
2. Click **Open a project or solution**
3. Navigate to the `Pawductivity` folder
4. Open `Pawductivity.slnx` *(solution file)*
5. Press **F5** to build and run → or go to **Debug › Start Debugging**

> 💡 **Tip:** Press `Ctrl + F5` to run without the debugger — it's faster!

---

## 📁 Project Structure

```
Pawductivity/
├── 📄 Pawductivity.slnx          ← Solution file
├── 📄 Pawductivity.csproj        ← Project file
├── 📄 Program.cs                 ← Entry point
├── 🎨 PawTheme.cs                ← Theme settings (colors & fonts)
│
├── 📂 Models/
│   ├── Pet.cs                    ← Abstract base class (Encapsulation + Inheritance)
│   ├── PetTypes.cs               ← CatPet & DogPet (Polymorphism)
│   ├── TaskItem.cs               ← Task data model
│   └── ShopItem.cs               ← Shop item model
│
├── 📂 Managers/
│   └── GameManager.cs            ← Core game logic (Abstraction)
│
└── 📂 Forms/
    ├── LoginForm.cs              ← Start screen
    ├── DashboardForm.cs          ← Main screen (pet + tasks)
    ├── TaskEditForm.cs           ← Add/Edit task dialog
    ├── ShopForm.cs               ← Shop system
    └── StatsForm.cs              ← Productivity analytics
```

---

## 🎮 Features

| Feature | Status |
|---|:---:|
| Login with username & pet name | ✅ |
| Choose Cat 🐱 or Dog 🐶 | ✅ |
| Add / Edit / Delete tasks | ✅ |
| Complete tasks → pet gains XP & mood | ✅ |
| Pet levels up and evolves | ✅ |
| Overdue tasks → pet loses health & mood | ✅ |
| Shop system with coins | ✅ |
| Streak tracking | ✅ |
| Stats & analytics screen | ✅ |
| Pink kawaii theme throughout | ✅ |

---

## 🎓 OOP Principles Applied

<table>
<tr>
<td width="50%">

### 🔒 Encapsulation
`Pet` uses **private fields** with public properties to protect health, mood, and XP data from direct manipulation.

</td>
<td width="50%">

### 🧬 Inheritance
`CatPet` and `DogPet` both **extend** the abstract `Pet` base class, sharing common behavior while adding their own personality.

</td>
</tr>
<tr>
<td width="50%">

### 🔀 Polymorphism
Methods like `ReactToTaskCompleted()`, `ReactToTaskMissed()`, and `GetGreeting()` **behave differently** depending on whether the pet is a cat or dog.

</td>
<td width="50%">

### 🏗️ Abstraction
`GameManager` handles all core logic behind the scenes — forms interact through **clean, high-level methods** without knowing the details.

</td>
</tr>
</table>

---

## 🌸 Theme Customization

All UI colors are centralized in **`PawTheme.cs`**. Simply modify values like `Primary` and `Background` to instantly retheme the entire application — no hunting through individual forms needed.

---

<div align="center">

*Made with 💖 by Team LAVA · Batangas State University · CS-2202*

</div>
