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

**Pawductivity** is a gamified productivity desktop app built with .NET WinForms. You adopt a virtual pet — a cat 🐱 or a dog 🐶 — and your tasks directly affect its health and happiness. Complete tasks on time and your pet thrives, levels up, and evolves through five distinct stages. Let them go overdue, and your pet suffers the consequences.

It's a productivity tool with stakes — and pixel-art animations to match.

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
├── Pawductivity.slnx              ← Solution file
├── Pawductivity.csproj            ← Project file
├── Program.cs                     ← Entry point
├── PawTheme.cs                    ← Centralized theme (colors & fonts)
│
├── Animations/                    ← Pixel-art animation engine
│   ├── PetAnimator.cs             ← Core animation engine (stage-aware sprites + effects)
│   ├── AnimationPanel.cs          ← Transparent panel that hosts PetAnimator
│   ├── FloatyLabel.cs             ← Floating "+XP" / "+Coins" popup labels
│   ├── CatSprites.cs              ← All 5 cat stage sprites (procedural GDI+)
│   ├── DogSprites.cs              ← All 5 dog stage sprites (procedural GDI+)
│   └── Paw.cs                     ← Centralized pixel colour palette
│
├── Models/
│   ├── Pet.cs                     ← Abstract base class (Encapsulation + Inheritance)
│   ├── PetTypes.cs                ← CatPet & DogPet (Polymorphism)
│   ├── TaskItem.cs                ← Task data model
│   ├── ShopItem.cs                ← Shop item model
│   └── SaveData.cs                ← Serializable snapshot models
│
├── Managers/
│   ├── GameManager.cs             ← Core game logic (Abstraction)
│   └── SaveManager.cs             ← File I/O: save, load, list, delete profiles
│
└── Forms/
    ├── LoginForm.cs               ← Profile selector & new profile creation
    ├── DashboardForm.cs           ← Main screen: pet + task list
    ├── TaskEditForm.cs            ← Add & edit task dialog
    ├── ShopForm.cs                ← Coin shop
    └── StatsForm.cs               ← Productivity analytics
```

---

## 🔄 Gameplay Loop

```
Login → Add Task → Complete Task → Pet Reacts → Earn Coins → Buy Items
           ↑                                                       |
           └───────────────────── loop ────────────────────────────┘
