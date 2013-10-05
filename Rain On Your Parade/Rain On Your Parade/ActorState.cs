using System;

namespace Rain_On_Your_Parade
{
    public class ActorState
    {

        enum ActorState { Sleep, Play, Nurture, Rampage };
        private ActorState state;

        public ActorState(ActorState aState)
        {
            state = aState;
        }

        public ActorState getActorState()
        {
            return state;
        }

        public void setActorState(ActorState newState)
        {
            state = newState;
        }
    }
}