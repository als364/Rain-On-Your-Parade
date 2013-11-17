using System;
using System.Collections;

namespace Rain_On_Your_Parade
{
    public class ObjectType
    {

        public enum Type {SunnySpot, Rainbow, Garden, Pool, Chalking, Laundry, House};
        Hashtable activatedImages = new Hashtable();
        Hashtable deactivatedImages = new Hashtable();
        private Type typeName;
        private bool canActivate;   //actor can activate this object
        private bool isWetObject;   //object deactivates if absorbed upon
        private bool startsWet;
        private int sleepLevel;
        private int playLevel;
        private int nurtureLevel;
        private int rampageLevel;
        private bool passable;
        private bool holdsWater;


        public ObjectType(Type oTypeName)
        {
            typeName = oTypeName;
            switch (typeName)
            {
                case ObjectType.Type.SunnySpot:
                    isWetObject = false;
                    startsWet = false;
                    canActivate = false;
                    holdsWater = false;
                    sleepLevel = 3;
                    playLevel = 0;
                    nurtureLevel = 0;
                    rampageLevel = 0;
                    passable = true;
                    activatedImages.Add(Type.SunnySpot, "sunspot");
                    deactivatedImages.Add(Type.SunnySpot, "rainbow");
                    break;

                case ObjectType.Type.Rainbow:
                    isWetObject = true;
                    startsWet = true;
                    canActivate = false;
                    holdsWater = false;
                    sleepLevel = 0;
                    playLevel = 10;
                    nurtureLevel = 10;
                    rampageLevel = 3;
                    passable = true;
                    activatedImages.Add(Type.Rainbow, "rainbow");
                    deactivatedImages.Add(Type.Rainbow, "sunspot");
                    break;

                case ObjectType.Type.Garden:
                    isWetObject = true;
                    startsWet = true;
                    canActivate = true;
                    holdsWater = true;
                    sleepLevel = 0;
                    playLevel = 0;
                    nurtureLevel = 3;
                    rampageLevel = 0;
                    passable = true;
                    activatedImages.Add(Type.Garden, "garden2");
                    deactivatedImages.Add(Type.Garden, "drygarden");
                    break;

                case ObjectType.Type.Pool:
                    isWetObject = true;
                    canActivate = false;
                    startsWet = true;
                    holdsWater = true;
                    sleepLevel = 0;
                    playLevel = 3;
                    nurtureLevel = 0;
                    rampageLevel = 0;
                    passable = true;
                    activatedImages.Add(Type.Pool, "pool");
                    deactivatedImages.Add(Type.Pool, "poolempty");
                    break;

                case ObjectType.Type.Chalking:
                    isWetObject = false;
                    canActivate = true;
                    startsWet = true;
                    holdsWater = false;
                    sleepLevel = 0;
                    playLevel = 3;
                    nurtureLevel = 0;
                    rampageLevel = 0;
                    passable = true;
                    activatedImages.Add(Type.Chalking, "chalksidewalk");
                    deactivatedImages.Add(Type.Chalking, "sidewalk");
                    break;

                case ObjectType.Type.Laundry:
                    isWetObject = true;
                    canActivate = true;
                    startsWet = false;
                    holdsWater = true;
                    sleepLevel = 0;
                    playLevel = 0;
                    nurtureLevel = 0;
                    rampageLevel = 0;
                    passable = true;
                    activatedImages.Add(Type.Laundry, "laundry");
                    deactivatedImages.Add(Type.Laundry, "wetlaundry");
                    break;

                case ObjectType.Type.House:
                    isWetObject = false;
                    canActivate = false;
                    startsWet = false;
                    holdsWater = false;
                    sleepLevel = 0;
                    playLevel = 0;
                    nurtureLevel = 0;
                    rampageLevel = 0;
                    passable = false;
                    activatedImages.Add(Type.House, "house");
                    deactivatedImages.Add(Type.House, "house");
                    break;
            }
        }

        public ObjectType(Type oType, bool startWet, bool wetGood, int oSleep, int oPlay, int oNurture, int oRampage)
        {
            typeName = oType;
            canActivate = false;
            startsWet = startWet;
            isWetObject = wetGood;
            sleepLevel = oSleep;
            playLevel = oPlay;
            nurtureLevel = oNurture;
            rampageLevel = oRampage;

            activatedImages.Add(Type.SunnySpot, "sunspot");
            deactivatedImages.Add(Type.SunnySpot, "rainbow");
            activatedImages.Add(Type.Rainbow, "rainbow");
            deactivatedImages.Add(Type.Rainbow, "sunspot");
            activatedImages.Add(Type.Garden, "garden2");
            deactivatedImages.Add(Type.Garden, "drygarden");
            activatedImages.Add(Type.Pool, "pool");
            deactivatedImages.Add(Type.Pool, "poolempty");
            activatedImages.Add(Type.Chalking, "chalksidewalk");
            deactivatedImages.Add(Type.Chalking, "sidewalk");
            activatedImages.Add(Type.Laundry, "laundry");
            deactivatedImages.Add(Type.Laundry, "wetlaundry");
            activatedImages.Add(Type.House, "house");
            deactivatedImages.Add(Type.House, "house");
        }

        public Type TypeName
        {
            get { return typeName; }
            set {typeName = value; }
        }

        public bool IsWetObject
        {
            get { return isWetObject; }
            set { isWetObject = value; }
        }

        public bool StartsWet
        {
            get { return startsWet; }
            set { startsWet = value; }
        }

        public bool CanActivate
        {
            get { return canActivate; }
            set { canActivate = value; }
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

        public bool HoldsWater
        {
            get { return holdsWater; }
        }

        public string activatedStringName()
        {
            return (string)activatedImages[typeName];
        }

        public string deactivatedStringName()
        {
            return (string)deactivatedImages[typeName];
        }

        public override string ToString()
        {
            return (string)activatedImages[typeName] + "\nSleep Level: " + sleepLevel + "\nPlay Level: " + playLevel + "\nNurture Level: " + nurtureLevel + "\nRampage Level: " + rampageLevel;
        }

    }
}
