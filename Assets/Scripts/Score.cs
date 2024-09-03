using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private CanvasGroup[] blocks;
    private int score;
    private bool scoreChanged;

    private void Update()
    {
        if (scoreChanged)
        {
            WriteScore();
            scoreChanged = false;
        }
        OpenDifficulties();
    }

    public void AddScore(MathQuiz quiz)
    {
        if (quiz.randomOperation == Operation.Multiplication)
        {
            score += (quiz.firstNumber + quiz.secondNumber) * 3;
            scoreChanged = true;
        }
        else
        {
            score += quiz.firstNumber + quiz.secondNumber;
            scoreChanged = true;
        }
    }

    private void WriteScore()
    {
        scoreText.text = "Очки: " + score.ToString();
    }


    /// <summary>
    /// should this method be here?
    /// </summary>
    private void OpenDifficulties()
    {
        if (score > 900)
        {
            //open difficulty 2
            blocks[0].alpha = 1;
        }
        //...
    }
}
