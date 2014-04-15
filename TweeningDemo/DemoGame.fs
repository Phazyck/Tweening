module TweeningDemo.Game

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input
open Microsoft.Xna.Framework.Graphics
 
 open TweeningDemo.Frame

type DemoGame () as this =
    inherit Microsoft.Xna.Framework.Game ()
 
    let graphics:GraphicsDeviceManager = new Microsoft.Xna.Framework.GraphicsDeviceManager (this)

    let mutable texture     : Texture2D   = Unchecked.defaultof<Texture2D>
    let mutable spriteBatch : SpriteBatch = Unchecked.defaultof<SpriteBatch>
    let mutable frame       : Frame       = Unchecked.defaultof<Frame>
 
    override Game.Initialize () =
        do base.Initialize ()
        do frame <-
            {
                Clock = 0.0
                X  = 32.0
                Y  = 32.0
                W  = 32.0
                H  = 32.0
                C1 = Color.Green
                C2 = Color.Red
                C  = 0.0
                Tweeners = 
                    [
                        {
                            Initial = 32.0
                            Value   = Frame.LensX
                            Start   = 1.0
                            Time    = Frame.LensClock
                            Tween   = tween3
                        }
                        {
                            Initial = 32.0
                            Value   = Frame.LensW
                            Start   = 1.0
                            Time    = Frame.LensClock
                            Tween   = tween2
                        }
                        {
                            Initial = 32.0
                            Value   = Frame.LensH
                            Start   = 1.0
                            Time    = Frame.LensClock
                            Tween   = tween2
                        }
                        {
                            Initial = 0.0
                            Value   = Frame.LensC
                            Start   = 1.0
                            Time    = Frame.LensClock
                            Tween   = tween1
                        }
                        {
                            Initial = 32.0
                            Value   = Frame.LensY
                            Start   = 1.0
                            Time    = Frame.LensClock
                            Tween   = tween3
                        }
                    ]
            }
 
    override Game.LoadContent () =
        do spriteBatch <- new SpriteBatch (this.GraphicsDevice)
        do texture <- new Texture2D(this.GraphicsDevice, 1, 1)
        do texture.SetData<Color> [|Color.White|]
 
    override Game.Update (gameTime:GameTime) =
        let k = Keyboard.GetState ()
        do if k.IsKeyDown Keys.Escape then this.Exit ()
        do if k.IsKeyDown Keys.Space  then this.Initialize ()
        do frame <- update frame gameTime.ElapsedGameTime.TotalSeconds
 
    override Game.Draw (gameTime:GameTime) =
        do this.GraphicsDevice.Clear Color.CornflowerBlue
        do spriteBatch.Begin ()
        do draw frame spriteBatch texture
        do spriteBatch.End ()

