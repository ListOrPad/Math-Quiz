using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rightWrongText;
    private Localization localization;

    private void Start()
    {
        localization = GameObject.Find("Game").GetComponent<Localization>();
    }

    public IEnumerator WriteRightOrWrong(bool answerCorrect)
    {
        if (answerCorrect)
        {
            if (localization.CurrentLang == "ru")
            {
                rightWrongText.text = "правильно";
            }
            else
            {
                rightWrongText.text = "correct";

            }
            rightWrongText.color = new Color32(131, 255, 0, 255);
            rightWrongText.fontSize = 200;
        }
        else if (!answerCorrect)
        {
            if (localization.CurrentLang == "ru")
            {
                rightWrongText.text = "неправильно";
            }
            else
            {
                rightWrongText.text = "wrong";

            }
            rightWrongText.color = new Color32(255, 17, 0, 255);
            rightWrongText.fontSize = 200;
        }
        yield return new WaitForSeconds(3);
        rightWrongText.alpha = 0;
    }
    public IEnumerator WriteRightOrWrong(MathQuiz quiz, int previousResult)
    {

        if (quiz.ExampleSkipped)
        {
            if (localization.CurrentLang == "ru")
            {
                rightWrongText.text = $"правильный ответ: {previousResult}";
            }
            else
            {
                rightWrongText.text = $"the answer is: {previousResult}";
            }
            rightWrongText.color = new Color(1,1,1,1);
            rightWrongText.fontSize = 100;
            quiz.ExampleSkipped = false;
        }

        yield return new WaitForSeconds(5);
        rightWrongText.alpha = 0;
    }
}
