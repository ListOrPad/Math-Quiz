using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private CanvasGroup[] blocks;
    [SerializeField] private Canvas menuCanvas;
    private int score;
    private bool scoreChanged;

    private void Update()
    {
        if (scoreChanged)
        {
            WriteScore();
            scoreChanged = false;
        }
        if (menuCanvas.isActiveAndEnabled == true)
        {
            ActivateDifficulties();
        }
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
        scoreText.text = "����: " + score.ToString();
    }

    private void ActivateDifficulties()
    {
        if (score > 900)
        {
            //open difficulty 2
            blocks[0].alpha = 0;
        }
        if (score > 9000)
        {
            //open difficulty 3
            blocks[1].alpha = 0;
        }
        if (score > 150_000)
        {
            //open difficulty 4
            blocks[2].alpha = 0;
        }
        if (score > 1_000_000)
        {
            //open difficulty 5
            blocks[3].alpha = 0;
        }
        if (score > 9_000_000)
        {
            //open victory pic
            blocks[4].alpha = 0;
        }
    }
}
