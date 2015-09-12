module GamesysExercise

module Test =
    open Xunit
    open GamesysExercise

    //For the xUnit Assert, concrete objects need to be of the same type
    //so we will convert both items to a list. To determine which overload to use
    //we also need to cast them to IEnumerable, this is just an unfortunate ugliness
    //of calling something with C# definitions from F#
    let coerceToEnumerable = fun result -> result |> Seq.map (fun x->x:>obj) |> Seq.toList:>System.Collections.IEnumerable


    let assertRoundToNearestQuarter = fun x expectedResult -> Assert.Equal(expectedResult, roundToNearest 0.25 x, 2)
    [<Fact>] let ``roundToNearest - round down``() = assertRoundToNearestQuarter 1.12 1.0
    [<Fact>] let ``roundToNearest - zero``() = assertRoundToNearestQuarter 0.0 0.0
    [<Fact>] let ``roundToNearest - long integer``() = assertRoundToNearestQuarter 1345.30 1345.25
    [<Fact>] let ``roundToNearest - mid point round up``() = assertRoundToNearestQuarter 5.125 5.25
    [<Fact>] let ``roundToNearest - long decimal``() = assertRoundToNearestQuarter 25.712354 25.75
    

    let assertFirstNumber = fun x expectedResult -> Assert.Equal(expectedResult, firstNumber x, 2)
    [<Fact>] let ``firstNumber - example``() = assertFirstNumber 1.0 1.62
    [<Fact>] let ``firstNumber - zero``() = assertFirstNumber 0.0 0.4
    [<Fact>] let ``firstNumber - negative number``() = assertFirstNumber -1.0 -0.78
    [<Fact>] let ``firstNumber - simple value``() = assertFirstNumber 10.0 14.4

    let assertGrowthRate = fun initial y expectedResult -> Assert.Equal(expectedResult, growthRate initial y, 2)
    [<Fact>] let ``growthRate - example``() = assertGrowthRate 1.62 5062.5 2.5
    [<Fact>] let ``growthRate - simple values``() = assertGrowthRate  1.0 100.0 0.08
    [<Fact>] let ``growthRate - zero initial value``() = assertGrowthRate  0.0 100.0 infinity

    let assertSeries = fun firstNumber growthRate length expectedResult ->
        let resultSeries = series firstNumber growthRate length
        Assert.Equal( coerceToEnumerable expectedResult, coerceToEnumerable resultSeries)
    [<Fact>] let ``series - example list``() = assertSeries 1.62 2.5 5 [1.5; 4.0; 6.5; 10.75; 17.25]
    [<Fact>] let ``series - error when cant produce unique items``() = Assert.Throws(typedefof<System.Exception>, fun _ -> assertSeries 0.0 5062.5 5 [])
    [<Fact>] let ``series - single value requested``() = assertSeries 0.0 5062.5 1 [0.0]
    [<Fact>] let ``series - remove dupliates``() = assertSeries 2.0 1.0 5 [2.0;4.0;8.0;16.0;32.0]
    [<Fact>] let ``series - zero length``() = assertSeries 2.0 1.0 0 []
    [<Fact>] let ``series - error when negative length``() = Assert.Throws(typedefof<System.ArgumentException>, fun _ -> assertSeries 1.0 5062.5 -5 [])
    

    let assertSeriesWithBaseInputs = fun x y length expectedResult ->
        let resultSeries = seriesWithBaseInputs x y length
        Assert.Equal( coerceToEnumerable expectedResult, coerceToEnumerable resultSeries)
    [<Fact>] let ``series2 - example list``() = assertSeriesWithBaseInputs 1.0 5062.5 5 [1.5; 4.0; 6.5; 10.75; 17.25]
    [<Fact>] let ``series2 - simple list``() = assertSeriesWithBaseInputs 3.0 100.0 3 [0.0; 0.25; 4.25]
    
    
    let assertFirstSpecialNumber = fun series expectedValue -> Assert.Equal(expectedValue, series |> firstSpecialNumber)
    [<Fact>] let ``firstSpecialNumber - example list``() = assertFirstSpecialNumber [1.5; 4.0; 6.5; 10.75; 17.25] (Some 6.5)
    [<Fact>] let ``firstSpecialNumber - sequential list``() = assertFirstSpecialNumber [1.0; 2.0;3.0;4.0;5.0] (Some 3.0)
    [<Fact>] let ``firstSpecialNumber - unordered list``() = assertFirstSpecialNumber [2.0; 5.0; 1.0; 3.0; 4.0]; Some(3.0)
    [<Fact>] let ``firstSpecialNumber - empty list``() = assertFirstSpecialNumber [] None
    [<Fact>] let ``firstSpecialNumber - short list``() = assertFirstSpecialNumber [1.0,2.0] None
    [<Fact>] let ``firstSpecialNumber - negative list``() = assertFirstSpecialNumber [-0.1;-0.2;-0.3;-0.4] (Some -0.3)


    let assertSecondSpecialNumber = fun y z series expectedValue -> Assert.Equal(expectedValue, series |> (secondSpecialNumber y z))
    [<Fact>] let ``secondSpecialNumber - example list``() = assertSecondSpecialNumber 1000.0 160.0 [1.5; 4.0; 6.5; 10.75; 17.25] (Some 6.5)
    [<Fact>] let ``secondSpecialNumber - empty list``() = assertSecondSpecialNumber 1000.0 160.0 [] None
    [<Fact>] let ``secondSpecialNumber - zero z``() = assertSecondSpecialNumber 1000.0 0.0 [1.5; 4.0; 6.5; 10.75; 17.25] (Some 17.25)
    [<Fact>] let ``secondSpecialNumber - negative numbers``() = assertSecondSpecialNumber 1000.0 1000.0 [-1.0;10.0;-10.0;100.0] (Some -1.0)
    [<Fact>] let ``secondSpecialNumber - equal distance``() = assertSecondSpecialNumber 2.0 1.0 [1.0;3.0;0.0;5.0] (Some 3.0)