using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    public GameObject view;

    public Button restartButton;
    public Button homeButton;

    private void Start()
    {
        restartButton.onClick.AddListener(Restart);
        homeButton.onClick.AddListener(Home);
    }
    private void OnDestroy()
    {
        restartButton.onClick.RemoveListener(Restart);
        homeButton.onClick.RemoveListener(Home);
    }
    public void Show()
    {
        view.SetActive(true);
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
