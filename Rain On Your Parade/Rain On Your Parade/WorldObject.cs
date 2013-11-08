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
        private int waterLevel;

        public WorldObject(ObjectType.Type oType, Point pos, int waterAmt)
        {
            type = new ObjectType(oType);
            PixelPosition =  new Vector2(pos.X * Canvas.SQUARE_SIZE, pos.Y * Canvas.SQUARE_SIZE);
            GridspacePosition = pos;
            activated = (type.StartsWet && type.IsWetObject) || !type.IsWetObject;
            waterLevel = waterAmt;
        }

        public override void LoadContent(ContentManager content)
        {
            activatedSprite = content.Load<Texture2D>(type.activatedStringName());
            deactivatedSprite = content.Load<Texture2D>(type.deactivatedStringName());

            spriteWidth = 80;
            spriteHeight = 80;
        }

        #region Getters and Setters
        public ObjectType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public bool Activated
        {
            get
            {
                return activated;
            }
            set
            {
                activated = value;
            }
        }

        public int WaterLevel
        {
            get
            {
                return waterLevel;
            }
            set
            {
                waterLevel = value;
            }
        }
        #endregion

        public void activate()
        {
            ObjectType.Type typeSwitch = type.TypeName;
            if (!activated)
            {
                activated = true;
                switch (typeSwitch)
                {
                    case ObjectType.Type.Pool:
                        type.PlayLevel = 3;
                        break;
                    case ObjectType.Type.Garden:
                        type.PlayLevel = 0;
                        type.SleepLevel = 0;
                        type.NurtureLevel = 3;
                        break;
                    case ObjectType.Type.Chalking:
                        type.PlayLevel = 0;
                        type.RampageLevel = 0;
                        break;
                    case ObjectType.Type.House:
                        break;
                    case ObjectType.Type.Laundry:
                        type.RampageLevel = 0;
                        type.NurtureLevel = 3;
                        break;
                    case ObjectType.Type.SunnySpot:
                        type.SleepLevel = 3;
                        type.NurtureLevel = 0;
                        type.PlayLevel = 0;
                        break;
                    case ObjectType.Type.Rainbow:
                        type.SleepLevel = 0;
                        type.NurtureLevel = 10;
                        type.PlayLevel = 10;
                        break;
                }
            }
        }


        public void deactivate()
        {
            ObjectType.Type typeSwitch = type.TypeName;
            if (activated)
            {
                switch (typeSwitch)
                {
                    case ObjectType.Type.Pool:
                        type.PlayLevel = -3;
                        type.RampageLevel = 0;
                        activated = false;
                        break;
                    case ObjectType.Type.Garden:
                        type.PlayLevel = 0;
                        type.SleepLevel = 0;
                        type.NurtureLevel = -2;
                        activated = false;
                        break;
                    case ObjectType.Type.Chalking:
                        type.PlayLevel = 0;
                        type.RampageLevel = 0;
                        activated = false;
                        break;
                    case ObjectType.Type.House:
                        break;
                    case ObjectType.Type.Laundry:
                        type.RampageLevel = 0;
                        type.NurtureLevel = -2;
                        activated = false;
                        break;
                    case ObjectType.Type.SunnySpot:
                        type.SleepLevel = 0;
                        type.NurtureLevel = 0;
                        type.PlayLevel = 0;
                        activated = false;
                        break;
                    case ObjectType.Type.Rainbow:
                        type.SleepLevel = 0;
                        type.NurtureLevel = 0;
                        type.PlayLevel = 0;
                        activated = false;
                        break;
                }
            }
        }

        public override string ToString()
        {
            return base.ToString() + type.ToString() + "\nActivated: " + activated;
        }
    }
}
