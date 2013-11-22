﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Rain_On_Your_Parade
{
    class View
    {
        protected Model viewedModel;
        public Model ViewedModel { get { return viewedModel; } }

        public View(Model model)
        {
            viewedModel = model;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            AnimatedSprite activatedImage = viewedModel.activatedSprite;
            AnimatedSprite deactivatedImage = viewedModel.deactivatedSprite;

            if (viewedModel is Player)
            {
                Player current = (Player)viewedModel;

                if (current.isRaining)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(current.waterImg, new Rectangle((int)viewedModel.PixelPosition.X + 37, (int)viewedModel.PixelPosition.Y + 61, 26, 27), Color.White);
                    spriteBatch.Draw(current.waterImg, new Rectangle((int)viewedModel.PixelPosition.X + 10, (int)viewedModel.PixelPosition.Y + 67, 20, 20), Color.White);
                    spriteBatch.Draw(current.waterImg, new Rectangle((int)viewedModel.PixelPosition.X + 18, (int)viewedModel.PixelPosition.Y + 66, 20, 25), Color.White);
                    spriteBatch.Draw(current.waterImg, new Rectangle((int)viewedModel.PixelPosition.X + 25, (int)viewedModel.PixelPosition.Y + 68, 23, 20), Color.White);
                    spriteBatch.Draw(current.waterImg, new Rectangle((int)viewedModel.PixelPosition.X + 38, (int)viewedModel.PixelPosition.Y + 72, 20, 20), Color.White);
                    spriteBatch.Draw(current.waterImg, new Rectangle((int)viewedModel.PixelPosition.X + 34, (int)viewedModel.PixelPosition.Y + 70, 20, 27), Color.White);
                    spriteBatch.Draw(current.waterImg, new Rectangle((int)viewedModel.PixelPosition.X + 45, (int)viewedModel.PixelPosition.Y + 68, 23, 20), Color.White);
                    spriteBatch.Draw(current.waterImg, new Rectangle((int)viewedModel.PixelPosition.X + 51, (int)viewedModel.PixelPosition.Y + 67, 22, 29), Color.White);

                    spriteBatch.End();
                }

                activatedImage.Draw(spriteBatch, viewedModel.PixelPosition,
                    new Color(viewedModel.colorAlpha, viewedModel.colorAlpha, viewedModel.colorAlpha));
           

            }

            else if (viewedModel is WorldObject)
            {
                WorldObject current = (WorldObject)viewedModel;
                switch (current.Activated)
                {
                    case true:
                        //if (current.Type.TypeName == ObjectType.Type.SunnyRainbowSpot)
                        //{
                        //    float alpha = ((int)level.rainbows[current] / (float)GameEngine.MAX_RAINBOW_TIME);
                        //    activatedImage.Draw(spriteBatch, viewedModel.PixelPosition, new Color(alpha, alpha, alpha, alpha));
                        //}
                        activatedImage.Draw(spriteBatch, viewedModel.PixelPosition, Color.White);
                        break;
                    case false:
                        deactivatedImage.Draw(spriteBatch, viewedModel.PixelPosition, Color.White);
                        break;
                }
            }
            else
            {
                if (viewedModel is Actor)
                {
                    Actor actor = (Actor)viewedModel;
                    if (actor.Mood < 4)
                        activatedImage.Draw(spriteBatch, viewedModel.PixelPosition, Color.Azure);
                    else activatedImage.Draw(spriteBatch, viewedModel.PixelPosition, Color.Brown);

                    Texture2D[] moods = new Texture2D[6] {actor.mood1,actor.mood2,actor.mood3,actor.mood4,actor.mood5,actor.mood6};
                    //Console.WriteLine(actor.Mood);

                    spriteBatch.Begin();

                    spriteBatch.Draw(moods[actor.Mood], new Rectangle((int)viewedModel.PixelPosition.X - 18,
                                                                        (int)viewedModel.PixelPosition.Y-25, 40, 40), Color.White);

                 if (actor.State.State == ActorState.AState.Nurture)
                     spriteBatch.Draw(actor.nurtureImg, new Rectangle((int)viewedModel.PixelPosition.X + 18, (int)viewedModel.PixelPosition.Y-25, 40, 40), Color.White);
                 if (actor.State.State == ActorState.AState.Play)
                     spriteBatch.Draw(actor.playImg, new Rectangle((int)viewedModel.PixelPosition.X + 18, (int)viewedModel.PixelPosition.Y-25, 40, 40), Color.White);
                 if (actor.State.State == ActorState.AState.Sleep)
                     spriteBatch.Draw(actor.sleepImg, new Rectangle((int)viewedModel.PixelPosition.X + 18, (int)viewedModel.PixelPosition.Y-25, 40, 40), Color.White);
                 if (actor.State.State == ActorState.AState.Fight)
                     spriteBatch.Draw(actor.mood6, new Rectangle((int)viewedModel.PixelPosition.X + 18, (int)viewedModel.PixelPosition.Y - 25, 60, 60), Color.White);
                 if (actor.State.State == ActorState.AState.Comfort)
                     spriteBatch.Draw(actor.nurtureImg, new Rectangle((int)viewedModel.PixelPosition.X + 18, (int)viewedModel.PixelPosition.Y - 25, 60, 60), Color.White);

                 spriteBatch.End();

                }
                else
                {
                    activatedImage.Draw(spriteBatch, viewedModel.PixelPosition, Color.Azure);
                }
            }
        }
    }
}
