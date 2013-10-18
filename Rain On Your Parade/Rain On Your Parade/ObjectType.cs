using System;
using System.Collections;

namespace Rain_On_Your_Parade
{
    public class ObjectType
    {

        public enum TypeName {SunnySpot, Garden, Pool, Chalking, Laundry, Rainbow, House};
        Hashtable stringNames = new Hashtable();
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

            stringNames.Add(TypeName.Chalking, "sidewalk");
            stringNames.Add(TypeName.SunnySpot, "sunspot");
            stringNames.Add(TypeName.Garden, "garden");
            stringNames.Add(TypeName.Laundry, "laundry");
            stringNames.Add(TypeName.Pool, "pool");
            stringNames.Add(TypeName.Rainbow, "sidewalk");
            stringNames.Add(TypeName.House, "house");
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
        public override string ToString()
        {
            return (string)stringNames[objectType];
        }

    }
}
