using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Rain_On_Your_Parade
{
    class Canvas
    {
        public const int SQUARE_SIZE = 80;
        private int squaresWide = GameEngine.SCREEN_WIDTH / SQUARE_SIZE;
        private int squaresTall = GameEngine.SCREEN_HEIGHT / SQUARE_SIZE;

        ///TODO: make this more extendable
        private GridSquare[,] canvasGrid;
        private Player player;
        private int malice;                 //total malice generated
        private int maliceObjective;        //amount of malice needed to win level
        private List<WorldObject> objects;
        private List<Actor> actors;

        public Canvas()
        {
            canvasGrid = new GridSquare[squaresWide, squaresTall];
            objects = new List<WorldObject>();
            actors = new List<Actor>();

            for (int i = 0; i < squaresWide; i++)
            {
                for (int j = 0; j < squaresTall; j++)
                {
                    canvasGrid[i,j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), new Point(i, j));
                }
            }

            actors.Add(new Actor(ActorType.Type.Cat, new Point(6, 0)));
            actors.Add(new Actor(ActorType.Type.Kid, new Point(1, 8)));
            actors.Add(new Actor(ActorType.Type.Mom, new Point(6, 6)));

            objects.Add(new WorldObject(ObjectType.Type.SunnySpot, new Point(1, 1)));
            objects.Add(new WorldObject(ObjectType.Type.SunnySpot, new Point(5, 7)));
            objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(10, 2)));
            objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(9, 7)));
            objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(2, 3)));
            objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(9, 5)));
            objects.Add(new WorldObject(ObjectType.Type.House, new Point(8, 1)));
            objects.Add(new WorldObject(ObjectType.Type.House, new Point(9, 1)));
            objects.Add(new WorldObject(ObjectType.Type.House, new Point(2, 8)));
            objects.Add(new WorldObject(ObjectType.Type.House, new Point(9, 2)));
            objects.Add(new WorldObject(ObjectType.Type.House, new Point(3, 2)));
            objects.Add(new WorldObject(ObjectType.Type.House, new Point(4, 2)));
            objects.Add(new WorldObject(ObjectType.Type.House, new Point(3, 3)));
            objects.Add(new WorldObject(ObjectType.Type.House, new Point(4, 3)));
            objects.Add(new WorldObject(ObjectType.Type.House, new Point(2, 6)));
            objects.Add(new WorldObject(ObjectType.Type.House, new Point(3, 6)));
            objects.Add(new WorldObject(ObjectType.Type.House, new Point(4, 6)));
            objects.Add(new WorldObject(ObjectType.Type.House, new Point(8, 5)));
            objects.Add(new WorldObject(ObjectType.Type.House, new Point(8, 6)));
            objects.Add(new WorldObject(ObjectType.Type.House, new Point(9, 6)));
            objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(0, 5)));
            objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(1, 5)));
            objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(2, 5)));
            objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(3, 5)));
            objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(4, 5)));
            objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(5, 5)));
            objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(6, 5)));
            objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(6, 4)));
            objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(6, 3)));
            objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(7, 3)));
            objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(8, 3)));
            objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(9, 3)));
            objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(10, 3)));

            foreach (WorldObject entity in objects)
            {
                canvasGrid[entity.GridspacePosition.X, entity.GridspacePosition.Y].add(entity);
            }
            foreach (Actor actor in actors)
            {
                canvasGrid[actor.GridspacePosition.X, actor.GridspacePosition.Y].add(actor);
            }
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
        #endregion
    }   
}