using System;
using System.Collections;

namespace Rain_On_Your_Parade
{
    public class ObjectType
    {

        public enum Type {SunnySpot, Garden, Pool, Chalking, Laundry, House};
        Hashtable activatedImages = new Hashtable();
        Hashtable deactivatedImages = new Hashtable();
        private Type typeName;
        private bool canActivate;
        private bool startsActivated;
        private bool rainActivates;
        private bool rainDeactivates;
        private bool absorbActivates;
        private bool absorbDeactivates;
        private bool startsContainingWater;
        private bool canContainWater;
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
                    startsActivated = false;
                    canActivate = false;
                    sleepLevel = 3;
                    playLevel = 0;
                    nurtureLevel = 0;
                    rampageLevel = 0;
                    rainActivates = true;
                    rainDeactivates = false;
                    absorbActivates = false;
                    absorbDeactivates = true;
                    startsContainingWater = false;
                    canContainWater = false;
                    passable = true;
                    activatedImages.Add(Type.SunnySpot, "rainbow");
                    deactivatedImages.Add(Type.SunnySpot, "sunspot");
                    break;

                case ObjectType.Type.Garden:
                    startsActivated = true;
                    canActivate = true;
                    sleepLevel = 0;
                    playLevel = 0;
                    nurtureLevel = 3;
                    rampageLevel = 0;
                    rainActivates = true;
                    rainDeactivates = false;
                    absorbActivates = false;
                    absorbDeactivates = true;
                    startsContainingWater = true;
                    canContainWater = true;
                    passable = true;
                    activatedImages.Add(Type.Garden, "garden2");
                    deactivatedImages.Add(Type.Garden, "drygarden");
                    break;

                case ObjectType.Type.Pool:
                    startsActivated = true;
                    canActivate = false;
                    sleepLevel = 0;
                    playLevel = 3;
                    nurtureLevel = 0;
                    rampageLevel = 0;
                    rainActivates = true;
                    rainDeactivates = false;
                    absorbActivates = false;
                    absorbDeactivates = true;
                    startsContainingWater = true;
                    canContainWater = true;
                    passable = true;
                    activatedImages.Add(Type.Pool, "pool");
                    deactivatedImages.Add(Type.Pool, "poolempty");
                    break;

                case ObjectType.Type.Chalking:
                    startsActivated = false;
                    canActivate = true;
                    sleepLevel = 0;
                    playLevel = 0;
                    nurtureLevel = 0;
                    rampageLevel = 0;
                    rainActivates = true;
                    rainDeactivates = false;
                    absorbActivates = false;
                    absorbDeactivates = true;
                    startsContainingWater = false;
                    canContainWater = false;
                    passable = true;
                    activatedImages.Add(Type.Chalking, "chalksidewalk");
                    deactivatedImages.Add(Type.Chalking, "sidewalk");
                    break;

                case ObjectType.Type.Laundry:
                    startsActivated = true;
                    canActivate = true;
                    sleepLevel = 0;
                    playLevel = 0;
                    nurtureLevel = 0;
                    rampageLevel = 0;
                    rainActivates = false;
                    rainDeactivates = true;
                    absorbActivates = true;
                    absorbDeactivates = false;
                    startsContainingWater = false;
                    canContainWater = true;
                    passable = true;
                    activatedImages.Add(Type.Laundry, "laundry");
                    deactivatedImages.Add(Type.Laundry, "wetlaundry");
                    break;

                case ObjectType.Type.House:
                    startsActivated = false;
                    canActivate = false;
                    sleepLevel = 0;
                    startsActivated = true;
                    sleepLevel = 0;
                    playLevel = 0;
                    nurtureLevel = 0;
                    rampageLevel = 0;
                    rainActivates = false;
                    rainDeactivates = false;
                    absorbActivates = false;
                    absorbDeactivates = false;
                    startsContainingWater = false;
                    canContainWater = false;
                    passable = false;
                    activatedImages.Add(Type.House, "house");
                    deactivatedImages.Add(Type.House, "house");
                    break;
            }
        }

        public ObjectType(Type oType, bool oActivated, int oSleep, int oPlay, int oNurture, int oRampage)
        {
            typeName = oType;
            canActivate = false;
            startsActivated = oActivated;
            sleepLevel = oSleep;
            playLevel = oPlay;
            nurtureLevel = oNurture;
            rampageLevel = oRampage;

            activatedImages.Add(Type.SunnySpot, "rainbow");
            deactivatedImages.Add(Type.SunnySpot, "sunspot");
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

        public bool StartsActivated
        {
            get { return startsActivated; }
            set { startsActivated = value; }
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

        public bool RainActivates
        {
            get { return rainActivates; }
            set { rainActivates = value; }
        }

        public bool RainDeactivates
        {
            get { return rainDeactivates; }
            set { rainDeactivates = value; }
        }

        public bool AbsorbActivates
        {
            get { return absorbActivates; }
            set { absorbActivates = value; }
        }

        public bool AbsorbDeactivates
        {
            get { return absorbDeactivates; }
            set { absorbDeactivates = value; }
        }

        public bool StartsContainingWater
        {
            get { return startsContainingWater; }
            set { startsContainingWater = value; }
        }

        public bool CanContainWater
        {
            get { return canContainWater; }
            set { canContainWater = value; }
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
