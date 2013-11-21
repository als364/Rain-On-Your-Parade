using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Rain_On_Your_Parade
{
    public class AnimatedSprite
    {
        private List<AnimationSequence> sequences;		// an ordering of sequences to animate
        private AnimationSequence currentSequence;
        private int currentFrame;
        private float timeElapsed;					    // for maintaining frame rate
        private int counter;                            // keep track of the number of iterations
        private int seqIndex;                           // keep track of current sequence index

        /// <summary>AnimatedSprite Constructor</summary>
        /// <param name="seq">list of AnimationSequences that make up this animation</param>
        /// <devdoc>
        /// Takes in a list of animation sequences and processes them in a set order. Wraps around. 
        /// Basically does all the work while the other two classes just contain grouped data.
        /// </devdoc>
        
        public AnimatedSprite(List<AnimationSequence> seq) 
        {
            sequences = seq;
            timeElapsed= 0f;
            currentSequence = seq[0];
            currentFrame = seq[0].StartFrame;
            counter = 0;
            seqIndex = 0;
        }

        /// <devdoc>
        /// Switches to the next sequence
        /// </devdoc>
        private void NextInSequence()
        {
            if (currentSequence.OverlaySequence != null)
            {
                seqIndex += 2;
            }
            else
            {
                seqIndex++;
            }

            if (seqIndex == sequences.Count)
            {
                seqIndex = 0;
            }
            currentSequence = sequences[seqIndex];
            currentFrame = currentSequence.StartFrame;
            currentSequence.GoForward = currentSequence.StartForward;
        }

        /// <devdoc>
        /// Calculates the current frame
        /// </devdoc>
        public void Update()
        {
            // not an animation; don't waste our time with it
            if (currentSequence.Texture.Rows == 1 && currentSequence.Texture.Columns == 1)
            {
                return;
            }

            timeElapsed += 1f/60f; //target time of game updates

            if (timeElapsed >= currentSequence.TimePerFrame) 
            {
                //Console.Write(currentFrame.ToString() + "\n");

                // process a reversing sequence
                if (currentSequence.ReverseFrames)
                {
                    // Increment or Decrement current frame accordingly
                    if (currentSequence.GoForward)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        currentFrame--;
                    }

                    // check if we reached the end of an iteration
                    if (currentFrame == currentSequence.StartFrame || 
                        currentFrame == currentSequence.EndFrame)
                    {
                        // check if we need to switch to the next sequence
                        if (counter == currentSequence.SeqCounter)
                        {
                            //Console.Write("switching sequences and resetting counter\n");
                            // switch to the next sequence
                            NextInSequence();
                            counter = 0;
                        }
                        else
                        {
                            //Console.Write("incrementing counter and reversing\n");
                            // increment counter and set current
                            counter++;
                            currentSequence.GoForward = !currentSequence.GoForward;
                        }
                    }

                }
                else
                {
                    // process a wrap around sequence
                    currentFrame++;

                    // check if we reached the end of an iteration
                    if (currentFrame == currentSequence.EndFrame)
                    {
                        if (counter == currentSequence.SeqCounter)
                        {
                            // switch to the next sequence
                            NextInSequence();
                            counter = 0;
                        }
                        else
                        {
                            // increment counter and set current
                            counter++;
                            currentFrame = currentSequence.StartFrame;
                        }
                    }
                }

                // update timing for framerate
                timeElapsed -= currentSequence.TimePerFrame;
            }
        }

        /// <devdoc>
        /// Draw the current frame to spritebatch
        /// </devdoc>
        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
            int width = currentSequence.Texture.Texture.Width / currentSequence.Texture.Columns;
            int height = currentSequence.Texture.Texture.Height / currentSequence.Texture.Rows;
            int row = (int)((float)currentFrame / (float)currentSequence.Texture.Columns);
            int column = currentFrame % currentSequence.Texture.Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            //Get overlay sequence
            AnimationSequence overlay = currentSequence.OverlaySequence;

            spriteBatch.Begin();
            if (overlay != null)
            {
                spriteBatch.Draw(overlay.Texture.Texture, destinationRectangle, sourceRectangle, color);
                spriteBatch.Draw(currentSequence.Texture.Texture, destinationRectangle, sourceRectangle, Color.White);
            }
            else
            {
                spriteBatch.Draw(currentSequence.Texture.Texture, destinationRectangle, sourceRectangle, color);
            }
            
            spriteBatch.End();
        }
    }
}
