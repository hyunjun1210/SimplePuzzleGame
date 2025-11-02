using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float m_speed = 3f;
    [SerializeField] private Rigidbody2D m_rigidbody = null;

    [SerializeField] private PlayerDie playerDie = null;

    private void Awake()
    {
        playerDie = GetComponent<PlayerDie>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerDie.IsDie)
            return;

        m_rigidbody.linearVelocityX = Input.GetAxisRaw("Horizontal") * m_speed;
    }
}
