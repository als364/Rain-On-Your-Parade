using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Rain_On_Your_Parade
{
    public class Actor : Model
    {
        private ActorType type;
        private Vector2 velocity;

        private int mood;
        private ActorState state;
        private ActorState.AState targetState;
        private int sleepLevel;
        private int playLevel;
        private int nurtureLevel;
        private int rampageLevel;
        private int gridSleepEffect;
        private int gridPlayEffect;
        private int gridNurtureEffect;
        private int gridRampageEffect;
        private List<GridSquare> path;

        public Actor(ActorType.Type newActType, Point pos)
        {
            ActorType aType = new ActorType(newActType);
            type = aType;
            Position = new Vector2(pos.X * Canvas.SQUARE_SIZE, pos.Y * Canvas.SQUARE_SIZE);
            mood = 0;
            state = aType.InitState;
            sleepLevel = aType.BaseSleepLevel;
            playLevel = aType.BasePlayLevel;
            nurtureLevel = aType.BaseNurtureLevel;
            path = new List<GridSquare>();
            rampageLevel = aType.BaseRampageLevel;
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(type.StringName());
            spriteWidth = Canvas.SQUARE_SIZE;
            spriteHeight = Canvas.SQUARE_SIZE;
        }

        #region Getters and Setters
        public ActorType Type
        {
            get { return type; }
            set { type = value; }
        }
        /*public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }*/
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        public int Mood
        {
            get { return mood; }
            set { mood = value; }
        }
        public ActorState State
        {
            get { return state; }
            set { state = value; }
        }
        public ActorState.AState TargetState
        {
            get { return targetState; }
            set { targetState = value; }
        }
        public int SleepLevel
        {
            get { return sleepLevel; }
            set { sleepLevel = value; }
        }
        public int PlayLevel
        {
            get { return playLevel; }
            set { playLevel = value; }
        }
        public int NurtureLevel
        {
            get { return nurtureLevel; }
            set { nurtureLevel = value; }
        }
        public List<GridSquare> Path
        {
            get { return path; }
            set { path = value; }
        }
        public int RampageLevel
        {
            get { return rampageLevel; }
            set { rampageLevel = value; }
        }
        #endregion

        public void IncrementMood()
        {
            mood++;
        }

        public void DecrementMood()
        {
            mood--;
        }

        public Point GridSquareLocation()
        {
            return new Point((int)(this.Position.X / 80), (int)(this.Position.Y / 80));
        }

        public override string ToString()
        {
            string gPath = "";
            foreach (GridSquare g in path)
            {
                gPath += g.Location.ToString() + "->";
            }
            return "Type: " + type.ToString() + "\n" + base.ToString() + "\nVelocity: " + velocity.ToString() + "\nMood: " + mood + "\nState: " + state.ToString() + " Target State: " + targetState.ToString() + "\nSleep Level = " + sleepLevel + "\nPlay Level = " + playLevel + "\nNurture Level = " +
                nurtureLevel + "\nRampage Level = " + rampageLevel + "\nGrid Sleep Effect: " + gridSleepEffect + "Grid Nurture Effect: " + gridNurtureEffect + "Grid Play Effect: " + gridPlayEffect +
                "Grid Rampage Effect: " + gridRampageEffect + gPath;
        }
    }
}