using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject _view;

    [SerializeField] private AvatarManager _avatarManager;

    [SerializeField] private Button _close;
    [SerializeField] private Button _avatar;
    [SerializeField] private Button _vibration;

    [SerializeField] private TMP_InputField _name;

    [SerializeField] private Sprite _off;
    [SerializeField] private Sprite _on;

    private void Start()
    {
        _close.onClick.AddListener(Close);
        _vibration.onClick.AddListener(Vibration);
        _avatar.onClick.AddListener(_avatarManager.PickFromGallery);

    }

    private void OnDestroy()
    {
        _close.onClick.RemoveListener(Close);
        _vibration.onClick.RemoveListener(Vibration);
        _avatar.onClick.RemoveListener(_avatarManager.PickFromGallery);
    }

    public void Show()
    {
        _view.SetActive(true);
        _avatarManager.SetSavedPicture();
        _name.text = PlayerPrefs.GetString("Name", "Username");

        int vibration = PlayerPrefs.GetInt("Vibration");
        if (vibration == 0)
        {
            _vibration.GetComponent<Image>().sprite = _off;
        }
        else
        {
            _vibration.GetComponent<Image>().sprite = _on;
        }
    }

    private void Close()
    {
        PlayerPrefs.SetString("Name", _name.text);
        _view.SetActive(false);
    }

    private void Vibration()
    {
        int vibration = PlayerPrefs.GetInt("Vibration");
        if (vibration == 0)
        {
            PlayerPrefs.SetInt("Vibration",1);
            _vibration.GetComponent<Image>().sprite = _on;
        }
        else
        {
            PlayerPrefs.SetInt("Vibration", 0);
            _vibration.GetComponent<Image>().sprite = _off;
        }
    }
}
