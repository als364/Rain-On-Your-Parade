using System.Collections.Generic;
using System.Collections;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System;

namespace Rain_On_Your_Parade
{
    public class Canvas
    {
        public const int SQUARE_SIZE = 80;
        private int squaresWide = GameEngine.SCREEN_WIDTH / SQUARE_SIZE;
        private int squaresTall = GameEngine.SCREEN_HEIGHT / SQUARE_SIZE;

        private GridSquare[,] canvasGrid;
        private Player player;
        private int mood;                 //total mood
        private int moodObjective;        //amount of mood needed to win level
        private List<WorldObject> objects;
        private List<Actor> actors;
        public float percentWon;
        public string objectiveMessage;
        public string hint;
        public string title;
        public int initialRain;

        public GameEngine.WinCondition win;
        public ActorType.Type? aType;
        public ObjectType.Type? oType;
        public List<Actor> angerActors = new List<Actor>();
        public List<WorldObject> angerObjects = new List<WorldObject>();
        public List<Actor> goalAngerActors = new List<Actor>();
        public List<WorldObject> goalAngerObjects = new List<WorldObject>();

        public int levelNum;

        public Hashtable rainbows = new Hashtable();

        public const int INTERACT_RADIUS = 50;

        public Canvas(string title, int width, int height, GameEngine.WinCondition cond, List<WorldObject> o, List<Actor> a, Player p, string message, List<WorldObject> angerObjs, List<Actor> angerActors, int moodGoal)
        {
            this.title = title;
            squaresTall = height;
            squaresWide = width;
            Grid = new GridSquare[squaresWide, squaresTall];
            objects = o;
            actors = a;
            percentWon = 0f;
            objectiveMessage = message;
            this.angerActors = angerActors;
            angerObjects = angerObjs;
            moodObjective = moodGoal;

            for (int i = 0; i < squaresWide; i++)
            {
                for (int j = 0; j < squaresTall; j++)
                {
                    Grid[i, j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), new Point(i, j));
                }
            }

            player = p;

            foreach (WorldObject entity in objects)
            {
                Grid[entity.GridspacePosition.X, entity.GridspacePosition.Y].add(entity);
            }
            foreach (Actor actor in actors)
            {
                Grid[actor.GridspacePosition.X, actor.GridspacePosition.Y].add(actor);
            }
            foreach (GridSquare square in Grid)
            {
                square.calculateLevels();
            }

            initializeAdjacencyLists();

            win = cond;
        }


        public Canvas(int level)
        {
            levelNum = level;

            Grid = new GridSquare[squaresWide, squaresTall];
            objects = new List<WorldObject>();
            actors = new List<Actor>();
            percentWon = 0f;
            objectiveMessage = "";
            title = "";

            for (int i = 0; i < squaresWide; i++)
            {
                for (int j = 0; j < squaresTall; j++)
                {
                    Grid[i, j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), new Point(i, j));
                }
            }

