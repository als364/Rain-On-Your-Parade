using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rain_On_Your_Parade
{
    class WorldState
    {
        private int malice;                 //total malice generated
        private int maliceObjective;        //amount of malice needed to win level
        private GridSquare[] stateOfWorld;  //state array of each space in level

        
        #region Getters and Setters
        
        private int Malice
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

        private int MaliceObjective
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

        private GridSquare[] StateOfWorld
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
        public WorldState(int quota, GridSquare[] levelmap)
        {
            malice = 0;
            maliceObjective = quota;
            stateOfWorld = levelmap;
        }


        /// <summary>Public stuff about the method</summary>
        /// <param name="foo">It's an integer apparently</param>
        /// <devdoc>This method doesn't do anything yet.</devdoc>
        public void doSomethingWorldState(int foo) 
        {
            //do stuff here
        }
    }
}
