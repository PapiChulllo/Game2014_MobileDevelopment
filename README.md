# Game2014 Mobile Development (Lab 4)

**Educational mobile-development coursework (GAME2014).** This public repo currently contains **Lab 4**: a 2D platformer-style lab with on-screen joystick input, animator states, Cinemachine, checkpoints, and a death plane.

Root layout is `Lab4/` (Unity project), not a multi-lab monorepo in the current tree.

---

## What Lab 4 demonstrates

- `PlayerBehavior`: Rigidbody2D move/jump with optional **Joystick Pack** left stick, air control factor, speed clamp, grounding circle, sprite flip, animator state ints (idle/walk/jump/fall)
- `Checkpoint`, `DeadPlane`, `GameController`, `AnimationStates`
- External **Joystick Pack** under `Lab4/Assets/ExternalAssets/`
- **Cinemachine** 2.10.3 in the package manifest (commit history: cinemachine + animations + checkpoint)

**Status / limitations:** **course lab**, not a complete game. Only Lab 4 is present in this clone. Unity **2022.3.46f1**. No automated tests; device/Editor play not re-verified here.

## Tech stack

| Area | What it uses |
|---|---|
| Engine | **Unity** `2022.3.46f1` (project: `Lab4/`) |
| 2D | `com.unity.feature.2d` |
| Camera | **Cinemachine** 2.10.3 |
| Mobile input | **Joystick Pack** (third-party under `ExternalAssets`) |
| Animation | Animator integer states via `AnimationStates` |

## What's in the project

| System | Key files |
|---|---|
| Player move/jump/anim | `Lab4/Assets/_Scripts/PlayerBehavior.cs` |
| Checkpoint / death / controller | `Checkpoint.cs`, `DeadPlane.cs`, `GameController.cs`, `AnimationStates.cs` |
| Joystick Pack | `Lab4/Assets/ExternalAssets/Joystick Pack/` |
| Scene | `Lab4/Assets/Scenes/SampleScene.unity` |

### Code / system highlights

- Joystick discovery via `GameObject.Find("OnScreenControllers")` / `LeftJoystick`.
- Jump uses joystick vertical above a threshold when grounded; fall state when `|velocity.y|` exceeds `_deathlyFallSpeed`.

## Scenes

| Scene | Purpose |
|---|---|
| `Lab4/Assets/Scenes/SampleScene.unity` | Lab 4 play scene |

## Third-party assets

| Asset | Notes |
|---|---|
| **Joystick Pack** | On-screen controls; redistrib terms per pack license (not duplicated here) |
| **Cinemachine** | Unity package |
| Course sprites/animations | Under Lab4 `Assets/` as imported for the lab |

## About this repository

**Labeled educational / course work (GAME2014 Mobile Development — Lab 4).** Public under **PapiChulllo**.
