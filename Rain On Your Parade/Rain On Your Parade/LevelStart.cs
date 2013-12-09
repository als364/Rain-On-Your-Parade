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
    public class LevelStart
    {
        Texture2D bg_box;
        Texture2D bg_box_trans;
        int startDelay = 50;
        Color normal = Color.White;
        Color hilite = Color.Yellow;
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        SpriteFont font;

        int OPTION_NUM = 3;

        int num_col = 2;
        int iconHeight = 90;
        int iconWidth = 90;

        int marLeft = 90;
        int marTop = 150;
        int padLeft = 28;
        int padTop = 28;

        List<String> levelTitles = new List<String>();
        List<String> levelSubTitles = new List<String>();

        public LevelStart()
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

        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) && oldKeyboardState.IsKeyDown(theKey);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        public int Update()
        {
            if (startDelay == 0)
            {
                oldKeyboardState = keyboardState;
                keyboardState = Keyboard.GetState();
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) || Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    return 1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape)) 
                {
                    return 2;
                }
            }
            if (startDelay > 0) startDelay--;
            return 0;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, string levelTitle, string levelHelp, int initialRain)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(bg_box_trans, new Rectangle(GameEngine.SCREEN_WIDTH / 8, GameEngine.SCREEN_HEIGHT / 4, 3 * GameEngine.SCREEN_WIDTH / 4, GameEngine.SCREEN_HEIGHT / 2), Color.Black);
            spriteBatch.DrawString(font, "Press [SPACEBAR] or [ENTER] to Start", new Vector2(GameEngine.SCREEN_WIDTH / 2 - 150, GameEngine.SCREEN_HEIGHT / 2 + 150), Color.White, 0, new Vector2(0, 0), 0.8f, SpriteEffects.None, 0);

            spriteBatch.DrawString(font, levelTitle, new Vector2(GameEngine.SCREEN_WIDTH / 2 - 250, GameEngine.SCREEN_HEIGHT / 2 - 80), Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Your initial rain level is " + initialRain.ToString() + " units", new Vector2(GameEngine.SCREEN_WIDTH / 2 - 250, GameEngine.SCREEN_HEIGHT / 2 - 40), Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, levelHelp, new Vector2(GameEngine.SCREEN_WIDTH / 2 - 250, GameEngine.SCREEN_HEIGHT / 2), Color.White, 0, new Vector2(0, 0), 0.8f, SpriteEffects.None, 0);

            spriteBatch.End();

        }
    }
}