            initialRain = 6;

            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(10, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(11, 0), 0));

            switch (level)
            {
                case 1:
                    //Level 1 - Soak the Cat (endless water, Goal: Actor Cat)
                    #region levelone
                    initialRain = Player.MAX_RAIN; //don't start with 10, this is a tutorial level -- it's supposed to be easy

                    title = "Level 1\nSoak the Cat";
                    hint = "Hint: Wait for the Cat to dry off before raining again";
                    objectiveMessage = "Goal: Rain on the cat to upset him,\nangering him by 1 each time.\nUse [SPACEBAR] to Rain.\nUse [WASD] or [Arrow] to move.";

                    //fence 1,9 to 8,9 
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 7), 0));

                    //fence 1,5 to 1,8 and 8,5 to 8,8
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 6), 0));

                    //fence 2,5 3,5 6,5 7,5
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 3), 0));

                    //house 3,4 5,3 4,4 5,4
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(5, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(6, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(5, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(6, 3), 0));

                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(5, 2), 0));

                    //cat 2,8
                    actors.Add(new Actor(ActorType.Type.Cat, new Point(3, 6)));

                    //Set win condition for level
                    win = GameEngine.WinCondition.Actors;
                    aType = ActorType.Type.Cat;

                    //Add actors/objects that need to be fully angered
                    updateAngerList();

                    #endregion levelone
                    break;
                case 2:
                    //Level 2 - Kill the Flowers (Goal: WorldObject Garden)
                    #region level2

                    initialRain = 0;
                    title = "Level 2\nKill Flowers: Upset Cat";
                    hint = "Hint: Cats don't like it when you drain their flowers";
                    objectiveMessage = "Goal: Drain flowers to gain water.\nUse [Alt] to Absorb water.\nIf you absorb water while the Cat is\nsleeping, his mood will worsen by 2!";

                    //TOP
                    //fence 2,2 to 7,2
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 2), 0));

                    //BOTTOM
                    //fence 1,9 to 8,9 
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 7), 0));


                    //LEFT/RIGHT
                    //fence 1,5 to 1,8 and 8,5 to 8,8
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 6), 0));


                    //garden 3,3 to 6,6
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(2, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(2, 6), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(7, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(7, 6), 1));

                    //cat 2,8
                    actors.Add(new Actor(ActorType.Type.Cat, new Point(4, 6)));

                    win = GameEngine.WinCondition.Actors;
                    #endregion level2
                    break;

                case 3:
                    #region level3

                    //case 5:
       
                    //#region level5

                    initialRain = 0;
                    title = "Level 3\nAbsorb the Pool";
                    hint = "Hint: You can rain to refill swimming pools!";
                    objectiveMessage = "Goal: " +
                    "Use your shadow to herd \nangry children towards each other. \nKids fight when both are frowning \nor if one is VERY upset! The \nmood of fighting kids worsens by 2.";

                    //fence 0,5 to 0,9
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 8), 0));


                    //fence 9,0 to 9,4

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(11, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(11, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(11, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(11, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(11, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(11, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(11, 8), 0));


                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(10, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 2), 0));



                    //sidewalk 0,0 0,2 0,4 9,5 9,7 9,9
                    //objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(0, 2), 0));
                    //objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(0, 4), 0));
                    //objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(11, 5), 0));
                    //objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(11, 7), 0));

                    //chalking 0,1 0,3 9,6 9,8
                    //objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(11, 8), 0));
                    //objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(11, 6), 0));
                    //objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(0, 3), 0));
                    //objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(0, 1), 0));

                    //pool 2,7 7,2
                    //objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(3, 7), 5));
                    //objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(8, 2), 5));

                    //house 2,1 3,2 
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(3, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(4, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(3, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(4, 2), 0));

                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(3, 1), 0));

                    //house 6,7 7,8
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(7, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(7, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(8, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(8, 8), 0));

                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(7, 7), 0));

                    //kid 3,3 and 6,6
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(2, 4)));
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(9, 5)));

                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(5, 5), 1));

                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(4, 4), 1));

                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(6, 6), 1));

                    //win = GameEngine.WinCondition.Mood;
                    //#endregion level5

                    //Set win condition for level
                    win = GameEngine.WinCondition.Mood;

                    //Add actors/objects that need to be fully angered
                    updateAngerList();

                    #endregion level3
                    break;
                case 4:
                    #region level4

                    initialRain = 0;

                    title = "Level 4\nMake Kids Cry";
                    hint = "Hint: Rain on sunny spots to lure kid near angry cat!";
                    objectiveMessage = "Goal: Make everyone upset.\nRaining on a sunnyspot will create a \nrainbow, attracting children for a \nshort time!"
                    + " Remember: Characters \nthat are very angry will fight.";


                    //title = "Level 3 - Make Kids Cry";
                    //objectiveMessage = "Goal: Make all the kids upset.\nTry raining on the sun spots to create a rainbow and lure kids!";



                    //fence 9,0 to 9,4
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(10, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(11, 8), 0));





                    //pool 2,7 7,2
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(2, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(8, 5), 1));

                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(1, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(7, 5), 1));

                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(2, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(8, 4), 1));

                    //house 2,1 3,2 
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(9, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(9, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(9, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(10, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(10, 7), 0));

                    

                    //house 6,7 7,8
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(5, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(5, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(5, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(6, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(6, 2), 0));

                   
               

                    //kid 3,3 and 6,6
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(3, 3)));
                    actors.Add(new Actor(ActorType.Type.Cat, new Point(6, 6)));

                 

                    //Set win condition for level
                    win = GameEngine.WinCondition.Actors;
                    aType = ActorType.Type.Kid;

                    //Add actors/objects that need to be fully angered
                    updateAngerList();

                    #endregion level4
                    break;
                case 5:
                    //Level 4 - Make Kids Cry (Goal: Actor Kid)
                    #region level5

                    initialRain = 0;

                    title = "Level 5\nMake Kids Cry";
                    hint = "Hint: Lure kids to full pools then ABSORB to upset them.";
                    objectiveMessage = "Goal: Make all the kids upset. \nLure the children to the pools \nthen drain out the water!";

                    //title = "Level 3 - Make Kids Cry";
                    //objectiveMessage = "Goal: Make all the kids upset.\nTry raining on the sun spots to create a rainbow and lure kids!";

                    //fence 0,5 to 0,9
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 8), 0));
                    //objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 9), 0));                  


                    //fence 5,0 to 5,4
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 0), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 4), 0));

                    //fence 9,0 to 9,4
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(10, 0), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(10, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(10, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(10, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(10, 4), 0));

                    //sidewalk 0,0 0,2 0,4 9,5 9,7 9,9
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(0, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(0, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(9, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(9, 7), 0));
                    //objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(9, 9), 0));

                    //chalking 0,1 0,3 9,6 9,8
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(9, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(9, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(0, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(0, 1), 0));

                    //pool 2,7 7,2
                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(2, 7), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(7, 2), 1));

                    //pool 2,7 7,2
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(2, 8), 1));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(7, 1), 1));

                    //house 2,1 3,2 
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(2, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(2, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(3, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(3, 3), 0));

                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(2, 2), 0));

                    //house 6,7 7,8
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(6, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(6, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(7, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(7, 7), 0));

                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(6, 6), 0));

                    //kid 3,3 and 6,6
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(3, 3)));
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(6, 6)));

                    //Set win condition for level
                    win = GameEngine.WinCondition.Actors;
                    aType = ActorType.Type.Kid;

                    //Add actors/objects that need to be fully angered
                    updateAngerList();

                    #endregion level5
                    break;
                case 6:
                    #region level6

                     initialRain = 5;
                     title = "Level 6\nFight!";
                     hint=  "Hint: Ruining mom's laundry will make her unhappy"; 
                     objectiveMessage = "Goal: Make Everyone Angry!\nBe careful though,"+
                     " Mom will not \nfight with others. Instead she'll\ncheer them up if she is happy!";

                  /*  //fence 0,5 to 0,9
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 8), 0));


                    //fence 9,0 to 9,4

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(11, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(11, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(11, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(11, 5), 0));


                    //fence 9,0 to 9,4

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 4), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 4), 0));

                    //sidewalk 0,0 0,2 0,4 9,5 9,7 9,9
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(0, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(0, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(0, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(11, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(11, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(11, 7), 0));

       

                    //pool 2,7 7,2
                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(3, 7), 5));
                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(8, 2), 5));

                    //house 2,1 3,2 
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(3, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(4, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(3, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(4, 2), 0));

                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(3, 1), 0));

                    //house 6,7 7,8
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(7, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(7, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(8, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(8, 8), 0));

                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(7, 7), 0));

                    //kid 3,3 and 6,6
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(2, 4)));
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(9, 5)));

                    */
                    
       

                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(0, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(0, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(1, 8), 0));
   

                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(11, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(11, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(11, 8), 0));


                    actors.Add(new Actor(ActorType.Type.Mom, new Point(1, 4)));
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(9, 7)));

                    //cat 0,0 and 9,9
                    actors.Add(new Actor(ActorType.Type.Cat, new Point(0, 7)));
                    actors.Add(new Actor(ActorType.Type.Cat, new Point(11, 1)));

                     objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(0, 8), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(1, 8), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(2, 8), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(3, 8), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(4, 8), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(5, 8), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(6, 8), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(7, 8), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(8, 8), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(9, 8), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(10, 8), 0));
                    // objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(11, 8), 0));


                     //fence 3,3 to 9,3
                    // objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 3), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 3), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 3), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 3), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 3), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 3), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 3), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 3), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(10, 3), 0));

                     //fence 0,6 to 7,6
                     objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 5), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 5), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 5), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 5), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 5), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 5), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 5), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(10, 5), 0));
                    // objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(11, 5), 0));

                     objects.Add(new WorldObject(ObjectType.Type.House, new Point(5, 3), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(5, 3), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(5, 4), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(6, 3), 0));
                     objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(6, 4), 0));

                     //sunnyspot 0,5 5,4 4,5 9,4
                     objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(0, 2), 0));
                     objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(11, 0), 0));
                     objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(2, 7), 0));
                     objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(11, 6), 0));

                     objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(1, 2), 0));

                     //Set win condition for level
                     win = GameEngine.WinCondition.Actors;

                     Console.WriteLine(actors.Count);

                     //Add actors/objects that need to be fully angered
                     updateAngerList();

                     #endregion level6
                     break;
                    
                case 7:
                    #region level7
                    
                    initialRain = 0;
                    title = "Level 7\nKill the Flowers, Wet the Laundry";
                    objectiveMessage = "Goal: Absorb the flowers and \nwet the laundry before the motherly \nnurturing heals them!";

                    //mom 1,1 4,4 8,7
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(1, 2)));
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(5, 4)));
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(10, 7)));

                    //mom 1,1 4,4 8,7
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(4, 2)));
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(5, 4)));
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(2, 3)));

                    //flower 0,0 to 0,3 
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(0, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(0, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(0, 4), 1));

                    //flower 1,0 2,1 2,2
                    objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(1, 1), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(2, 2), 1));
           

                    //flower 3,0 3,3 to 3,6
                    objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(3, 1), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 4), 1));
                

                    //flower 5,3 to 5,9
                   // objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(5, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(5, 6), 1));

                    objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(6, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(6, 4), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(6, 5), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(6, 6), 1));

                    //flower 6,6 7,7 7,8 8,6
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(7, 6), 1));

                    //flower 6,6 7,7 7,8 8,6
                    objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(7, 7), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(7, 8), 1));

                    objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(8, 5), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(8, 6), 1));

                    //flower 9,6 to 9,9
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(9, 6), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(9, 7), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(9, 8), 1));

                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(10, 5), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(10, 6), 1));

                   

                    //Set win condition for level
                    win = GameEngine.WinCondition.Objects;

                    //Add actors/objects that need to be fully angered
                    updateAngerList();

                    #endregion level7
                    break;
                case 8:
                    //Level 7 
                    #region level7

                    initialRain = 4;
                    title = "Level 8 - Kill the Flowers v2.0";
                    hint = "hint";
                    objectiveMessage = "Goal: Drain the flowers of water\nBeware the motherly nurturing!";

                    //mom 1,1 4,4 8,7
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(9, 1)));
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(5, 3)));
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(2, 4)));

                     actors.Add(new Actor(ActorType.Type.Mom, new Point(2, 7)));
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(0, 7)));
                    

                    objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(0, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(2, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(2, 8), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(10, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(8, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(0, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(2, 6), 0));

                    // objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(0, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(2, 8), 0));

                     objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(7, 8), 0));
                    //objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(9, 8), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 5), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(10, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(11, 3), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 2), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(2, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(1, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(3, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(6, 7), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(8, 7), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(10, 7), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(10, 5), 1));



 

                    //Set win condition for level
                    win = GameEngine.WinCondition.Objects;
                

                    //Add actors/objects that need to be fully angered
                    updateAngerList();

                    #endregion level3
                    //#endregion level4
                    break;
                case 9:
                    //Level 8 - Experimental Level - C is for Cat / Cats and Flowers [Not Final]
                    #region level8

                    title = "Level 8\nC is for Cat";
                    objectiveMessage = "Goal: Make cats unhappy.";

                    actors.Add(new Actor(ActorType.Type.Cat, new Point(9, 1)));
                    actors.Add(new Actor(ActorType.Type.Cat, new Point(0, 4)));
                    actors.Add(new Actor(ActorType.Type.Cat, new Point(0, 5)));
                    actors.Add(new Actor(ActorType.Type.Cat, new Point(9, 8)));

                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(0, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(4, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(6, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(6, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(0, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(4, 8), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(3, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(5, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(6, 2), 1));

                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(2, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(3, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(2, 4), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(2, 5), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(2, 6), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(3, 6), 1));

                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(3, 7), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 7), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(5, 7), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(6, 7), 1));

                    win = GameEngine.WinCondition.Actors;
                    #endregion level8
                    break;
                case 10:
                    //Level 9 - Experimental Level - Backyard Business / Super Progressive [Not Final]
                    #region level9

                    title = "Level 9\nBackyard Business";
                    objectiveMessage = "Goal: Make kids and moms upset.";

                    actors.Add(new Actor(ActorType.Type.Kid, new Point(3, 2)));
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(6, 2)));
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(4, 6)));
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(5, 6)));

                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(0, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(1, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(8, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(9, 1), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(4, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(5, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(4, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Invisible, new Point(5, 3), 0));

                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(4, 2), 0));

                    //pool
                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(3, 5), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(6, 5), 1));

                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(0, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(1, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(8, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(9, 2), 1));

                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(9, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(0, 8), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(1, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(2, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(3, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(4, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(5, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(6, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(7, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(8, 8), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 1), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 6), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 6), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 6), 0));

                    win = GameEngine.WinCondition.Actors;
                    #endregion level9
                    break;
                case 11:
                    //Level 10 - Experimental Level - Spiral Fences [Not Final]
                    #region level10
                    initialRain = 0;

                    title = "Level 10 - Spiral Fences";
                    objectiveMessage = "Goal: Make the kid unhappy.\nYou have limited water so\nuse the kid to your advantage.";

                    actors.Add(new Actor(ActorType.Type.Kid, new Point(0, 1)));

                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(0, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(2, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(2, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(5, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(5, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(7, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(7, 8), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(4, 4), 1));

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 8), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 8), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 1), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 7), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 7), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 6), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 3), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 5), 0));

                    win = GameEngine.WinCondition.Actors;
                    #endregion level10
                    break;

                case 12:
                    //Level 11 - Experimental Level  - Showdown v2.0 (Goal: Some reasonable malice level given 2 kids, 2 moms)  [Not Final]
                    #region levelEX2

                    initialRain = 20;
                    title = "Level 7 - Showdown v2.0";
                    objectiveMessage = "Goal: Make kids and moms fight each other!";

                    //mom 0,0 and 9,9
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(0, 1)));
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(9, 8)));

                    //kid 1,8 and 9,1
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(1, 8)));
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(9, 1)));

                    //flower 0,5 1,5 8,4 9,4
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(0, 6), 2));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(1, 6), 2));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(8, 4), 2));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(9, 4), 2));

                    //sunnyspot 4,5 5,4
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(4, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(5, 5), 0));

                    //chalking 3,0 6,0 3,3 6,6 6,9 3,9
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(3, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(6, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(6, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(6, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(3, 8), 0));

                    //sidewalk 3,1 3,2 4,0 5,0 7,0 8,0 9,0
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(3, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(3, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(4, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(5, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(7, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(8, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(9, 1), 0));

                    //sidewalk 0,9 1,9 2,9 4,9 5,9 6,8 6,7
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(0, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(1, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(2, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(4, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(5, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(6, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(6, 7), 0));

                    //fence 2,0 to 2,3
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 3), 0));

                    //fence 0,6 to 3,6
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 7), 0));

                    //fence 6,3 to 9,3
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 3), 0));

                    //fence 7,6 to 9,6
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 8), 0));

                    win = GameEngine.WinCondition.Mood;
                    #endregion levelEX2
                    break;

                case 13:
                    //Level 12 - Experimental Level - Lambs to the Slaughter (Goal: Some Reasonable Malice Quota for 3 actors)  [Not Final]
                    #region levelEX4

                    initialRain = 30;
                    title = "Level 5 - Lambs to the Slaughter";
                    objectiveMessage = "Goal: Make everyone miserable.\nRaining on Sunny Spots generates\na temporary Rainbow.";

                    //flower 4,0 5,0 4,9 5,9
                    objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(4, 1), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(4, 8), 1));

                    //sunnyspot 1,1 1,8 8,8 8,1
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(1, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(1, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(8, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(8, 1), 0));

                    //pool 0,4 9,4
                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(0, 4), 5));
                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(9, 4), 5));

                    //sidewalk 4,1 to 4,8 and 5,1 to 5,8
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(4, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(4, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(4, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(4, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(4, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(4, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(4, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(5, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(5, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(5, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(5, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(5, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(5, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(5, 8), 0));

                    //kid 3,2
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(3, 2)));

                    //mom 7,6
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(7, 6)));

                    //cat 3,7
                    actors.Add(new Actor(ActorType.Type.Cat, new Point(3, 7)));

                    win = GameEngine.WinCondition.Mood;
                    #endregion levelEX4
                    break;

                default: break;
            }


            player = new Player(initialRain);

            foreach (WorldObject entity in objects)
            {
                Grid[entity.GridspacePosition.X, entity.GridspacePosition.Y].add(entity);
            }
            foreach (Actor actor in actors)
            {
                Grid[actor.GridspacePosition.X, actor.GridspacePosition.Y].add(actor);
                //angerActors.Add(actor);
            }
            foreach (GridSquare square in Grid)
            {
                square.calculateLevels();
            }

            initializeAdjacencyLists();
        }

        #region Getters & Setters
        public GridSquare[,] Grid
        {
            get
            {
                return canvasGrid;
            }
            set
            {
                canvasGrid = value;
            }
        }

        public int Mood
        {
            get
            {
                return mood;
            }

            set
            {
                mood = value;
            }
        }

        public int MoodObjective
        {
            get
            {
                return moodObjective;
            }

            set
            {
                moodObjective = value;
            }
        }

        public Player Player
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
            }
        }

        public List<Actor> Actors
        {
            get
            {
                return actors;
            }
            set
            {
                actors = value;
            }
        }

        public List<WorldObject> Objects
        {
            get
            {
                return objects;
            }
            set
            {
                objects = value;
            }
        }

        public int Height
        {
            get
            {
                return squaresTall;
            }
        }

        public int Width
        {
            get
            {
                return squaresWide;
            }
        }
        #endregion

        public float moodTint()
        {
            //return 1 - mood / moodObjective;
            return 1;
        }

        /// <summary>Initialize adjacency lists in level map for ease of search</summary>
        /// <devdoc>
        /// Sets up adjacency lists for the entire levelmap. This will use pointers if at all possible later, but for now, I have an i7.
        /// </devdoc>
        private void initializeAdjacencyLists()
        {
            for (int x = 0; x < squaresWide; x++)
            {
                for (int y = 0; y < squaresTall; y++)
                {
                    GridSquare currentSquare = canvasGrid[x, y];
                    if (x == 0)
                    {
                        if (y == 0) //top left
                        {
                            if (SquareIsPassable(canvasGrid[x + 1, y])) currentSquare.adjacent.Add(Grid[x + 1, y]);
                            if (SquareIsPassable(canvasGrid[x, y + 1])) currentSquare.adjacent.Add(Grid[x, y + 1]);
                        }
                        else if (y == squaresTall - 1) //bottom left
                        {
                            if (SquareIsPassable(canvasGrid[x + 1, y])) currentSquare.adjacent.Add(canvasGrid[x + 1, y]);
                            if (SquareIsPassable(canvasGrid[x, y - 1])) currentSquare.adjacent.Add(canvasGrid[x, y - 1]);
                        }
                        else //left edge
                        {
                            if (SquareIsPassable(canvasGrid[x + 1, y])) currentSquare.adjacent.Add(canvasGrid[x + 1, y]);
                            if (SquareIsPassable(canvasGrid[x, y + 1])) currentSquare.adjacent.Add(canvasGrid[x, y + 1]);
                            if (SquareIsPassable(canvasGrid[x, y - 1])) currentSquare.adjacent.Add(canvasGrid[x, y - 1]);
                        }
                    }
                    else if (x == squaresWide - 1)
                    {
                        if (y == 0) //top right
                        {
                            if (SquareIsPassable(Grid[x - 1, y])) currentSquare.adjacent.Add(Grid[x - 1, y]);
                            if (SquareIsPassable(Grid[x, y + 1])) currentSquare.adjacent.Add(Grid[x, y + 1]);
                        }
                        else if (y == squaresTall - 1) //bottom right
                        {
                            if (SquareIsPassable(Grid[x - 1, y])) currentSquare.adjacent.Add(Grid[x - 1, y]);
                            if (SquareIsPassable(Grid[x, y - 1])) currentSquare.adjacent.Add(Grid[x, y - 1]);
                        }
                        else //right edge
                        {
                            if (SquareIsPassable(Grid[x - 1, y])) currentSquare.adjacent.Add(Grid[x - 1, y]);
                            if (SquareIsPassable(Grid[x, y + 1])) currentSquare.adjacent.Add(Grid[x, y + 1]);
                            if (SquareIsPassable(Grid[x, y - 1])) currentSquare.adjacent.Add(Grid[x, y - 1]);
                        }
                    }
                    else
                    {
                        if (y == 0) //top edge
                        {
                            if (SquareIsPassable(Grid[x + 1, y])) currentSquare.adjacent.Add(Grid[x + 1, y]);
                            if (SquareIsPassable(Grid[x - 1, y])) currentSquare.adjacent.Add(Grid[x - 1, y]);
                            if (SquareIsPassable(Grid[x, y + 1])) currentSquare.adjacent.Add(Grid[x, y + 1]);
                        }
                        else if (y == squaresTall - 1) //bottom edge
                        {
                            if (SquareIsPassable(Grid[x + 1, y])) currentSquare.adjacent.Add(Grid[x + 1, y]);
                            if (SquareIsPassable(Grid[x - 1, y])) currentSquare.adjacent.Add(Grid[x - 1, y]);
                            if (SquareIsPassable(Grid[x, y - 1])) currentSquare.adjacent.Add(Grid[x, y - 1]);
                        }
                        else //finally not a corner case
                        {
                            if (SquareIsPassable(Grid[x + 1, y])) currentSquare.adjacent.Add(Grid[x + 1, y]);
                            if (SquareIsPassable(Grid[x - 1, y])) currentSquare.adjacent.Add(Grid[x - 1, y]);
                            if (SquareIsPassable(Grid[x, y + 1])) currentSquare.adjacent.Add(Grid[x, y + 1]);
                            if (SquareIsPassable(Grid[x, y - 1])) currentSquare.adjacent.Add(Grid[x, y - 1]);
                        }
                    }
                }
            }
        }

        private bool SquareIsPassable(GridSquare square)
        {
            foreach (WorldObject entity in square.Objects)
            {
                //Debug.WriteLine("Square " + square.Location + " is passable: " + entity.Type.Passable);
                if (!entity.Type.Passable)
                {
                    square.IsPassable = false;
                    return false;
                }
            }
            return true;
        }

        /// <summary>Update each grid square to contain the correct actors and objects</summary>
        public void upateGridSquares()
        {
            foreach (GridSquare g in canvasGrid)
            {
                g.clearActObjLists();
            }
            foreach (WorldObject o in objects)
            {
                GridSquare square = Grid[o.GridspacePosition.X, o.GridspacePosition.Y];
                square.add(o);
            }
            foreach (Actor a in actors)
            {
                GridSquare square = Grid[a.GridspacePosition.X, a.GridspacePosition.Y];
                square.add(a);
            }
            foreach (GridSquare g in Grid)
            {
                g.calculateLevels();
            }
        }

        public List<Actor> interactableActors(Actor currAct)
        {
            List<Actor> acts = new List<Actor>();
            foreach (Actor a in actors)
            {
                if (Math.Abs(Vector2.Distance(currAct.PixelPosition, a.PixelPosition)) < INTERACT_RADIUS && !a.Equals(currAct))
                {
                    acts.Add(a);
                }
            }
            return acts;
        }

        public List<WorldObject> interactableObjects(Actor currAct)
        {
            List<WorldObject> objs = new List<WorldObject>();
            foreach (WorldObject o in objects)
            {
                if (Math.Abs(Vector2.Distance(currAct.PixelPosition, o.PixelPosition)) < INTERACT_RADIUS)
                {
                    objs.Add(o);
                }
            }
            return objs;
        }

        public bool nearEnoughForInteraction(Model p, Model q)
        {
            Vector2 p_pos = (p is Player) ? (p.PixelPosition + new Vector2(0, 80)) : p.PixelPosition; //detect cloud when near shadow
            return (Math.Abs(Vector2.Distance(p_pos, q.PixelPosition)) < INTERACT_RADIUS && !p.Equals(q));
        }

        public void updateMood()
        {
            angerActors = new List<Actor>();
            angerObjects = new List<WorldObject>();
            mood = 0;

            foreach (Actor a in actors)
            {
                Mood += a.Mood;
                if (a.Mood >= 5)
                {
                    angerActors.Add(a);
                }
            }
            foreach (WorldObject o in objects)
            {
                if (o.Type.CanActivate && !o.Activated)
                {
                    angerObjects.Add(o);
                }
            }
        }

        public void updateAngerList()
        {
            goalAngerActors = new List<Actor>();
            goalAngerObjects = new List<WorldObject>();

            switch (win)
            {
                case GameEngine.WinCondition.Mood:
                    foreach (Actor a in actors)
                    {
                        goalAngerActors.Add(a);
                    }
                    break;
                case GameEngine.WinCondition.Actors:
                    foreach (Actor a in actors)
                    {
                        if (a.Type.TypeName == aType || aType == null)
                        {
                            goalAngerActors.Add(a);
                        }
                    }
                    Console.WriteLine("Total actors: " + actors.Count);
                    Console.WriteLine("Goal anger actors: " + goalAngerActors.Count);
                    break;
                case GameEngine.WinCondition.Objects:
                    foreach (WorldObject o in objects)
                    {
                        if ((o.Type.TypeName == oType && o.Type.CanActivate) || oType == null)
                        {
                            goalAngerObjects.Add(o);
                        }
                    }
                    break;
            }
        }

        public override string ToString()
        {
            string grid = "";
            foreach (GridSquare g in Grid)
            {
                grid += g.ToString() + "\n";
            }
            return "World Width: " + squaresWide + "\nWorld Height: " + squaresTall +
                "\nMood: " + mood + "\nMood Objective: " + moodObjective + "\n" + "Player: \n" + player.ToString() + "\nGrid: \n" + grid;
        }
    }
}