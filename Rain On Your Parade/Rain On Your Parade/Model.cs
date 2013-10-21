using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Rain_On_Your_Parade
{
    abstract public class Model
    {
        public int spriteWidth, spriteHeight;
        public string spriteFilePath = "Content/default";
        public Vector2 Position;
        public Texture2D sprite;

        public Model(){}

        public Model(string filepath)
        {
            spriteFilePath = filepath;
        }

        public override string ToString()
        {
            return "Position: " + Position.ToString() + "\n";
        }

        abstract public void LoadContent(ContentManager content);
    }
}
