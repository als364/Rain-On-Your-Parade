using System;

namespace Rain_On_Your_Parade
{
    public class ActorState
    {

        public enum AState { Sleep, Play, Nurture, Rampage };
        private AState state;

        public ActorState(AState conState)
        {
            state = conState;
        }

        public AState getActorState()
        {
            return state;
        }

        public void setActorState(AState newState)
        {
            state = newState;
        }
    }
}