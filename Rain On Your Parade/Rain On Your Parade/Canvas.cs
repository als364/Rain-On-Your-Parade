using System.Collections.Generic;
using System.Collections;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System;

namespace Rain_On_Your_Parade
{
    class Canvas
    {
        public const int SQUARE_SIZE = 80;
        private int squaresWide = GameEngine.SCREEN_WIDTH / SQUARE_SIZE;
        private int squaresTall = GameEngine.SCREEN_HEIGHT / SQUARE_SIZE;

        private GridSquare[,] canvasGrid;
        private Player player;
        private int malice;                 //total malice generated
        private int maliceObjective;        //amount of malice needed to win level
        private List<WorldObject> objects;
        private List<Actor> actors;

        public GameEngine.WinCondition win;
        public List<Actor> maliceActors = new List<Actor>();
        public List<WorldObject> maliceObjects = new List<WorldObject>();

        public int levelNum;

        public Hashtable rainbows = new Hashtable();

        public const int INTERACT_RADIUS = 50;

        public Canvas(int level)
        {
            levelNum = level;

            Grid = new GridSquare[squaresWide, squaresTall];
            objects = new List<WorldObject>();
            actors = new List<Actor>();

            for (int i = 0; i < squaresWide; i++)
            {
                for (int j = 0; j < squaresTall; j++)
                {
                    Grid[i, j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), new Point(i, j));
                }
            }

