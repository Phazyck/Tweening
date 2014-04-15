module TweeningDemo.Frame

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open FSharpx

open Tweening.Tween
open Tweening.Functions
open Tweening.Lensed

type Frame = 
    {
        Clock : float
        X  : float
        Y  : float
        W  : float
        H  : float
        C1 : Color
        C2 : Color
        C  : float
        Tweeners : Tweener<Frame,Frame> list
    }
    member this.Rectangle = 
        new Rectangle(
            int this.X,
            int this.Y,
            int this.W,
            int this.H
        )

    member this.Color = 
        Color.Lerp(this.C1, this.C2, float32 this.C)

    static member LensClock : Lens<Frame,float> = 
        {
            Get = fun s -> s.Clock
            Set = fun c s -> { s with Clock = c }
        }

    static member LensX : Lens<Frame,float> = 
        {
            Get = fun s -> s.X
            Set = fun v s -> { s with X = v }
        }

    static member LensY : Lens<Frame,float> = 
        {
            Get = fun s -> s.Y
            Set = fun v s -> { s with Y = v }
        }

    static member LensW : Lens<Frame,float> = 
        {
            Get = fun s -> s.W
            Set = fun v s -> { s with W = v }
        }

    static member LensH : Lens<Frame,float> = 
        {
            Get = fun s -> s.H
            Set = fun v s -> { s with H = v }
        }

    static member LensC : Lens<Frame,float> = 
        {
            Get = fun s -> s.C
            Set = fun v s -> { s with C = v }
        }
    

let tween1 = 
    let a =Tween.make Quad.easeInOut  1.0 1.0
    let b =Tween.make Quad.easeInOut -0.5 2.0
    a .-. b

let tween2 =
    let a = Tween.make Quad.easeInOut 256.0 1.0
    let b = Tween.make Quad.easeInOut -128.0 2.0
    a .-. b

let tween3 = 
    let a = Tween.make Line.slope  300.0 3.0
    let b = Tween.make Quad.easeOut -100.0 1.0
    a .|. b

let update (frame:Frame) time = 
    let c = Frame.LensClock.Update ((+) time) frame
    List.fold 
        (fun f (t:Tweener<Frame,Frame>) -> t.Update f c) 
        c
        c.Tweeners

let draw (frame:Frame) (spriteBatch:SpriteBatch) (texture:Texture2D) = 
    spriteBatch.Draw(texture, frame.Rectangle, frame.Color)