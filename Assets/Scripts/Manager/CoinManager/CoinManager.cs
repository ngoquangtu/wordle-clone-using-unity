using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Drawing;
using System.Collections;


public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    [SerializeField] private TextMeshProUGUI textCoin;
    private int initCoin = 100;
    private int targetCoin = 0;
    private bool isAnimating = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey(PrefConst.coin))
        {
            PlayerPrefs.SetInt(PrefConst.coin, initCoin);
        }
        targetCoin = PlayerPrefs.GetInt(PrefConst.coin);
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        textCoin.text = targetCoin.ToString();
    }

    public void AddCoinAds(int coins)
    {
        targetCoin += coins;
        PlayerPrefs.SetInt(PrefConst.coin, targetCoin);

        if (!isAnimating)
        {
            StartCoroutine(AnimateCoinText(coins));
        }
    }

    private IEnumerator AnimateCoinText(int coins)
    {
        isAnimating = true;

        int startCoin = targetCoin - coins; // Giá trị ban đầu của coin trước khi thay đổi
        float animationDuration = 1.0f; // Thời gian của animation (tính bằng giây)
        float elapsedTime = 0f;
        float delayTime = 1.5f;
        yield return new WaitForSeconds(delayTime);

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            int currentCoin = (int)Mathf.Lerp(startCoin, targetCoin, elapsedTime / animationDuration);
            textCoin.text = currentCoin.ToString();
            yield return null;
        }

        textCoin.text = targetCoin.ToString(); // Đảm bảo hiển thị số coin đúng khi kết thúc animation
        isAnimating = false;
    }
}
