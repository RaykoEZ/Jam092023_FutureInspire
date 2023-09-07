using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWallCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Enemy enemy = GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.OnDefeated();
            }
        }
    }
}
