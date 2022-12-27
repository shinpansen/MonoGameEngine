using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShinpansenEngine._2D.Graphics.Shapes;
using ShinpansenEngine.Engines;
using System.Collections.Generic;
using System.Linq;

namespace ShinpansenEngine;

public class EngineTest : Game
{
    private GraphicsDeviceManager _graphicsDeviceManager;
    private GraphicsEngine _graphicsEngine;
    private RectangleShape _rs;
    private OvalShape _cs;
    private OvalShape _os;
    private List<CircleShape> lesCercles = new List<CircleShape>();

    public EngineTest()
    {
        _graphicsDeviceManager = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphicsEngine = new GraphicsEngine(GraphicsDevice, Content);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _rs = new RectangleShape(_graphicsEngine, 
            Color.LimeGreen, new Vector2(100, 100), 
            new Vector2(300, 300));
        _cs = new OvalShape(_graphicsEngine,
            Color.Orange, new Vector2(10, 10), new Vector2(300, 300),
            50, Color.BlueViolet);
        _os = new OvalShape(_graphicsEngine,
            new Color(255, 0, 66), new Vector2(0, 0), new Vector2(300, 150),
            50, Color.LimeGreen);

        /*_texture2D = new Texture2D(GraphicsDevice, 1, 1);
        _texture2D.SetData(new Color[] { Color.Red });
        _texture2D = Content.Load<Texture2D>("pattern");
        _testEffect = Content.Load<Effect>("Circle");
        _param = _testEffect.Parameters["Param"];
        _param.SetValue(0.25f);*/
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if(Keyboard.GetState().IsKeyDown(Keys.Space))
            _os.Size = new Vector2(_os.Size.X + 1, _os.Size.Y + 1);

        /*if(Keyboard.GetState().IsKeyDown(Keys.Right))
            lesCercles.Add(new CircleShape(_emagonomGraphicsEngine,
                Color.LimeGreen, new Vector2(100, 100), 150));
        if(Keyboard.GetState().IsKeyDown(Keys.Right))
            lesCercles.Add(new CircleShape(_emagonomGraphicsEngine,
                Color.HotPink, new Vector2(50, 100), 150));*/

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(36, 36, 36));

        //_rs.Draw(gameTime);

        _os.Location = new Vector2(Mouse.GetState().X - 150, Mouse.GetState().Y - 75);
        _cs.Draw(gameTime);
        _os.Draw(gameTime);
        //lesCercles.ForEach(c => c.Draw(gameTime));

        base.Draw(gameTime);
    }
}
