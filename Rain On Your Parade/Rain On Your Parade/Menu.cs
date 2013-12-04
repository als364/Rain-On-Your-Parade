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
    public class Menu
    {

        Texture2D levelicon;
        Texture2D levelSelected;

        string[] menuItems;
        int selectedIndex;
        Color normal = Color.White;
        Color hilite = Color.Yellow;
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        SpriteFont spriteFont;
        Vector2 position;
        float width = 0f;
        float height = 0f;
        SpriteFont font;

        public int STAGE_ROWS = 1;


        public Menu()
        {
            STAGE_ROWS = GameEngine.SCREEN_WIDTH / 150;
        }


 
        public void Initialize()
        {
            // TODO: Add your initialization logic here
           
        }


        public void LoadContent(ContentManager content)
        {
            levelicon = content.Load<Texture2D>("poolempty");
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
                selectedIndex++;
                if (selectedIndex == GameEngine.STAGE_NUM)
                    selectedIndex = 0;
            }
            if (CheckKey(Keys.Down))
            {
                selectedIndex += STAGE_ROWS;
                if (selectedIndex >= GameEngine.STAGE_NUM)
                    selectedIndex -= GameEngine.STAGE_NUM;
            }
            if (CheckKey(Keys.Up))
            {
                selectedIndex -= STAGE_ROWS;
                if (selectedIndex < 0)
                    selectedIndex += GameEngine.STAGE_NUM;
            }
            if (CheckKey(Keys.Left))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = GameEngine.STAGE_NUM - 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                return selectedIndex+1;
            }
            return -1;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        public  void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
         for (int i=0; i< GameEngine.STAGE_NUM; i++){
             if (i == selectedIndex)
             {
                 spriteBatch.Draw(levelSelected, new Rectangle(i * 150 - ((i / STAGE_ROWS) * 750), (i / STAGE_ROWS) * 150, 90, 80), Color.White);
                 spriteBatch.DrawString(font, (""+(i + 1)), new Vector2(20+ i * 150 - ((i / STAGE_ROWS) * 750), 20 + (i / STAGE_ROWS) * 150), Color.White);
             }
             else
             {
                 spriteBatch.Draw(levelicon, new Rectangle(i * 150 - ((i / STAGE_ROWS) * 750), (i / STAGE_ROWS) * 150, 90, 80), Color.White);
                 spriteBatch.DrawString(font, (""+(i + 1)), new Vector2(20+ i * 150 - ((i / STAGE_ROWS) * 750), 20 + (i / STAGE_ROWS) * 150), Color.Black);
             }

             
         }
            spriteBatch.End();

        }
    }
}
