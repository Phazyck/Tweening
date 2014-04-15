/// Contains the definition for a lensed tweener.
module Tweening.Lensed

open Tweening.Tween

open FSharpx

/// A Tweener applies tweening on a subject's value using a timer's time.
type Tweener<'Subject,'Timer> = 
    {
        /// The initial value of the tween.
        Initial : float
        /// The lens from a subject to its value.
        Value   : Lens<'Subject,float>
        /// The time from which the tweening should start.
        Start   : float
        /// The lens from a timer to its time.
        Time    : Lens<'Timer,float>
        /// The actual tween.
        Tween   : Tween
    }

    /// <summary>
    /// Given a subject and a timer, this function returns a new subject 
    /// with it's value tweened according to the timers time.
    /// </summary>
    /// <param name="subject">The subject whose value should be updated.</param>
    /// <param name="timer">The timer which holds the time.</param>
    member this.Update (subject:'Subject) (timer:'Timer) = 
        this.Time.Get timer - this.Start 
        |> this.Tween.Apply this.Initial 
        |>  this.Value.Set 
            <| subject
