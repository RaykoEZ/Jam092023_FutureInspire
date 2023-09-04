using System.Runtime.ConstrainedExecution;
using System.Text;
using UnityEngine;
public class HandController : MonoBehaviour
{
    [SerializeField] Hand m_hand = default;
    [SerializeField] DropItemDetector m_detect = default;
    Vector3 m_currentPos;
    Vector3 m_prevPos;
    // current direction of player hand movement
    float m_pointerXInfluence;
    private void Start()
    {
        m_currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_detect.OnItemLanded += OnDropItemLand;
        m_detect.OnItemLeave += OnDropItemLeave;
    }
    void OnDropItemLand(DropItem item)
    {
        m_hand?.OnDropItemLand(item);
        // if mouse button not held, we launch immediately
        if (!Input.GetMouseButton(0))
        {
            m_hand?.LaunchItem(m_pointerXInfluence);
        }
        // hold food in hand if mouse button held down
        else 
        { 
        
        }
    }
    void OnDropItemLeave(DropItem item)
    {
        m_hand?.OnDropItemLeave(item);
    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        m_prevPos = m_currentPos;
        m_currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_currentPos.z = 0f;
        transform.position = new Vector3(m_currentPos.x, transform.position.y, transform.position.z);
        m_pointerXInfluence = Mathf.Clamp(m_currentPos.x - m_prevPos.x, -1f, 1f);
        // If lmb held down, start charging
        if (Input.GetMouseButton(0)) 
        { 
        
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            m_hand.LaunchItem(m_pointerXInfluence);
        }
    }
}
