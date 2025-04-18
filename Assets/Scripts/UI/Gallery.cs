using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gallery : MonoBehaviour
{
    [SerializeField] private GameObject _view;
    [SerializeField] private AnimalConfig[] _animals;
    [SerializeField] private Button _close;
    [SerializeField] private Button _next;
    [SerializeField] private Button _prev;
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _animalPref;
    [SerializeField] private Home _home;
    [SerializeField] private TMP_Text _categoryText;

    private List<GameObject> _prefabs = new List<GameObject>();

    private List<int> animalIndex = new List<int>();

    [SerializeField] private Category[] _categories;
    private int _currentCategory;

    [Header("Popup")]
    [SerializeField] private GameObject _viewPopup;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Button _closePopup;

    private void Start()
    {
        _close.onClick.AddListener(Close);
        _next.onClick.AddListener(Next);
        _prev.onClick.AddListener(Previous);
        _closePopup.onClick.AddListener(ClosePopup);

        
    }

    private void OnDestroy()
    {
        _close.onClick.RemoveListener(Close);
        _next.onClick.RemoveListener(Next);
        _prev.onClick.RemoveListener(Previous);
        _closePopup.onClick.RemoveListener(ClosePopup);

        
    }
    public void Show()
    {
        _view.SetActive(true);
        ConfigScreen();
    }

    private void ConfigScreen()
    {
        animalIndex.Clear();

        _categoryText.text = _categories[_currentCategory].ToString();
        for (int i = 0; i < _prefabs.Count; i++)
        {
            int index = i;

            Button button = _prefabs[i].GetComponent<Button>();
            button.onClick.RemoveListener(() => OpenPopup(index));
        }
        foreach (var item in _prefabs)
        {
            Destroy(item.gameObject);
        }
        _prefabs.Clear();

        for (int i = 0; i < _animals.Length; i++)
        {
            if (_animals[i].category == _categories[_currentCategory])
            {
                GameObject animal = Instantiate(_animalPref, _content);
                animal.GetComponent<Image>().sprite = _animals[i].namedImage;
                _prefabs.Add(animal);
                animalIndex.Add(i);
            }
        }

        

        for (int i = 0; i < _prefabs.Count; i++)
        {
            int index = i;
            Button button = _prefabs[i].GetComponent<Button>();
            button.onClick.AddListener(() => OpenPopup(index));
        }
    }

    private void Close()
    {
        _view.SetActive(false);
        _home.Show();
    }

    private void Next()
    {
        if(_currentCategory < _categories.Length-1)
        {
            _currentCategory++;
            ConfigScreen();
        }
    }
    private void Previous()
    {
        if (_currentCategory > 0)
        {
            _currentCategory--;
            ConfigScreen();
        }
    }

    private void OpenPopup(int index)
    {
        _image.sprite = _animals[animalIndex[index]].image;
        _name.text = _animals[animalIndex[index]].name;
        _description.text = _animals[animalIndex[index]].description;
        _viewPopup.SetActive(true);
    }

    private void ClosePopup()
    {
        _viewPopup.SetActive(false);
    }
}
