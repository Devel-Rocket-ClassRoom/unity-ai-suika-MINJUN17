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
        var toRemove = new List<Fruit>();

        foreach (var kv in timers)
        {
            Fruit f = kv.Key;
            if (f == null) { toRemove.Add(f); continue; }
            if (Time.time - f.spawnTime < gracePeriod) continue;

            timers[f] += Time.deltaTime;
            if (timers[f] >= overflowTime)
            {
                GameManager.Instance?.TriggerGameOver();
                return;
            }
        }

        foreach (var f in toRemove) timers.Remove(f);
    }
}
