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
    public class GameEngine : Game
    {
        public const int SCREEN_WIDTH = 880;
        public const int SCREEN_HEIGHT = 720;
        

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Model> models;
        List<View> views;
        List<Controller> controllers;

        WorldState worldState;

        public GameEngine()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            //content = new ContentManager(Services);
            Content.RootDirectory = "Content";
            models = new List<Model>();
            views = new List<View>();
            controllers = new List<Controller>();
            //worldState = new WorldState();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            //This is where the level building goes. I don't care about an XML parsing framework yet.
            //Slider slider = new Slider(SCREEN_WIDTH-50, 0, 50, SCREEN_HEIGHT-50);
            //SliderView sliderView = new SliderView(slider);
            //SliderController sliderController = new SliderController(slider);

            Canvas level = new Canvas();

           // Debug.WriteLine("Y: " + level.canvasGrid[6, 6].Actors[0].Position.Y);
            int quota = 100;
            worldState = new WorldState(quota,level.canvasGrid);

            //Debug.WriteLine("Y: " + worldState.getActors().ToArray()[1].Position.Y);

            foreach (Actor a in worldState.getActors()){
                View actors = new View(a);
                models.Add(a);
                //Debug.WriteLine("Y: " + a.Position.Y);
                views.Add(actors);
                Controller actorController = new ActorController(a);
                controllers.Add(actorController);
            }
            foreach (WorldObject o in worldState.getObjects()){
                View objects= new View(o);
                models.Add(o);
                views.Add(objects);
            }
            View player = new View(worldState.Player);
            views.Add(player);
            models.Add(worldState.Player);
            controllers.Add(new PlayerController(worldState.Player));

            //models.Add(slider);
            //views.Add(sliderView);
            //controllers.Add(sliderController);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            foreach (Model model in models)
            {
                model.LoadContent(this.Content);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach (Controller controller in controllers)
            {
                controller.Update(gameTime, worldState);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            foreach (View view in views)
            {
                view.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
