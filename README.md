# Commandos-Like
Video: https://youtu.be/UkqusQHuKXE

In project, I use design patterns
- MVC (Model, View, Controller)
- Singleton
- State Machine (For enemy AI and party members)
- Command Pattern (Create command queries for party members)

For dialogue system, I use Ink.

## How enemy AI works?
I use state machine design pattern for creating enemy AI. Every enemy has State Manager and I create states with "State Class" witch is a scriptable object.
![image](https://github.com/Bolzac/Commandos-Like/assets/70448242/85b9ac9f-d1df-473e-a9bc-4c48a6d81f79)
Above image shows what states the AI have in the scene.
- In patrol, enemy move specific points in loop.
- In Suspicious, if player get inside in the sight of enemy, enemy start to look at the player to recognize he/she is the enemy.
- In Search, I planned to make it search for player after enemy lost player but it doesn't work as expected. (Not finished)
- In Check, If enemy hear a voice in some point in the world, then enemy look at that point. If it see the source then stops staring and back to patrol. If the point can not seen by enemy, it goes to check for it.
- In chase, when enemy suspicios correct then enemy starts to chase player while drawing its sword (which is not visible but animation plays)
- In Attack, if enemy close enough to attack player then starts to attack.
- In dead, enemy is dead.

## How to command party members?
Every click on the except UI and outside of the navmesh, creates a command for selected members. If I choose an empty place where it can go then it goes there. If I click twice then it starts to run.
If I click on interaction entity, then two commands added to command query. First command is getting close enough to entity, then interact. To cancel interaction just click a movable place and command query get cleared and new command added to go there.

Some gameobjects that you see like DataPersistenceManager and some classes is not completed yet.
