﻿using System;
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
        public Texture2D buttonSprite;
        public string sliderSpriteFilePath = "SliderBackground";
        public string buttonSpriteFilePath = "SliderButton";

        public Slider() : base(){}

        public override void LoadContent(ContentManager content)
        {
            sliderSprite = content.Load<Texture2D>(sliderSpriteFilePath);
            buttonSprite = content.Load<Texture2D>(buttonSpriteFilePath);
        }
    }
}
