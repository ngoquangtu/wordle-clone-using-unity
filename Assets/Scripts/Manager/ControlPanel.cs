using DG.Tweening;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject dailyPanel;

    public void exitPanel(GameObject panel)
    {
        panel.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).OnComplete(() => panel.SetActive(false));
    }
    public void enterPanel(GameObject panel)
    {
        panel.transform.localScale = Vector3.zero;
        panel.SetActive(true);
        panel.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }
    public void ConfirmExitGame()
    {
        Application.Quit();
    }
}
