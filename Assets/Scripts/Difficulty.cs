using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    public static DifficultyEnum currentDifficulty;

    private void Start()
    {
        currentDifficulty = DifficultyEnum.VeryEasy;
    }

    private void ChangeDifficulty(Button difficultyButton) //wut?
    {

    }
}

public enum DifficultyEnum
{
    VeryEasy,
    Easy,
    Normal,
    Hard,
    VeryHard,
}
