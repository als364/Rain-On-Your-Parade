using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rain_On_Your_Parade
{
    class Player
    {
        private Point pos;      //position of player (x, y)
        private double v;       //velocity of player
        private int rain;       //amount of rain held by player


        #region Getters and Setters

        private Point Pos
        {
            get
            {
                return pos;
            }

            set
            {
                pos = value;
            }
        }

        private double Velocity
        {
            get
            {
                return v;
            }

            set
            {
                v = value;
            }
        }

        private int Rain
        {
            get
            {
                return rain;
            }

            set
            {
                rain = value;
            }
        }

        #endregion


        /// <summary>Player Constructor</summary>
        /// <devdoc>
        /// Create a player with position pos = center of screen,
        /// velocity v = 0.0, and rain = full capacity
        /// </devdoc>
        public Player()
        {
            pos = new Point(250,250);   //TODO:change to be relative to screensize
            v = 0.0;
            rain = 6;                   //TODO:change this to a constant MAX_RAIN
        }


        /// <summary>Public stuff about the method</summary>
        /// <param name="foo">It's an integer apparently</param>
        /// <devdoc>This method doesn't do anything yet.</devdoc>
        public void doSomethingPlayer(int foo) 
        {
            //do stuff here
        }
    }
}
