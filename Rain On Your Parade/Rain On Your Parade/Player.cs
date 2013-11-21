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
            Texture2D textureBase = content.Load<Texture2D>("white_cloud_base");
            AnimatedTexture aniTexBase = new AnimatedTexture(textureBase, 12, 1, false, false);

            List<AnimationSequence> aniSequences = new List<AnimationSequence>(2);
            //aniSequences.Add(new AnimationSequence(0, 11, true, true, 1, 0.1f, aniTexBase, null));
            aniSequences.Add(new AnimationSequence(11,5,true,false,1,0.1f,aniTexBase,null));
            aniSequences.Add(new AnimationSequence(4,0,true,false,1,0.1f,aniTexBase,null));
            activatedSprite = new AnimatedSprite(aniSequences);

            spriteWidth = 80;
            spriteHeight = 80;
        }

        public override string ToString()
        {
            return base.ToString() + "Velocity: " + velocity.ToString() + "\nRain: " + rain;
        }
    }
}
