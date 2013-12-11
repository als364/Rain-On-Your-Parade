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
    /// This is the main type for your game
    /// </summary>
    public class MainMenu
    {
        Texture2D topbar;
        Texture2D levelicon;
        Texture2D levelSelected;

        int selectedIndex;
        int menuDelay = 10;
        Color normal = Color.White;
        Color hilite = Color.Yellow;
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        SpriteFont font;

        int num_col = 3;
        int iconHeight = 90;
        int iconWidth = 90;

        int marLeft = 60;
        int marTop = 150;
        int padLeft = 28;
        int padTop = 28;

        List<String> levelTitles = new List<String>();
        List<String> levelSubTitles = new List<String>();

        public int STAGE_ROWS = 1;


        public MainMenu()
        {
            STAGE_ROWS = GameEngine.SCREEN_WIDTH / iconWidth;
        }


 
        public void Initialize()
        {
            // TODO: Add your initialization logic here
           
        }


        public void LoadContent(ContentManager content)
        {
            //levelicon = content.Load<Texture2D>("poolempty");
            topbar = content.Load<Texture2D>("SliderBackground");
            levelicon = content.Load<Texture2D>("cloud5");
            levelSelected = content.Load<Texture2D>("pool");
            font = content.Load<SpriteFont>("DefaultFont");

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
        public int Update()
        {
            if (menuDelay == 0)
            {
                oldKeyboardState = keyboardState;
                keyboardState = Keyboard.GetState();
                if (NewlyPressed(Keys.Right))
                {
                    selectedIndex++;
                    if (selectedIndex == GameEngine.STAGE_NUM)
                        selectedIndex = 0;
                }
                if (NewlyPressed(Keys.Down))
                {
                    selectedIndex += num_col;
                    if (selectedIndex >= GameEngine.STAGE_NUM)
                        selectedIndex -= GameEngine.STAGE_NUM;
                }
                if (NewlyPressed(Keys.Up))
                {
                    selectedIndex -= num_col;
                    if (selectedIndex < 0)
                        selectedIndex += GameEngine.STAGE_NUM;
                }
                if (NewlyPressed(Keys.Left))
                {
                    selectedIndex--;
                    if (selectedIndex < 0)
                        selectedIndex = GameEngine.STAGE_NUM - 1;
                }

                if (NewlyPressed(Keys.Enter) || NewlyPressed(Keys.Space))
                {
                    return selectedIndex + 1;
                }
            }
            if (menuDelay > 0) menuDelay--;
            return -1;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //Header
            spriteBatch.Draw(topbar, new Rectangle(0,0,GameEngine.SCREEN_WIDTH,100), Color.Black);
            spriteBatch.DrawString(font, "Select a Level", new Vector2(10,10), Color.White);
            spriteBatch.DrawString(font, "(arrows to select, enter to confirm)", new Vector2(10, 50), Color.White, 0, new Vector2(0, 0), 0.6f, SpriteEffects.None, 0);

            //Side panel
            spriteBatch.Draw(topbar, new Rectangle(GameEngine.SCREEN_WIDTH / 2, marTop - 20, 3 * GameEngine.SCREEN_WIDTH / 4, 3 * GameEngine.SCREEN_HEIGHT / 4), Color.Black);

            //Controls list
            string controls = "[WASD] or [ARROWS] to Move\n[SPACEBAR] to Rain\n[SHIFT] to Absorb\n[ESC] to Pause\n[P] to Restart";
            spriteBatch.DrawString(font, "Controls", new Vector2(GameEngine.SCREEN_WIDTH / 2 + 10, 3* marTop), Color.White, 0, new Vector2(0, 0), 0.8f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, controls, new Vector2(GameEngine.SCREEN_WIDTH / 2 + 10, 3 * marTop + 30), Color.White, 0, new Vector2(0, 0), 0.6f, SpriteEffects.None, 0);

         for (int i=0; i< GameEngine.STAGE_NUM; i++){

             string num = (i < 9) ? "0" + (i+1) : "" + (i+1);
             int row = (i % num_col);
             int col = (i / num_col);

             if (i == selectedIndex)
             {
                 Canvas level = LevelParser.parse(GameEngine.levels[i]);
                 //Side panel info
                 spriteBatch.DrawString(font, "Level " + num + "\n" + level.title , new Vector2(GameEngine.SCREEN_WIDTH / 2 + 10, marTop - 20), Color.White, 0, new Vector2(0, 0), 0.8f, SpriteEffects.None, 0);
                 spriteBatch.DrawString(font, level.objectiveMessage, new Vector2(GameEngine.SCREEN_WIDTH / 2 +10, 3*marTop/2), Color.White, 0, new Vector2(0, 0), 0.6f, SpriteEffects.None, 0);

                 //Highlight icon
                 spriteBatch.Draw(levelicon, new Rectangle(iconWidth*row + row*iconWidth/2 + marLeft, iconHeight*col + col*iconHeight/8 + marTop + 25, iconWidth, iconHeight), Color.Black);
                 spriteBatch.DrawString(font, num, new Vector2(iconWidth*row + row*iconWidth/2 + marLeft + padLeft, iconHeight*col + col*iconHeight/8 + marTop + 25 + padTop), Color.White);
             }
             else
             {
                 spriteBatch.Draw(levelicon, new Rectangle(iconWidth * row + row * iconWidth / 2 + marLeft, iconHeight * col + col * iconHeight / 8 + marTop + 25, iconWidth, iconHeight), Color.White);
                 spriteBatch.DrawString(font, num, new Vector2(iconWidth * row + row * iconWidth / 2 + marLeft + padLeft, iconHeight * col + col * iconHeight / 8 + marTop + 25 + padTop), Color.Black);
             }

             
         }
            spriteBatch.End();

        }
    }
}
