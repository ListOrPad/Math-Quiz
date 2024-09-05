using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MathQuiz : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI exampleText;
    [SerializeField] private FadingText fadingText;
    [SerializeField] private TMP_InputField resultText;
    [SerializeField] private Button checkButton;
    [SerializeField] private Button backToMenuButton;
    [HideInInspector] public int firstNumber;
    [HideInInspector] public int secondNumber;
    [HideInInspector] public Operation randomOperation;
    private Difficulty difficulty;
    private string example;
    private int result;
    private Score score;
    private bool isAnswerCorrect;
    private bool exampleWasSolved;
    private ExampleCache cache = new ExampleCache();

    private void Start()
    {
        difficulty = GetComponent<Difficulty>();
        exampleWasSolved = wasExampleSolved(true);
        GetRandomOperation();
        WriteExample();
        checkButton.onClick.AddListener(() => ClickCheckButton());
        score = GameObject.Find("Score").GetComponent<Score>();
        backToMenuButton.onClick.AddListener(() => ReturnToMainMenu());
    }

    private void Update()
    {
        if (difficulty.DifficultyChanged)
        {
            WriteExample();
            difficulty.DifficultyChanged = false;
        }
    }

    private void ClickCheckButton()
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
                exampleWasSolved = wasExampleSolved(true);
                Debug.Log("Correct answer!");
            }
            else
            {
                // Code to execute if the answer is incorrect
                isAnswerCorrect = false;
                StartCoroutine(fadingText.WriteRightOrWrong(isAnswerCorrect));
                exampleWasSolved = wasExampleSolved(false);
                Debug.Log("Incorrect answer!");
            }
        }
        else
        {
            Debug.LogWarning("Write answer first");
        }
    }

    private void WriteExample()
    {
        if(exampleWasSolved)
        {
            exampleText.text = GenerateExample();
        }
        else
        {
            exampleText.text = KeepPreviousExample();
        }
    }

    private string GenerateExample()
    {
        if (Difficulty.currentDifficulty == DifficultyEnum.VeryEasy)
        {
            if (randomOperation == Operation.Addition)
            {
                firstNumber = Random.Range(0, 10);
                secondNumber = Random.Range(0, 10);

                result = firstNumber + secondNumber;
                example = $"{firstNumber} + {secondNumber}";
                cache.CacheExample(example, result);
                return example;
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
                example = $"{firstNumber} - {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
            else if (randomOperation == Operation.Multiplication)
            {
                firstNumber = Random.Range(0, 10);
                secondNumber = Random.Range(0, 10);

                result = firstNumber * secondNumber;
                example = $"{firstNumber} x {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
            else if (randomOperation == Operation.Division)
            {
                System.Random random = new System.Random();
                firstNumber = random.Next(0, 2) * 5;
                secondNumber = 5;

                result = firstNumber / secondNumber;
                example = $"{firstNumber} : {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
        }
        else if (Difficulty.currentDifficulty == DifficultyEnum.Easy)
        {
            if (randomOperation == Operation.Addition)
            {
                firstNumber = Random.Range(10, 100);
                secondNumber = Random.Range(10, 100);

                result = firstNumber + secondNumber;
                example = $"{firstNumber} + {secondNumber}";
                cache.CacheExample(example, result);
                return example;
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
                example = $"{firstNumber} - {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
            else if (randomOperation == Operation.Multiplication)
            {
                firstNumber = Random.Range(0, 10);
                secondNumber = Random.Range(10, 100);

                result = firstNumber * secondNumber;
                example = $"{firstNumber} x {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
            else if (randomOperation == Operation.Division)
            {
                System.Random random = new System.Random();
                // Generate a number in the range of 0 to 99 that is divisible by 5
                int multipleOfFive = random.Next(0, 20); // Generates a number between 0 and 19
                firstNumber = multipleOfFive * 5;
                secondNumber = 5;

                result = firstNumber / secondNumber;
                example = $"{firstNumber} : {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
        }
        else if (Difficulty.currentDifficulty == DifficultyEnum.Normal)
        {
            if (randomOperation == Operation.Addition)
            {
                firstNumber = Random.Range(100, 1000);
                secondNumber = Random.Range(100, 1000);

                result = firstNumber + secondNumber;
                example = $"{firstNumber} + {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
            else if (randomOperation == Operation.Subtraction)
            {
                do
                {
                    firstNumber = Random.Range(100, 1000);
                    secondNumber = Random.Range(100, 1000);
                }
                while (firstNumber < secondNumber);

                result = firstNumber - secondNumber;
                example = $"{firstNumber} - {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
            else if (randomOperation == Operation.Multiplication)
            {
                firstNumber = Random.Range(0, 10);
                secondNumber = Random.Range(100, 1000);

                result = firstNumber * secondNumber;
                example = $"{firstNumber} x {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
            else if (randomOperation == Operation.Division)
            {
                System.Random random = new System.Random();
                // Generate a number in the range of 0 to 99 that is divisible by 5
                int multipleOfFive = random.Next(100, 200); // Generates a number between 100 and 199
                firstNumber = multipleOfFive * 5;
                secondNumber = 5;

                result = firstNumber / secondNumber;
                example = $"{firstNumber} : {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
        }
        else if (Difficulty.currentDifficulty == DifficultyEnum.Hard)
        {
            if (randomOperation == Operation.Addition)
            {
                firstNumber = Random.Range(1000, 10000);
                secondNumber = Random.Range(1000, 10000);

                result = firstNumber + secondNumber;
                example = $"{firstNumber} + {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
            else if (randomOperation == Operation.Subtraction)
            {
                do
                {
                    firstNumber = Random.Range(1000, 10000);
                    secondNumber = Random.Range(1000, 10000);
                }
                while (firstNumber < secondNumber);

                result = firstNumber - secondNumber;
                example = $"{firstNumber} - {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
            else if (randomOperation == Operation.Multiplication)
            {
                firstNumber = Random.Range(0, 10);
                secondNumber = Random.Range(1000, 10000);

                result = firstNumber * secondNumber;
                example = $"{firstNumber} x {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
            else if (randomOperation == Operation.Division)
            {
                System.Random random = new System.Random();
                // Generate a number in the range of 0 to 99 that is divisible by 5
                int multipleOfFive = random.Next(1000, 2000); // Generates a number between 1000 and 1999
                firstNumber = multipleOfFive * 5;
                secondNumber = 5;

                result = firstNumber / secondNumber;
                example = $"{firstNumber} : {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
        }
        else if (Difficulty.currentDifficulty == DifficultyEnum.VeryHard)
        {
            if (randomOperation == Operation.Addition)
            {
                firstNumber = Random.Range(10000, 100000);
                secondNumber = Random.Range(10000, 100000);

                result = firstNumber + secondNumber;
                example = $"{firstNumber} + {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
            else if (randomOperation == Operation.Subtraction)
            {
                do
                {
                    firstNumber = Random.Range(10000, 100000);
                    secondNumber = Random.Range(10000, 100000);
                }
                while (firstNumber < secondNumber);

                result = firstNumber - secondNumber;
                example = $"{firstNumber} - {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
            else if (randomOperation == Operation.Multiplication)
            {
                firstNumber = Random.Range(0, 10);
                secondNumber = Random.Range(10000, 100000);

                result = firstNumber * secondNumber;
                example = $"{firstNumber} x {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
            else if (randomOperation == Operation.Division)
            {
                System.Random random = new System.Random();
                // Generate a number in the range of 0 to 99 that is divisible by 5
                int multipleOfFive = random.Next(10000, 20000); // Generates a number between 10000 and 19999
                firstNumber = multipleOfFive * 5;
                secondNumber = 5;

                result = firstNumber / secondNumber;
                example = $"{firstNumber} : {secondNumber}";
                cache.CacheExample(example, result);
                return example;
            }
        }
        return "something went wrong";
    }
    
    private string KeepPreviousExample()
    {
        if (Difficulty.currentDifficulty == DifficultyEnum.VeryEasy)
        {
            result = cache.CachedResult[0];
            example = cache.CachedExample[0];
            return example;
        }
        else if (Difficulty.currentDifficulty == DifficultyEnum.Easy)
        {
            result = cache.CachedResult[1];
            example = cache.CachedExample[1];
            return example;
        }
        else if (Difficulty.currentDifficulty == DifficultyEnum.Normal)
        {
            result = cache.CachedResult[2];
            example = cache.CachedExample[2];
            return example;
        }
        else if (Difficulty.currentDifficulty == DifficultyEnum.Hard)
        {
            result = cache.CachedResult[3];
            example = cache.CachedExample[3];
            return example;
        }
        else if (Difficulty.currentDifficulty == DifficultyEnum.VeryHard)
        {
            result = cache.CachedResult[4];
            example = cache.CachedExample[4];
            return example;
        }
        return "Something went wrong";
    }

    private void GetRandomOperation()
    {
        System.Random random = new System.Random();
        int randomValue = random.Next(0, 4); // Generates a number between 0 and 3
        randomOperation = (Operation)randomValue;
    }

    private void ReturnToMainMenu()
    {
        difficulty.menuCanvas.transform.SetSiblingIndex(1);
        exampleWasSolved = wasExampleSolved(false);
    }

    private bool wasExampleSolved(bool solved)
    {
        if (solved && Difficulty.currentDifficulty == DifficultyEnum.VeryEasy)
        {
            cache.exampleWasSolved[0] = true;
            return true;
        }
        else if (solved && Difficulty.currentDifficulty == DifficultyEnum.Easy)
        {
            cache.exampleWasSolved[1] = true;
            return true;
        }
        else if (solved && Difficulty.currentDifficulty == DifficultyEnum.Normal)
        {
            cache.exampleWasSolved[2] = true;
            return true;
        }
        else if (solved && Difficulty.currentDifficulty == DifficultyEnum.Hard)
        {
            cache.exampleWasSolved[3] = true;
            return true;
        }
        else if (solved && Difficulty.currentDifficulty == DifficultyEnum.VeryHard)
        {
            cache.exampleWasSolved[4] = true;
            return true;
        }
        return false;
    }
}
