using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public RectTransform Player;
    public float damping;
    private Vector3 velocity = Vector3.zero;
    void Update()
    {
        Vector3 movePosition = new Vector3(Player.position.x, Player.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
    }
}
