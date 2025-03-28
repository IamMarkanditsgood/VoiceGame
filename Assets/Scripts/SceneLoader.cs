using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Image loadingImage;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float spriteChangeInterval = 0.5f;

    private void Start()
    {
        StartCoroutine(AnimateSprites());
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator AnimateSprites()
    {
        int index = 0;
        while (true)
        {
            loadingImage.sprite = sprites[index];
            index = (index + 1) % sprites.Length;
            yield return new WaitForSeconds(spriteChangeInterval);
        }
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (progress >= 1f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
