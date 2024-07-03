using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Button : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("Stage");
    }
}
