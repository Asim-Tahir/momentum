# Momentum

Momentum is a 3D Unity movement demo with touch controls, a third-person camera, and animated player movement.

## Screenshots

<img width="450" alt="Third-person view of the mannequin on a platform, with the virtual joystick at lower left and Jump button at lower right." src="https://github.com/user-attachments/assets/d005898e-fbaa-4595-aefb-d8156d2c48dc" />
<img width="450" alt="The mannequin steps forward as the joystick is pushed upward; the Jump button remains visible." src="https://github.com/user-attachments/assets/87f18d1a-a713-41d4-9c2e-88203e98b59d" />
<img width="450" alt="Rear view of the mannequin walking across the platform with the joystick and Jump button on screen." src="https://github.com/user-attachments/assets/cd7b6fd5-d870-4650-87c6-feaddb2ebe36" />
<img width="450" alt="Third-person view of the mannequin on the platform with the virtual joystick and Jump button visible." src="https://github.com/user-attachments/assets/c90d330a-16b5-402d-88ac-c2ac92da7b74" />

## Gameplay

- Use the on-screen joystick to move the player.
- Tap the Jump button to jump. The player can jump while grounded.
- Drag on the right side of the screen to rotate the player and camera. Touches over UI controls are ignored.
- In the Unity Editor, hold the left mouse button and move the mouse to look around.
- Player animations transition between idle, walking, running, and jumping.

## Requirements

- Unity Editor `6000.6.7f1`
- Universal Render Pipeline `17.6.0`
- Input System package `1.20.1`
- Joystick Pack, included under `Assets/Joystick Pack/`

## Open and Run

1. Open the project folder in Unity Hub using Unity `6000.6.7f1`.
2. Open `Assets/Scenes/MainScene.unity`.
3. Enter Play mode. Use the on-screen joystick and Jump button to control the player.

## Project Structure

- `Assets/Scenes/MainScene.unity` - Main gameplay scene and touch UI.
- `Assets/Scripts/PlayerMoveController.cs` - Player movement, jumping, and animation parameters.
- `Assets/Scripts/TouchCameraController.cs` - Touchscreen and Editor mouse look.
- `Assets/Animations/Manniquin/Manniquin.controller` - Player idle, walk, run, and jump animations.
- `Assets/Prefabs/Manniquin.prefab` - Player character prefab.
- `Assets/Joystick Pack/` - Virtual joystick scripts and sprites.
- `Packages/manifest.json` - Unity package dependencies.

## Credits

"Manniquin" (https://skfb.ly/pGBtZ) by Lluc Guardiolaa is licensed under Creative Commons Attribution-NonCommercial (http://creativecommons.org/licenses/by-nc/4.0/)
