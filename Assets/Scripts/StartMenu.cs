using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject gameManager;
    public GameObject restartBtn;
    public GameObject stMenu;
    public GameObject HUD1;
    public GameObject HUD2;

    private void Awake()
    {
        HUD1.SetActive(false);
        HUD2.SetActive(false);
        stMenu.SetActive(true);
        player.SetActive(false);
        gameManager.SetActive(false);
        Time.timeScale = 0;
    }
    public void StartGame()
    {
        restartBtn.SetActive(false);
        HUD1.SetActive(false);
        HUD2.SetActive(false);
        HUD1.SetActive(true);
        HUD2.SetActive(true);
        stMenu.SetActive(false);
        player.SetActive(false);
        player.SetActive(true);
        gameManager.SetActive(false);
        gameManager.SetActive(true);
        gameManager.GetComponent<SpawnParticles>().startTime = Time.time;
        Time.timeScale = 1;
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    public void Restart() 
    {
        SceneManager.LoadScene(0);
    }


}
