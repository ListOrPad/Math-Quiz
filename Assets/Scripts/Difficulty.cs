using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    public static DifficultyEnum currentDifficulty;
    [SerializeField] private Button[] difficultyButtons;
    [HideInInspector] public Canvas menuCanvas;
    public bool DifficultyChanged { get; set; }

    private void Start()
    {
        menuCanvas = GameObject.Find("Choose Difficulty Menu(Canvas)").GetComponent<Canvas>();
        currentDifficulty = DifficultyEnum.VeryEasy; //maybe delete this line?
        for (int i = 0; i < difficultyButtons.Length; i++)
        {
            int capturedIndex = i;
            difficultyButtons[capturedIndex].onClick.AddListener(() => ChangeDifficulty(capturedIndex));
            difficultyButtons[capturedIndex].onClick.AddListener(() => SwitchCanvas());
        }
    }

    private void ChangeDifficulty(int difficultyID)
    {
        if (difficultyID == 0)
        {
            currentDifficulty = DifficultyEnum.VeryEasy;
        }
        else if (difficultyID == 1)
        {
            currentDifficulty = DifficultyEnum.Easy;
        }
        else if (difficultyID == 2)
        {
            currentDifficulty = DifficultyEnum.Normal;
        }
        else if (difficultyID == 3)
        {
            currentDifficulty = DifficultyEnum.Hard;
        }
        else if (difficultyID == 4)
        {
            currentDifficulty = DifficultyEnum.VeryHard;
        }

        DifficultyChanged = true;
    }

    private void SwitchCanvas()
    {
        menuCanvas.transform.SetSiblingIndex(0);
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
