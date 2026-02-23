<img width="1919" height="1029" alt="image" src="https://github.com/user-attachments/assets/581754af-eb99-4ed0-89f1-91d614267d28" />

<h1 align="center">Floppy Bird</h1>
<div align="center">

![GitHub License](https://img.shields.io/github/license/zwcory/floppyBird)
![GitHub repo size](https://img.shields.io/github/repo-size/zwcory/floppyBird)
![GitHub last commit](https://img.shields.io/github/last-commit/zwcory/floppyBird)

</div>

A Flappy Bird-inspired game built in Unity with unlockable characters, game
modes, a coin economy, and an achievement system.

---

## Features

- 🐦 Multiple unlockable bird skins (Red Bird, Space Bird, Concept, Santa)
- 🎨 Unlockable game modes (Default, Christmas, Underwater)
- 🏆 Achievement system with coin rewards
- 💰 In-game coin economy earned through gameplay and achievements
- 📊 High score and stats tracking via Unity PlayerPrefs
- 🎵 Dynamic music system with per-mode soundtracks

---

## How to Play

- Press **Space** to flap (or hold **Space** to dive in Underwater mode)
- Avoid the pipes
- Collect coins mid-flight
- Earn achievements to unlock cosmetics and bonus coins

---

## Built With

- Unity 6000.2.10f1
- C#

---

## Getting Started

### Playing the Game

1. Head to the [Releases](https://github.com/zwcory/floppyBird/releases) page
2. Download the build matching your machine (Windows or Mac)
3. Navigate to `/build`
4. Run the `FloppyBird` executable
5. Follow any additional instructions on the releases page

### Running from Source

1. Clone the repository
2. Open the project in Unity 6000.2.10f1
3. Open the `Menu` scene and press Play

---

## For Developers

### Project Structure

> **Note:** Scripts are currently being migrated to a dedicated `Scripts/`
> folder. See the open issue on GitHub for progress.

```text
Assets/
├── AchievementManager.cs  # Achievement definitions and completion checks
├── AudioManager.cs        # Singleton audio manager, handles music and SFX
├── BirdScript.cs          # Core bird physics and input (flap mode)
├── BubbleManager.cs       # Destroys particle manager on underwater scene load
├── CheckboxUpdater.cs     # UI helper - toggles achievement/skin checkbox state
├── coinScript.cs          # Coin pickup collision and reward logic
├── CoinMoveScript.cs      # Moves coins left across the screen, destroys off-screen
├── ColourChanger.cs       # UI helper - changes text colour for selected/achieved states
├── LevelLoader.cs         # Handles animated scene transitions with audio crossfading
├── LogicScript.cs         # Core game state (score, coins, difficulty, game over)
├── MenuManager.cs         # Restores achievement UI state on menu load
├── ModePurchaser.cs       # Handles mode purchasing, UI state, and PlayerPrefs
├── ParticleManager.cs     # Singleton that manages persistent particle effects
├── PipeMiddleScript.cs    # Detects when player passes through a pipe gap, adds score
├── PipeMoveScript.cs      # Moves pipes left across the screen, destroys off-screen
├── PipeSpawnerScript.cs   # Spawns pipes and randomly spawns coins at set intervals
├── SkinManager.cs         # Applies saved skin/mode selections to the scene
├── SkinPurchaser.cs       # Handles skin purchasing, UI state, and PlayerPrefs
├── TitleLogic.cs          # Main menu UI logic, scene loading, coin display
├── UnderWater.cs          # Underwater mode physics (buoyancy + dive)
├── Scenes/
├── Sprites/
└── Audio/
```

### How the Coin System Works

Coins are earned by:
- Passing through a pipe gap (1 coin per pipe)
- Collecting coins mid-flight (1–3 coins randomly per pickup)
- Completing achievements (fixed coin rewards per achievement)

Coins are stored via `PlayerPrefs` as both spendable `"Coins"` and a cumulative
`"TotalCoins"` (used for the FilthyRich achievement). `LogicScript.addCoin()`
is the single entry point for granting coins and updates both values.

Coin display in the menu is formatted by `TitleLogic.FormatCoins()` which
abbreviates large values (e.g. 1,500 → 1.5K).

### How the Achievement System Works

Achievements are defined in `AchievementManager.InitializeAchievements()` using
a `Predicate<object>` for flexible requirement checking:

```csharp
achievements.Add(new Achievement(
    "HappyFlappy",
    "Get a high score of 20",
    "none",
    200,
    (object o) => logic.highScore >= 20
));
```

Each frame, `CheckAchievements()` iterates all achievements and calls
`UpdateCompletion()`. Once an achievement's predicate is met, it:
1. Marks itself as achieved
2. Grants coin reward via `logic.addCoin()`
3. Persists completion via `PlayerPrefs.SetInt(title, 1)`
4. Optionally unlocks an item reward (e.g. the `"Coiny"` skin)

Achievement completion is loaded on startup and restored without re-granting
rewards via `SetCompletion()`.

### How to Add a New Skin

Skins come in two types:

- **Purchasable** — bought with coins via the shop
- **Achievement-unlockable** — granted as an achievement reward (price of `0`)

> **Note:** Achievement-unlockable skins (e.g. Coiny) have a price of `0` and
> their purchase button exists in the UI to display a lock icon, but has no
> OnClick handler assigned in the Inspector, making it intentionally
> non-interactable.

Adding a new skin involves three steps: registering it in the purchaser,
setting up the UI, and adding the assets to the manager.

**1. Register the skin in `SkinPurchaser.InitializeSkins()`:**
```csharp
// Purchasable skin
skins.Add(new Skin("NewBird", false, false, 300));

// Achievement-unlockable skin
skins.Add(new Skin("NewBird", false, false, 0));
```

**2. Add the UI select/purchase button cases in `SkinPurchaser.cs`:**
```csharp
// In GetSelectButton()
case "NewBird": return newBirdSelectButton;

// In GetPurchaseButton()
case "NewBird": return newBirdPurchaseButton;
```
- Wire up the select button's OnClick handler in the Inspector
- For purchasable skins, also wire up the purchase button's OnClick
- For achievement-unlockable skins, add the lock icon button but leave
  OnClick unassigned
- The skin icon shown in the customization UI is the sprite assigned on the
  `SkinPurchaser` component in the Inspector

**3. Add the skin assets in `SkinManager.ApplySavedSkin()`:**
```csharp
else if (selectedSkin == "NewBird")
{
    bird.GetComponent<SpriteRenderer>().sprite = newBird;
    wing.GetComponent<SpriteRenderer>().sprite = newWing;
}
```
- Assign the in-game bird and wing sprites in the Inspector on the
  `SkinManager` component

#### Wings

All wings use the same animation: the sprite is flipped on the Y axis every
`0.2` seconds to simulate flapping. Because of this, **the sprite's pivot
point must be set correctly** so it attaches naturally to the bird's body
when flipping.

The Santa skin's gingerbread wing is a good example of correct pivot
placement. The pivot is set to the point where the wing meets the bird's
body (far right, slightly below center):

| Wing Sprite | Pivot Settings |
|-------------|----------------|
| ![Gingerbread Wing](https://github.com/user-attachments/assets/efdb5aed-782e-43e7-9e56-bdc9f384f554) | ![Pivot](https://github.com/user-attachments/assets/2a7d8792-e352-433a-8586-1a9ab71d1eb4) |

```text
Custom Pivot X: 0.9825737  Y: 0.3951854
```

> ⚠️ If the pivot is set incorrectly, the wing will appear to jump or
> offset when flipping rather than flapping naturally in place. Adjust the
> custom pivot in the Unity Sprite Editor to the point where the wing
> attaches to the bird's body.
### How to Add a New Achievement

Add a new entry in `AchievementManager.InitializeAchievements()`:

```csharp
achievements.Add(new Achievement(
    "UniqueTitle",   // used as PlayerPrefs key, must be unique
    "Description",
    "none",          // or a skin name string to unlock as an item reward
    coinReward,
    (object o) => logic.someCondition
));
```

### How to Add a New Game Mode

1. Create a new Unity scene for the mode
2. Register the mode in `ModePurchaser.InitializeModes()`:
   ```csharp
   modes.Add(new Skin("NewMode", false, false, 500));
   ```
3. Add cases to `GetSelectButton()`, `GetPurchaseButton()`, and
   `SetModeSelector()` in `ModePurchaser.cs`
4. Add a `SelectNewMode()` method in `SkinManager.cs` following the existing
   pattern
5. If the mode has unique music, handle it in `LevelLoader.LoadSceneByName()`

### Game Modes

| Mode | Bird Script | Notes |
|------|-------------|-------|
| Default | `BirdScript.cs` | Space to flap, standard gravity |
| Christmas | `BirdScript.cs` | Same physics, different scene and music |
| Underwater | `UnderWater.cs` | Buoyancy-based physics, hold Space to dive |

Mode and skin selections are persisted via `PlayerPrefs` and applied on scene
load by `SkinManager`. Scene transitions including music crossfading are handled
by `LevelLoader.LoadSceneByName()`.

### Difficulty Scaling

Difficulty increases every 5 points up to a score of 100 (capped by the
`difficultyTriggered` array in `LogicScript`). Each trigger:
- Reduces pipe spawn interval (min `1.5s`)
- Increases pipe and coin move speed (max `10f`)

---

## License

MIT
