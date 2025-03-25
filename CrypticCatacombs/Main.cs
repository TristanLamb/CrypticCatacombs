using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CrypticCatacombs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Windows.Forms.AxHost;

namespace CrypticCatacombs;

public class Main : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;


    Basic2d cursor;
	GamePlay gamePlay;

	public Main()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
		//IsMouseVisible = true;

		Globals.appDataFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

	}

    protected override void Initialize()
    {

        Globals.screenWidth = 1674;
        Globals.screenHeight = 930;
        graphics.PreferredBackBufferWidth = Globals.screenWidth;
        graphics.PreferredBackBufferHeight = Globals.screenHeight;
        graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        Globals.content = this.Content;
        Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

        Globals.save = new Save(1,"CrypticCatacombs"); 


		cursor = new Basic2d("2d/Misc/CursorArrow", new Vector2(0, 0), new Vector2(24, 24));


        Globals.keyboard = new CustomKeyboard();
        Globals.mouse = new CustomMouseControls();


		gamePlay = new GamePlay();
		gamePlay.LoadContent(Content);


        if(File.Exists(Globals.appDataFilePath + "\\" + Globals.save.gameName + "\\XML\\KeyBinds.xml"))
        {
            GameGlobals.keyBinds = new KeyBindList(Globals.save.GetFile("XML\\KeyBinds.xml"));
		}
		else
        {
            //Make File
            XDocument keybindXML = XDocument.Parse("<Root><Keys><Key n=\"Move Left\"><value>A</value></Key><Key n=\"Move Right\"><value>D</value></Key><Key n=\"Move Up\"><value>W</value></Key><Key n=\"Move Down\"><value>S</value></Key></Keys></Root>");
			//Save File
            Globals.save.HandleSaveFormats(keybindXML, "KeyBinds.xml");
			//Load File
			GameGlobals.keyBinds = new KeyBindList(Globals.save.GetFile("XML\\KeyBinds.xml"));
		}
	}


    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();


        Globals.gameTime = gameTime;
        Globals.keyboard.Update();
        Globals.mouse.Update();


		//System.Diagnostics.Debug.WriteLine($"Main Updating, gameState: {Globals.gameState}");


		gamePlay.Update();




		cursor.Update(new Vector2(Globals.mouse.newMousePos.X + cursor.dims.X/2, Globals.mouse.newMousePos.Y + cursor.dims.Y/2));

		Globals.keyboard.UpdateOld();
        Globals.mouse.UpdateOld();

        base.Update(gameTime);
    }


    public virtual void ChangeGameState(object INFO)
    {
		System.Diagnostics.Debug.WriteLine($"Changing gameState from {Globals.gameState} to {Globals.ConvertToInt(INFO)}");
		Globals.gameState = Globals.ConvertToInt(INFO);
		System.Diagnostics.Debug.WriteLine($"Game State Changed: {Globals.gameState}");
	}

    public virtual void ExitGame(object INFO)
	{
		System.Environment.Exit(0);
	}

	protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);




		gamePlay.Draw(Globals.spriteBatch);
		



		//cursor.Draw(new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y), new Vector2(0, 0), Color.White);
		cursor.Draw();

		Globals.spriteBatch.End();

        base.Draw(gameTime);
    }

    public static class Program
    {
        static void Main()
        {
            using var game = new CrypticCatacombs.Main();
            game.Run();
        }
    }
    

}
