using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private const string PAUSE_BUTTON = "Pause";
    private const string INVENTORY_BUTTON = "Inventory";
    private bool isPaused = false;
    public GameObject pausePanel;
    public GameObject inventoryPanel;
    public string mainMenu;
    public bool usingPausePanel;

    private void Start()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        usingPausePanel = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(PAUSE_BUTTON))
        {
            ChangePause(pausePanel);
        } else if (Input.GetButtonDown(INVENTORY_BUTTON))
        {
            ChangePause(inventoryPanel);
        }
    }

    public void ChangePause(GameObject panelToActivate)
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            ActivatePanelAndPause(panelToActivate);
        }
        else
        {
            TurnOffBothPanelsAndResume();
        }
    }

    private static void ActivatePanelAndPause(GameObject panelToActivate)
    {
        panelToActivate.SetActive(true);
        Time.timeScale = 0f;
    }

    private void TurnOffBothPanelsAndResume()
    {
        inventoryPanel.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

    public void SwitchPanels()
    {
        usingPausePanel = !usingPausePanel;
        if (usingPausePanel)
        {
            pausePanel.SetActive(true);
            inventoryPanel.SetActive(false);
        }
        else
        {
            inventoryPanel.SetActive(true);
            pausePanel.SetActive(false);
        }
    }
}
