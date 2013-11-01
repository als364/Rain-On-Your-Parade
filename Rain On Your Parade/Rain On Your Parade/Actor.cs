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
            PixelPosition = new Vector2(pos.X * Canvas.SQUARE_SIZE, pos.Y * Canvas.SQUARE_SIZE);
            GridspacePosition = pos;
            mood = 0;
            state = aType.InitState;
            sleepLevel = aType.BaseSleepLevel;
            playLevel = aType.BasePlayLevel;
            nurtureLevel = aType.BaseNurtureLevel;
            path = new List<GridSquare>();
            rampageLevel = aType.BaseRampageLevel;
            gridSleepEffect = aType.GridSleepEffect;
            gridPlayEffect = aType.GridPlayEffect;
            gridNurtureEffect = aType.GridNurtureEffect;
            gridRampageEffect = aType.GridRampageEffect;
        }

        public override void LoadContent(ContentManager content)
        {
            activatedSprite = content.Load<Texture2D>(type.activatedStringName());
            deactivatedSprite = content.Load<Texture2D>(type.activatedStringName());
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
        public int GridSleepEffect
        {
            get { return gridSleepEffect; }
            set { gridSleepEffect = value; }
        }
        public int GridPlayEffect
        {
            get { return gridPlayEffect; }
            set { gridPlayEffect = value; }
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
        #endregion

        public void IncrementMood()
        {
            mood++;
        }

        public void DecrementMood()
        {
            mood--;
        }

        public override string ToString()
        {
            string gPath = "";
            if (path != null && path.Count > 0)
            {
                foreach (GridSquare g in path)
                {
                    gPath += g.Location.ToString() + "->";
                }
                gPath = gPath.Substring(0, gPath.Length - 2);
            }
            
            return type.ToString() + "\n" + base.ToString() + "Velocity: " + velocity.ToString() + "\nMood: " + mood + "\n" + state.ToString() + " Target State: " + targetState.ToString() + "\nSleep Level = " + sleepLevel + "\nPlay Level = " + playLevel + "\nNurture Level = " +
                nurtureLevel + "\nRampage Level = " + rampageLevel + "\nGrid Sleep Effect: " + gridSleepEffect + " Grid Nurture Effect: " + gridNurtureEffect + " Grid Play Effect: " + gridPlayEffect +
                " Grid Rampage Effect: " + gridRampageEffect + "\n" + gPath;
        }
    }
}