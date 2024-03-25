using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_6 : MonoBehaviour
{
    public static Data_6 Instance;
    public string[] solutions;
    public string[] validWords;
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
        LoadData();
    }

    public void LoadData()
    {
        TextAsset textFile = Resources.Load("word_6") as TextAsset;
        solutions = textFile.text.Split("\n");
        textFile = Resources.Load("word_6_common") as TextAsset;
        validWords = textFile.text.Split("\n");
    }
}
