﻿using System;

namespace Rain_On_Your_Parade
{
    public class ActorState
    {
        public enum AState { Sleep, Play, Nurture, Rampage, Seek, Walk, Wander };
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

        public override string ToString()
        {
            switch (state)
            {
                case AState.Sleep: return "State: Sleep";
                case AState.Play: return "State: Play";
                case AState.Nurture: return "State: Nurture";
                case AState.Rampage: return "State: Rampage";
                case AState.Seek: return "State: Seek";
                case AState.Walk: return "State: Walk";
                case AState.Wander: return "State: Wander";
                default: return "";
            }
        }
    }
}