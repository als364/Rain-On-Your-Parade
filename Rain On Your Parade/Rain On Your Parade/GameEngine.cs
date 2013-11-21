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

        public const int LOG_FRAMES = 60;
        private int framesTillLog = 0;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Model> models;
        List<View> views;
        List<Controller> controllers;

        //WorldState worldState;
        Canvas level;

        Texture2D batterybar;
        Texture2D battery;

        Logger log;

        public GameEngine()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            //GraphicsDevice.BlendState = BlendState.AlphaBlend;
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

            //This is where the level building goes. I don't care about an XML parsing framework yet
            level = new Canvas();

           // Debug.WriteLine("Y: " + level.Grid[6, 6].Actors[0].Position.Y);
            int quota = 100;
            //worldState = new WorldState(quota,level.Grid);
            level.MaliceObjective = quota;

            //Debug.WriteLine("Y: " + worldState.getActors().ToArray()[1].Position.Y);
            foreach (WorldObject o in level.Objects)
            {
                View objects = new View(o);
                models.Add(o);
                views.Add(objects);
            }
            foreach (Actor a in level.Actors){
                View actors = new View(a);
                models.Add(a);
                views.Add(actors);
                Controller actorController = new ActorController(a);
                controllers.Add(actorController);
            }
            
            View player = new View(level.Player);
            views.Add(player);
            models.Add(level.Player);
            controllers.Add(new PlayerController(level.Player));

            //models.Add(slider);
            //views.Add(sliderView);
            //controllers.Add(sliderController);

            log = new Logger();

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
            foreach (Model model in models)
            {
                if (model != null)
                {
                    model.LoadContent(this.Content);
                }
            }

            batterybar = Content.Load<Texture2D>("batterybar");
            battery = Content.Load<Texture2D>("grass");

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
            if (framesTillLog == 0)
            {
                log.Log(level, gameTime);
                framesTillLog = LOG_FRAMES;
            }
            else
            {
                framesTillLog--;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            foreach (Model model in models)
            {
                if (model != null)
                {   //TODO: Encapsulate this in individual classes
                    model.activatedSprite.Update();

                    if (model.deactivatedSprite != null)
                    {
                        model.deactivatedSprite.Update();
                    }
                }
            }

            foreach (Controller controller in controllers)
            {
                controller.Update(gameTime, level);
            }
            level.upateGridSquares();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LimeGreen);
            
            // TODO: Add your drawing code here
            foreach (View view in views)
            {
                view.Draw(spriteBatch); //calls the AnimatedSprite draw function which includes begin/end
            }
            spriteBatch.Begin();
            //TODO: update this to reflect Player.MAX_RAIN
            spriteBatch.Draw(batterybar, new Rectangle(0, 0, 155, 30), Color.Azure);
            for (int i = 0; i < level.Player.Rain; i++)
            {
                spriteBatch.Draw(battery, new Rectangle(i*150/6 +5 , 3, 150/6, 25), Color.Azure);
            }
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
