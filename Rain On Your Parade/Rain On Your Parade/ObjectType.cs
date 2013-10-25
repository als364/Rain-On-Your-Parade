using System;
using System.Collections;

namespace Rain_On_Your_Parade
{
    public class ObjectType
    {

        public enum Type {SunnySpot, Garden, Pool, Chalking, Laundry, House};
        Hashtable stringNames = new Hashtable();
        private Type typeName;
        private bool startsActivated;
        private int sleepLevel;
        private int playLevel;
        private int nurtureLevel;
        private int rampageLevel;
        private bool passable;


        public ObjectType(Type oTypeName)
        {
            typeName = oTypeName;
            switch (typeName)
            {
                case ObjectType.Type.SunnySpot:
                    startsActivated = true;
                    sleepLevel = 3;
                    playLevel = 0;
                    nurtureLevel = 0;
                    rampageLevel = 0;
                    passable = true;
                    stringNames.Add(Type.SunnySpot, "sunspot");
                    break;

                case ObjectType.Type.Garden:
                    startsActivated = true;
                    sleepLevel = 0;
                    playLevel = 0;
                    nurtureLevel = 3;
                    rampageLevel = 0;
                    passable = true;
                    stringNames.Add(Type.Garden, "garden");
                    break;

                case ObjectType.Type.Pool:
                    startsActivated = true;
                    sleepLevel = 0;
                    playLevel = 3;
                    nurtureLevel = 0;
                    rampageLevel = 0;
                    passable = true;
                    stringNames.Add(Type.Pool, "pool");
                    break;

                case ObjectType.Type.Chalking:
                    startsActivated = false;
                    sleepLevel = 0;
                    playLevel = 1;
                    nurtureLevel = 0;
                    rampageLevel = 0;
                    passable = true;
                    stringNames.Add(Type.Chalking, "sidewalk");
                    break;

                case ObjectType.Type.Laundry:
                    startsActivated = true;
                    sleepLevel = 0;
                    playLevel = 1;
                    nurtureLevel = 2;
                    rampageLevel = 3;
                    passable = true;
                    stringNames.Add(Type.Laundry, "laundry");
                    break;

                case ObjectType.Type.House:
                    startsActivated = false;
                    sleepLevel = 0;
                    playLevel = 0;
                    nurtureLevel = 0;
                    rampageLevel = 0;
                    passable = false;
                    stringNames.Add(Type.House, "house");
                    break;
            }
        }

        public ObjectType(Type oType, bool oActivated, int oSleep, int oPlay, int oNurture, int oRampage)
        {
            typeName = oType;
            startsActivated = oActivated;
            sleepLevel = oSleep;
            playLevel = oPlay;
            nurtureLevel = oNurture;
            rampageLevel = oRampage;

            stringNames.Add(Type.Chalking, "sidewalk");
            stringNames.Add(Type.SunnySpot, "sunspot");
            stringNames.Add(Type.Garden, "garden");
            stringNames.Add(Type.Laundry, "laundry");
            stringNames.Add(Type.Pool, "pool");
            stringNames.Add(Type.House, "house");
        }

        public Type TypeName
        {
            get { return typeName; }
            set {typeName = value; }
        }

        public bool StartsActivated
        {
            get { return startsActivated; }
            set { startsActivated = value; }
        }

        public int SleepLevel
        {
            get { return sleepLevel; }
            set { sleepLevel = value; }
        }
        public int PlayLevel
        {
            get { return playLevel; }
            set { playLevel = value; }
        }
        public int NurtureLevel
        {
            get { return nurtureLevel; }
            set { nurtureLevel = value; }
        }
        public int RampageLevel
        {
            get { return rampageLevel; }
            set { rampageLevel = value; }
        }

        public bool Passable
        {
            get { return passable; }
            set { passable = value; }
        }
        //public override string ToString()
        //{
        //    return (string)stringNames[typeName];
        //}
        public string StringName()
        {
            return (string)stringNames[typeName];
        }
        public override string ToString()
        {
            return (string)stringNames[typeName] + "\nSleep Level: " + sleepLevel + "\nPlay Level: " + playLevel + "\nNurture Level: " + nurtureLevel + "\nRampage Level: " + rampageLevel;
        }

    }
}
