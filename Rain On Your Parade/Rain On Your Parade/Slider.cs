using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Rain_On_Your_Parade
{
    class Slider : Model
    {
        public Texture2D sliderSprite;
        public Rectangle sliderBounds;
        public Texture2D buttonSprite;
        public Rectangle buttonBounds;

        public string sliderSpriteFilePath = "SliderBackground";
        public string buttonSpriteFilePath = "SliderButton";
        public int buttonPosition;
        //public Color buttonColor = Color.Gray;

        public Slider() : base(){}

        public Slider(int originX, int originY, int width, int height)
        {
            pos.X = originX;
            pos.Y = originY;
            spriteWidth = width;
            spriteHeight = height;
            buttonPosition = height/2;
        }

        public override void LoadContent(ContentManager content)
        {
            sliderSprite = content.Load<Texture2D>(sliderSpriteFilePath);
            buttonSprite = content.Load<Texture2D>(buttonSpriteFilePath);
        }
    }
}
