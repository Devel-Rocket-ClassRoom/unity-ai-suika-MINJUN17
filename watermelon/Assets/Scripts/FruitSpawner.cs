using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public static FruitSpawner Instance;

    public FruitData[] fruitDataList;
    public GameObject  fruitPrefab;

    public int MaxLevel    => fruitDataList.Length - 1;
    public int MaxDropLevel => 4;

    void Awake() => Instance = this;

    public GameObject Spawn(int level, Vector2 pos, bool isPreview = false)
    {
        if (fruitPrefab == null || level < 0 || level >= fruitDataList.Length) return null;

        FruitData data = fruitDataList[level];
        GameObject go  = Instantiate(fruitPrefab, pos, Quaternion.identity);
        go.name = data.fruitName;

        go.transform.localScale = Vector3.one * data.radius * 2f;

        var sr = go.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            if (data.sprite != null)
            {
                sr.sprite = data.sprite;
                sr.color = isPreview ? new Color(1, 1, 1, 0.45f) : Color.white;
            }
            else
            {
                sr.color = isPreview
                    ? new Color(data.color.r, data.color.g, data.color.b, 0.45f)
                    : data.color;
            }
        }

        var col = go.GetComponent<CircleCollider2D>();
        if (col != null) col.enabled = !isPreview;

        var rb = go.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = !isPreview;

        var fruit = go.GetComponent<Fruit>();
        if (fruit != null)
        {
            fruit.level     = level;
            fruit.spawnTime = Time.time;
        }

        return go;
    }
}
