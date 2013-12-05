using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rain_On_Your_Parade
{
    public class AnimationSequence
    {
        #region fields
        private int startFrame; 		// first frame of the sequence
        private int endFrame; 		    // last frame of the sequence
        private bool reverseFrames; 	// 1,2...9,10,9,...2,1
        private bool goForward; 		// whether to increment or decrement frame count this update
        private int seqCounter; 		// how many times to play this sequence before switching
        private float timePerFrame;		// frame rate for this sequence
        private AnimatedTexture texture;            //spritesheet for this sequence
        private List<AnimationSequence> overlaySequence;	//which AS should overlay this sequence
        #endregion

        /// <summary>AnimationSequence Constructor</summary>
        /// <param name="a">sprite sheet frame index of first frame in sequence</param>
        /// <param name="z">sprite sheet frame index of last frame in sequence</param>
        /// <param name="rev">is last frame followed by second-to-last frame?</param>
        /// <param name="rpt">how many times should this sequence iterate through the frames?</param>
        /// <param name="fRate">frame rate for this sequence</param>
        /// <param name="tex">AnimatedTexture spritesheet for this sequence</param>
        /// <param name="aSeq">animation sequence list that overlays this sequence
        /// aSeq should occur DIRECTLY AFTER this sequence in AnimatedSprite's sequence list</param>
        /// <devdoc>
        /// Used by AnimatedSprite to process complex animations while
        /// keeping sprite sheets reasonably sized and memory efficient
        /// </devdoc>
        
        public AnimationSequence(int a, int z, bool rev, int rpt, float fRate, 
            AnimatedTexture tex, List<AnimationSequence> aSeq)
        {
            startFrame = a;
            endFrame = z;
            reverseFrames = rev;
            goForward = (a < z);
            seqCounter = rpt;
            timePerFrame = fRate;
            texture = tex;
            overlaySequence = aSeq;
        }

        #region Getters and Setters
        public int StartFrame
        {
            get
            {
                return startFrame;
            }
            set
            {
                startFrame= value;
            }
        }

        public int EndFrame
        {
            get
            {
                return endFrame;
            }
            set
            {
                endFrame = value;
            }
        }

        public bool ReverseFrames
        {
            get
            {
                return reverseFrames;
            }
            set
            {
                reverseFrames = value;
            }
        }

        public bool GoForward
        {
            get
            {
                return goForward;
            }
            set
            {
                goForward = value;
            }
        }

        public int SeqCounter
        {
            get
            {
                return seqCounter;
            }
            set
            {
                seqCounter = value;
            }
        }

        public float TimePerFrame
        {
            get
            {
                return timePerFrame;
            }
            set
            {
                timePerFrame = value;
            }
        }

        public AnimatedTexture Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
            }
        }
        
        public List<AnimationSequence> OverlaySequence
        {
            get
            {
                return overlaySequence;
            }
            set
            {
                overlaySequence = value;
            }
        }
        #endregion
    }
}
