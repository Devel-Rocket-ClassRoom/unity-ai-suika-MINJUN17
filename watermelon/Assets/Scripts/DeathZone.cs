using UnityEngine;
using System.Collections.Generic;

public class DeathZone : MonoBehaviour
{
    public float gracePeriod = 1.0f;
    public float overflowTime = 2.0f;

    readonly Dictionary<Fruit, float> timers = new();

    void OnTriggerEnter2D(Collider2D other)
    {
        Fruit f = other.GetComponent<Fruit>();
        if (f != null && !timers.ContainsKey(f))
            timers[f] = 0f;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Fruit f = other.GetComponent<Fruit>();
        if (f != null) timers.Remove(f);
    }

    void Update()
    {
        var keys = new List<Fruit>(timers.Keys);

        foreach (var f in keys)
        {
            if (f == null) { timers.Remove(f); continue; }
            if (Time.time - f.spawnTime < gracePeriod) continue;

            timers[f] += Time.deltaTime;
            if (timers[f] >= overflowTime)
            {
                GameManager.Instance?.TriggerGameOver();
                return;
            }
        }
    }
}