            int initialRain = 6;
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 0), 0));
            objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 0), 0));

            switch (level)
            {
                case 1:
                    //Level 1 - Soak the Cat (endless water, Goal: Actor Cat)
                    #region levelone

                    initialRain = (GameEngine.SCREEN_WIDTH/20);

                    //fence 1,9 to 8,9 
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 7), 0));

                    //fence 1,5 to 1,8 and 8,5 to 8,8
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 6), 0));

                    //fence 2,5 3,5 6,5 7,5
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 3), 0));

                    //house 3,4 5,3 4,4 5,4
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(4, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(5, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(4, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(5, 3), 0));

                    //cat 2,8
                    actors.Add(new Actor(ActorType.Type.Cat, new Point(2, 6)));

                    win = GameEngine.WinCondition.Actors;
                    #endregion levelone
                    break;
                case 2:
                    //Level 2 - Kill the Flowers (Goal: WorldObject Garden)
                    #region level2

                    initialRain = 10;

                    //fence 2,2 to 7,2
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 2), 0));

                    //fence 2,8 to 7,8
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 7), 0));

                    //fence 7,3 to 7,7
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 7), 0));

                    //fence 2,3 to 2,7
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 7), 0));

                    //garden 3,3 to 6,6
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(3, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(3, 4), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(3, 5), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(3, 6), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 4), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 5), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 6), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(5, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(5, 4), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(5, 5), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(5, 6), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(6, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(6, 4), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(6, 5), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(6, 6), 1));

                    win = GameEngine.WinCondition.Objects;
                    #endregion level2
                    break;
                case 3:
                    //Level 3 - Make Kids Cry (Goal: Actor Kid)
                    #region level3

                    initialRain = 20;

                    //fence 0,5 to 0,9
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 9), 0));

                    //fence 4,5 to 4,9
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 9), 0));

                    //fence 5,0 to 5,4
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 0), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 4), 0));

                    //fence 9,0 to 9,4
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 0), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 4), 0));

                    //sidewalk 0,0 0,2 0,4 9,5 9,7 9,9
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(0, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(0, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(9, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(9, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(9, 9), 0));

                    //chalking 0,1 0,3 9,6 9,8
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(9, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(9, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(0, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(0, 1), 0));

                    //pool 2,7 7,2
                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(2, 7), 5));
                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(7, 2), 5));

                    //house 2,1 3,2 
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(2, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(2, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(3, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(3, 2), 0));

                    //house 6,7 7,8
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(6, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(6, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(7, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(7, 7), 0));

                    //kid 3,3 and 6,6
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(3, 3)));
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(6, 6)));

                    win = GameEngine.WinCondition.Actors;
                    #endregion level3
                    break;
                case 4:
                    //Level 5 - Kill the Flowers v2.0 (Goal: WorldObject Garden)
                    #region level5

                    initialRain = 10;

                    //mom 1,1 4,4 8,7
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(1, 1)));
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(4, 4)));
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(8, 7)));

                    //flower 0,0 to 0,3 
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(0, 1), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(0, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(0, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(0, 4), 1));

                    //flower 1,0 2,1 2,2
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(1, 1), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(2, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(2, 3), 1));

                    //flower 3,0 3,3 to 3,6
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(3, 1), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(3, 4), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(3, 5), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(3, 6), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(3, 7), 1));

                    //flower 4,0 to 4,3 4,6
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 1), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 4), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 7), 1));

                    //flower 5,3 to 5,9
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(5, 4), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(5, 5), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(5, 6), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(5, 7), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(5, 8), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(5, 9), 1));

                    //flower 6,6 7,7 7,8 8,6
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(6, 6), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(7, 7), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(7, 8), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(8, 6), 1));

                    //flower 9,6 to 9,9
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(9, 6), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(9, 7), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(9, 8), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(9, 9), 1));

                    win = GameEngine.WinCondition.Objects;
                    #endregion level5
                    break;
                case 5:
                    //Level 4 - Lambs to the Slaughter (Goal: Some Reasonable Malice Quota for 3 actors)
                    #region level4

                    initialRain = 30;

                    //flower 4,0 5,0 4,9 5,9
                    objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(4, 1), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Laundry, new Point(4, 9), 1));

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

                    win = GameEngine.WinCondition.Malice;
                    #endregion level4
                    break;
                case 6:
                    //Level 6 - Showdown (Goal: Actor Cat)
                    #region level6

                    initialRain = 10;

                    //fence 2,0 to 2,3
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 0), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 3), 0));

                    //fence 3,3 to 9,3
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(8, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(9, 3), 0));

                    //fence 0,6 to 7,6
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(0, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(1, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(2, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(3, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(4, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(5, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(6, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 6), 0));

                    //fence 7,6 to 7,9
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 9), 0));

                    //sunnyspot 0,5 5,4 4,5 9,4
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(0, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(5, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(4, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(9, 4), 0));

                    //cat 0,0 and 9,9
                    actors.Add(new Actor(ActorType.Type.Cat, new Point(0, 1)));
                    actors.Add(new Actor(ActorType.Type.Cat, new Point(9, 9)));

                    win = GameEngine.WinCondition.Actors;
                    #endregion level6
                    break;
                case 7:
                    //Level 7 - Showdown v2.0 (Goal: Some reasonable malice level given 2 kids, 2 moms)
                    #region level7

                    initialRain = 20;

                    //mom 0,0 and 9,9
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(0, 1)));
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(9, 9)));

                    //kid 1,8 and 9,1
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(1, 9)));
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(9, 1)));

                    //flower 0,5 1,5 8,4 9,4
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(0, 6), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(1, 6), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(8, 4), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(9, 4), 1));

                    //sunnyspot 4,5 5,4
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(4, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(5, 5), 0));

                    //chalking 3,0 6,0 3,3 6,6 6,9 3,9
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(3, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(6, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(6, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(6, 9), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(3, 9), 0));

                    //sidewalk 3,1 3,2 4,0 5,0 7,0 8,0 9,0
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(3, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(3, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(4, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(5, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(7, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(8, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(9, 1), 0));

                    //sidewalk 0,9 1,9 2,9 4,9 5,9 6,8 6,7
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(0, 9), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(1, 9), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(2, 9), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(4, 9), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Sidewalk, new Point(5, 9), 0));
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
                    objects.Add(new WorldObject(ObjectType.Type.Fence, new Point(7, 9), 0));

                    win = GameEngine.WinCondition.Malice;
                    #endregion level7
                    break;
                case 8:
                    //Level 8
                    #region level8
                    actors.Add(new Actor(ActorType.Type.Cat, new Point(9, 6)));
                    actors.Add(new Actor(ActorType.Type.Cat, new Point(0, 6)));
                    actors.Add(new Actor(ActorType.Type.Cat, new Point(5, 4)));

                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(8, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(2, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(2, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(8, 6), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(2, 1), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(3, 1), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 1), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(5, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(6, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(6, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(7, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(8, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(8, 4), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(7, 4), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(6, 4), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(6, 5), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(5, 5), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 5), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(4, 6), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(3, 6), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(2, 6), 1));
                    win = GameEngine.WinCondition.Actors;
                    #endregion level8
                    break;
                case 9:
                    //Level 9
                    #region level9
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(9, 6)));
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(5, 6)));
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(0, 6)));
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(3, 3)));
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(7, 3)));

                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(0, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(1, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(2, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(3, 0), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(3, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(4, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(5, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(6, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(7, 0), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(7, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(8, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(9, 1), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(0, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(1, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(2, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(3, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(4, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(5, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(6, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(7, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(8, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(9, 2), 0));

                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(1, 5), 3));
                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(9, 5), 3));

                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(1, 3), 2));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(5, 7), 2));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(9, 3), 2));
                    win = GameEngine.WinCondition.Actors;
                    #endregion level9
                    break;
                case 10:
                    //Level 10
                    #region level10
                    actors.Add(new Actor(ActorType.Type.Cat, new Point(6, 0)));
                    actors.Add(new Actor(ActorType.Type.Kid, new Point(1, 8)));
                    actors.Add(new Actor(ActorType.Type.Mom, new Point(6, 6)));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(1, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.SunnyRainbowSpot, new Point(5, 7), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(9, 2), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(9, 7), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(2, 3), 1));
                    objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(9, 5), 1));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(8, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(9, 1), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(2, 8), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(9, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(3, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(4, 2), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(3, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(4, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(2, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(3, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(4, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(8, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(8, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.House, new Point(9, 6), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(0, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(1, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(2, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(3, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(4, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(5, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(6, 5), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(6, 4), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(6, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(7, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(8, 3), 0));
                    objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(9, 3), 0));
                    win = GameEngine.WinCondition.Actors;
                    #endregion level10
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
                maliceActors.Add(actor);
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

        public int Malice
        {
            get
            {
                return malice;
            }

            set
            {
                malice = value;
            }
        }

        public int MaliceObjective
        {
            get
            {
                return maliceObjective;
            }

            set
            {
                maliceObjective = value;
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

        public float maliceTint() {
            return 1 - malice / maliceObjective;
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
                Debug.WriteLine("Square " + square.Location + " is passable: " + entity.Type.Passable);
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
            foreach (Actor a in actors) {
                if (Math.Abs(Vector2.Distance(currAct.PixelPosition, a.PixelPosition)) < INTERACT_RADIUS && !a.Equals(currAct)) {
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
            Vector2 p_pos = (p is Player) ? (p.PixelPosition + new Vector2(0,80)) : p.PixelPosition; //detect cloud when near shadow
            return (Math.Abs(Vector2.Distance(p_pos, q.PixelPosition)) < INTERACT_RADIUS && !p.Equals(q));
        }

        public void updateMalice()
        {
            maliceActors = new List<Actor>();
            maliceObjects = new List<WorldObject>();
            malice = 0;

            foreach(Actor a in actors) {
                malice += a.Mood;
                if (a.Mood >= 5)
                {
                    maliceActors.Add(a);
                }
            }
            foreach(WorldObject o in objects)
            {
                if (o.Type.CanActivate && !o.Activated)
                {
                    maliceObjects.Add(o);
                }
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
                "\nMalice: " + malice + "\nMalice Objective: " + maliceObjective + "\n" + "Player: \n" + player.ToString() + "\nGrid: \n" + grid;
        }
    }   
}