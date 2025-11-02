using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float m_speed = 3f;
    [SerializeField] private Rigidbody2D m_rigidbody = null;

    void Update()
    {
        m_rigidbody.linearVelocityX = Input.GetAxisRaw("Horizontal") * m_speed;
    }
}
