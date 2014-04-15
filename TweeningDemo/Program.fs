module TweeningDemo.Main

open TweeningDemo.Game

[<EntryPoint>]
let main argv = 
    use g = new DemoGame ()
    g.Run ()
    0 
