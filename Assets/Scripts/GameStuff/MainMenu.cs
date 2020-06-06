using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private const string STARTING_SCENE = "OpeningCutscene";

    public void NewGame()
    {
        //need to add wiping out inventory in here
        SceneManager.LoadScene(STARTING_SCENE);
    }

    public void ResumeGame()
    {
        //need to Resume from where the player was when they quit
        SceneManager.LoadScene(STARTING_SCENE);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
