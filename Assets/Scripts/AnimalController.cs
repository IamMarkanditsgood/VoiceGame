using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalController : MonoBehaviour
{
    public Image _image;
    public float speed = 5f; 

    void Update()
    {
        transform.position += -transform.up * speed * Time.deltaTime;
    }

    public void SetImage(Sprite image)
    {
        _image.sprite = image;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.Touched();
        }
        Destroy(gameObject);
    }
}
