/// Contains the definition of a tween as well as utility functions for such.
module Tweening.Tween

open Tweening.Functions

/// A tween is a function that applies a change to a value over a given duration of time.
/// During that duration, the value is determined by the given tweening function.
type Tween = 
    {
        /// The overall change of the tween.
        C : float
        /// The duration of the change.
        D : float
        /// The original tweening function.
        F : TweenFunc
    } 
    /// A convenience function for making a tween from a given function, change and duration.
    static member make : TweenFunc -> float -> float -> Tween = fun f c d -> { C = c ; D = d ; F = f }
    /// Calculates the tweened value of the beginning value, b, at the current time, t.
    member this.Apply b t =
        if   t < 0.0    then b
        elif t > this.D then b + this.C
                        else this.F this.C this.D b t
    
/// Makes a tween that is the first tween, f, followed by the second tween, g.
let serialize (f:Tween) (g:Tween) = 
    {
        C = f.C + g.C ; D = f.D + g.D
        F = fun c d b t -> if t < f.D then f.Apply b t else g.Apply (b + f.C) (t - f.D)
    }

/// Makes a tween that is the first tween, f, and the second tween g, run at the same time.
let parallelize (f:Tween) (g:Tween) = 
    {
        C = f.C + g.C ; D = max f.D g.D
        F = fun c d b t ->  f.Apply b t + g.Apply b t - b
    }

type Tween with
    /// Makes a tween that is the first tween, f, followed by the second tween, g.
    static member (.-.) (f,g) = serialize f g
    /// Makes a tween that is the first tween, f, and the second tween g, run at the same time.
    static member (.|.) (f,g) = parallelize f g