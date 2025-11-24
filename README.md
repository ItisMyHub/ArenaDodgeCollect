# Assignment Game – 3 Scene Project

## Overview

This Unity project implements a simple 3‑scene game with:

- **MainMenu** – start the game.
- **Arena** – player moves, avoids a moving hazard, and collects items.
- **Result** – shows game over information (time survived and score) and allows replay or return to the menu.

The project uses the new **Unity Input System** for player movement and a persistent `GameManager` to handle game state, time, and score.

---

## Scenes and Flow (Scene Management)

**Scenes:**
- `MainMenu`
- `Arena`
- `Result`

**Flow:**
1. The game starts in **MainMenu**.
2. Pressing **Play** loads the **Arena** scene.
3. In **Arena**, the player moves around, collects items, and tries to avoid hazards.
4. When the player collides with a hazard, the game goes to the **Result** scene.
5. In **Result**, the player can:
   - See a **“You lost”** message,
   - See **time survived** and **score**,
   - Click **Play Again** (go back to Arena),
   - Click **Main Menu** (go back to MainMenu).

Scene changes are handled by the `SceneLoader` script.

---

## Controls and Gameplay (Gameplay / Input)

- **Movement:** `W`, `A`, `S`, `D` keys.
- Movement uses the **new Input System** with a generated `InputSystem_Actions` class.
- The player is a capsule with a `Rigidbody`, moved using `linearVelocity`.

In the **Arena**:

- The player can move around the arena bounded by walls.
- There is at least one **moving hazard** which ends the game on collision.
- There are **collectibles** (spheres) the player can pick up for score.

---

## Game Logic and State (Game Logic)

The `GameManager` script:

- Is a **singleton** that persists across scenes.
- Tracks:
  - `GameState` (`Playing`, `GameOver`),
  - `ElapsedTime` for each run,
  - `Score` from collected items.
- Registers the current scene’s `SceneLoader` whenever a new scene loads.
- Resets `ElapsedTime` and `Score` when entering the **Arena** scene.
- When the player hits a hazard, `GameManager.GameOver()` is called:
  - Sets the state to `GameOver`,
  - Loads the **Result** scene.

**Hazards:**
- `Hazard` script listens for collisions with objects tagged `Player` and calls `GameOver()`.
- `MovingHazard` script moves the hazard back and forth along a direction.

**Collectibles:**
- `Collectible` script uses triggers to detect when the `Player` enters.
- Calls `GameManager.AddScore(scoreValue)` and destroys the collectible.

---

## UI and Feedback (UI / Feedback)

### MainMenu

- A **Play** button that loads the `Arena` scene.

### Arena

- 3D arena with:
  - Ground,
  - Player capsule,
  - Walls,
  - Moving hazard,
  - Collectible objects.

### Result

- `ResultUI` script displays:
  - “You lost!”
  - `Time survived: X.X seconds`
  - `Score: N`
- Buttons:
  - **Play Again** – loads `Arena` directly.
  - **Main Menu** – returns to `MainMenu`.

---

## How to Run

1. Open the project in **Unity**.
2. Open the `MainMenu` scene.
3. Press **Play**.
4. Use `WASD` to move in the **Arena**.
5. Collect items, then collide with a hazard to see the **Result** screen.

---
