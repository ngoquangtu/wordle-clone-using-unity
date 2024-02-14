using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_5 : MonoBehaviour
{
    public static Data_5 Instance;
    public string[] solutions;
    public string[] validWords;
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
        LoadData();
    }
    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        TextAsset textFile = Resources.Load("official_wordle_common") as TextAsset;
        solutions = textFile.text.Split("\n");
        textFile = Resources.Load("official_wordle_all") as TextAsset;
        validWords = textFile.text.Split("\n");
    }
}
