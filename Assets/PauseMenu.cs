using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isActive;
    public GameObject pauseButton;
    
    void Start()
    {
        Image pausePanelImage = pauseMenu.GetComponent<Image>();
        pausePanelImage.color = Color.white;
        isActive = pauseMenu.activeInHierarchy;
    }

    void Update()
    {
        isActive = pauseMenu.activeInHierarchy;
        if (Input.GetKeyDown("escape")) 
        {
            if (isActive)
            {
                pauseMenu.SetActive(false);
                pauseButton.SetActive(true);
            }
            else 
            {
                pauseMenu.SetActive(true);
                pauseButton.SetActive(false);
            } 
        }
    }
}
