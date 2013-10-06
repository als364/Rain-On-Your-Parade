﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Rain_On_Your_Parade
{
    public class Actor : Model
    {
        private ActorType type;
        private float velocity = 0;

        private int mood;
        private ActorState state;
        private int sleepLevel;
        private int playLevel;
        private int nurtureLevel;
        private int rampageLevel;

        public Actor(ActorType aType, Point initPos, int initMood, ActorState initState)
        {
            type = aType;
            pos = initPos;
            mood = initMood;
            state = initState;
            sleepLevel = aType.BaseSleepLevel;
            playLevel = aType.BaseSleepLevel;
            nurtureLevel = aType.BaseNurtureLevel;
            rampageLevel = aType.BaseRampageLevel;
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Actor");
            spriteWidth = sprite.Width;
            spriteHeight = sprite.Height;
        }

        #region Getters and Setters
        public ActorType Type
        {
            get { return type; }
            set { type = value; }
        }
        public Point Position
        {
            get { return pos; }
            set { pos = value; }
        }
        public float Velocity
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
    }
}