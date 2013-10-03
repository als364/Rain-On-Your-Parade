using System;

namespace Rain_On_Your_Parade
{
    public class Actor
    {
        private ActorType type;
        private Point position;
        private float velocity = 0;

        private int mood;
        private ActorState state;
        private int sleepLevel;
        private int playLevel;
        private int nurtureLevel;

        public Actor(ActorType aType, Point initPos, int initMood, ActorState initState)
        {
            type = aType;
            position = initPos;
            mood = initMood;
            state = initState;
            sleepLevel = aType.getBaseSleepLevel();
            playLevel = aType.getBasePlayLevel();
            nurtureLevel = aType.getBaseNurtureLevel();
        }

        public ActorType getType()
        {
            return type;
        }

        public Point getPosition()
        {
            return position;
        }

        public void setPosition(Point newPos)
        {
            position = newPos;
        }

        public float getVelocity()
        {
            return velocity;
        }

        public void setVelocity(float newVel)
        {
            velocity = newVel;
        }

        public int getMood()
        {
            return mood;
        }

        public void setMood(int newMood)
        {
            mood = newMood;
        }

        public void incMood()
        {
            mood++;
        }

        public void decMood()
        {
            mood--;
        }

        public ActorState getState()
        {
            return state;
        }

        public void setState(ActorState newState)
        {
            state = newState;
        }

        public int getSleepLevel()
        {
            return sleepLevel;
        }

        public void setSleepLevel(int newSleep)
        {
            sleepLevel = newSleep;
        }

        public int getPlayLevel()
        {
            return playLevel;
        }

        public void setPlayLevel(int newPlay)
        {
            playLevel = newPlay;
        }

        public int getNurtureLevel()
        {
            return nurtureLevel;
        }

        public void setNurtureLevel(int newNurture)
        {
            nurtureLevel = newNurture;
        }

    }
}