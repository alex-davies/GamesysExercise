#Gamesys coding exercise
This is a coding excersie from Gamesys. The details of the excercise can be found in DotNETCodinTest.pdf

##Running

1. git clone the repository
2. Open GamesysExercise in Visual Studio 2015 (it may work on older versions, but has not been tested)
3. Build (should also refetch nuget packages)
4. Run tests in GamesysExercise.Test

##Assumptions
The following assumptions were made when completing the exercise. The lack of context around the exercise made it difficult made it difficult to determine whether these assumptions are valid and reasonable, with more information about the problem it may be determined that my assumptions are incorrect.

* All inputs that were not specified otherwise are signed doubles
* The inputs do not exceed the length of a double
* Accuracy of a double is assumed to be acceptable (can convert to decimal if floating point rounding could cause issues )
* When removing duplicates (1d) we are checking the rounded values for duplicates, not the raw calculated values
* Midpoint rounding is away from zero, e.g. 1.125 is rounded up to 1.25 rather than to 1
* The first number is included in the ordering, this can cause results where after ordering the first number is not the first number in the series
* If we can not produce the required number of unique values (1d) in a fixed number of iterations we will fail with an error. This is needed to prevent running indefinatly when the series is all of the same single value. This fixed number is currently set to be 10x the total length.
* Divide by zero calculations are equal to infinite and the flow on affects of this are expected
* The series length will remain small enough that the whole series can exist in memory (this restriction allows the first special number to be calculated efficiently)

##F#
Both the code and test cases are written in F#. The functional nature of the exercise made F# a more suitable choice and although I am more familiar with C# the level of F# required was well within my abilities

##xUnit Tests
Tests are written with xUnit. The test runner is included as a nuget package so should allow Visual Studio test runner to be used. 
 
