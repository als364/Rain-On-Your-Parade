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

        private GridSquare[,] canvasGrid;
        private Player player;
        private int malice;                 //total malice generated
        private int maliceObjective;        //amount of malice needed to win level
        private List<WorldObject> objects;
        private List<Actor> actors;

        public Canvas()
        {
            Grid = new GridSquare[squaresWide, squaresTall];
            objects = new List<WorldObject>();
            actors = new List<Actor>();

            for (int i = 0; i < squaresWide; i++)
            {
                for (int j = 0; j < squaresTall; j++)
                {
                    Grid[i,j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), new Point(i, j));
                }
            }

            /*
            //Level One
            #region levelone
            actors.Add(new Actor(ActorType.Type.Cat, new Point(10, 6)));
            actors.Add(new Actor(ActorType.Type.Cat, new Point(0, 6)));
            actors.Add(new Actor(ActorType.Type.Cat, new Point(5, 4)));

            objects.Add(new WorldObject(ObjectType.Type.SunnySpot, new Point(8, 1), 0));
            objects.Add(new WorldObject(ObjectType.Type.SunnySpot, new Point(2, 3), 0));
            objects.Add(new WorldObject(ObjectType.Type.SunnySpot, new Point(2, 4), 0));
            objects.Add(new WorldObject(ObjectType.Type.SunnySpot, new Point(8, 6), 0));

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
            #endregion levelone
            
            //Level 2
            #region leveltwo
            actors.Add(new Actor(ActorType.Type.Kid, new Point(10, 6)));
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
            objects.Add(new WorldObject(ObjectType.Type.House, new Point(10, 1), 0));

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
            objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(10, 2), 0));

            objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(1, 5), 3));
            objects.Add(new WorldObject(ObjectType.Type.Pool, new Point(9, 5), 3));

            objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(1, 3), 2));
            objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(5, 7), 2));
            objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(9, 3), 2));
            #endregion leveltwo
            */

            //Level 3
            #region levelthree
            actors.Add(new Actor(ActorType.Type.Cat, new Point(6, 0)));
            actors.Add(new Actor(ActorType.Type.Kid, new Point(1, 8)));
            actors.Add(new Actor(ActorType.Type.Mom, new Point(6, 6)));
            objects.Add(new WorldObject(ObjectType.Type.SunnySpot, new Point(1, 1), 0));
            objects.Add(new WorldObject(ObjectType.Type.SunnySpot, new Point(5, 7), 0));
            objects.Add(new WorldObject(ObjectType.Type.Garden, new Point(10, 2), 1));
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
            objects.Add(new WorldObject(ObjectType.Type.Chalking, new Point(10, 3), 0));
            #endregion levelthree
           

            player = new Player();

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