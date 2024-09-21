using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class MathQuiz : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI exampleText;
    [SerializeField] private FadingText fadingText;
    [SerializeField] private InputField resultText;
    [SerializeField] private Button checkButton;
    [SerializeField] private Button backToMenuButton;
    [HideInInspector] public int firstNumber;
    [HideInInspector] public int secondNumber;
    [HideInInspector] public Operation randomOperation;
    private Difficulty difficulty;
    private string example;
    public int result { get; set; }
    private Score score;
    private bool isAnswerCorrect;
    private ExampleCache cache = new ExampleCache();

    //skipping example(maybe create script smth like "RevAd"?)
    public bool ExampleSkipped { get; set; }

    [Header("Audio")]
    [SerializeField] private AudioClip ApproveSound;
    [SerializeField] private AudioClip WrongSound;

    //Advertisment
    private int correctAnswerCounter;


    private void Start()
    {
        difficulty = GetComponent<Difficulty>();
        difficulty.PreviousDifficulty = DifficultyEnum.VeryEasy;
        GetRandomOperation();
        WriteExample();
        checkButton.onClick.AddListener(() => ClickCheckButton());
        score = GameObject.Find("Score").GetComponent<Score>();
        backToMenuButton.onClick.AddListener(() => ReturnToMainMenu());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            ClickCheckButton();
        }

        if (difficulty.DifficultyChanged)
        {
            WriteExample();
            difficulty.DifficultyChanged = false;
        }
    }

    private void ClickCheckButton()
    {
        int userAnswer;
        int difficultyIndex = (int)Difficulty.currentDifficulty;

        if (int.TryParse(resultText.text, out userAnswer))
        {
            if (userAnswer == result)
            {
                // Mark the example as solved
                cache.exampleSolved[difficultyIndex] = true;
                isAnswerCorrect = true;

                ShowFullscreenAd();

                SoundManager.Instance.PlaySound(ApproveSound);

                // Generate a new example after solving
                score.AddScore(this);
                GetRandomOperation();
                WriteExample();

                StartCoroutine(fadingText.WriteRightOrWrong(isAnswerCorrect));
                Debug.Log("Correct answer!");
            }
            else
            {
                // Code to execute if the answer is incorrect
                isAnswerCorrect = false;

                SoundManager.Instance.PlaySound(WrongSound);

                StartCoroutine(fadingText.WriteRightOrWrong(isAnswerCorrect));
                Debug.Log("Incorrect answer!");
            }
        }
        else
        {
            Debug.LogWarning("Write answer first");
        }
    }

    private void ShowFullscreenAd()
    {
        correctAnswerCounter++;

        if (correctAnswerCounter >= 25 && Difficulty.currentDifficulty == DifficultyEnum.VeryEasy)
        {
            YandexGame.FullscreenShow();
            correctAnswerCounter = 0;
        }
        else if (correctAnswerCounter >= 10 && Difficulty.currentDifficulty == DifficultyEnum.Easy)
        {
            YandexGame.FullscreenShow();
            correctAnswerCounter = 0;
        }
        else if (correctAnswerCounter >= 5 && 
            (Difficulty.currentDifficulty == DifficultyEnum.Normal ||
             Difficulty.currentDifficulty == DifficultyEnum.Hard ||
             Difficulty.currentDifficulty == DifficultyEnum.VeryHard))
        {
            YandexGame.FullscreenShow();
            correctAnswerCounter = 0;
        }
    }

    private void WriteExample()
    {
        int difficultyIndex = (int)Difficulty.currentDifficulty;

        // Check if an example has been generated and not solved yet
        if (ExampleSkipped)
        {
            exampleText.text = GenerateExample();
            cache.exampleGenerated[difficultyIndex] = true; // Mark that an example has been generated
        }
        else if (cache.exampleGenerated[difficultyIndex] && !cache.exampleSolved[difficultyIndex])
        {
            exampleText.text = KeepPreviousExample();
        }
        else
        {
            exampleText.text = GenerateExample();
            cache.exampleGenerated[difficultyIndex] = true; // Mark that an example has been generated
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

        int difficultyIndex = (int)Difficulty.currentDifficulty;

        cache.exampleSolved[difficultyIndex] = false;
    }


    #region rewarded ad
    private void SkipExample()
    {
        ExampleSkipped = true;
        score.AddScore(this);
        int previousResult = result;
        GetRandomOperation();
        WriteExample();
        StartCoroutine(fadingText.WriteRightOrWrong(this, previousResult));
    }

    // Subscribe to the ad opening event in OnEnable
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    // Unsubscribe from the ad opening event in OnDisable
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    // Method subscribed to receive a reward
    private void Rewarded(int id)
    {
        if (id == 1)
            SkipExample();
    }

    // Method for calling video ads
    public void ExampleOpenRewardAd(int id)
    {
        // Call the method to open video ads
        YandexGame.RewVideoShow(id);
    }
    #endregion
    
}
