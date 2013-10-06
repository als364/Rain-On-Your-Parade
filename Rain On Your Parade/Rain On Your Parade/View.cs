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
            Texture2D image = viewedModel.sprite;
            Debug.WriteLine(image);

            spriteBatch.Draw(image, new Rectangle(viewedModel.pos.X, viewedModel.pos.Y, viewedModel.spriteWidth, viewedModel.spriteHeight), Color.Azure);
        }
    }
}
