using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Rain_On_Your_Parade
{
    abstract public class Model
    {
        public int spriteWidth, spriteHeight;
        public string spriteFilePath = "Content/default";
        public Vector2 prevPos;
        private Vector2 pixelPosition;
        private Point gridspacePosition;
        public Texture2D activatedSprite;
        public Texture2D deactivatedSprite;

        public Model(){}

        public Model(string filepath)
        {
            spriteFilePath = filepath;
        }

        #region Getters and Setters
        public Vector2 PixelPosition
        {
            get
            {
                return pixelPosition;
            }
            set
            {
                pixelPosition = value;
            }
        }

        public Point GridspacePosition
        {
            get
            {
                gridspacePosition = new Point((int)(PixelPosition.X / Canvas.SQUARE_SIZE), (int)(PixelPosition.Y / Canvas.SQUARE_SIZE));
                return gridspacePosition;
            }
            set
            {
                gridspacePosition = value;
            }
        }
        #endregion

        public override string ToString()
        {
            return "Position: " + pixelPosition.ToString() + "\n";
        }

        abstract public void LoadContent(ContentManager content);
    }
}
