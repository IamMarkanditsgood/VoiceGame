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
    [SerializeField] private Button _sounds;

    [SerializeField] private TMP_InputField _name;

    [SerializeField] private Sprite _offV;
    [SerializeField] private Sprite _onV;
    [SerializeField] private Sprite _offS;
    [SerializeField] private Sprite _onS;

    private void Start()
    {
        _close.onClick.AddListener(Close);
        _vibration.onClick.AddListener(Vibration);
        _sounds.onClick.AddListener(Sounds);
        _avatar.onClick.AddListener(_avatarManager.PickFromGallery);

    }

    private void OnDestroy()
    {
        _close.onClick.RemoveListener(Close);
        _vibration.onClick.RemoveListener(Vibration);
        _sounds.onClick.AddListener(Sounds);
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
            _vibration.GetComponent<Image>().sprite = _offV;
        }
        else
        {
            _vibration.GetComponent<Image>().sprite = _onV;
        }

        int sounds = PlayerPrefs.GetInt("Sounds");
        if (sounds == 0)
        {
            _sounds.GetComponent<Image>().sprite = _offS;
        }
        else
        {
            _sounds.GetComponent<Image>().sprite = _onS;
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
            _vibration.GetComponent<Image>().sprite = _onV;
        }
        else
        {
            PlayerPrefs.SetInt("Vibration", 0);
            _vibration.GetComponent<Image>().sprite = _offV;
        }
    }
    private void Sounds()
    {
        int sounds = PlayerPrefs.GetInt("Sounds");
        if (sounds == 0)
        {
            PlayerPrefs.SetInt("Sounds", 1);
            _sounds.GetComponent<Image>().sprite = _onS;
        }
        else
        {
            PlayerPrefs.SetInt("Sounds", 0);
            _sounds.GetComponent<Image>().sprite = _offS;
        }
    }
}
