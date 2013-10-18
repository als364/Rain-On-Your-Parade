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

        private int gridSleepEffect;
        private int gridPlayEffect;
        private int gridNurtureEffect;
        private int gridRampageEffect;

        public const int SLEEP_THRESHOLD = 3;
        public const int PLAY_THRESHOLD = 3;
        public const int NURTURE_THRESHOLD = 3;
        public const int RAMPAGE_THRESHOLD = 3;

        public ActorType(Type aTypeName)
        {
            typeName = aTypeName;
            switch (typeName)
            {
                case ActorType.Type.Cat:
                    sleepNeed = 3;
                    playNeed = 2;
                    nurtureNeed = 0;
                    baseSleepLevel = 3;
                    basePlayLevel = 2;
                    baseNurtureLevel = 0;
                    baseRampageLevel = 1;
                    gridSleepEffect = 1;
                    gridPlayEffect = 1;
                    gridNurtureEffect = 1;
                    gridRampageEffect = 3;
                    break;
                case ActorType.Type.Kid:
                    sleepNeed = 1;
                    playNeed = 3;
                    nurtureNeed = 0;
                    baseSleepLevel = 1;
                    basePlayLevel = 3;
                    baseNurtureLevel = 0;
                    baseRampageLevel = 1;
                    gridSleepEffect = -1;
                    gridPlayEffect = 2;
                    gridNurtureEffect = 3;
                    gridRampageEffect = 3;
                    break;
                case ActorType.Type.Mom:
                    sleepNeed = 0;
                    playNeed = 0;
                    nurtureNeed = 3;
                    baseSleepLevel = 0;
                    basePlayLevel = 0;
                    baseNurtureLevel = 3;
                    baseRampageLevel = 1;
                    gridSleepEffect = -1;
                    gridPlayEffect = -1;
                    gridNurtureEffect = -1;
                    gridRampageEffect = 3;
                    break;
            }
        }

        public ActorType(Type aTypeName, int aSleepNeed, int aPlayNeed, int aNurtureNeed,
            int aBaseSleepLevel, int aBasePlayLevel, int aBaseNurtureLevel, int aBaseRampageLevel, 
            int aGridSleepEffect, int aGridPlayEffect, int aGridNurtureEffect, int aGridRampageEffect)
        {
            typeName = aTypeName;
            sleepNeed = aSleepNeed;
            playNeed = aPlayNeed;
            nurtureNeed = aNurtureNeed;
            baseSleepLevel = aBaseSleepLevel;
            basePlayLevel = aBasePlayLevel;
            baseNurtureLevel = aBaseNurtureLevel;
            baseRampageLevel = aBaseRampageLevel;
            gridSleepEffect = aGridSleepEffect;
            gridPlayEffect = aGridPlayEffect;
            gridNurtureEffect = aGridNurtureEffect;
            gridRampageEffect = aGridRampageEffect;
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
        public int GridSleepEffect
        {
            get { return gridSleepEffect; }
            set { gridSleepEffect = value; }
        }
        public int GridPlayEffect
        {
            get { return gridSleepEffect; }
            set { gridSleepEffect = value; }
        }
        public int GridNurtureEffect
        {
            get { return gridNurtureEffect; }
            set { gridNurtureEffect = value; }
        }
        public int GridRampageEffect
        {
            get { return gridRampageEffect; }
            set { gridRampageEffect = value; }
        }
        public override string ToString()
        {
            //return (string)stringNames[typeName];
            switch (typeName)
            {
                case ActorType.Type.Cat:
                    return "cat";
                case ActorType.Type.Kid:
                    return "kid";
                case ActorType.Type.Mom:
                    return "mom";
                default:
                    return "Actor";
            }
        }
        #endregion
    }
}