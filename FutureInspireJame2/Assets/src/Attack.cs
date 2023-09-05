using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private PolygonCollider2D polygonCollider;
    private float enableTime;
    private bool isEnabled = false;
    public float attackDuration = 1f;

    private void Start()
    {
        // Get the Polygon Collider 2D component attached to the GameObject
        polygonCollider = GetComponent<PolygonCollider2D>();

        // Initially, disable the collider
        if (polygonCollider != null)
        {
            polygonCollider.enabled = false;
        }
    }

    private void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Enable the Polygon Collider 2D component
            if (polygonCollider != null)
            {
                polygonCollider.enabled = true;
                enableTime = Time.time;
                isEnabled = true;
            }
        }

        // Check if the Polygon Collider has been enabled and one second has passed
        if (isEnabled && Time.time - enableTime >= attackDuration)
        {
            // Disable the Polygon Collider 2D component
            if (polygonCollider != null)
            {
                polygonCollider.enabled = false;
                isEnabled = false;
            }
        }
    }
}

