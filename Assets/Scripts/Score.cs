using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;
using YG.Utils.LB;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private CanvasGroup[] blocks;
    [SerializeField] private Canvas menuCanvas;
    private Localization localization;
    public int ScoreCount;
    public bool ScoreChanged { get; set; }
    private int previousRecord;

    private void Start()
    {
        localization = GameObject.Find("Game").GetComponent<Localization>();

        YandexGame.onGetLeaderboard += RewriteRecord;
    }

    private void Update()
    {
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

        YandexGame.GetLeaderboard("Score", 1000000, 3, 10, "42"); //change parameters to actual data
        Debug.Log(previousRecord + "is your PREV record");
        //Adding score to the leaderboard if previous record from the leaderboard is lesser than current Score
        if (previousRecord < ScoreCount)
        {
            previousRecord = ScoreCount;
            Debug.Log(previousRecord + "is your NEW record");
            YandexGame.NewLeaderboardScores("Score", ScoreCount);
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
        if (ScoreCount > 444)
        {
            //open difficulty 2
            blocks[0].alpha = 0;
            blocks[0].interactable = false;
            blocks[0].blocksRaycasts = false;
        }
        if (ScoreCount > 4444)
        {
            //open difficulty 3
            blocks[1].alpha = 0;
            blocks[1].interactable = false;
            blocks[1].blocksRaycasts = false;
        }
        if (ScoreCount > 44_444)
        {
            //open difficulty 4
            blocks[2].alpha = 0;
            blocks[2].interactable = false;
            blocks[2].blocksRaycasts = false;
        }
        if (ScoreCount > 444_444)
        {
            //open difficulty 5
            blocks[3].alpha = 0;
            blocks[3].interactable = false;
            blocks[3].blocksRaycasts = false;
        }
        if (ScoreCount > 4_444_444)
        {
            //open victory pic
            blocks[4].alpha = 0;
            blocks[4].interactable = false;
            blocks[4].blocksRaycasts = false;
        }
    }

    private void RewriteRecord(LBData lbData)
    {
        string currentPlayerId = YandexGame.playerId;

        // Find the current player's data in the leaderboard
        foreach (var player in lbData.players)
        {
            if (player.name == currentPlayerId)
            {
                previousRecord = player.score;
                break;
            }
        }
    }
}
