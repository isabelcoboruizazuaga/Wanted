using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private bool gameOverMenu=false;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    // Start is called before the first frame update
    void Start()
    {
        if (gameOverMenu)
        {
            scoreTxt.text = "Score: " + GameLogic.score;
        }
        
    }


    public void PlayGame()
    {
        Time.timeScale = 1;
        GameLogic.score = -1;
        SceneManager.LoadScene("GameScene");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetEasy()
    {
        GameLogic.npcNumber = 10;
        PlayGame();
    }
    public void SetNormal()
    {
        GameLogic.npcNumber = 30;
        PlayGame();
    }

    public void SetHard()
    {
        GameLogic.npcNumber = 50;
        PlayGame();
    }

    public void SetHell()
    {
        GameLogic.npcNumber = 100;
        PlayGame();
    }

}
