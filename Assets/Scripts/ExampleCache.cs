using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleCache
{
    public string[] CachedExample { get; private set; } = new string[5];
    public int[] CachedResult { get; private set; } = new int[5];
    public bool[] exampleGenerated { get; set; } = new bool[5]; // Track if an example has been generated for each difficulty
    public bool[] exampleSolved { get; set; } = new bool[5]; // Track if the example for each difficulty has been solved

    public void CacheExample(string example, int result)
    {
        if (Difficulty.currentDifficulty == DifficultyEnum.VeryEasy)
        {
            CachedExample[0] = example;
            CachedResult[0] = result;
        }
        else if (Difficulty.currentDifficulty == DifficultyEnum.Easy)
        {
            CachedExample[1] = example;
            CachedResult[1] = result;
        }
        else if (Difficulty.currentDifficulty == DifficultyEnum.Normal)
        {
            CachedExample[2] = example;
            CachedResult[2] = result;
        }
        else if (Difficulty.currentDifficulty == DifficultyEnum.Hard)
        {
            CachedExample[3] = example;
            CachedResult[3] = result;
        }
        else if (Difficulty.currentDifficulty == DifficultyEnum.VeryHard)
        {
            CachedExample[4] = example;
            CachedResult[4] = result;
        }
        
    }


}
