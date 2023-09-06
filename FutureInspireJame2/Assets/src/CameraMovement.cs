using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Player;
    public float offset;
    public float damping;
    private Vector3 velocity = Vector3.zero;
    void FixedUpdate()
    {
        Vector3 movePosition = new Vector3(Player.position.x + offset, Player.position.y + offset, -1);
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
    }
}
