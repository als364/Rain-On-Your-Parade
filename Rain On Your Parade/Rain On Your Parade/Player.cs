using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Rain_On_Your_Parade
{
    class Player : Model
    {
        private Vector2 velocity;       //velocity of player
        private int rain;       //amount of rain held by player

        public const int MAX_RAIN = 6;

        #region Getters and Setters

        /*public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }*/

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }

            set
            {
                velocity = value;
            }
        }

        public int Rain
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
            PixelPosition = new Vector2(GameEngine.SCREEN_WIDTH/2,GameEngine.SCREEN_HEIGHT/2);
            velocity = new Vector2();
            rain = MAX_RAIN;
        }

        public override void LoadContent(ContentManager content)
        {
            activatedSprite = content.Load<Texture2D>("cloud");
            spriteWidth = 80;
            spriteHeight = 80;
        }

        /// <summary>Public stuff about the method</summary>
        /// <param name="foo">It's an integer apparently</param>
        /// <devdoc>This method doesn't do anything yet.</devdoc>
        public void doSomethingPlayer(int foo) 
        {
            //do stuff here
        }

        public override string ToString()
        {
            return base.ToString() + "Velocity: " + velocity.ToString() + "\nRain: " + rain;
        }
    }
}
