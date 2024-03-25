using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinReward : MonoBehaviour
{
    public static CoinReward CoinRewardInstance;
    [SerializeField] private GameObject pileOfCoins;
    [SerializeField] private RectTransform shopRect;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private Vector2[] initialPos;
    [SerializeField] private Quaternion[] initialRotation;
    [SerializeField] private int coinsAmount;
    void Start()
    {

        initialPos = new Vector2[coinsAmount];
        initialRotation = new Quaternion[coinsAmount];

        for (int i = 0; i < Mathf.Min(pileOfCoins.transform.childCount,coinsAmount); i++)
        {
            initialPos[i] = pileOfCoins.transform.GetChild(i).position;
            initialRotation[i] = pileOfCoins.transform.GetChild(i).rotation;
        }

        CoinRewardInstance = this;
    }


    public void CountCoins()
    {
        pileOfCoins.SetActive(true);
        var delay = 0f;
        for (int i = 0; i < Mathf.Min(pileOfCoins.transform.childCount, coinsAmount); i++)
        {
            pileOfCoins.transform.GetChild(i).position = initialPos[i];
            pileOfCoins.transform.GetChild(i).rotation = initialRotation[i];

            pileOfCoins.transform.GetChild(i).DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);
            pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(new Vector3(-87f,-52f,0), 0.8f)
                .SetDelay(delay + 0.5f).SetEase(Ease.InBack);


            pileOfCoins.transform.GetChild(i).DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f)
                .SetEase(Ease.Flash);


            pileOfCoins.transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(delay + 1.5f).SetEase(Ease.OutBack);

            delay += 0.1f;

            counter.transform.parent.GetChild(0).transform.DOScale(1.1f, 0.1f).SetLoops(10, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(1.2f);
        }
        
        CoinManager.instance.AddCoinAds(100);

    }
}