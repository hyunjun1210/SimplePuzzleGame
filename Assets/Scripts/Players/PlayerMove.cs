using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    const string ANIMA_SPEED_NAME = "speed";

    [SerializeField] private float speed = 3f;
    [SerializeField] private Rigidbody2D rigidbody = null;

    [SerializeField] private PlayerDie playerDie = null;
    [SerializeField] private Animator animator = null;

    private void Awake()
    {
        playerDie = GetComponent<PlayerDie>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerDie.IsDie)
            return;

        if (collision.CompareTag("Ladder") && Input.GetKey(KeyCode.W))
        {
            rigidbody.linearVelocityY = speed;
        }
    }


    void Update()
    {
        if (playerDie.IsDie)
            return;

        rigidbody.linearVelocityX = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetFloat(ANIMA_SPEED_NAME, Mathf.Abs(rigidbody.linearVelocityX));
        if (Input.GetAxisRaw("Horizontal") > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else if (Input.GetAxisRaw("Horizontal") < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }
}
