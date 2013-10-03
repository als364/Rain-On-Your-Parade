using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Rain_On_Your_Parade
{
    class SliderView : View
    {
        Slider slider;
        Texture2D sliderBackgroundTexture;
        Texture2D sliderButtonTexture;

        public SliderView(Slider slider) : base(slider)
        {
            this.slider = slider;
        }

        public void draw(SpriteBatch spriteBatch)
        {
        }
    }
}
