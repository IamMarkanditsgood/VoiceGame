using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.UI;

public class Fact : MonoBehaviour
{
    [Serializable] 
    public class FactData
    {
        public Sprite animalImage;
        public string name;
        public string fact;
    }

    [SerializeField] private FactData[] _facts;
    [SerializeField] private GameObject _view;
    [SerializeField] private Button _backButton;
    [SerializeField] private TMP_Text _timer;

    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _fact;
    private const string LastClaimTimeKey = "LastClaimTime";
    private TimeSpan rewardCooldown = TimeSpan.FromHours(24);
    private void Start()
    {
        _backButton.onClick.AddListener(BackButton);
    }

    private void OnDestroy()
    {
        _backButton.onClick.RemoveListener(BackButton);
    }
    public void Show()
    {
        DateTime lastClaimTime = GetLastClaimTime();
        DateTime nextClaimTime = lastClaimTime + rewardCooldown;
        TimeSpan timeRemaining = nextClaimTime - DateTime.Now;

        if (timeRemaining <= TimeSpan.Zero)
        {
            _view.SetActive(true);
            GiveFact();
        }
    }
    private void GiveFact()
    {
        int randomIndex = UnityEngine.Random.Range(0, _facts.Length);
        FactData randomFact = _facts[randomIndex];
        _image.sprite = randomFact.animalImage;
        _name.text = randomFact.name;
        _fact.text = randomFact.fact;

        PlayerPrefs.SetString(LastClaimTimeKey, DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    private void BackButton()
    {
        _view.SetActive(false);
    }
    private void Update()
    {
        if (PlayerPrefs.HasKey(LastClaimTimeKey))
        {
            DateTime lastClaimTime = GetLastClaimTime();
            DateTime nextClaimTime = lastClaimTime + rewardCooldown;
            TimeSpan timeRemaining = nextClaimTime - DateTime.Now;
            _timer.text = $"{timeRemaining.Hours:D2}H";
        }
        else
        {
            _timer.text = "0H";
        }
    }

    private DateTime GetLastClaimTime()
    {
        string lastClaimStr = PlayerPrefs.GetString(LastClaimTimeKey, string.Empty);
        return string.IsNullOrEmpty(lastClaimStr) ? DateTime.MinValue : DateTime.Parse(lastClaimStr);
    }
}
