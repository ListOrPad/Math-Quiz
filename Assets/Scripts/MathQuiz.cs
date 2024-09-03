using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MathQuiz : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI exampleText;
    [SerializeField] private TMP_InputField resultText;
    [SerializeField] private Button checkButton;
    [HideInInspector] public int firstNumber;
    [HideInInspector] public int secondNumber;
    [HideInInspector] public Operation randomOperation;
    private int result;
    private Score score;
    private bool isAnswerCorrect;
    private FadingText fadingText;

    private void Start()
    {
        GetRandomOperation();
        WriteExample();
        checkButton.onClick.AddListener(() => clickCheckButton());
        score = GameObject.Find("Score").GetComponent<Score>();
        fadingText = GameObject.Find("Right Wrong").GetComponent<FadingText>();
    }

    private void Update()
    {
        
    }

    private void clickCheckButton()
    {
        int userAnswer;

        if (int.TryParse(resultText.text, out userAnswer))
        {
            if (userAnswer == result)
            {
                // Code to execute if the answer is correct
                score.AddScore(this);
                GetRandomOperation();
                WriteExample();
                isAnswerCorrect = true;
                StartCoroutine(fadingText.WriteRightOrWrong(isAnswerCorrect));
                Debug.Log("Correct answer!");
            }
            else
            {
                // Code to execute if the answer is incorrect
                isAnswerCorrect = false;
                StartCoroutine(fadingText.WriteRightOrWrong(isAnswerCorrect));
                Debug.Log("Incorrect answer!");
            }
        }
        else
        {
            WriteExample();
        }
    }

    private void WriteExample()
    {
        //here should be logic of choosing difficulty
        exampleText.text = GenerateExample(Difficulty.VeryEasy);
    }

    private string GenerateExample(Difficulty difficulty)
    {
        if (difficulty == Difficulty.VeryEasy)
        {
            if (randomOperation == Operation.Addition)
            {
                firstNumber = Random.Range(0, 10);
                secondNumber = Random.Range(0, 10);

                result = firstNumber + secondNumber;
                return $"{firstNumber} + {secondNumber}";
            }
            else if (randomOperation == Operation.Subtraction)
            {
                do
                {
                    firstNumber = Random.Range(0, 10);
                    secondNumber = Random.Range(0, 10);
                }
                while (firstNumber < secondNumber);

                result = firstNumber - secondNumber;
                return $"{firstNumber} - {secondNumber}";
            }
            else if (randomOperation == Operation.Multiplication)
            {
                firstNumber = Random.Range(0, 10);
                secondNumber = Random.Range(0, 10);

                result = firstNumber * secondNumber;
                return $"{firstNumber} x {secondNumber}";
            }
            else if (randomOperation == Operation.Division)
            {
                System.Random random = new System.Random();
                firstNumber = random.Next(0, 2) * 5;
                secondNumber = 5;

                result = firstNumber / secondNumber;
                return $"{firstNumber} : {secondNumber}";
            }
        }
        else if (difficulty == Difficulty.Easy)
        {
            if (randomOperation == Operation.Addition)
            {
                firstNumber = Random.Range(10, 100);
                secondNumber = Random.Range(10, 100);

                result = firstNumber + secondNumber;
                return $"{firstNumber} + {secondNumber}";
            }
            else if (randomOperation == Operation.Subtraction)
            {
                do
                {
                    firstNumber = Random.Range(10, 100);
                    secondNumber = Random.Range(10, 100);
                }
                while (firstNumber < secondNumber);

                result = firstNumber - secondNumber;
                return $"{firstNumber} - {secondNumber}";
            }
            else if (randomOperation == Operation.Multiplication)
            {
                firstNumber = Random.Range(0, 10);
                secondNumber = Random.Range(10, 100);

                result = firstNumber * secondNumber;
                return $"{firstNumber} x {secondNumber}";
            }
            else if (randomOperation == Operation.Division)
            {
                System.Random random = new System.Random();
                // Generate a number in the range of 0 to 99 that is divisible by 5
                int multipleOfFive = random.Next(0, 20); // Generates a number between 0 and 19
                firstNumber = multipleOfFive * 5;
                secondNumber = 5;

                result = firstNumber / secondNumber;
                return $"{firstNumber} : {secondNumber}";
            }
        }
        return "something went wrong";
    }

    private void GetRandomOperation()
    {
        System.Random random = new System.Random();
        int randomValue = random.Next(0, 4); // Generates a number between 0 and 3
        randomOperation = (Operation)randomValue;
    }


}
