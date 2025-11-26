# Simple Arena Game

## Overview

This Unity project implements a small 3‑scene arena game that demonstrates:

- A main menu, gameplay arena, and result screen.
- Player movement using Unity's **new Input System**.
- Hazards that end the run.
- Collectibles that increase score.
- A persistent game manager handling game state, time survived, and score.
- A simple debug mode (FPS + god mode toggle).
- Screenshot capture with a key press.
- Audio Mixer with separate music in menu and gameplay.

---

## Scenes and Flow

### MainMenu

- The project starts in the **MainMenu** scene.
- UI contains at least:
  - **Play** button – loads the `Arena` scene.
  - (Optionally) **Quit** button – quits the build.

Main features in this scene:

- An `AudioSource` (`Music_MainMenu`) plays looping background music, routed to the `Music` group in the `MainMixer` Audio Mixer.
- (Optionally) a `ScreenshotManager` object listens for the screenshot hotkey.

### Arena

- The main gameplay happens in the **Arena** scene.
- Contains:
  - A player character (capsule) with a `Rigidbody`.
  - Walls forming the play area.
  - At least one **moving hazard**.
  - Several **collectibles** placed around the arena.

Flow:

1. The player moves around using `WASD` (new Input System).
2. Collectibles increase the score and then disappear.
3. Colliding with a hazard ends the run and sends the player to the `Result` scene.

Additional systems:

- A `DebugManager` in this scene:
  - Uses the new Input System to toggle **debug mode** with a key (e.g. `F1`).
  - Shows a small overlay (if configured with a UI Text) that displays:
    - An approximate FPS.
    - The current **god mode** status.
  - When god mode is enabled, `GameManager` ignores GameOver calls.
- Background music for gameplay:
  - Either via an `AudioSource` in Arena routed to `MainMixer/Music`, **or**
  - Via a persistent music object that survives scene loads (see *Audio* section).

### Result

- The **Result** scene shows the outcome of the last run.

Features:

- `ResultUI` script displays:
  - "You lost!"
  - `Time survived: X.X seconds`
  - `Score: N`
- Buttons:
  - **Play Again** – loads the `Arena` scene directly.
  - **Main Menu** – returns to the `MainMenu` scene.

---

## Controls

- **Movement:** `W` / `A` / `S` / `D`
  - Implemented using Unity's **new Input System**:
    - A generated `InputSystem_Actions` class is used in `PlayerController` to read a `Vector2` from the `Player.Move` action.
- **Debug mode toggle:** key bound via the new Input System (e.g. `F1`)
  - Toggles:
    - A debug overlay showing FPS and god mode status (if debug text is configured).
    - God mode on/off (GameOver ignored while god mode is on).
- **Screenshot:** key bound via the new Input System (e.g. `F12`)
  - Triggers screenshot capture via `ScreenshotManager`.

---

## Core Scripts

### GameManager

- Singleton that persists between scenes.
- Responsibilities:
  - Tracks current `GameState` (`Playing`, `GameOver`).
  - Tracks `ElapsedTime` for each run.
  - Tracks `Score` for each run.
  - On scene load:
    - Finds a `SceneLoader` in the active scene.
    - Optionally finds a `DebugManager` in the active scene.
    - When `Arena` loads:
      - Resets `ElapsedTime` and `Score`.
      - Sets state to `Playing`.
  - On `GameOver()`:
    - Checks `DebugManager.IsGodMode`:
      - If god mode is enabled, ignores GameOver.
      - Otherwise:
        - Sets state to `GameOver`.
        - Loads the `Result` scene via `SceneLoader`.

### PlayerController

- Handles player movement using the **new Input System**.
- Uses a `Rigidbody` and sets its velocity in `FixedUpdate` based on the `Player.Move` input action.
- Reads a 2D movement vector (`Vector2`) from the new Input System and converts it to XZ movement in the world.

### SceneLoader

- Simple helper script attached to a GameObject in each scene that needs it.
- Public methods like `LoadArena()`, `LoadMainMenu()`, and `LoadResult()` are wired to UI buttons.

### Hazard / MovingHazard

- `Hazard`:
  - Detects collisions with objects tagged `Player`.
  - Calls `GameManager.GameOver()` on collision.
- `MovingHazard`:
  - Moves an object back and forth along a direction using `Mathf.PingPong` relative to a start position.

### Collectible

- Uses a trigger collider to detect when the player enters.
- On trigger enter with an object tagged `Player`:
  - Calls `GameManager.AddScore(scoreValue)`.
  - Destroys the collectible GameObject.

### ResultUI

- Reads `ElapsedTime` and `Score` from `GameManager` when the Result scene loads.
- Updates a UI text element with the final time and score.

### DebugManager

- Uses the **new Input System** to bind a key (e.g. `<Keyboard>/f1`) as a toggle action.
- When toggled:
  - Enables/disables a debug overlay (if a UI Text is assigned).
  - Toggles **god mode**, which `GameManager` uses to optionally ignore GameOver.
- Continuously calculates and displays an approximate FPS when the overlay is visible.

### ScreenshotManager

- Uses the **new Input System** to bind a key (e.g. `<Keyboard>/f12`) to a screenshot action.
- On trigger:
  - Captures a screenshot using `ScreenCapture.CaptureScreenshot`.
  - Saves the PNG file to `Application.persistentDataPath`.
  - Logs the full file path in the Unity Console.

---

## Audio

### Audio Mixer

- `MainMixer` Audio Mixer asset.
- Groups:
  - `Master`
  - `Music`
  - `SFX` (optional but available for routing SFX).

### Music Routing

- `Music_MainMenu` (in MainMenu) has an `AudioSource`:
  - `AudioClip`: menu music.
  - `Loop`: enabled.
  - `Play On Awake`: enabled.
  - `Output`: set to `MainMixer/Music`.
- During gameplay:
  - Either a separate `AudioSource` in `Arena` plays gameplay music and is also routed to `MainMixer/Music`, **or**
  - A persistent `MusicPlayer` object (with `DontDestroyOnLoad`) continues to play music across all scenes, also routed to the `Music` group.

This setup demonstrates the use of an Audio Mixer with groups and proper routing of AudioSources.

---

## Screenshots

- While the game is running in the Unity editor (Play Mode), pressing the screenshot hotkey (e.g. **F12**) captures the current Game view.
- `ScreenshotManager`:
  - Saves each screenshot as a PNG to `Application.persistentDataPath`.
  - Logs the path in the Console so it can be opened in Finder/Explorer.

---

## How to Run

1. Open the project in **Unity**.
2. Open the `MainMenu` scene.
3. Press the **Play** button (triangle) in the editor.
4. In the Main Menu:
   - Background music should play.
   - Click **Play** to load the `Arena`.
5. In the Arena:
   - Move with `WASD`.
   - Collect collectibles to increase score.
   - Avoid hazards; colliding with them ends the run.
   - Toggle debug mode with the configured key (e.g. F1) to show/hide debug info and god mode.
   - Take a screenshot with the screenshot key (e.g. F12).
6. When a hazard is hit (with god mode off), the `Result` scene appears:
   - Shows final time survived and score.
   - Offers **Play Again** and **Main Menu** buttons to continue.

---
