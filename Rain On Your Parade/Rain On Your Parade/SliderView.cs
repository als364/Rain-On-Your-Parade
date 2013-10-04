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
            spriteBatch.Draw(slider.sliderSprite, slider.sliderSprite.Bounds, Color.White);
            spriteBatch.Draw(slider.buttonSprite, slider.buttonSprite.Bounds, Color.White);
        }
    }
}
