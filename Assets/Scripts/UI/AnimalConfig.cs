using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalConfig", menuName = "ScriptableObjects/AnimalConfig", order = 1)]
public class AnimalConfig : ScriptableObject
{
    public Category category;
    public string name;
    public string description;
    public Sprite image;
    public Sprite namedImage;
}