```

Every task you complete rewards you and your pet. Every task you miss costs you both. The shop gives you something to work toward, and the streak system keeps you coming back daily.

Progress is **automatically saved** when the app closes and restored when you reopen it — no progress is ever lost.

---

## 🌱 Pet Evolution

Your pet evolves through five stages as you level up. Each level costs `current_level × 50 XP` — so leveling gets progressively harder.

| Stage | Level | Cat 🐱 | Dog 🐶 |
|---|---|---|---|
| 🥚 **Egg** | 1 | Warm shell oval, sleeping eyes | Wider oval with a tiny paw print on the shell |
| 🐱 **Baby** | 2–3 | Oversized round head, huge eyes, blush spots | Same proportions, stubby floppy ear nubs |
| 🐈 **Junior** | 4–6 | Tabby stripes on forehead & back, longer whiskers | Speckle markings, longer ears, tail wag begins |
| 🐈 **Adult** | 7–9 | Sleek adult cat, cheek stripes | Full grown dog with wide snout |
| ✨ **Legend** | 10+ | Gold crown, teal glowing eyes, orbiting aura | Gold collar + harness, amber eyes, wild wagging tail |

Each stage is drawn procedurally with GDI+ — no external image assets required.

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
| **Data persistence across sessions** | ✅ |
| **Multi-profile support** | ✅ |
| **Atomic save (crash-safe)** | ✅ |
| **Stage-specific pixel-art sprites** | ✅ |
| **Per-stage idle animations (blink + breathe)** | ✅ |
| **Level-up transition animations** | ✅ |
| **Task complete / overdue / shop animations** | ✅ |
| **Floating XP / coin / mood labels** | ✅ |

---

## 🎨 Animations

All animations are rendered procedurally using GDI+ — no sprite sheets or external assets needed. The app works out of the box.

### Pet Idle Animations (all stages)

- **Breathing loop** — subtle up/down bob driven by a sine wave (~60 fps). Eggs bob slowly; all other stages use the full range.
- **Blinking** — randomised every 3–5 seconds. Eggs skip blinking (they're sealed). All other stages close and reopen smoothly across 3 frames.
- **Legend aura** — at the Legend stage, 8 glowing dots orbit the pet and pulse in sync with the breath phase.
- **Dog tail wag** — from Junior onward, the dog's tail swings left and right using the breath phase as a driver.

### Level-Up Transition Animations

Each stage change plays a two-phase transition unique to the pet type:

| Transition | Cat 🐱 | Dog 🐶 |
|---|---|---|
| 🥚 → 🐱 Baby | Shell shards fly out → pink hearts burst as kitten emerges | Cream shards fly out → paw prints burst as puppy appears |
| 🐱 → 🐈 Junior | Pink hearts expand outward + ring | Excited paw prints + yellow mood glow ring |
| 🐈 → 🐈 Adult | Periwinkle double rings + white sparkles | Brown double rings + cream sparkles |
| 🐈 → ✨ Legend | White flash → gold/diamond explosion + floaty text | Goldenrod flash → gold rings + flying paw prints |
| Other levels | Gold ring + sparkles | Yellow ring + sparkles |

### Overlay Effect Animations

| Trigger | Animation |
|---|---|
| Task completed | `+XP ⭐` and `+😸 Mood` float upward + sparkles radiate outward |
| Coins earned | `+🪙` floats upward + gold sparkles |
| Overdue task | `-❤️` and `-😸` descend with red shake crosses |
| Shop — Star Cookie | "Nom nom! 🍪" + `+❤️ +😸` float up |
| Shop — Strawberry Milk | `+❤️ Health` floats up + rose sparkles |
| Shop — any mood item | `+😸 Mood` floats up + yellow sparkles |

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

Pawductivity is built as a deliberate showcase of all four core OOP concepts. Here's exactly where and how each one appears in the code.

---

### 🔒 Encapsulation — `Pet.cs`

All five stat fields are declared `private` — nothing outside `Pet` can touch them directly:

```csharp
private int _health;
private int _mood;
private int _xp;
private int _level;
private int _coins;
```

Each public property enforces its own invariant on `set`:

```csharp
public int Health
{
    get => _health;
    set => _health = Math.Clamp(value, 0, 100);  // can't go below 0 or above 100
}

public int XP
{
    get => _xp;
    set { _xp = value; CheckLevelUp(); }  // every XP gain automatically checks for level-up
}

public int Coins
{
    get => _coins;
    set => _coins = Math.Max(0, value);   // coins can never go negative
}
```

`_level` is even stricter — its setter is `private`, so only `Pet`'s own internal `CheckLevelUp()` method can increment it. No form or manager can manually set the pet's level.

The persistence layer respects this encapsulation too. `SaveManager` calls `pet.RestoreStats(...)` — a dedicated method on `Pet` that writes directly to the backing fields without triggering `CheckLevelUp()`. Saved stats are restored exactly as they were, level-up logic never fires on load.

---

### 🧬 Inheritance — `Pet.cs` → `CatPet` / `DogPet`

`Pet` is declared `abstract`, which means it can never be instantiated directly — you always get a `CatPet` or `DogPet`. The base class owns all shared behavior:

- **Level-up logic** (`CheckLevelUp`, `Evolve`) — runs identically for both pets
- **Derived properties** (`CurrentMood`, `MoodEmoji`, `XpForNextLevel`) — computed once, inherited by both
- **Starting stats** — `Health = 80`, `Mood = 70`, `Level = 1` set in the base constructor

`CatPet` and `DogPet` call `base(name)` to reuse that constructor, then only add what's unique to them.

```csharp
public abstract class Pet { ... }          // base — shared logic lives here
public class CatPet : Pet { ... }          // inherits everything, adds cat personality
public class DogPet  : Pet { ... }         // inherits everything, adds dog personality
```

Inheritance also appears in the animation layer. `CatSprites` and `DogSprites` are separate static classes that share the same `DrawEye()` helper from `CatSprites`, keeping eye-rendering logic in one place while each pet type draws its own distinct body.

---

### 🔀 Polymorphism — `PetTypes.cs`

Three methods are declared `abstract` in `Pet`, forcing every subclass to provide its own implementation:

```csharp
public abstract void ReactToTaskCompleted(TaskItem task);
public abstract void ReactToTaskMissed();
public abstract string GetGreeting();
```

The same call on different pet types produces completely different behavior:

| Behavior | 🐱 CatPet | 🐶 DogPet |
|---|---|---|
| Task completed (High) | `+30 XP, +15 Mood, +5 Health` | `+25 XP, +20 Mood, +8 Health` |
| Task missed | `−20 Mood, −8 Health` | `−12 Mood, −10 Health` |
| Greeting (Happy) | `"[Name] purrs and bumps your head! 🐱💕"` | `"[Name] wags their tail super fast! 🐶💖"` |

`StageEmoji` is declared `virtual` in `Pet` (with a default cat emoji set) and `override`n in `DogPet` to return dog-specific emojis per evolution stage — without touching any cat logic.

`DashboardForm` and `GameManager` never check `if pet is CatPet` — they just call the method and let the object decide how to respond. That's polymorphism in action.

Polymorphism also appears in `PetAnimator`: `TriggerLevelUp()` dispatches to `DrawCatLevelUp()` or `DrawDogLevelUp()` based on the current `PetType`, and `DrawCurrentSprite()` routes to the correct stage method inside `CatSprites` or `DogSprites` — the caller never needs to know which one runs.

---

### 🏗️ Abstraction — `GameManager.cs`

`GameManager` is the single source of truth for all game state. Every form interacts with the game through it — none of them touch `Pet`, `TaskItem`, or streak logic directly.

```csharp
// What DashboardForm calls:
_gm.CompleteTask(task.Id);
_gm.AddTask(dlg.Result);
_gm.DeleteTask(task.Id);
_gm.BuyItem(selectedItem);

