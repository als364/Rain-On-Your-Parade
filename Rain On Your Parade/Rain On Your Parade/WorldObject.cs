using System;

namespace Rain_On_Your_Parade
{
    public class WorldObject : Model
    {
        private ObjectType type;
        private bool activated;

        public WorldObject(ObjectType oType)
        {
            type = oType;
            activated = oType.getStartsActivated();
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
