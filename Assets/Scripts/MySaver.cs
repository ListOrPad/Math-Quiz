using System.Collections;
using UnityEngine;
using YG;

public class MySaver : MonoBehaviour
{
    private Coroutine myCoroutine;
    private Score score;

    private void OnEnable()
    {
        score = GameObject.Find("Score").GetComponent<Score>();

        YandexGame.GetDataEvent += GetLoad;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }

    /// <summary>
    /// Get data from plugin and do with it what we want
    /// </summary>
    public void GetLoad()
    {
        score.ScoreCount = YandexGame.savesData.Score;
        score.ScoreChanged = true;
    }

    /// <summary>
    /// Our method for saving
    /// </summary>
    public IEnumerator MySave()
    {
        yield return new WaitForSeconds(3);

        YandexGame.savesData.Score = score.ScoreCount;
        //save
        YandexGame.SaveProgress();

        //reset coroutine
        myCoroutine = null;
    }

    private void Update()
    {
        if (myCoroutine != null)
        {

        }
        else
        {
            myCoroutine = StartCoroutine(MySave());
        }
    }
}