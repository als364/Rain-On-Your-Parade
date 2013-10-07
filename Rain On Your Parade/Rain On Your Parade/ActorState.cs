using System;

namespace Rain_On_Your_Parade
{
    public class ActorState
    {
        public enum AState { Sleep, Play, Nurture, Rampage, Seek, Walk };
        private AState state;

        public ActorState(AState aState)
        {
            state = aState;
        }

        public AState State
        {
            get { return state; }
            set { state = value; }
        }
    }
}