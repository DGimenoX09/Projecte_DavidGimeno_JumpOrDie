using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollector : MonoBehaviour
{
    public HealthManager healthManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            healthManager.lives += 1;
            Destroy(other.gameObject);
        }
    }
}
