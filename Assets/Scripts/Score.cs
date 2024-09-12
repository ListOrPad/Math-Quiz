using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private CanvasGroup[] blocks;
    [SerializeField] private Canvas menuCanvas;
    private Localization localization;
    public int ScoreCount { get; set; }
    public bool ScoreChanged { get; set; }

    private void Start()
    {
        localization = GameObject.Find("Game").GetComponent<Localization>();
    }

    private void Update()
    {
        ScoreCount += 5;
        if (ScoreChanged)
        {
            WriteScore();
            ScoreChanged = false;
        }
        if (menuCanvas.isActiveAndEnabled == true)
        {
            ActivateDifficulties();
        }
        if(localization.LangChanged == true)
        {
            WriteScore();
            localization.LangChanged = false;
        }
    }

    public void AddScore(MathQuiz quiz)
    {
        if (quiz.randomOperation == Operation.Multiplication)
        {
            ScoreCount += (quiz.firstNumber + quiz.secondNumber) * 3;
            ScoreChanged = true;
        }
        else
        {
            ScoreCount += quiz.firstNumber + quiz.secondNumber;
            ScoreChanged = true;
        }
    }

    private void WriteScore()
    {
        if (localization.CurrentLang == "ru")
        {
            scoreText.text = "Очки: " + ScoreCount.ToString();
        }
        else
        {
            scoreText.text = "Score: " + ScoreCount.ToString();
        }
    }

    private void ActivateDifficulties()
    {
        if (ScoreCount > 500)
        {
            //open difficulty 2
            blocks[0].alpha = 0;
            blocks[0].interactable = false;
            blocks[0].blocksRaycasts = false;
        }
        if (ScoreCount > 9000)
        {
            //open difficulty 3
            blocks[1].alpha = 0;
            blocks[1].interactable = false;
            blocks[1].blocksRaycasts = false;
        }
        if (ScoreCount > 150_000)
        {
            //open difficulty 4
            blocks[2].alpha = 0;
            blocks[2].interactable = false;
            blocks[2].blocksRaycasts = false;
        }
        if (ScoreCount > 1_000_000)
        {
            //open difficulty 5
            blocks[3].alpha = 0;
            blocks[3].interactable = false;
            blocks[3].blocksRaycasts = false;
        }
        if (ScoreCount > 9_000_000)
        {
            //open victory pic
            blocks[4].alpha = 0;
            blocks[4].interactable = false;
            blocks[4].blocksRaycasts = false;
        }
    }
}
