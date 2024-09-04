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
        score += 5;
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
        scoreText.text = "Очки: " + score.ToString();
    }

    private void ActivateDifficulties()
    {
        if (score > 500)
        {
            //open difficulty 2
            blocks[0].alpha = 0;
            blocks[0].interactable = false;
            blocks[0].blocksRaycasts = false;
        }
        if (score > 9000)
        {
            //open difficulty 3
            blocks[1].alpha = 0;
            blocks[1].interactable = false;
            blocks[1].blocksRaycasts = false;
        }
        if (score > 150_000)
        {
            //open difficulty 4
            blocks[2].alpha = 0;
            blocks[2].interactable = false;
            blocks[2].blocksRaycasts = false;
        }
        if (score > 1_000_000)
        {
            //open difficulty 5
            blocks[3].alpha = 0;
            blocks[3].interactable = false;
            blocks[3].blocksRaycasts = false;
        }
        if (score > 9_000_000)
        {
            //open victory pic
            blocks[4].alpha = 0;
            blocks[4].interactable = false;
            blocks[4].blocksRaycasts = false;
        }
    }
}
