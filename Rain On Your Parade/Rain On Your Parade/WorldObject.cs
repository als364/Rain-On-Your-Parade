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
            if (type.getObjectType() == ObjectType.TypeName.Pool)
            {
                type.PlayLevel = 3;
            }
            if (type.getObjectType() == ObjectType.TypeName.Garden)
            {
                type.PlayLevel = 3;
                type.SleepLevel = 3;
                type.NurtureLevel = 0;
            }
            if (type.getObjectType() == ObjectType.TypeName.Chalking)
            {
                type.PlayLevel = 3;
                type.RampageLevel = 0;
            }
            if (type.getObjectType() == ObjectType.TypeName.House)
            {
            }
            if (type.getObjectType() == ObjectType.TypeName.Laundry)
            {
                type.RampageLevel = 0;
                type.NurtureLevel = 0;
            }
            if (type.getObjectType() == ObjectType.TypeName.SunnySpot)
            {
                type.SleepLevel = 0;
                type.NurtureLevel = 3;
                type.PlayLevel = 3;
            }
        }

        public void deactivate()
        {
            activated = false;
            if (type.getObjectType() == ObjectType.TypeName.Pool)
            {
                type.PlayLevel = 0;
            }
            if (type.getObjectType() == ObjectType.TypeName.Garden)
            {
                type.PlayLevel = 0;
                type.SleepLevel = 0;
                type.NurtureLevel = 3;
            }
            if (type.getObjectType() == ObjectType.TypeName.Chalking)
            {
                type.PlayLevel = 0;
                type.RampageLevel = 3;
            }
            if (type.getObjectType() == ObjectType.TypeName.House)
            {
            }
            if (type.getObjectType() == ObjectType.TypeName.Laundry)
            {
                type.RampageLevel = 3;
                type.NurtureLevel = 3;
            }
            if (type.getObjectType() == ObjectType.TypeName.SunnySpot)
            {
                type.SleepLevel = 3;
                type.NurtureLevel = 0;
                type.PlayLevel = 0;
            }
            
        }
    }
}
