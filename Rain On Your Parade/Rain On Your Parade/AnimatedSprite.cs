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
        private int max;
        private int min;

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
            min = Math.Min(currentSequence.EndFrame, currentSequence.StartFrame);
            max = Math.Max(currentSequence.EndFrame, currentSequence.StartFrame);
        }

        /// <devdoc>
        /// Switches to the next sequence
        /// </devdoc>
        private void NextInSequence()
        {
            seqIndex++;

            if (seqIndex == sequences.Count)
            {
                seqIndex = 0;
            }
            currentSequence = sequences[seqIndex];
            currentFrame = currentSequence.StartFrame;
            currentSequence.GoForward = (currentSequence.StartFrame < currentSequence.EndFrame);
            min = Math.Min(currentSequence.EndFrame, currentSequence.StartFrame);
            max = Math.Max(currentSequence.EndFrame, currentSequence.StartFrame);
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
                    if (currentFrame == min || currentFrame > max)
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
                    if (currentFrame == max)
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
                            currentFrame = min;
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
        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, bool flip, float rot)
        {
            int width = currentSequence.Texture.Texture.Width / currentSequence.Texture.Columns;
            int height = currentSequence.Texture.Texture.Height / currentSequence.Texture.Rows;
            int row = (int)((float)currentFrame / (float)currentSequence.Texture.Columns);
            int column = currentFrame % currentSequence.Texture.Columns;

            //Console.Write("("+row.ToString()+", "+column.ToString()+")\n");

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            //Vector2 origin = new Vector2((float)width/2f, (float)height/2f);
            Vector2 origin = new Vector2(0f, 0f);

            //Get overlay sequence
            List<AnimationSequence> overlays = currentSequence.OverlaySequence;

            spriteBatch.Begin();

            //Draw current sequence
            SpriteEffects effect = (flip) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            
            //Console.Write(flip.ToString() + "\n");
            //Console.Write(rot.ToString() + "\n");

            spriteBatch.Draw(currentSequence.Texture.Texture, destinationRectangle, sourceRectangle, color, rot, origin, effect, 0f);

            if (overlays != null)
            {
                //Draw overlays
                foreach (AnimationSequence o in overlays) {
                    int shift = Math.Max(o.StartFrame,o.EndFrame) - Math.Max(currentSequence.StartFrame, currentSequence.EndFrame);

                    int o_width = o.Texture.Texture.Width / o.Texture.Columns;
                    int o_height = o.Texture.Texture.Height / o.Texture.Rows;
                    int o_row = (int)((float)(currentFrame+shift) / (float)o.Texture.Columns);
                    int o_column = (currentFrame+shift) % o.Texture.Columns;

                    //Console.Write((currentFrame+shift).ToString() + "\n");

                    Rectangle o_sourceRectangle = new Rectangle(o_width * o_column, o_height * o_row, o_width, o_height);
                    Rectangle o_destinationRectangle = new Rectangle((int)location.X, (int)location.Y, o_width, o_height);
                    //Vector2 o_origin = new Vector2((float)o_width / 2f, (float)o_height / 2f);
                    Vector2 o_origin = new Vector2(0f, 0f);

                    spriteBatch.Draw(o.Texture.Texture, o_destinationRectangle, o_sourceRectangle, Color.White, rot, o_origin, effect, 0f);
                }                
            }
            
            spriteBatch.End();
        }
    }
}
