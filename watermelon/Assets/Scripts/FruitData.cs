using UnityEngine;

[CreateAssetMenu(fileName = "FruitData", menuName = "Suika/FruitData")]
public class FruitData : ScriptableObject
{
    public string fruitName;
    public Sprite sprite;
    public float  radius;
    public Color  color;
    public int    score;
}
