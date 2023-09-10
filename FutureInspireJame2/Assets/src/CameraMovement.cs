using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Vector2 m_maxMoveExtent = default;
    [SerializeField] Vector2 m_minMoveExtent = default;
    public RectTransform Player;
    public float damping;
    private Vector3 velocity = Vector3.zero;
    void Update()
    {
        Vector3 movePosition = new Vector3(
            Mathf.Clamp(Player.position.x, m_minMoveExtent.x, m_maxMoveExtent.x),
            Mathf.Clamp(Player.position.y, m_minMoveExtent.y, m_maxMoveExtent.y),
            transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
    }
}
