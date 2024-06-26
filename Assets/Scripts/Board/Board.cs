﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Board : MonoBehaviour
{
    private static readonly KeyCode[] SUPPORTED_KEYS = new KeyCode[] {
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y,
        KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P, KeyCode.A, KeyCode.S,
        KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K,
        KeyCode.L, KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B,
        KeyCode.N, KeyCode.M,
    };
    public static Board Instance;

    public Row[] rows;
    public int rowIndex;
    public int columnIndex;

    private string[] solutions;
    private string[] validWords;
    private string[] meaningWords;
    [HideInInspector]
    public string word;

    [Header("Tiles")]
    public Tile.State emptyState;
    public Tile.State occupiedState;
    public Tile.State correctState;
    public Tile.State wrongSpotState;
    public Tile.State incorrectState;

    [Header("UI")]
    public GameObject tryAgainButton;
    public GameObject newWordButton;
    public GameObject invalidWordText;
    public TextMeshProUGUI meaningWordText;
    [HideInInspector] public  Row currentRow;
    public KeyBoardColor keyBoardColor;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        rows = GetComponentsInChildren<Row>();
    }

    private void Start()
    {
        LoadData();
        NewGame();
    }

    private void LoadData()
    {
            validWords = Data_5.Instance.validWords;
            solutions = Data_5.Instance.solutions;
            meaningWords=Data_5.Instance.meaningWords;
    }

    public void NewGame()
    {
        ClearBoard();
        keyBoardColor.ClearColor();
        SetRandomWord();
        meaningWordText.gameObject.SetActive(false);
        enabled = true;
    }

    public void TryAgain()
    {
        ClearBoard();
        meaningWordText.gameObject.SetActive(false);
        enabled = true;
    }

    private void SetRandomWord()
    {
        string savedWord = PlayerPrefs.GetString(PrefConst.saveWord, ""); 

        do
        {
            int randomIndex = Random.Range(0, solutions.Length);
            word = solutions[randomIndex];
            word = word.ToLower().Trim();
            meaningWordText.text = meaningWords[randomIndex];
            Debug.Log(word);
        } while (word == savedWord);
    }

    private void Update()
    {
        currentRow = rows[rowIndex];
    }

    public void SubmitRow(Row row)
    {
        if (!IsValidWord(row.Word))
        {
            invalidWordText.SetActive(true);
            SoundManager.instance.playSound(SoundManager.instance.invalidSound);
            return;
        }

        string remaining = word;
        keyBoardColor.Colorize(remaining, row.Word);
        // check correct/incorrect letters first
        for (int i = 0; i < row.tiles.Length; i++)
        {
            Tile tile = row.tiles[i];

            if (tile.letter == word[i])
            {
                tile.SetState(correctState);
                

                remaining = remaining.Remove(i, 1);
                remaining = remaining.Insert(i, " ");
            }
            else if (!word.Contains(tile.letter))
            {
                tile.SetState(incorrectState);
            }
        }
        for (int i = 0; i < row.tiles.Length; i++)
        {
            Tile tile = row.tiles[i];

            if (tile.state != correctState && tile.state != incorrectState)
            {
                if (remaining.Contains(tile.letter))
                {
                    tile.SetState(wrongSpotState);

                    int index = remaining.IndexOf(tile.letter);
                    remaining = remaining.Remove(index, 1);
                    remaining = remaining.Insert(index, " ");
                }
                else
                {
                    tile.SetState(incorrectState);
                }
            }
        }
        if (HasWon(row)) 
        {
            SoundManager.instance.playSound(SoundManager.instance.correctwordSource);
            PlayerPrefs.SetString(PrefConst.saveWord, word);
            meaningWordText.gameObject.SetActive(true);
            enabled = false;
        }
        rowIndex++;
        columnIndex = 0;

        if (rowIndex >= rows.Length) {
            enabled = false;
        }
        if(rowIndex>=rows.Length && !enabled && !HasWon(row))
        {
            SoundManager.instance.playSound(SoundManager.instance.loseSound);
        }
    }
    public bool IsValidWord(string word)
    {
        for (int i = 0; i < validWords.Length; i++)
        {
            if (validWords[i] == word) {
                
                return true;
            }
        }
        return false;
    }
    public bool HasWon(Row row)
    {
        for (int i = 0; i < row.tiles.Length; i++)
        {
            if (row.tiles[i].state != correctState) {

                return false;
            }
        }
        return true;
    }

    public void ClearBoard()
    {
        for (int row = 0; row < rows.Length; row++)
        {
            for (int col = 0; col < rows[row].tiles.Length; col++)
            {
                rows[row].tiles[col].SetLetter('\0');
                rows[row].tiles[col].SetState(emptyState);
            }
        }
        rowIndex = 0;
        columnIndex = 0;

    }

    private void OnEnable()
    {
        tryAgainButton.SetActive(false);
        newWordButton.SetActive(false);
    }

    private void OnDisable()
    {
        tryAgainButton.SetActive(true);
        newWordButton.SetActive(true);
    }

}
