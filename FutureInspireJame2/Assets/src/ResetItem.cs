using UnityEngine;

public class ResetItem : MonoBehaviour
{
    [SerializeField] DropItem m_toReset = default;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            m_toReset.transform.position = transform.position;
        }
    }
}
