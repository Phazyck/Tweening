/// Contains a type definition and examples of tweening functions.
module Tweening.Functions

/// <summary>
/// A tweening function takes four parameters
/// <para/> - c : The total change applied by the tween.
/// <para/> - d : The duraction of the tween.
/// <para/> - b : The beginning value of the tween.
/// <para/> - t : The elapsed time of the tween.
/// <para/> and returns the value at the given time of the tween.
/// </summary>
type TweenFunc = float -> float -> float -> float -> float

/// A collection of linear tweening functions.
module Line = 
    /// A tweening function that does nothing.
    let flat : TweenFunc = fun _ _ b _ -> 
        b

    /// A linear tweening function.
    let slope : TweenFunc = fun c d b t -> 
        c * t / d + b
    
/// A collection of quadratic tweening functions.
module Quad = 

    /// A quadratic ease-in function.
    let easeIn : TweenFunc = fun c d b t -> 
        let t = t / d
        c * t * t + b

    /// A quadratic ease-out function.
    let easeOut : TweenFunc = fun c d b t ->
        let t = t / d
        -c * t * (t - 2.0) + b

    /// A quadratic ease-in-out function.
    let easeInOut : TweenFunc = fun c d b t ->
        let t = 2.0 * t / d
        if t < 1.0 then
            c / 2.0 * t * t + b
        else 
            let t = t - 1.0
            -c / 2.0 * (t * (t-2.0) - 1.0) + b