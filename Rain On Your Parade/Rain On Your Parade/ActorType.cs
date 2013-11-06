using System;
using System.Collections;

namespace Rain_On_Your_Parade
{
    public class ActorType
    {
        public enum Type { Kid, Cat, Mom };

        private Type typeName;

        Hashtable activatedImages = new Hashtable();
        Hashtable deactivatedImages = new Hashtable();

        private int sleepNeed;
        private int playNeed;
        private int nurtureNeed;

        private int baseSleepLevel;
        private int basePlayLevel;
        private int baseNurtureLevel;
        private int baseRampageLevel;
        private ActorState initState;

        private int[] fastNeedIncrease; // 0:sleep, 1:play, 3:nurture
        private int[] slowNeedIncrease;

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
                    playNeed = 0;
                    nurtureNeed = 0;
                    baseSleepLevel = 3;
                    basePlayLevel = 0;
                    baseNurtureLevel = 0;
                    baseRampageLevel = 1;
                    gridSleepEffect = 1;
                    gridPlayEffect = 1;
                    gridNurtureEffect = 1;
                    gridRampageEffect = 3;
                    initState = new ActorState(ActorState.AState.Sleep);
                    activatedImages.Add(Type.Cat, "cat");
                    deactivatedImages.Add(Type.Kid, "kid");
                    fastNeedIncrease = new int[3] {1, 0, 0};
                    slowNeedIncrease = new int[3] {0, 0, 1 };
                    break;
                case ActorType.Type.Kid:
                    sleepNeed = 0;
                    playNeed = 3;
                    nurtureNeed = 0;
                    baseSleepLevel = 0;
                    basePlayLevel = 3;
                    baseNurtureLevel = 0;
                    baseRampageLevel = 1;
                    gridSleepEffect = -1;
                    gridPlayEffect = 2;
                    gridNurtureEffect = 3;
                    gridRampageEffect = 3;
                    initState = new ActorState(ActorState.AState.Seek);
                    activatedImages.Add(Type.Kid, "kid");
                    deactivatedImages.Add(Type.Kid, "kid");
                    fastNeedIncrease = new int[3] { 0, 1, 0 };
                    slowNeedIncrease = new int[3] { 1, 0, 0 };
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
                    initState = new ActorState(ActorState.AState.Seek);
                    activatedImages.Add(Type.Mom, "mom");
                    deactivatedImages.Add(Type.Mom, "mom");
                    fastNeedIncrease = new int[3] { 0, 0, 1 };
                    slowNeedIncrease = new int[3] { 0, 0, 0 };
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

            activatedImages.Add(Type.Cat, "cat");
            activatedImages.Add(Type.Kid, "kid");
            activatedImages.Add(Type.Mom, "mom");
            deactivatedImages.Add(Type.Cat, "cat");
            deactivatedImages.Add(Type.Kid, "kid");
            deactivatedImages.Add(Type.Mom, "mom");
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
        public int[] FastNeedIncrease
        {
            get { return fastNeedIncrease; }
            set { fastNeedIncrease = value; }
        }
        public int[] SlowNeedIncrease
        {
            get { return slowNeedIncrease; }
            set { slowNeedIncrease = value; }
        }

        public ActorState InitState
        {
            get { return initState; }
            set { initState = value; }
        }
        //public override string ToString()
        //{
        //    return (string)stringNames[typeName];
        //}
        public string activatedStringName()
        {
            return (string)activatedImages[typeName];
        }
        #endregion


        public override string ToString()
        {
            return "Type: " + activatedImages[typeName] + "\nSleep Need: " + sleepNeed + "\nPlay Need: " + playNeed + "\nNurture Need: " + nurtureNeed + "\nBase Sleep Level = " + baseSleepLevel +
                "\nBase Play Level = " + basePlayLevel + "\nBase Nurture Level = " + baseNurtureLevel + "\nBase Rampage Level = " + baseRampageLevel + "\nGrid Sleep Effect: " + gridSleepEffect +
                " Grid Nurture Effect: " + gridNurtureEffect + " Grid Play Effect: " + gridPlayEffect + " Grid Rampage Effect: " + gridRampageEffect;
        }
    }
}