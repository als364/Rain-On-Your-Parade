#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Diagnostics;
#endregion

namespace Rain_On_Your_Parade
{
    /// <summary>
    /// Index Map: 1=Continue 2=MainMenu 3=Replay
    /// </summary>
    public class LevelComplete
    {
        int optionIndex;
        int completeDelay = 10;
        Color normal = Color.White;
        Color hilite = Color.Yellow;
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        Texture2D bg_box;
        Texture2D bg_box_trans;
        SpriteFont font;

        int OPTION_NUM = 3;

        int iconHeight = 50;
        int iconWidth = 150;

        int marLeft = 5;
        int marTop = 120;
        int padLeft = 18;
        int padTop = 10;


        List<String> levelTitles = new List<String>();
        List<String> levelSubTitles = new List<String>();

        public LevelComplete()
        {
        }



        public void Initialize()
        {
            // TODO: Add your initialization logic here

        }


        public void LoadContent(ContentManager content)
        {
            bg_box = content.Load<Texture2D>("SliderBackground");
            bg_box_trans = content.Load<Texture2D>("trans_box");
            font = content.Load<SpriteFont>("DosisTitle");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) && oldKeyboardState.IsKeyDown(theKey);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        public int Update()
        {
            if (completeDelay == 0)
            {
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            if (CheckKey(Keys.Right))
            {
                optionIndex++;
                if (optionIndex == OPTION_NUM) optionIndex = 0;
            }

            if (CheckKey(Keys.Left))
            {
                optionIndex--;
                if (optionIndex < 0) optionIndex = OPTION_NUM-1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                return optionIndex;
            }
            }
            if (completeDelay > 0) completeDelay--;
            return -1;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //BG
            spriteBatch.Draw(bg_box_trans, new Rectangle(GameEngine.SCREEN_WIDTH / 8, GameEngine.SCREEN_HEIGHT / 4, 3 * GameEngine.SCREEN_WIDTH / 4, GameEngine.SCREEN_HEIGHT / 2), Color.Black);
            
            //Score
            string score = "Your Score Data Here!";
            spriteBatch.DrawString(font, "You Win!", new Vector2(GameEngine.SCREEN_WIDTH / 2 - 250, GameEngine.SCREEN_HEIGHT / 2 - 80), Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, score, new Vector2(GameEngine.SCREEN_WIDTH / 2 - 250, GameEngine.SCREEN_HEIGHT / 2 - 40), Color.White, 0, new Vector2(0, 0), 0.8f, SpriteEffects.None, 0);
            
            for (int i = 0; i < OPTION_NUM; i++)
            {

                string button_text = (i == 0) ? "Continue" :
                    (i == 1) ? "Replay" : "Main Menu";

                if (i == optionIndex)
                {
                    //Highlight icon
                    spriteBatch.Draw(bg_box, new Rectangle(iconWidth * i + GameEngine.SCREEN_WIDTH / 4 + i * 10 + marLeft, 3 * GameEngine.SCREEN_HEIGHT / 8 + marTop + 70, iconWidth, iconHeight), Color.White);
                    spriteBatch.Draw(bg_box, new Rectangle(iconWidth * i + GameEngine.SCREEN_WIDTH / 4 + i * 10 + marLeft + 5, 3 * GameEngine.SCREEN_HEIGHT / 8 + marTop + 70 + 5, iconWidth - 10, iconHeight - 10), Color.Black);
                    spriteBatch.DrawString(font, button_text, new Vector2(iconWidth * i + GameEngine.SCREEN_WIDTH / 4 + i * 10 + marLeft + iconWidth / 2, 3 * GameEngine.SCREEN_HEIGHT / 8 + marTop + 70 + padTop), Color.White, 0, new Vector2(font.MeasureString(button_text).X / 2, 0), 0.8f, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(bg_box, new Rectangle(iconWidth * i + GameEngine.SCREEN_WIDTH / 4 + i * 10 + marLeft, 3 * GameEngine.SCREEN_HEIGHT / 8 + marTop + 70, iconWidth, iconHeight), Color.Black);
                    spriteBatch.DrawString(font, button_text, new Vector2(iconWidth * i + GameEngine.SCREEN_WIDTH / 4 + i * 10 + marLeft + iconWidth / 2, 3 * GameEngine.SCREEN_HEIGHT / 8 + marTop + 70 + padTop), Color.White, 0, new Vector2(font.MeasureString(button_text).X / 2, 0), 0.8f, SpriteEffects.None, 0);
                }
            }

            spriteBatch.End();

        }
    }
}
