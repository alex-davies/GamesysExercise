module GamesysExercise

let roundToNearest = fun (nearest:float) (value:float) ->
    if nearest = 0.0 then value
    else System.Math.Round(value/nearest, System.MidpointRounding.AwayFromZero) * nearest

let firstNumber = fun x -> ((0.5 * x**2.0) + (30.0 * x) + 10.0) / 25.0

let growthRate = fun initial y -> 0.02 * y / 25.0 / initial

let series = fun firstNumber growthRate length ->
    let validateLength = if length < 0 then  invalidArg "length" "length must be positive"  else "ok"

    let rawSeries = seq{
        yield firstNumber
        yield! Seq.initInfinite (fun n -> n+1) |> Seq.map (fun i -> growthRate * (firstNumber**(float i)))
    } 

    //Certain inputs can cause the series to never produce enough unique inputs (e.g. all items equal 0). 
    //We will add a safegaud to prevent it from running indefiniatly. 
    //Without a full context on what exactly we are calculting and how often 
    //this case is likely to occur all we can do is give a best guess of how 
    //long we will keep looking for unique values
    let maxIterations = 10 * length;

    //clean up the raw series to meet the formatting requirements of the series
    let formattedSeries = rawSeries 
                            |> (Seq.truncate maxIterations)
                            |> Seq.map (roundToNearest 0.25)
                            |> Seq.distinct
                            |> Seq.truncate length
                            |> Seq.sort
                            |> Seq.toList

    if formattedSeries |> List.length = length then formattedSeries
    else failwith (sprintf "series could not produce %d unique items" length)
 
let seriesWithBaseInputs = fun x y length ->
    let initial = firstNumber x
    let growth = growthRate initial y
    series initial growth length

let firstSpecialNumber = fun series ->
    //if our series is shorter than three items then we will assume we dont have a first special item
    let length = series |> List.length
    if(length >= 3) then Some(series |> List.sort |> List.item (length-3)) 
    else None

let secondSpecialNumber = fun (y:float) (z:float) series ->
    let approximateNumber = y / z
    //ascending value on the absolute difference and descending on the actual value.
    if series |> List.isEmpty then None
    else series |> List.minBy (fun value -> abs (approximateNumber-value), -value) |> Some