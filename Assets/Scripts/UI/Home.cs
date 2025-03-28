using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    [SerializeField] private GameObject _view;

    [SerializeField] Button _playButton;
    [SerializeField] Button _gallertButton;
    [SerializeField] Button _settingsButton;
    [SerializeField] Button _factButton;

    [SerializeField] Gallery _gallery;
    [SerializeField] Settings _settings;
    [SerializeField] Fact _fact;

    [SerializeField] TMP_Text _score;

   
    public void Show()
    {
        _score.text = PlayerPrefs.GetInt("Score").ToString();
        _view.SetActive(true);
    }

    private void Start()
    {
        Show();
        _playButton.onClick.AddListener(Play);
        _gallertButton.onClick.AddListener(Gallery);
        _settingsButton.onClick.AddListener(Settings);
        _factButton.onClick.AddListener(Fact);
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(Play);
        _gallertButton.onClick.RemoveListener(Gallery);
        _settingsButton.onClick.RemoveListener(Settings);
        _factButton.onClick.RemoveListener(Fact);
    }

    private void Play()
    {
        SceneManager.LoadScene("Game");
    }
    private void Gallery()
    {
        _view.SetActive(false);
        _gallery.Show();
    }
    private void Settings()
    {
        _settings.Show();
    }
    private void Fact()
    {
        _fact.Show();
    }
}