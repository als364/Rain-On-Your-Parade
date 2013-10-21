using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;

namespace Rain_On_Your_Parade
{
    public class WorldObject : Model
    {
        private ObjectType type;
        private bool activated;

        public WorldObject(ObjectType.Type oType, Point pos)
        {
            type = new ObjectType(oType);
            Position =  new Vector2(pos.X * Canvas.SQUARE_SIZE, pos.Y * Canvas.SQUARE_SIZE);
            activated = type.StartsActivated;
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(type.StringName());
            spriteWidth = 80;
            spriteHeight = 80;
        }

        public ObjectType Type
        {
            get { return type; }
            set { type = value; }
        }

        public bool Activated
        {
            get { return activated; }
            set { activated = value; }
        }

        public void activate()
        {
            activated = true;
            if (type.TypeName == ObjectType.Type.Pool)
            {
                type.PlayLevel = 3;
            }
            if (type.TypeName == ObjectType.Type.Garden)
            {
                type.PlayLevel = 3;
                type.SleepLevel = 3;
                type.NurtureLevel = 0;
            }
            if (type.TypeName == ObjectType.Type.Chalking)
            {
                type.PlayLevel = 3;
                type.RampageLevel = 0;
            }
            if (type.TypeName == ObjectType.Type.House)
            {
            }
            if (type.TypeName == ObjectType.Type.Laundry)
            {
                type.RampageLevel = 0;
                type.NurtureLevel = 0;
            }
            if (type.TypeName == ObjectType.Type.SunnySpot)
            {
                type.SleepLevel = 0;
                type.NurtureLevel = 3;
                type.PlayLevel = 3;
            }
        }

        public void deactivate()
        {
            activated = false;
            if (type.TypeName == ObjectType.Type.Pool)
            {
                type.PlayLevel = 0;
            }
            if (type.TypeName == ObjectType.Type.Garden)
            {
                type.PlayLevel = 0;
                type.SleepLevel = 0;
                type.NurtureLevel = 3;
            }
            if (type.TypeName == ObjectType.Type.Chalking)
            {
                type.PlayLevel = 0;
                type.RampageLevel = 3;
            }
            if (type.TypeName == ObjectType.Type.House)
            {
            }
            if (type.TypeName == ObjectType.Type.Laundry)
            {
                type.RampageLevel = 3;
                type.NurtureLevel = 3;
            }
            if (type.TypeName == ObjectType.Type.SunnySpot)
            {
                type.SleepLevel = 3;
                type.NurtureLevel = 0;
                type.PlayLevel = 0;
            }
            
        }

        public override string ToString()
        {
            return base.ToString() + type.ToString() + "\nActivated: " + activated;
        }
    }
}
