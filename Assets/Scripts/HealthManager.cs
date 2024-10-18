using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int lives = 5;

    public void TakeDamage()
    {
        lives -= 1;
    }
}
