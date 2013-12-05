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
        public bool isRaining = false;
        public Texture2D waterImg;

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
            colorAlpha = (1f - ((float)rain / (float)MAX_RAIN));
        }

        public override void LoadContent(ContentManager content)
        {
            waterImg = content.Load<Texture2D>("water");

            Texture2D textureBase = content.Load<Texture2D>("white_cloud_base");
            AnimatedTexture aniTexBase = new AnimatedTexture(textureBase, 12, 1, false, false);

            Texture2D textureFace = content.Load<Texture2D>("white_cloud_face");
            AnimatedTexture aniTexFace = new AnimatedTexture(textureFace, 12, 1, false, false);

            Texture2D textureShadow = content.Load<Texture2D>("CloudShadow_padded");
            AnimatedTexture aniTexShadow = new AnimatedTexture(textureShadow, 6, 1, false, false);

            List<AnimationSequence> aniSequences = new List<AnimationSequence>(2);
            List<AnimationSequence> overlay1 = new List<AnimationSequence>();
            List<AnimationSequence> overlay2 = new List<AnimationSequence>();

            AnimationSequence cloud_1 = new AnimationSequence(6, 11, true, 1, 0.1f, aniTexBase, null);
            AnimationSequence cloud_2 = new AnimationSequence(5, 0, true, 1, 0.1f, aniTexBase, null);

            AnimationSequence cloud_face1 = new AnimationSequence(6, 11, true, 1, 0.2f, aniTexFace, null);
            AnimationSequence cloud_face2 = new AnimationSequence(5, 0, true, 1, 0.2f, aniTexFace, null);

            //Draw shadow first, then cloud, then face
            overlay1.Add(cloud_1);
            overlay1.Add(cloud_face1);
            overlay2.Add(cloud_2);
            overlay2.Add(cloud_face2);
            
            AnimationSequence cloud_shadow1 = new AnimationSequence(5, 0, true, 1, 0.1f, aniTexShadow, overlay1);
            AnimationSequence cloud_shadow2 = new AnimationSequence(5, 0, true, 1, 0.1f, aniTexShadow, overlay2);

            aniSequences.Add(cloud_shadow1);
            aniSequences.Add(cloud_shadow2);
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
