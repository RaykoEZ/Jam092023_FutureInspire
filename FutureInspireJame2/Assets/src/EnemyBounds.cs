using UnityEngine;

public class EnemyBounds : MonoBehaviour 
{
    [SerializeField] Enemy m_enemy = default;
    public Enemy Enemy => m_enemy;
}
