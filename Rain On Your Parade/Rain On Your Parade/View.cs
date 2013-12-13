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
            AnimatedSprite activatedImage = viewedModel.activatedSprite;
            AnimatedSprite deactivatedImage = viewedModel.deactivatedSprite;

            if (viewedModel is Player)
            {
                Player current = (Player)viewedModel;

                if (current.isRaining)
                {

                    current.rainSprite.Draw(spriteBatch, viewedModel.PixelPosition,Color.White, false, 0f);
         
                }

                if (current.isAbsorbing)
                {
                    current.abSprite.Draw(spriteBatch, new Vector2 (viewedModel.PixelPosition.X, viewedModel.PixelPosition.Y - 80f), Color.Crimson , true, 0f);
                }

                activatedImage.Draw(spriteBatch, viewedModel.PixelPosition,
                    new Color(viewedModel.colorAlpha, viewedModel.colorAlpha, viewedModel.colorAlpha),false,0f);
           

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
                        activatedImage.Draw(spriteBatch, viewedModel.PixelPosition, Color.White,false,0f);
                        break;
                    case false:
                        deactivatedImage.Draw(spriteBatch, viewedModel.PixelPosition, Color.White,false,0f);
                        break;
                }
            }
            else
            {
                if (viewedModel is Actor)
                {
                    Actor actor = (Actor)viewedModel;

                    if (actor.Mood < 4)
                    {
                        if (actor.State.State == ActorState.AState.Run ||
                            actor.State.State == ActorState.AState.Nurture ||
                            actor.State.State == ActorState.AState.Rampage ||
                            actor.State.State == ActorState.AState.Fight ||
                            actor.State.State == ActorState.AState.Comfort)
                        {
                            deactivatedImage.Draw(spriteBatch, viewedModel.PixelPosition, Color.GhostWhite, actor.isFacingLeft(), actor.rot_level());
                        }
                        else if (actor.State.State == ActorState.AState.Sleep && actor.Type.TypeName == ActorType.Type.Cat)
                        {
                            actor.sleepSprite.Draw(spriteBatch, viewedModel.PixelPosition, Color.GhostWhite, false, 0f);
                        }
                        else activatedImage.Draw(spriteBatch, viewedModel.PixelPosition, Color.Azure, actor.isFacingLeft(), actor.rot_level());
                    }
                    else deactivatedImage.Draw(spriteBatch, viewedModel.PixelPosition, Color.Brown, actor.isFacingLeft(), actor.rot_level());

                    Texture2D[] moods = new Texture2D[6] {actor.mood1,actor.mood2,actor.mood3,actor.mood4,actor.mood5,actor.mood6};

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
                     spriteBatch.Draw(actor.moodFight, new Rectangle((int)viewedModel.PixelPosition.X + 18, (int)viewedModel.PixelPosition.Y - 25, 60, 60), Color.White);
                 if (actor.State.State == ActorState.AState.Comfort)
                     spriteBatch.Draw(actor.nurtureImg, new Rectangle((int)viewedModel.PixelPosition.X + 18, (int)viewedModel.PixelPosition.Y - 25, 60, 60), Color.White);

                 if (actor.rainCooldown > 0)
                 {
                     if (actor.Type.TypeName == ActorType.Type.Cat)

                         spriteBatch.Draw(actor.catWet, new Rectangle((int)viewedModel.PixelPosition.X, (int)viewedModel.PixelPosition.Y, 80, 80), Color.White);
                     else 
                         spriteBatch.Draw(actor.personWet, new Rectangle((int)viewedModel.PixelPosition.X, (int)viewedModel.PixelPosition.Y, 80, 80), Color.White);
                 }


                 spriteBatch.End();

                }
                else
                {
                    activatedImage.Draw(spriteBatch, viewedModel.PixelPosition, Color.Azure,false,0f);
                }
            }
        }
    }
}
