using System;

namespace Rain_On_Your_Parade
{
    public class ObjectType
    {

        public enum TypeName {SunnySpot, Garden, Pool, Chalking, Laundry, Rainbow};

        private TypeName objectType;
        private bool startsActivated;
        private int sleepLevel;
        private int playLevel;
        private int nurtureLevel;
        private int rampageLevel;

        public ObjectType(TypeName oType, bool oActivated, int oSleep, int oPlay, int oNurture, int oRampage)
        {
            objectType = oType;
            startsActivated = oActivated;
            sleepLevel = oSleep;
            playLevel = oPlay;
            nurtureLevel = oNurture;
            rampageLevel = oRampage;
        }

        public TypeName getObjectType()
        {
            return objectType;
        }

        public bool getStartsActivated()
        {
            return startsActivated;
        }

        public int getSleepLevel()
        {
            return sleepLevel;
        }

        public int getPlayLevel()
        {
            return playLevel;
        }

        public int getNurtureLevel()
        {
            return nurtureLevel;
        }

        public int getRampageLevel()
        {
            return rampageLevel;
        }
    }
}
