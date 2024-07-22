using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    static public bool isFullScrean;
    static public int rezIndex = -1;
    public void SceneChange(string name)
    {
        if (name == "Main Menu") 
        {
            isFullScrean = MainMenu.isFullScreanVal;
            rezIndex = MainMenu.rezIndex;
        }
        SceneManager.LoadScene(name);

    }
}
