using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private const string STARTING_SCENE = "OpeningCutscene";

    public void NewGame()
    {
        SceneManager.LoadScene(STARTING_SCENE);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
