using UnityEngine;
public class HandController : MonoBehaviour
{
    [SerializeField] Hand m_hand = default;
    [SerializeField] DropItemDetector m_detect = default;
    Vector3 m_currentPos;
    private void Start()
    {
        m_detect.OnItemLanded += OnDropItemLand;
        m_detect.OnItemLeave += OnDropItemLeave;
    }
    void OnDropItemLand(DropItem item)
    {
        m_hand?.OnDropItemLand(item);
    }
    void OnDropItemLeave(DropItem item)
    {
        m_hand?.OnDropItemLeave(item);
    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        m_currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_currentPos.z = 0f;
        transform.position = m_currentPos;

        // If lmb held down, start charging
        if (Input.GetMouseButton(0)) 
        { 
        
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            m_hand.LaunchItem();
        }
    }
}
