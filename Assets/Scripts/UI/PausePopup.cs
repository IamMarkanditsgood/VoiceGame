using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePopup : MonoBehaviour
{
    public GameObject view;

    public Button continueButton;
    public Button restartButton;
    public Button homeButton;

    private void Start()
    {
        continueButton.onClick.AddListener(Continue);
        restartButton.onClick.AddListener(Restart);
        homeButton.onClick.AddListener(Home);
    }
    private void OnDestroy()
    {
        continueButton.onClick.RemoveListener(Continue);
        restartButton.onClick.RemoveListener(Restart);
        homeButton.onClick.RemoveListener(Home);
    }
    public void Show()
    {
        view.SetActive(true);
    }

    private void Continue()
    {
        Time.timeScale = 1.0f;
        view.SetActive(false);  
    }
    private void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    private void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
