# Culinary Chaos


## 1. **Introduction**


-   **Game Description:** Culinary Chaos is a 3D cooking simulation game that **Features**
-   **Mode**: Single-player mode.
-   **Various levels:** Players navigate through three unique levels, each with its own kitchen layouts and challenges.
-   **Unique gameplay mechanics**: Jumping to different floors to collect ingredients for cooking.
-   **Start Scene File: Title Screen


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


## 7. **Manifest**




### **Angela**


#### Tasks:


-   Files organization
-   Models and objects collection
-   Audio handler system, main character audio for walking, jumping, cooking, landing, picking      up, holding bowl, running
-   Teleports the player back to the kitchen when they fall from a plane
-   Environmental design improvement
-  Arranged plane structures for each level
-  Interaction with NPC: when customers enter the restaurant they greet you, and when they wait for too long they make frustrated sounds
-  adjusted reward system
-  adjusted skybox exposure and background terrains


#### Assets:


-   Asset/Sound
-   Asset/Art/Objects/Kitchen1
-   Asset/Art/Objects/Kitchen2
-   Asset/Art/Objects/Crops1
-   Asset/Art/Objects/Crops2
-   Asset/Art/Objects/KitchenAppliances


#### Scripts:


-   Player/PlayerFallHandler.cs
-   Player/SoundManager/AudioManager.cs
-   Player/SoundManager/PickUpSound.cs
-   Player/SoundManager/PlayerFootStepSound.cs
-   Player/SoundManager/PlayerLandingSound.cs
-   NPC/NPC_AI.cs
-   NPC/DiningOrderScript




### **Doan**


#### Tasks:


-   Environmental design improvement
-   Created NPC characters animators
-   Created custom animations for spawning, sitting, eating, leaving animation
-   NPC's functionalities
-   Generate multiple customers and Door animation
-   Created Fire hazard and Banana hazard animation
-   Environmental background for level 2
-   Added hazards for easy, medium, and hard levels
-   Added NPC customers to medium, and hard levels
-   Added more ingredients such as pepper and potato for medium and hard level


#### Assets:


-   Asset/NPC/Rabbit
-   Asset/NPC/Horse
-   Asset/Furnitures
-   Asset/Art/Prefab/Banana
-   Asset/Art/Prefab/Fire
-   Asset/Art/Prefab/GreenPeper
-   Asset/Art/Prefab/Potato
-   Asset/Environment


#### Scripts:


-   NPC/NPC_AI.cs
-   NPC/NPCManager.cs
-   NPC/DiningOrderScript.cs
-   Hazard/Hazard.cs
-   Player/HazardCollision.cs


### **Katelynn**


#### Tasks:


-   Order and Ingredient definition
-   Order generation system
-   Order UI Panel
-   Delivery coin increment
-   Order timer
-   Order and table number matching
-   Pause menu, summary UI redesign
-   UI synchronization across levels
-   Table number over NPC head
-   Coins to win panel


#### Assets:


-   All UI Panel


#### Scripts:


-   Objects/Order.cs
-   Objects/Recipe.cs
-   Objects/OrderUI.cs
-   Objects/OrderSystem.cs
-   Utility/Coins.cs
-   CoinsToWin.cs




### **Thuan**


#### Tasks:


-   Quality control and finalized the easy mode map
-   The main character's 3rd person controller
-   Main character's animators and custom animations: picking up, holding plates
-   Main player interaction with ingredients, plate, sink, cooking components, and customers.
-   Supported NPC and Order functionalities
-   Supported Order UI Panel and End screen summary
-   Created the canvas screen to pick a player
-   Time deduct and remove the holding object when the player jumps out of the restaurant
-   Supported Order UI Panel and End screen summary
-   Added the cooking material for the new ingredients of medium and hard-level
-   Added the UI panel to show what the player holding and display the Q button when facing the ingredients
-   Fixed the pickup animation and fixed the camera sensitivity for all levels
-   Added the timer and NPC disappears when it waits too long
-   Holding Panel to display what’s the player is holding
-   NPCs bubble texts on serving correct/wrong orders
-   Player’s suggestion text on top


#### Assets:


-   Main character player + Holding Objects
-   Holding Ingredient Panel
-   Stoves, bowls prefabs


#### Scripts:


-   FoodProcessor/CookingComponent.cs
-   FoodProcessor/Cookware.cs
-   functional scripts/CameraManager.cs
-   functional scripts/InputManager.cs
-   functional scripts/ResetBool.cs
-   functional scripts/ResetIsJumping.cs
-   functional scripts/BubblePhraseGenerator.cs
-   functional scripts.PlayerPickerImageClickHandler.cs
-   Objects/IngredientProps.cs
-   Objects/WallScript.cs
-   Player/PickUpController.cs
-   Player/HoldingObjectScript.cs
-   Player/PlayerLocomotion.cs
-   Player/PlayerManager.cs




