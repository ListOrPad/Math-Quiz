using UnityEngine;
using UnityEngine.UI;
using YG;

public class Localization : MonoBehaviour
{
    [SerializeField] private Button ruLanguageButton;
    [SerializeField] private Button enLanguageButton;
    private string currentLang;

    private void Start()
    {
        currentLang = YandexGame.lang;
        UpdateLanguageButton();
    }

    private void UpdateLanguageButton()
    {
        if (currentLang == "en")
        {
            enLanguageButton.gameObject.SetActive(true);
            ruLanguageButton.gameObject.SetActive(false);
        }
        else
        {
            ruLanguageButton.gameObject.SetActive(true);
            enLanguageButton.gameObject.SetActive(false);
        }
    }

    //on language button click
    public void ChangeLanguageButton()
    {
        if (currentLang != YandexGame.lang)
        {
            currentLang = YandexGame.lang;
            UpdateLanguageButton();
        }
    }
}
