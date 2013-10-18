using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace Rain_On_Your_Parade
{
    class WorldState
    {
        private int malice;                 //total malice generated
        private int maliceObjective;        //amount of malice needed to win level
        private GridSquare[,] stateOfWorld;  //state array of each space in level
        private Player player;
        public int worldWidth;              //# gridsquares wide
        public int worldHeight;             //# gridsquares tall

        
        #region Getters and Setters
        
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

        public GridSquare[,] StateOfWorld
        {
            get
            {
                return stateOfWorld;
            }

            set
            {
                stateOfWorld = value;
            }
        }

        #endregion

        /// <summary>WorldState Constructor</summary>
        /// <param="quota">an integer representing the level's malice quota</param>
        /// <param="levelmap">an array of GridSquares representing the level</param>
        /// <devdoc>
        /// Create a WorldState to keep track of the current state
        /// of all spaces in the level. Initialize the total
        /// amount of malice generated to 0 and set the maliceObjective
        /// to the specified quota.
        /// </devdoc>
        public WorldState(int quota, GridSquare[,] levelmap)
        {
            malice = 0;
            maliceObjective = quota;
            stateOfWorld = levelmap;
            worldWidth = stateOfWorld.GetLength(0);
            worldHeight = stateOfWorld.GetLength(1);
            player = new Player();
            initializeAdjacencyLists();
        }

        public List<Actor> getActors()
        {
            List<Actor> allActors = new List<Actor>();

            for (int x = 0; x < worldWidth; x++)
            {
                for (int y = 0; y < worldHeight; y++)
                {
                    GridSquare currentSquare = stateOfWorld[x, y];
                    allActors.AddRange(currentSquare.Actors);
           
                }
            }

            return allActors;
        }

        public List<WorldObject> getObjects()
        {
            List<WorldObject> allObjects = new List<WorldObject>();

            for (int x = 0; x < worldWidth; x++)
            {
                for (int y = 0; y < worldHeight; y++)
                {
                    GridSquare currentSquare = stateOfWorld[x,y];
                    allObjects.AddRange(currentSquare.Objects);
                  //  if (currentSquare.Objects.ToArray().Length > 0)
                  // Debug.WriteLine("Y Pos: " + stateOfWorld[x, y].Objects.ToArray()[0].Position.Y);
                
                }
            }

            return allObjects;
        }

        /// <summary>Public stuff about the method</summary>
        /// <param name="foo">It's an integer apparently</param>
        /// <devdoc>This method doesn't do anything yet.</devdoc>
        public void doSomethingWorldState(int foo) 
        {
            //do stuff here
        }

        /// <summary>Initialize adjacency lists in level map for ease of search</summary>
        /// <devdoc>
        /// Sets up adjacency lists for the entire levelmap. This will use pointers if at all possible later, but for now, I have an i7.
        /// </devdoc>
        private void initializeAdjacencyLists()
        {
            for (int x = 0; x < worldWidth; x++)
            {
                for (int y = 0; y < worldHeight; y++)
                {
                    GridSquare currentSquare = stateOfWorld[x,y];
                    if (x == 0)
                    {
                        if (y == 0) //top left
                        {
                            currentSquare.adjacent.Add(stateOfWorld[x + 1, y]);
                            currentSquare.adjacent.Add(stateOfWorld[x, y + 1]);
                        }
                        else if (y == worldHeight - 1) //bottom left
                        {
                            currentSquare.adjacent.Add(stateOfWorld[x + 1, y]);
                            currentSquare.adjacent.Add(stateOfWorld[x, y - 1]);
                        }
                        else //left edge
                        {
                            currentSquare.adjacent.Add(stateOfWorld[x + 1, y]);
                            currentSquare.adjacent.Add(stateOfWorld[x, y + 1]);
                            currentSquare.adjacent.Add(stateOfWorld[x, y - 1]);
                        }
                    }
                    else if (x == worldWidth - 1)
                    {
                        if (y == 0) //top right
                        {
                            currentSquare.adjacent.Add(stateOfWorld[x - 1, y]);
                            currentSquare.adjacent.Add(stateOfWorld[x, y + 1]);
                        }
                        else if (y == worldHeight - 1) //bottom right
                        {
                            currentSquare.adjacent.Add(stateOfWorld[x - 1, y]);
                            currentSquare.adjacent.Add(stateOfWorld[x, y - 1]);
                        }
                        else //right edge
                        {
                            currentSquare.adjacent.Add(stateOfWorld[x - 1, y]);
                            currentSquare.adjacent.Add(stateOfWorld[x, y + 1]);
                            currentSquare.adjacent.Add(stateOfWorld[x, y - 1]);
                        }
                    }
                    else
                    {
                        if (y == 0) //top edge
                        {
                            currentSquare.adjacent.Add(stateOfWorld[x + 1, y]);
                            currentSquare.adjacent.Add(stateOfWorld[x - 1, y]);
                            currentSquare.adjacent.Add(stateOfWorld[x, y + 1]);
                        }
                        else if (y == worldHeight - 1) //bottom edge
                        {
                            currentSquare.adjacent.Add(stateOfWorld[x + 1, y]);
                            currentSquare.adjacent.Add(stateOfWorld[x - 1, y]);
                            currentSquare.adjacent.Add(stateOfWorld[x, y - 1]);
                        }
                        else //finally not a corner case
                        {
                            currentSquare.adjacent.Add(stateOfWorld[x + 1, y]);
                            currentSquare.adjacent.Add(stateOfWorld[x - 1, y]);
                            currentSquare.adjacent.Add(stateOfWorld[x, y + 1]);
                            currentSquare.adjacent.Add(stateOfWorld[x, y - 1]);
                        }
                    }
                }
            }
        }
    }
}
