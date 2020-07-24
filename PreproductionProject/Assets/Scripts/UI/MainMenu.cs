using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, newGame, extras, options, credits;

    private int levelToLoad = 0;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Destroy(GameObject.Find("GameDependencies"));
    }

    public void OnLevelButtonClick(int level)
    {
        levelToLoad = level;
        StartCoroutine(LoadLevelRoutine());
    }

    public void M_NewGame()
    {
        mainMenu.SetActive(false);
        newGame.SetActive(true);
    }    
    public void M_Extras()
    {
        mainMenu.SetActive(false);
        extras.SetActive(true);

    }
    
    public void M_Options()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
    }

    public void M_Credits()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
    }


    public void M_Back()
    {
        mainMenu.SetActive(true);
        newGame.SetActive(false);
        extras.SetActive(false);
        options.SetActive(false);
        credits.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private IEnumerator LoadLevelRoutine()
    {
        yield return SceneManager.LoadSceneAsync(levelToLoad);
    }
}
