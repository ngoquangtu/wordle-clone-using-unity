using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ControlScene : MonoBehaviour
{
    public void LoadClassicScene()
    {
        SceneManager.LoadScene(1);
    }
    public void BackMainScene()
    {
        SceneManager.LoadScene(0);
    }
}
