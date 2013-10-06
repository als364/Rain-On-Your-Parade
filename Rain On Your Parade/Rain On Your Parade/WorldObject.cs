using System;
using Microsoft.Xna.Framework.Content;

namespace Rain_On_Your_Parade
{
    public class WorldObject : Model
    {
        public ObjectType type;
        private bool activated;

        public WorldObject(ObjectType oType)
        {
            type = oType;
            activated = oType.getStartsActivated();
        }

        public override void LoadContent(ContentManager content)
        {
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
