using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Rain_On_Your_Parade
{
    class SliderController : Controller
    {
        Slider slider;
        bool pressStarted = false;
        int oldY = 0;

        public SliderController (Slider slider) : base(slider)
        {
            this.slider = slider;
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (slider.buttonBounds.Contains(new Point(mouseState.X, mouseState.Y)))
            {
                if (mouseState.LeftButton.Equals(ButtonState.Pressed))
                {
                    if (!pressStarted)
                    {
                        pressStarted = true;
                        oldY = mouseState.Y;
                    }
                    else
                    {
                        int dY = mouseState.Y - oldY;
                        int newPosition = slider.buttonPosition += dY;
                        if (newPosition <= 0)
                        {
                            slider.buttonPosition = 0;
                            Console.WriteLine("<0");
                        }
                        else if (newPosition >= (slider.sliderBounds.Height - slider.buttonBounds.Height))
                        {
                            slider.buttonPosition = slider.sliderBounds.Height - slider.buttonBounds.Height;
                            Console.WriteLine(">600");
                        }
                        else
                        {
                            slider.buttonPosition += dY;
                            Console.WriteLine("ping");
                        }
                    }
                }
                else
                {
                    pressStarted = false;
                }
            }

            slider.sliderBounds = new Rectangle(slider.spriteOriginX, slider.spriteOriginY, slider.spriteWidth, slider.spriteHeight);
            slider.buttonBounds = new Rectangle(slider.spriteOriginX, 
                                                (int)Math.Max(Math.Min(slider.buttonPosition, slider.spriteHeight - slider.spriteWidth), 0), 
                                                slider.spriteWidth, slider.spriteWidth);
        }
    }
}
