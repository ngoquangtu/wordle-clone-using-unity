using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public AdsInitializer adsInitializer;
    public BannerAds bannerAds;
    public InterstitialAds interstitialAds;
    public RewardedAds rewardedAds;

    public static AdsManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance!=null && Instance!=this)
         {
            Destroy(gameObject);
        }        Instance = this;
        DontDestroyOnLoad(gameObject);
        bannerAds.LoadBanner();
        interstitialAds.LoadAd();
        rewardedAds.LoadAd();
    }
}
