using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rain_On_Your_Parade
{
    class SliderView : View
    {
        Slider slider;

        public SliderView(Slider slider) : base(slider)
        {
            this.slider = slider;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Console.WriteLine("ping");
            spriteBatch.Draw(slider.sliderSprite, new Rectangle((int)slider.PixelPosition.X, 
                                                                (int)slider.PixelPosition.Y, 
                                                                slider.spriteWidth, slider.spriteHeight), Color.White);
            spriteBatch.Draw(slider.buttonSprite, new Rectangle((int)slider.PixelPosition.Y, 
                                                                (int)Math.Max(Math.Min(slider.buttonPosition, slider.spriteHeight - slider.spriteWidth), 0), 
                                                                slider.spriteWidth, slider.spriteWidth), Color.White);
        }
    }
}
