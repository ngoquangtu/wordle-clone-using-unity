using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class KeyBoardColor : MonoBehaviour,IObserver
{
    [Header("Elements")]
    public KeyBoard[] keys;

    private Dictionary<char, List<KeyBoard>> letterToKeys;
    [SerializeField] private Button newGameBtn;
    private HashSet<char> chosenLetters = new HashSet<char>();
    private HashSet<char> hashLength = new HashSet<char>();
    private int count;
    [SerializeField] private TextMeshProUGUI hintCharCount;

    public void Awake()
    {
        keys = GetComponentsInChildren<KeyBoard>();
        letterToKeys = new Dictionary<char, List<KeyBoard>>();

        foreach (KeyBoard key in keys)
        {
            char letter = key.GetLetter();
            if (!letterToKeys.ContainsKey(letter))
            {
                letterToKeys.Add(letter, new List<KeyBoard>());
            }
            letterToKeys[letter].Add(key);
        }
    }

    public void Colorize(string secretWord, string wordToCheck)
    {
        HashSet<char> secretChars = new HashSet<char>(secretWord);
        
        foreach (char guessChar in wordToCheck)
        {
            if (!letterToKeys.ContainsKey(guessChar))
            {
                continue;
            }

            List<KeyBoard> matchingKeys = letterToKeys[guessChar];
            foreach (KeyBoard key in matchingKeys)
            {
                if (secretChars.Contains(guessChar) && key.GetLetter() == secretWord[wordToCheck.IndexOf(guessChar)])
                {
                    key.SetColor(Color.green);
                }
                else if (secretChars.Contains(guessChar))
                {
                    key.SetColor(Color.yellow);
                }

            }
        }
    }
    public void hintchangeColor()
    {
        count = 0;
        char randomChar;
        foreach(char letter in Board.Instance.word)
        {
            hashLength.Add(letter);
        }
        if (ShopManager.Instance.hintChar>0)
        {
            ShopManager.Instance.hintChar--;
            PlayerPrefs.SetInt(PrefConst.hintChar, ShopManager.Instance.hintChar);
            UpdateTextChar(ShopManager.Instance.hintChar);

            do
            {
                int randomIndex = Random.Range(0, Board.Instance.word.Length);
                randomChar = Board.Instance.word[randomIndex];
                count++;
                if (count > hashLength.Count)
                {
                    break;
                }
            }
            while (chosenLetters.Contains(randomChar));
            chosenLetters.Add(randomChar);
            foreach (KeyBoard key in letterToKeys[randomChar])
            {
                key.SetColor(Color.green);
            }
        }
        else
        {
            return;
        }
    }
    public void ClearColor()
    {
        foreach (KeyBoard key in keys)
        {
            key.SetColor(Color.white);
        }
        chosenLetters.Clear();
        hashLength.Clear();
        count = 0;
    }

    public  void UpdateText(int hintMeaningWord, int hintChar)
    {
        return;
    }

    public void UpdateTextChar(int hintChar)
    {

            hintCharCount.text = hintChar.ToString();
   
    }
}