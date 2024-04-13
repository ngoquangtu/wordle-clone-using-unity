
using UnityEngine;
using TMPro;
public class HintAds : MonoBehaviour, IObserver
{
    [SerializeField] private int priceMeanWords = -100;
    [SerializeField] private TextMeshProUGUI countMeaning;
    [SerializeField] private TextMeshProUGUI countChar;

    private void Start()
    {
        ShopManager.Instance.RegisterObserver(this); 
        UpdateCounts(ShopManager.Instance.hintMeaningWord, ShopManager.Instance.hintChar); 
    }

    public void ShowMeaningWord()
    {
        if (ShopManager.Instance.hintMeaningWord > 0 && !Board.Instance.meaningWordText.gameObject.activeSelf)
        {
            ShopManager.Instance.hintMeaningWord--;
            PlayerPrefs.SetInt(PrefConst.hintMeaningWord, ShopManager.Instance.hintMeaningWord);
            Board.Instance.meaningWordText.gameObject.SetActive(true);
            UpdateText(ShopManager.Instance.hintMeaningWord, ShopManager.Instance.hintChar);
        }
    }

    public void UpdateText(int hintMeaningWord, int hintChar)
    {
        UpdateCounts(hintMeaningWord, hintChar);
    }

    private void UpdateCounts(int hintMeaningWord, int hintChar)
    {
        if (countMeaning != null)
        {
            countMeaning.text = hintMeaningWord.ToString();
        }
        if (countChar != null)
        {
            countChar.text = hintChar.ToString();
        }
    }

    public void UpdateTextChar(int hintChar)
    {
        return;
    }
}

