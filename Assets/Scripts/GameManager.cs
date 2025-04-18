using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Whisper.Samples;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject _animalPrefab;
    [SerializeField] private AnimalConfig[] _animals;
    public Transform spawnPoint;
    public GameObject player;
    public float spawnInterval;
    public MicrophoneDemo microphoneDemo;

    private int currentAnimal;
    public string _currentAnimalName;

    private bool isCorrectAnswer = true;

    [Header("UI")]
    [SerializeField] private Image[] _healthImage;
    [SerializeField] private Sprite _hitHealth;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private WinPopup winPopup;
    [SerializeField] private LosePopup losePopup;
    [SerializeField] private GameObject _correctText;
    [SerializeField] private GameObject _wrongText;
    [SerializeField] private Button _pause;
    [SerializeField] private PausePopup pausePopup;

    [SerializeField] private TMP_Text _resultScore;
    [SerializeField] private TMP_Text _resultWords;

    int score;
    int health = 5;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentAnimal = 0;
        StartGame();
        microphoneDemo.OnResultReceived += ResultReceived;

        _pause.onClick.AddListener(Pause);
    }

    private void OnDestroy()
    {
        microphoneDemo.OnResultReceived -= ResultReceived;

        _pause.onClick.RemoveListener(Pause);
    }
    private void StartGame()
    {
        _scoreText.text = $"0/{_animals.Length}";
        score = 0;
        Time.timeScale = 1;
        StartCoroutine(Game());
        Recording();
    }

    private void StopGame()
    {
        Time.timeScale = 0;
        StopCoroutine(Game());
    }


    private IEnumerator Game()
    {
        while (true)
        {
            GameObject newAnimal = Instantiate(_animalPrefab, spawnPoint.position, spawnPoint.rotation);
            newAnimal.GetComponent<AnimalController>().SetImage(_animals[currentAnimal].image);

            _currentAnimalName = _animals[currentAnimal].name; 
            currentAnimal++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void ResultReceived(string result)
    {
        isCorrectAnswer = ContainsIgnoringCaseAndPunctuation(_currentAnimalName, result);
        Debug.Log(isCorrectAnswer);
    }

    public void Touched()
    {
        if(isCorrectAnswer)
        {
            score++;
            _scoreText.text = $"{score}/{_animals.Length}";
            
            if (score == _animals.Length - 1)
            {
                int newScore =  1000 + PlayerPrefs.GetInt("Score");
                PlayerPrefs.SetInt("Score", newScore);

                winPopup.Show();
                StopGame();
            }
            else
            {
                _correctText.SetActive(true);
                StartCoroutine(TextTimer());
                Recording();
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("Vibration") == 1)
            {
                Handheld.Vibrate();
            }
            health--;
            _healthImage[health].sprite = _hitHealth;
            if (health == 0)
            {
                losePopup.Show();

                int newScore = score * 10;
                _resultScore.text = newScore.ToString();
                _resultWords.text = $"You guessed {score} out of 197 animals. Don’t be discouraged – every animal you guess brings you closer to perfection.";
                newScore += PlayerPrefs.GetInt("Score");
                PlayerPrefs.SetInt("Score", newScore);

               

                StopGame();
            }
            else
            {
                StartCoroutine(TextTimer());
                _wrongText.SetActive(true);
                Recording();
            }
        }
    }

    private void Recording()
    {
        Debug.Log("StartRecording");
        microphoneDemo.ChangeRecordState();
        StartCoroutine(RecordTimer());
    }

    private IEnumerator RecordTimer()
    {
        yield return new WaitForSeconds(5);
        microphoneDemo.ChangeRecordState();
        Debug.Log("StopRecording");
    }

    private IEnumerator TextTimer()
    {
        yield return new WaitForSeconds(2);
        _wrongText.SetActive(false);
        _correctText.SetActive(false);
    }

    static bool ContainsIgnoringCaseAndPunctuation(string name, string text)
    {
        // Видаляємо всі знаки пунктуації та пробіли
        string cleanName = Regex.Replace(name, @"[\p{P}\s]+", "");
        string cleanText = Regex.Replace(text, @"[\p{P}\s]+", "");

        // Порівнюємо без урахування регістру
        return cleanText.IndexOf(cleanName, StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private void Pause()
    {
        Time.timeScale = 0;
        pausePopup.Show();
    }
}