### **Whitney**


#### Tasks:


-   Set up the initial project
-   Created Title Screen UI and its functionalities
-   Handled Scene Switching and handled tasks between scenes.
-   Money Counter Scripts Scripts and Placeholder UI
-   Round Timers Scripts and Placeholder UI
-   Round End Screens Scripts and Placeholder UI
-   Backstory Scripts, UI, and wrote Backstory
-   Instructions Screen, Credits Screen, and Level Select Screens + Functionality and Placeholder UI created
-   Misc Debugging
-   Lead on designing playtest survey
-   Video Demos and Trailers Recording


#### Assets:


-   Placeholder Title Screen 
-   Placeholder Level Screen
-   Majority of the Instructions Screen
-   Majority of the Credits Screen
-   Placeholder Pause Screen
-   Placeholder Round Over 
-   Placeholder Gameplay UI
-   Final All Backstory Screens
-   Final End Screen
-   Royalty Free Background Music
-   Misc Minor Design Assets throughout Each Screen (Some Buttons, Text, etc)




#### Scripts:


-   functional scripts/Summary.cs
-   functional scripts/RoundOver.cs
-   Utility/PauseMenuToggle.cs (and a bit of Player/DO_NOT_MOVE/PlayerControls.cs to get Pause to work)
-   Utility/SceneSwitcher.cs
-   Utility/Timer.cs
-   Utility/Coins.cs
-   Utility/StoryLoader.cs
-   Utility/BackgroundMusic.cs


## 7. **3rd Party Assets**


### Models:


-   RPG Tiny Hero Duo PBR Polyart by Dungeon Mason
-   Banana Peel Free 3D print model by Orakio
-   Fire by Edgar_koh
-   Chibi Rabbit Animated For Games Free low-poly 3D model by MayaZ
-   Chibi Horse Animated For Games Free low-poly 3D model by MayaZ
-   Low Poly Table and Chair Free low-poly 3D model by mahamadjonovshaxzod
-   FREE Single Door with molds frame handle and hinges Free 3D model by Deltahedra
-   cutting board Lou- Poli Free low-poly 3D model by anatolii2586
-   Lowpoly potato FREE Free low-poly 3D model by xaviervanlooo
-   Pepper Free 3D model by BrutalRender
-   Stylized Snow Forest by Frag112
-   Simple Water Shader URP by IgniteCoders
-   Stylize Snow Texture by LowlyPoly
-   Skybox Series Free by Avionx
-   Polar Region Free 3D model by animation-3dartworks
-   Cartoon Farm Crops by False Wisp Studios
-   Low Poly Food Lite by JustCreate
-   Low Poly Farm Pack Lite by JustCreate
-   Free Lava Shader by AYPRODUCT
-   Idyllic Fantasy Nature by EDENITY
-   Free Kitchen -Cabinets and Equipments by Boxx-Games Assets


### Image Assets:


-   “Restaurant on rooftop or building terrace with city view” background by Dreamstime
-   Complete blue menu of Graphical User Interface GUI Pro Vector by johnstocker


### Music: 


-   NightShade by Adhesive Wombat
-   A Night Of Dizzy Spells by Eric Skiff
-   Powerup by Jeremy Blake
-   Up In My Jam by Kubbi
-   Operatic3 by Vibe Mountain
-   Coupe by The Grand Affair


### Misc. 
Audio clips from freesound.org: 
-   Mco_walk_b02.wav by Fantozzi
-   368386__madamvicious__small-village-hobbit elf-girl-rpg-greeting goodbye ct-sounds.wav by MadamVicious
-   698768__ienba__game-pickup.wav by ienba
-   166290__fantozzi__mco_walk_b02.wav by fantozzi

### Showcase

Title

![title](https://github.com/user-attachments/assets/f10c655b-0f90-4fb4-a95b-5f9b5425627e)

Running Overview

![running](https://github.com/user-attachments/assets/73ac4ab6-b3cc-4d64-8810-fb85a6ca50c8)

Actions

| **Running** | **Falling** | **Jumping** | **Pickup** |
|---|---|---|---|
| <img src="https://github.com/user-attachments/assets/fb986c80-f6c8-4c5a-9bd9-e937a7683af7" width=200>   | <img src="https://github.com/user-attachments/assets/420b9631-16d1-461d-b880-8fc6cd7302dd" width=200>   | <img src="https://github.com/user-attachments/assets/e905d01d-7f6f-4198-8945-29991cd7a020" width=200>   | <img src="https://github.com/user-attachments/assets/0832f709-eb17-4381-87d5-72fad0b43396" width=200>   |

NPCs

![customers](https://github.com/user-attachments/assets/ad010d5c-e233-424b-bb18-89994aca6a8e)
