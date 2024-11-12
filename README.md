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

## 7. **Manifest**

### **Angela**

Tasks:

-   Files organization
-   Models and objects collection
-   Audio handler system, main character audio for walking, jumping, cooking, landing, picking up, holding bowl, running
-   Kill plane
-   Environmental design improvement

Assets:

-   All Sound files
-   Furnitures

Scripts:

-   Player/PlayerFallHandler.cs
-   Player/SoundManager/AudioManager.cs
-   Player/SoundManager/PickUpSound.cs
-   Player/SoundManager/PlayerFootStepSound.cs
-   Player/SoundManager/PlayerLandingSound.cs

### **Doan**

#### Tasks:

-   Environmental design improvement
-   Created NPC characters animators
-   Created custom animations for spawning, sitting, eating, leaving animation
-   NPC's functionalities
-   Generate multiple customers and Door animation

#### Assets:

-   NPC/Rabbit
-   NPC/Horse
-   Furnitures

#### Scripts:

-   NPC/NPC_AI.cs
-   NPC/NPCManager.cs
-   NPC/DiningOrderScript

### **Katelynn**

#### Tasks:

-   Order and Ingredient definition
-   Order generation system
-   Order UI Panel
-   Delivery coin increment

#### Assets:

-   All UI Panel

#### Scripts:

-   Objects/Order.cs
-   Objects/Recipe.cs
-   Objects/OrderUI.cs
-   Objects/OrderSystem.cs
-   Utility/Coins.cs

### **Thuan**

#### Tasks:

-   Quality control and finalized the easy mode map
-   The main character's 3rd person controller
-   Main character's animators and custom animations: picking up, holding plates
-   Main player interaction with ingredients, plate, sink, cooking components, and customers.
-   Supported NPC and Order functionalities
-   Supported Order UI Panel and End screen summary

#### Assets:

-   Main character player

#### Scripts:

-   FoodProcessor/CookingComponent.cs
-   FoodProcessor/Cookware.cs
-   functional scripts/CameraManager.cs
-   functional scripts/InputManager.cs
-   functional scripts/ResetBool.cs
-   functional scripts/ResetIsJumping.cs
-   Objects/IngredientProps.cs
-   Objects/WallScript.cs
-   Player/PickUpController.cs
-   Player/HoldingObjectScript.cs
-   Player/PlayerLocomotion.cs
-   Player/PlayerManager.cs

### **Whitney**

#### Tasks:

-   Set up the project
-   Created Title Screen UI and its functionalities
-   Money Counter UI
-   Round Timers
-   End screens
-   Video demos

#### Assets:

-   Title Screen and Pause Screen UI

#### Scripts:

-   functional scripts/Summary.cs
-   functional scripts/RoundOver.cs
-   Utility/PauseMenuToggle.cs
-   Utility/SceneSwitcher.cs
-   Utility/Timer.cs
