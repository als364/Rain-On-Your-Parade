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
    /// </summary>
    public class PauseScreen
    {
        int selectedPauseOption;
        int PAUSE_OPTION_NUM = 1;
        Texture2D bg_box;
        Texture2D bg_box_trans;
        int pauseDelay = 10;
        Color normal = Color.White;
        Color hilite = Color.Yellow;
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        SpriteFont font;

        int num_col = 2;
        int iconHeight = 50;
        int iconWidth = 150;

        int marLeft = 90;
        int marTop = 120;
        int padLeft = 18;
        int padTop = 10;

        List<String> levelTitles = new List<String>();
        List<String> levelSubTitles = new List<String>();

        public PauseScreen()
        {
        }



        public void Initialize()
        {
            // TODO: Add your initialization logic here

        }


        public void LoadContent(ContentManager content)
        {
            //levelicon = content.Load<Texture2D>("poolempty");
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

        private bool NewlyPressed(Keys theKey)
        {
            return keyboardState.IsKeyDown(theKey) && oldKeyboardState.IsKeyUp(theKey);
        }
        private bool NewlyReleased(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) && oldKeyboardState.IsKeyDown(theKey);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        public int Update() //0 = Continue, 1 = Main, other = Paused
        {
            if (pauseDelay == 0)
            {
                oldKeyboardState = keyboardState;
                keyboardState = Keyboard.GetState();
                if (NewlyPressed(Keys.Enter) || NewlyPressed(Keys.Space))
                {
                    pauseDelay = 10;
                    return selectedPauseOption;
                }
                if (NewlyPressed(Keys.Right) || NewlyPressed(Keys.Down))
                {
                    if (selectedPauseOption++ == PAUSE_OPTION_NUM) selectedPauseOption = 0;
                }
                if (NewlyPressed(Keys.Up) || NewlyPressed(Keys.Left))
                {
                    if (selectedPauseOption-- == 0) selectedPauseOption = PAUSE_OPTION_NUM;
                }
                if (NewlyPressed(Keys.Escape))
                {
                    return 0;
                }
            }
            if (pauseDelay > 0) pauseDelay--;
            return -1;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, string levelTitle, string levelHelp, int initialRain)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(bg_box_trans, new Rectangle(GameEngine.SCREEN_WIDTH / 8, GameEngine.SCREEN_HEIGHT / 4, 3 * GameEngine.SCREEN_WIDTH / 4, GameEngine.SCREEN_HEIGHT / 2), Color.Black);

            spriteBatch.DrawString(font, levelTitle, new Vector2(GameEngine.SCREEN_WIDTH / 2 - 250, GameEngine.SCREEN_HEIGHT / 2 - 80), Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Your initial rain level is " + initialRain.ToString() + " units", new Vector2(GameEngine.SCREEN_WIDTH / 2 - 250, GameEngine.SCREEN_HEIGHT / 2 - 40), Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, levelHelp, new Vector2(GameEngine.SCREEN_WIDTH / 2 - 250, GameEngine.SCREEN_HEIGHT / 2), Color.White, 0, new Vector2(0, 0), 0.8f, SpriteEffects.None, 0);

            spriteBatch.DrawString(font, "GAME PAUSED", new Vector2(GameEngine.SCREEN_WIDTH / 2 - 70, GameEngine.SCREEN_HEIGHT / 8 + 100), Color.White, 0, new Vector2(0, 0), 1.2f, SpriteEffects.None, 0);


            for (int i = 0; i <= PAUSE_OPTION_NUM; i++)
            {

                string optionName = (i == 0) ? "Continue" : "Main Menu" ;

                if (i == selectedPauseOption)
                {
                    //Highlight icon
                    spriteBatch.Draw(bg_box, new Rectangle(iconWidth * i + GameEngine.SCREEN_WIDTH / 4 + i*30 + marLeft, 3 * GameEngine.SCREEN_HEIGHT / 8 + marTop + 70, iconWidth, iconHeight), Color.White);
                    spriteBatch.Draw(bg_box, new Rectangle(iconWidth * i + GameEngine.SCREEN_WIDTH / 4 + i * 30 + marLeft + 5, 3 * GameEngine.SCREEN_HEIGHT / 8 + marTop + 70 + 5, iconWidth - 10, iconHeight - 10), Color.Black);
                    spriteBatch.DrawString(font, optionName, new Vector2(iconWidth * i + GameEngine.SCREEN_WIDTH / 4 + i * 30 + marLeft + padLeft, 3 * GameEngine.SCREEN_HEIGHT / 8 + marTop + 70 + padTop), Color.White);
                }
                else
                {
                    spriteBatch.Draw(bg_box, new Rectangle(iconWidth * i + GameEngine.SCREEN_WIDTH / 4 + i * 30 + marLeft, 3 * GameEngine.SCREEN_HEIGHT / 8 + marTop + 70, iconWidth, iconHeight), Color.Black);
                    spriteBatch.DrawString(font, optionName, new Vector2(iconWidth * i + GameEngine.SCREEN_WIDTH / 4 + i * 30 + marLeft + padLeft, 3 * GameEngine.SCREEN_HEIGHT / 8 + marTop + 70 + padTop), Color.White);
                }
            }

            spriteBatch.End();

        }
    }
}
