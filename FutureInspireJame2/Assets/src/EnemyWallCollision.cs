using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWallCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out EnemyBounds bounds))
        {
            bounds?.Enemy?.OnDefeated();           
        }
    }
}
