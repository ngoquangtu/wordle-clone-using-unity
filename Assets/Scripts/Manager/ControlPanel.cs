using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;




    public void exitShopPanel()
    {
        shopPanel.SetActive(false);
    }
    public void enterShopPanel()
    {
        shopPanel.SetActive(true);
    }
}
