using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyGift : MonoBehaviour
{
    private int LastDate;
    [SerializeField] private int Day_1;
    public GameObject OFF_1;
    [SerializeField] private GameObject ACTIVE_1;
    [SerializeField] private GameObject CHECK_1;

    private void Start()
    {
        if(!PlayerPrefs.HasKey(PrefConst.LastDate))
        {
            PlayerPrefs.SetInt(PrefConst.LastDate, System.DateTime.Now.Day);
        }
        Day_1 = PlayerPrefs.GetInt("Day_1");
        
        LastDate = PlayerPrefs.GetInt(PrefConst.LastDate);
        Reward();
        if(LastDate!=System.DateTime.Now.Day)
        {
            if(Day_1==0)
            {
                Day_1 = 1;
            }
            Reward();
        }
    }
    public void Reward()
    {
        if(Day_1 == 0)
        {
            OFF_1.SetActive(true);
            ACTIVE_1.SetActive(false);
            CHECK_1.SetActive(false);
        }
        if(Day_1==1)
        {
            ACTIVE_1.SetActive(true);
            OFF_1.SetActive(false);
            CHECK_1.SetActive(false);
        }
        if(Day_1 == 2)
        {
            CHECK_1.SetActive(true);
            OFF_1.SetActive(false);
            ACTIVE_1.SetActive(false);
        }
    }
    public void GetReward()
    {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt(PrefConst.LastDate,LastDate);
        Day_1 = 2;
        PlayerPrefs.SetInt("Day_1", 2);
        Reward();

    }
}
