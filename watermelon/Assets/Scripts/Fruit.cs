using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int   level;
    public bool  isMerging;
    public float spawnTime;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (isMerging) return;
        var other = col.gameObject.GetComponent<Fruit>();
        if (other == null || other.isMerging || other.level != level) return;

        var spawner = FruitSpawner.Instance;
        if (spawner == null || level >= spawner.MaxLevel) return;

        isMerging       = true;
        other.isMerging = true;

        Vector2 mid = ((Vector2)transform.position + (Vector2)other.transform.position) * 0.5f;
        GameManager.Instance?.SpawnMerged(level + 1, mid);

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
