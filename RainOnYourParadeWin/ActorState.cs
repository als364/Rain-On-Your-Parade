using System;

public class ActorState
{
    enum State { Kid, Cat, Mom };

    private State actorState;

    public ActorState(State aState)
	{
        actorState = aState;
	}

    public State getState()
    {
        return actorState;
    }

    public void setState(State newState)
    {
        actorState = newState;
    }
}
