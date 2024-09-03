using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rightWrongText;

    public IEnumerator WriteRightOrWrong(bool answerCorrect)
    {
        if (answerCorrect)
        {
            rightWrongText.text = "���������";
            rightWrongText.color = new Color32(131, 255, 0, 255);
        }
        if(!answerCorrect)
        {
            rightWrongText.text = "�����������";
            rightWrongText.color = new Color32(255, 17, 0, 255);
        }

        yield return new WaitForSeconds(3);
        rightWrongText.alpha = 0;
    }
}