using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    Vector3 m_currentPos;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        m_currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_currentPos.z = 0f;
        transform.position = m_currentPos;
    }
}
