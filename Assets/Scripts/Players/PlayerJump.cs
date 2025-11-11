using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float m_jumpPower = 4f;
    [SerializeField] private Rigidbody2D m_rigidbody = null;

    [SerializeField] private PlayerDie playerDie = null;
    bool m_isJumping = true;

    private void Awake()
    {
        playerDie = GetComponent<PlayerDie>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_isJumping = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        m_isJumping = true;
    }

    void Update()
    {
        if (playerDie.IsDie)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && m_isJumping == false)
        {
            m_rigidbody.AddForceY(m_jumpPower, ForceMode2D.Impulse);
            SoundManager.Instance.Play(SoundType.SFX, "Jump");
        }
    }
}
