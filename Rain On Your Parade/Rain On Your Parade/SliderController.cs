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
        int pixelOnButtonClicked;

        public SliderController (Slider slider) : base(slider)
        {
            this.slider = slider;
        }

        public override void Update(GameTime gameTime, WorldState worldState)
        {
            MouseState mouseState = Mouse.GetState();
            if (slider.buttonBounds.Contains(new Point(mouseState.X, mouseState.Y)))
            {
                if (mouseState.LeftButton.Equals(ButtonState.Pressed))
                {
                    if (!pressStarted)
                    {
                        pressStarted = true;
                        //Console.WriteLine("pressed");
                        pixelOnButtonClicked = mouseState.Y - slider.buttonBounds.Y;
                        //Console.WriteLine("pixel clicked: " + pixelOnButtonClicked);
                    }
                    else
                    {
                        //Console.WriteLine("MousePosition: " + mouseState.Y);
                        int newPosition = mouseState.Y - pixelOnButtonClicked;
                        if (newPosition <= 0)
                        {
                            slider.buttonPosition = 0;
                            //Console.WriteLine("<0");
                        }
                        else if (newPosition >= (slider.sliderBounds.Height - slider.buttonBounds.Height))
                        {
                            slider.buttonPosition = slider.sliderBounds.Height - slider.buttonBounds.Height;
                            //Console.WriteLine(">600");
                        }
                        else
                        {
                            slider.buttonPosition = newPosition;
                        }
                    }
                }
                else
                {
                    if (pressStarted)
                    {
                        pressStarted = false;
                        //Console.WriteLine("released");
                    }
                }
            }

            slider.sliderBounds = new Rectangle(slider.pos.X, slider.pos.Y, slider.spriteWidth, slider.spriteHeight);
            slider.buttonBounds = new Rectangle(slider.pos.X, 
                                                (int)Math.Max(Math.Min(slider.buttonPosition, slider.spriteHeight - slider.spriteWidth), 0), 
                                                slider.spriteWidth, slider.spriteWidth);
        }
    }
}