// What GameManager actually does internally (hidden from forms):
// → finds the task, marks it complete, calls pet.ReactToTaskCompleted(task),
//   increments TotalCompleted, updates streak dates, checks LongestStreak
```

`StatsForm` reads `_gm.CompletionRate`, `_gm.CurrentStreak`, `_gm.LongestStreak` without knowing how any of those are computed. The decay timer in `DashboardForm` just calls `_gm.ApplyOverduePenalties()` every 60 seconds — it has no idea which tasks are overdue or how much health each one costs.

Abstraction also applies to the animation system. `DashboardForm` only calls `_animPanel.Animator.TriggerLevelUp(level)` — it has no knowledge of rings, sparkles, shards, breath phases, or sprite routing. All of that complexity is encapsulated inside `PetAnimator`.

The same principle extends to persistence. `DashboardForm` calls `SaveManager.Save(_gm)` on close — one line. It doesn't know about JSON, file paths, temp files, or atomic writes. That complexity lives entirely inside `SaveManager`.

---

## 🌸 Theming

All colors and fonts live in `PawTheme.cs`. Change a value here and it updates every form, button, and progress bar in the app — no hunting through individual files needed.

```csharp
// Colors
public static readonly Color Background = Color.FromArgb(255, 240, 245); // soft blush
public static readonly Color Surface    = Color.FromArgb(255, 220, 230); // light pink card
public static readonly Color Primary    = Color.FromArgb(255, 105, 150); // rose pink
public static readonly Color Secondary  = Color.FromArgb(255, 182, 193); // pastel pink
public static readonly Color TextDark   = Color.FromArgb( 80,  30,  50); // deep rose-brown
public static readonly Color TextMuted  = Color.FromArgb(160,  90, 120);
public static readonly Color HealthBar  = Color.FromArgb(255,  80, 120);
public static readonly Color MoodBar    = Color.FromArgb(255, 200,  80); // sunny yellow
public static readonly Color XpBar      = Color.FromArgb(140, 200, 255); // periwinkle

// Fonts
public static readonly Font FontTitle   = new("Segoe UI", 22f, FontStyle.Bold);
public static readonly Font FontHeading = new("Segoe UI", 13f, FontStyle.Bold);
public static readonly Font FontBody    = new("Segoe UI",  9f, FontStyle.Regular);
public static readonly Font FontSmall   = new("Segoe UI",  8f, FontStyle.Regular);
```

`PawTheme.StyleButton(btn)` and `PawTheme.StyleButton(btn, outlined: true)` apply consistent pink styling (including hover effects) to every button in the app from a single helper method.

The `AnimationPanel` uses `ControlStyles.SupportsTransparentBackColor` and a custom `OnPaintBackground` override so the pet sprite renders directly over the pink card surface — no black or white box behind the pet.

---

<div align="center">

## 👥 Team

**Team LAVA** · CS-2202 · Batangas State University

*Made with 💖 for CS 222 — Advanced Object-Oriented Programming*

</div>
