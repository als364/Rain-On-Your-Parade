using System;

namespace Rain_On_Your_Parade
{
    public class ActorType
    {
        enum Type { Kid, Cat, Mom };

        private Type typeName;

        private int sleepNeed;
        private int playNeed;
        private int nurtureNeed;

        private int baseSleepLevel;
        private int basePlayLevel;
        private int baseNurtureLevel;

        public const int SLEEP_THRESHOLD = 3;
        public const int PLAY_THRESHOLD = 3;
        public const int NURTURE_THRESHOLD = 3;
        public const int RAMPAGE_THRESHOLD = 3;

        public ActorType(Type aTypeName, int aSleepNeed, int aPlayNeed, int aNurtureNeed,
            int aBaseSleepLevel, int aBasePlayLevel, int aBaseNurtureLevel)
        {
            typeName = aTypeName;
            sleepNeed = aSleepNeed;
            playNeed = aPlayNeed;
            nurtureNeed = aNurtureNeed;
            baseSleepLevel = aBaseSleepLevel;
            basePlayLevel = aBasePlayLevel;
            baseNurtureLevel = aBaseNurtureLevel;
        }

        public Type getType()
        {
            return typeName;
        }

        public int getSleepNeed()
        {
            return sleepNeed;
        }

        public int getPlayNeed()
        {
            return playNeed;
        }

        public int getNurtureNeed()
        {
            return nurtureNeed;
        }

        public int getBaseSleepLevel()
        {
            return baseSleepLevel;
        }

        public int getBasePlayLevel()
        {
            return basePlayLevel;
        }

        public int getBaseNurtureLevel()
        {
            return baseNurtureLevel;
        }


    }
}