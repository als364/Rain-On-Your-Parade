using System;
using System.Collections;

namespace Rain_On_Your_Parade
{
    public class ActorType
    {
        public enum Type { Kid, Cat, Mom };

        private Type typeName;

        Hashtable stringNames = new Hashtable();

        private int sleepNeed;
        private int playNeed;
        private int nurtureNeed;

        private int baseSleepLevel;
        private int basePlayLevel;
        private int baseNurtureLevel;
        private int baseRampageLevel;

        public const int SLEEP_THRESHOLD = 3;
        public const int PLAY_THRESHOLD = 3;
        public const int NURTURE_THRESHOLD = 3;
        public const int RAMPAGE_THRESHOLD = 3;

        public ActorType(Type aTypeName, int aSleepNeed, int aPlayNeed, int aNurtureNeed,
            int aBaseSleepLevel, int aBasePlayLevel, int aBaseNurtureLevel, int aBaseRampageLevel)
        {
            typeName = aTypeName;
            sleepNeed = aSleepNeed;
            playNeed = aPlayNeed;
            nurtureNeed = aNurtureNeed;
            baseSleepLevel = aBaseSleepLevel;
            basePlayLevel = aBasePlayLevel;
            baseNurtureLevel = aBaseNurtureLevel;
            baseRampageLevel = aBaseRampageLevel;

            stringNames.Add(Type.Cat, "cat");
            stringNames.Add(Type.Mom, "mom");
            stringNames.Add(Type.Kid, "kid");
        }

        #region Getters and Setters
        public Type TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }
        public int SleepNeed
        {
            get { return sleepNeed; }
            set { sleepNeed = value; }
        }
        public int PlayNeed
        {
            get { return playNeed; }
            set { playNeed = value; }
        }
        public int NurtureNeed
        {
            get { return nurtureNeed; }
            set { nurtureNeed = value; }
        }
        public int BaseSleepLevel
        {
            get { return baseSleepLevel; }
            set { baseSleepLevel = value; }
        }
        public int BasePlayLevel
        {
            get { return basePlayLevel; }
            set { basePlayLevel = value; }
        }
        public int BaseNurtureLevel
        {
            get { return baseNurtureLevel; }
            set { baseNurtureLevel = value; }
        }
        public int BaseRampageLevel
        {
            get { return baseRampageLevel; }
            set { baseRampageLevel = value; }
        }
        public override string ToString()
        {
            return (string)stringNames[typeName];
        }
        #endregion
    }
}