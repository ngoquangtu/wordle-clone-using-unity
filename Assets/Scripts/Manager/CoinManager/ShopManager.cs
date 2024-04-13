using UnityEngine;
using TMPro;
using System.Collections.Generic;


public interface IObserver
{
    void UpdateText(int hintMeaningWord, int hintChar);
    void UpdateTextChar(int  hintChar);
}


public class Subject
{
    private List<IObserver> observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void NotifyObservers(int hintMeaningWord, int hintChar)
    {
        foreach (var observer in observers)
        {
            observer.UpdateText(hintMeaningWord, hintChar);
        }
    }
}

public class ShopManager : MonoBehaviour, IObserver
{
    public int hintMeaningWord;
    public int hintChar;
    public static ShopManager Instance;
    [SerializeField] private TextMeshProUGUI counthintMeanWord;
    [SerializeField] private TextMeshProUGUI counthintChar;

    private Subject subject = new Subject();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey(PrefConst.hintChar))
        {
            PlayerPrefs.SetInt(PrefConst.hintChar, 2);
            PlayerPrefs.SetInt(PrefConst.hintMeaningWord, 2);
        }
        hintMeaningWord = PlayerPrefs.GetInt(PrefConst.hintMeaningWord);
        hintChar = PlayerPrefs.GetInt(PrefConst.hintChar);
        UpdateText(hintMeaningWord,hintChar);
    }

    public void UpdateText(int hintMeaningWord, int hintChar)
    {
        counthintMeanWord.text = hintMeaningWord.ToString();
        counthintChar.text = hintChar.ToString();
    }
    public void UpdateTextChar( int hintChar)
    {
        counthintChar.text = hintChar.ToString();
    }

    public void RegisterObserver(IObserver observer)
    {
        subject.AddObserver(observer);
    }

    public void buyHintMeaningWord()
    {
        if (PlayerPrefs.GetInt(PrefConst.coin) >= 100)
        {
            StartCoroutine(CoinManager.instance.AnimateCoinText(-100, 0.0f));
            CoinManager.instance.targetCoin += -100;
  
            PlayerPrefs.SetInt(PrefConst.coin, CoinManager.instance.targetCoin);

            hintMeaningWord++;
            PlayerPrefs.SetInt(PrefConst.hintMeaningWord, hintMeaningWord);
            subject.NotifyObservers(hintMeaningWord, hintChar); 
            UpdateText(hintMeaningWord, hintChar);
        }
    }

    public void buyHintChar()
    {
        if (PlayerPrefs.GetInt(PrefConst.coin) >= 50)
        {
            StartCoroutine(CoinManager.instance.AnimateCoinText(-50, 0.0f));
            CoinManager.instance.targetCoin += -50;
            PlayerPrefs.SetInt(PrefConst.coin, CoinManager.instance.targetCoin);
            hintChar++;
            PlayerPrefs.SetInt(PrefConst.hintChar, hintChar);
            subject.NotifyObservers(hintMeaningWord, hintChar); 
            UpdateText(hintMeaningWord, hintChar);
        }
    }

    public void buyComboHint()
    {
        if (PlayerPrefs.GetInt(PrefConst.coin) >= 140)
        {
            StartCoroutine(CoinManager.instance.AnimateCoinText(-140, 0.0f));
            CoinManager.instance.targetCoin += -140;
            PlayerPrefs.SetInt(PrefConst.coin, CoinManager.instance.targetCoin);
            hintChar++;
            hintMeaningWord++;
            PlayerPrefs.SetInt(PrefConst.hintChar, hintChar);
            PlayerPrefs.SetInt(PrefConst.hintMeaningWord, hintMeaningWord);
            subject.NotifyObservers(hintMeaningWord, hintChar); 
            UpdateText(hintMeaningWord, hintChar);
        }
    }
}
