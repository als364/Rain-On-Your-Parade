using System;
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
            Texture2D activatedImage = viewedModel.activatedSprite;
            Texture2D deactivatedImage = viewedModel.deactivatedSprite;

            if (viewedModel is WorldObject)
            {
                WorldObject current = (WorldObject)viewedModel;
                switch (current.Activated)
                {
                    case true:
                        spriteBatch.Draw(activatedImage, new Rectangle((int)viewedModel.PixelPosition.X, 
                                                                       (int)viewedModel.PixelPosition.Y, 
                                                                       viewedModel.spriteWidth, viewedModel.spriteHeight), Color.White);
                        break;
                    case false:
                        spriteBatch.Draw(deactivatedImage, new Rectangle((int)viewedModel.PixelPosition.X, 
                                                                         (int)viewedModel.PixelPosition.Y, 
                                                                         viewedModel.spriteWidth, viewedModel.spriteHeight), Color.White);
                        break;
                }
            }
            else
            {
                if (viewedModel is Actor)
                {
                    Actor actor = (Actor)viewedModel;
                    if (actor.Mood < 4)
                        spriteBatch.Draw(activatedImage, new Rectangle((int)viewedModel.PixelPosition.X,
                                                                       (int)viewedModel.PixelPosition.Y,
                                                                       viewedModel.spriteWidth, viewedModel.spriteHeight), Color.Azure);
                    else spriteBatch.Draw(activatedImage, new Rectangle((int)viewedModel.PixelPosition.X,
                                                                        (int)viewedModel.PixelPosition.Y,
                                                                        viewedModel.spriteWidth, viewedModel.spriteHeight), Color.Brown);

                    Texture2D[] moods = new Texture2D[6] {actor.mood1,actor.mood2,actor.mood3,actor.mood4,actor.mood5,actor.mood6};
                    Console.WriteLine(actor.Mood);

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
                }
                else
                {
                    spriteBatch.Draw(activatedImage, new Rectangle((int)viewedModel.PixelPosition.X, 
                                                                   (int)viewedModel.PixelPosition.Y, 
                                                                   viewedModel.spriteWidth, viewedModel.spriteHeight), Color.Azure);
                }
            }
        }
    }
}
