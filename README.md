# Culinary Chaos

## 1. **Introduction**

-   **Game Description:** Culinary Chaos is a 3D cooking simulation game that **Features**
-   **Mode**: Single-player mode.
-   **Various levels:** Players navigate through three unique levels, each with its own kitchen layouts and challenges.
-   **Unique gameplay mechanics**: Jumping to different floors to collect ingredients for cooking.
-   **Start Scene File: Easy

## 2. **Installation**

Provide step-by-step instructions to install and launch the game.

1. **Download** the zip file includes the source code and the build.
2. **Extract** zip file.
3. **Open** the launch file for the compatible platform by double-clicking on:
    1. CulinaryChaos.exe in Build/Windows (for Windows) or
    2. CulinaryChaos.app in Build/MacOS (macOS).
4. **Enjoy** the game\!

## 3. **How to Play**

-   **Objective:** Your goal is to complete a series of cooking tasks within a set time limit while earning the minimum required income to pass each level.
-   **Basic Mechanics:**
    -   Players must collect ingredients one by one scattered across multiple kitchen floors.
    -   Take an ingredient and put it into the pot to cook.
    -   Pick a bowl at the sink station and bring it near the pot to get the soup.
    -   Serve dishes to the customers in record time.
-   **Game Progression:** Each new level introduces additional complexity, such as time constraints, and dynamic obstacles.

## 4. **Controls**

#### **Mouse:** Rotation to view.

#### **Keyboard:** List default keys for movement, interaction, etc.

-   **Movement**: WASD or Arrow Keys
-   **Jump**: Space
-   **Picking/Dropping Ingredients**: Q
-   **Dispose Dishes/Bowls at the sink**: Q
-   **Put Ingredient in the Cooking Component**: Q
-   **Get an empty bowls**: Q
-   **Put the cooked food in a holding bowl**: Q
-   **Put bowl on the dining table**: Q
-   **Menu**: P
-   **Chopping**: C

## 5. **Gameplay Tips**

-   The player must face the interactable game objects such as ingredients, bowls, sinks, cooking components, tables, etc to trigger actions on the game objects.
-   Only collecting one ingredient at a time.
-   Always keep track of the recipe steps to avoid wasting time on the wrong actions.

## 6. **Known Issues**

List any known bugs or issues that players might encounter.

1.  Only have one level.
2.  Do not have the main character chopping animation.
3.  If the player is walking and picking up a plate at the same time, the player starts flying around

    -   Workaround: Stand still while picking up plate

4.  Player and Ingredients: The player must face the ingredients, and keep a proper amount (not too far and not too close) to be able to pick up the ingredients.

    -   Since the game is using BoxCast which doesn't allow it to detect objects when it's too close. OverlapBox is a better method and will be implemented in the post-alpha to fix this limitation.

## 7. **Showcase**

![title](https://github.com/user-attachments/assets/f10c655b-0f90-4fb4-a95b-5f9b5425627e)

![running](https://github.com/user-attachments/assets/73ac4ab6-b3cc-4d64-8810-fb85a6ca50c8)

| **Running** | **Falling** | **Jumping** | **Pickup** |
|---|---|---|---|
| <img src="https://github.com/user-attachments/assets/fb986c80-f6c8-4c5a-9bd9-e937a7683af7" width=200>   | <img src="https://github.com/user-attachments/assets/420b9631-16d1-461d-b880-8fc6cd7302dd" width=200>   | <img src="https://github.com/user-attachments/assets/e905d01d-7f6f-4198-8945-29991cd7a020" width=200>   | <img src="https://github.com/user-attachments/assets/0832f709-eb17-4381-87d5-72fad0b43396" width=200>   |

![customers](https://github.com/user-attachments/assets/ad010d5c-e233-424b-bb18-89994aca6a8e)

## 8. **Credits**

- **Angela**
- **Doan**
- **Katelynn**
- **Thuan**
- **Whitney**
