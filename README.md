# ICAT IP Test

A third-person Unity demo built as the selection test for the IP (Individual Project) capstone team
at ICAT Design and Media College, Bangalore, in September 2020. The brief was to show a range of
systems rather than a finished game, so it is a village on a terrain that you can walk around while
the sun crosses the sky, plant crops and wait for them to grow, pick the fruit into an inventory,
and watch a tornado wander in and drag the scenery away.

Passing it got me the lead developer slot on the IP game itself.

[Gameplay video](https://youtu.be/smuaM09F8Qs)

## Controls

| | |
|---|---|
| `W` `A` `S` `D` | move |
| `F` | toggle the plant selector |
| `I` | toggle the inventory |
| `G` | pick up the plant you are standing next to |

## The systems

**Day/night cycle** — `DynamicSunScript` runs `[ExecuteAlways]`, so the lighting updates while you
scrub the time-of-day slider in the editor and only advances on its own in play mode. The colours
come from a `DynamicSunObject` ScriptableObject holding three gradients — ambient, directional and
fog — sampled at the normalised time of day, with the sun's rotation driven off the same value.
Keeping the palette in an asset rather than the script means the whole mood of the scene is
editable without touching code.

It also drives post-processing: between 05:00 and 16:00 it lerps the URP volume's exposure and
white balance toward warm daylight, and outside that window toward a cold night. Both are lerped
per-frame rather than switched, so dawn and dusk ease across instead of popping.

**Planting** — a `PlantObject` ScriptableObject defines one crop as four prefabs: the dirt mound,
the grown plant, the fruit, and the inventory icon. `PlantManager.CreatePlant` starts a coroutine
that drops the dirt in front of the player, waits ten seconds, then swaps it for the grown plant at
the same transform and hands it the fruit it should yield. Adding a new crop is a new asset, not new
code.

**Inventory** — `ThirdPersonMovement` tracks whichever plant you are standing in the trigger of and
`G` calls `PickUp` on it, which asks the game controller to instantiate that plant's icon prefab
into the inventory list and destroys the plant.

**Tornado** — `TornadoScript` picks a destination in the opposite quadrant of the map, moves toward
it, and picks a new one on arrival, so it crosses the level rather than circling. Anything tagged
`Pullable` that enters its trigger gets a coroutine that repeatedly adds force toward the tornado's
centre until the object leaves. The coroutine re-invokes itself on a refresh interval rather than
looping in `FixedUpdate`, so the pull is per-object and stops cleanly on trigger exit.

**Rain** — the particle system snaps to the player's XZ position at a fixed height every frame, so
a small emitter covers an arbitrarily large world.

**Movement** — a `CharacterController` moved along a camera-relative direction, with the character's
facing eased onto the camera yaw through `SmoothDampAngle` so turns are not instant. The camera
itself is Cinemachine.

## Built with

Unity 2020.1.5f1 · Universal Render Pipeline 8.2 · Cinemachine · Terrain Tools · ScriptableObjects ·
C#

## Opening the project

Clone the repo and open the root folder as a Unity project. It was built against Unity
**2020.1.5f1**; a newer editor will trigger a one-way project upgrade.

The scene is `Assets/Scenes/Village.unity`.

## About the assets

Most of `Assets/` is third-party — Unity's Standard Assets and Terrain Tools sample content, plus
the GrassFlowers, VillagePack, Cartoon Farm Crops, VertexColorFarmAnimals and Particle Ribbon packs.
My own work is `Assets/Scripts/`, the scene, the terrain and the way the rest is put together.

Only the sample content the scene actually references is committed. The Terrain Tools *sculpting
brush* set is editor-only tooling that nothing in the scene references, so it is not vendored here;
re-import it from Package Manager if you want to reshape the terrain.
