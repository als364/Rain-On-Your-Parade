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
                        spriteBatch.Draw(activatedImage, new Rectangle((int)viewedModel.Position.X, (int)viewedModel.Position.Y, viewedModel.spriteWidth, viewedModel.spriteHeight), Color.White);
                        break;
                    case false:
                        spriteBatch.Draw(deactivatedImage, new Rectangle((int)viewedModel.Position.X, (int)viewedModel.Position.Y, viewedModel.spriteWidth, viewedModel.spriteHeight), Color.White);
                        break;
                }
            }
            else
            {
                if (viewedModel is Actor)
                {
                    Actor actor = (Actor)viewedModel;
                    if (actor.Mood == 0)
                        spriteBatch.Draw(activatedImage, new Rectangle((int)viewedModel.Position.X, (int)viewedModel.Position.Y, viewedModel.spriteWidth, viewedModel.spriteHeight), Color.Azure);
                    else spriteBatch.Draw(activatedImage, new Rectangle((int)viewedModel.Position.X, (int)viewedModel.Position.Y, viewedModel.spriteWidth, viewedModel.spriteHeight), Color.Brown);
                }
                else
                    spriteBatch.Draw(activatedImage, new Rectangle((int)viewedModel.Position.X, (int)viewedModel.Position.Y, viewedModel.spriteWidth, viewedModel.spriteHeight), Color.Azure);
            }
        }
    }
}
