using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;

namespace Rain_On_Your_Parade
{
    public class WorldObject : Model
    {
        public ObjectType type;
        private bool activated;

        public WorldObject(ObjectType oType, Vector2 pos)
        {
            type = oType;
            Position = pos;
            activated = oType.getStartsActivated();
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(type.ToString());
            spriteWidth = 80;
            spriteHeight = 80;
        }

        public bool getActivated()
        {
            return activated;
        }

        public void activate()
        {
            activated = true;
        }

        public void deactivate()
        {
            activated = false;
        }
    }
}
