# ld38-unbreakable
Repo for my LD38 Compo entry

The Game Plan
---

ALRIGHT. At this point I've got something like 45-ish hours left. Let's say I'm sensible and sleep and eat for a bit in there too, what that means is it's not worth breaking up that time into "by time X, I should have done this". OK. Fine. I can work with that. Regardless of anything else, I've got a limited amount of time, and I want to leave time for debugging too.

At each top level, I want to do at least the first thing for each one (some game, some art, some music), and delve deeper into the trees as time permits.

LAST MINUTE LIST
* Finish level 1
* Fix janky animations
* Splash screen/menu

* Make the game
  * Make the unity project
  * Top down framework
    * Controls - DONE
    * Simple level - DONE
    * Triggers (level completion)
  * Level generation - NOT DOING (not enough time to learn _and_ do)
    * More complicated levels, obstacles (add as needed?)
    * Procedural generation?
  * AI
    * Make some simple AI - DONE
    * Add damage/health (damage is to key? Maybe you get a bunch of 'lives' at that level, until the key is gone?)
    * Generate some more complex AI behaviours - NOT DOING
      * Pathfinding?
  * Level transitions
    * "Zoom out" effect - kind of important for the theme? Maybe context is enough - you become the enemy you faced in the previous level.
  * Level theming
    * What makes each level interesting and unique?
      * Different enemies/weapons
    * Create the different levels
      * Mouse
      * Cat
      * Person
      * ?Car
      * Mech
      * Ship
      * ?Bigger ship
      * Spacestation
      * Moon
* Make the arts
  * Make the programmer arts
    * Simple sprites, no animation - DONE
  * Make the animations
    * Animate main sprites
    * Animate tertiary sprites
  * Make the splash screens
    * Do the thing
    * Do a logo?
* Make the musics
  * Make a simple level loop
  * Make the menu music
  * Make _different_ level music for each level

Misc ideas:
---
* +/-1 effect
  * There's going to be a level above and a level below you, after the first level. 
  * Will you affect the level below? 
    * Does killing the things below you hurt your chances on the current level? * If you lose at a level, do you go back to the previous one?

Future ideas/Out of Scope
---
* Refactorings!
  * At the moment the AI has several different behaviours, that trigger different things based on the context, so a given 'behaviour' is spread out among many contexts. Would be good to group all the behaviour's behaviours together in one class, and register callbacks or something?
  * Try AddForce instead of modifying velocity directly