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
        Color normal = Color.White;
        Color hilite = Color.Yellow;
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        Texture2D bg_box;
        Texture2D bg_box_trans;
        SpriteFont font;

        int OPTION_NUM = 3;

        int button_margin_left = 80;
        int button_margin_top = 500;
        int button_width = 150;
        int button_height = 70;

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
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            if (CheckKey(Keys.Right))
            {
                optionIndex++;
                if (optionIndex > OPTION_NUM) optionIndex = 1;
            }

            if (CheckKey(Keys.Left))
            {
                optionIndex--;
                if (optionIndex == 0) optionIndex = OPTION_NUM;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                return optionIndex;
            }
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

            //Buttons
            Color col = (optionIndex == 1) ? Color.Blue :
                (optionIndex == 2) ? Color.Yellow : Color.Red;
            string button_text = (optionIndex == 1) ? "Continue" :
                (optionIndex == 2) ? "Main Menu" : "Replay";

            for (int i = 0; i < 3; i++)
            {
                if(i == optionIndex) {
                    spriteBatch.Draw(bg_box, new Rectangle(optionIndex * button_margin_left + 50, button_margin_top, button_width, button_height), Color.Black);
                    spriteBatch.DrawString(font, button_text, new Vector2(optionIndex * button_margin_left + 60, button_margin_top+10), Color.White, 0, new Vector2(0, 0), 0.8f, SpriteEffects.None, 0);

                } else {
                    spriteBatch.Draw(bg_box, new Rectangle(optionIndex * button_margin_left + 50, button_margin_top, button_width, button_height), col);
                    spriteBatch.DrawString(font, button_text, new Vector2(optionIndex * button_margin_left + 60, button_margin_top + 10), Color.White, 0, new Vector2(0, 0), 0.8f, SpriteEffects.None, 0);

                }
            }

            spriteBatch.End();

        }
    }
}
