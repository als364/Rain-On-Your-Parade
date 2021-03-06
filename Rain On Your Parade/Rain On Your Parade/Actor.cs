﻿using System;
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
        private Actor targetActor;
        private bool targetIsActor;
        private int sleepLevel;
        private int playLevel;
        private int nurtureLevel;
        private int rampageLevel;
        private int gridSleepEffect;
        private int gridPlayEffect;
        private int gridNurtureEffect;
        private int gridRampageEffect;
        private List<GridSquare> path;
        private int interactionTimer;
        public const int RAIN_COOLDOWN = 120;
        public int rainCooldown;
        private int rot_mode;
        private bool facesLeft;


        public Texture2D mood1;
        public Texture2D mood2;
        public Texture2D mood3;
        public Texture2D mood4;
        public Texture2D mood5;
        public Texture2D mood6;
        public Texture2D moodFight;
        

        public Texture2D nurtureImg;
        public Texture2D playImg;
        public Texture2D sleepImg;

        public Texture2D personWet;
        public Texture2D catWet;

        private Actor interactingActor;
        private WorldObject interactingObject;

        private List<WorldObject> interactedObjects = new List<WorldObject>();

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
            targetIsActor = false;
            colorAlpha = 0f;
            rot_mode = -1;
            facesLeft = false;

        }

        public override void LoadContent(ContentManager content)
        {
            Texture2D activatedTexture = content.Load<Texture2D>(type.activatedStringName());
            Texture2D deactivatedTexture = content.Load<Texture2D>(type.deactivatedStringName());
            
            switch (type.TypeName) {

                case ActorType.Type.Kid:
                    AnimatedTexture aniTexBaseActive = new AnimatedTexture(activatedTexture, 6, 3, false, false);
                    List<AnimationSequence> aniSequencesAct = new List<AnimationSequence>(2);
                    aniSequencesAct.Add(new AnimationSequence(8, 14, true, 1, 0.1f, aniTexBaseActive, null));
                    aniSequencesAct.Add(new AnimationSequence(7, 0, true, 1, 0.1f, aniTexBaseActive, null));
                    activatedSprite = new AnimatedSprite(aniSequencesAct);

                    AnimatedTexture aniTexBaseDeactive = new AnimatedTexture(deactivatedTexture, 6, 1, false, false);
                    List<AnimationSequence> aniSequencesDeact = new List<AnimationSequence>(1);
                    aniSequencesDeact.Add(new AnimationSequence(5, 0, true, 1, 0.05f, aniTexBaseDeactive, null));
                    deactivatedSprite = new AnimatedSprite(aniSequencesDeact);
                    break;

                case ActorType.Type.Mom:
                    AnimatedTexture momActive = new AnimatedTexture(activatedTexture, 3, 3, false, false);
                    List<AnimationSequence> momAct = new List<AnimationSequence>(1);
                    momAct.Add(new AnimationSequence(6, 0, true, 1, 0.1f, momActive, null));
                    activatedSprite = new AnimatedSprite(momAct);

                    AnimatedTexture momDeactive = new AnimatedTexture(deactivatedTexture, 6, 1, false, false);
                    List<AnimationSequence> momDeact = new List<AnimationSequence>(1);
                    momDeact.Add(new AnimationSequence(5, 0, true, 1, 0.05f, momDeactive, null));
                    deactivatedSprite = new AnimatedSprite(momDeact);
                    break;

                case ActorType.Type.Cat:
                    AnimatedTexture catActive = new AnimatedTexture(activatedTexture, 5, 2, false, false);
                    List<AnimationSequence> catAct = new List<AnimationSequence>(2);
                    catAct.Add(new AnimationSequence(4, 0, true, 1, 0.1f, catActive, null));
                    catAct.Add(new AnimationSequence(9, 5, true, 1, 0.1f, catActive, null));
                    activatedSprite = new AnimatedSprite(catAct);

                    Texture2D csleep = content.Load<Texture2D>("CatSleeping");
                    AnimatedTexture catSleep = new AnimatedTexture(csleep, 5, 1, false, false);
                    List<AnimationSequence> catS = new List<AnimationSequence>(1);
                    catS.Add(new AnimationSequence(0, 4, true, 0, 0.1f, catSleep, null));
                    sleepSprite = new AnimatedSprite(catS);
                    
                    AnimatedTexture catDeactive = new AnimatedTexture(deactivatedTexture, 3, 3, false, false);
                    List<AnimationSequence> catDeact = new List<AnimationSequence>(1);
                    //catDeact.Add(new AnimationSequence(2, 0, true, 1, 0.1f, catDeactive, null));
                    //catDeact.Add(new AnimationSequence(5, 3, true, 1, 0.1f, catDeactive, null));
                    catDeact.Add(new AnimationSequence(0, 4, true, 0, 0.1f, catDeactive, null));  
                    deactivatedSprite = new AnimatedSprite(catDeact);
                    break;
            }

            mood1 = content.Load<Texture2D>("Emote1");
            mood2 = content.Load<Texture2D>("Emote2");
            mood3 = content.Load<Texture2D>("Emote3");
            mood4 = content.Load<Texture2D>("Emote4");
            mood5 = content.Load<Texture2D>("Emote5");
            mood6 = content.Load<Texture2D>("Emote6");
            moodFight = content.Load<Texture2D>("EmoteFighting");

            catWet = content.Load<Texture2D>("wetCat");
            personWet = content.Load<Texture2D>("wetPerson");

            nurtureImg = content.Load<Texture2D>("EmoteNurture");
            playImg = content.Load<Texture2D>("EmotePlay");
            sleepImg = content.Load<Texture2D>("EmoteSleep");

            spriteWidth = Canvas.SQUARE_SIZE;
            spriteHeight = Canvas.SQUARE_SIZE;
        }

        #region Getters and Setters

        public List<WorldObject> InteractedObjects
        {
            get { return interactedObjects; }
            set { interactedObjects = value; }
        }
        public Actor InteractingActor
        {
            get { return interactingActor; }
            set { interactingActor = value; }
        }
        public WorldObject InteractingObject
        {
            get { return interactingObject; }
            set { interactingObject = value; }
        }
        public ActorType Type
        {
            get { return type; }
            set { type = value; }
        }

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
        public int InteractionTimer
        {
            get { return interactionTimer; }
            set { interactionTimer = value; }
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
        public bool TargetIsActor
        {
            get { return targetIsActor; }
            set { targetIsActor = value; }
        }
        public Actor TargetActor
        {
            get { return targetActor; }
            set { targetActor = value; }
        }
        public bool FacesLeft
        {
            get { return facesLeft; }
            set { facesLeft = value; }
        }
        #endregion

        public void IncrementMood()
        {
            if (mood+1 > 5) mood = 5;
            else  mood++;
        }

        public void DecrementMood()
        {
            if (mood < 5)
            {
                if (mood - 1 < 0) mood = 0;
                else
                    mood--;
            }

        }

        public void increaseFastNeeds(){

             sleepLevel+= type.FastNeedIncrease[0];
             playLevel+= type.FastNeedIncrease[1];
             nurtureLevel += type.FastNeedIncrease[2];
         }

        public void increaseSlowNeeds()
        {
            sleepLevel += type.SlowNeedIncrease[0];
            playLevel += type.SlowNeedIncrease[1];
            nurtureLevel += type.SlowNeedIncrease[2];

        }

        public bool isFacingLeft()
        {
            if (path != null && path.Count != 0) return (path[0].Location.X < GridspacePosition.X);
            if (facesLeft && state.State == ActorState.AState.Run) return true;
            else return false;
        }

        public float rot_level() // rot_mode is -1 or 1 for rotate 0, -2 for rotate negative, 2 for rotate positive 
        {
            float x = 0f;
            if (state.State == ActorState.AState.Fight || state.State == ActorState.AState.Rampage)
            {
                switch (rot_mode)
                {
                    case -2:
                        rot_mode++;
                        x = -0.02f;
                        break;
                    case -1:
                        rot_mode += 3;
                        break;
                    case 1:
                        rot_mode -= 3;
                        break;
                    case 2:
                        rot_mode--;
                        x = 0.02f;
                        break;
                }
            }
            return x;
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