using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Rain_On_Your_Parade
{
    public class AnimatedTexture
    {
        #region fields
        private Texture2D texture; 		// spritesheet for this animation
        private int columns; 		    // number of frames horizontally in texture
        private int rows; 		        // number of frames vertically in texture
        private bool flipX;
        private bool flipY;
        #endregion

        /// <summary>AnimatedTexture Constructor</summary>
        /// <param name="spriteSheet">sprite sheet for an animation</param>
        /// <param name="numCols">number of frames vertically in the sprite sheet</param>
        /// <param name="numRows">number of frames horizontally in the sprite sheet</param>
        /// <param name="fX">flip the texture horizontally</param>
        /// <param name="fY">flip the texture vertically</param>
        /// <devdoc>
        /// Used by AnimatedSprite to process complex animations while
        /// keeping sprite sheets reasonably sized and memory efficient
        /// </devdoc>
        
        public AnimatedTexture(Texture2D spriteSheet, int numColumns, int numRows, bool fX, bool fY)
        {
            texture = spriteSheet;
            columns = numColumns;
            rows = numRows;
            flipX = fX;
            flipY = fY;
        }

        #region Getters and Setters
        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture= value;
            }
        }

        public int Columns
        {
            get
            {
                return columns;
            }
            set
            {
                columns = value;
            }
        }

        public int Rows
        {
            get
            {
                return rows;
            }
            set
            {
                rows = value;
            }
        }
        #endregion
    }
}
