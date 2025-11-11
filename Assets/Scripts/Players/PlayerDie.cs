using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    const string ANIMA_SPEED_NAME = "speed";

    [SerializeField] private GameObject player = null;
    [SerializeField] private Transform startPos = null;

    [SerializeField] private Camera camera;
    [SerializeField] private Animator animator = null;

    private bool isDie = false;

    public bool IsDie => isDie;

    private void Awake()
    {
        startPos = GameObject.FindWithTag("Spawn").transform.GetChild(0); //나중에 하드코드된 부분을 변수로 수정
        camera = Camera.main;
        player = Resources.Load<GameObject>("Prefabs/Player");
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trick") && isDie == false)
        {
            SoundManager.Instance.Play(SoundType.SFX, "Damage");
            isDie = true;
            animator.SetFloat(ANIMA_SPEED_NAME, 0);
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            GameObject g = Instantiate(player, startPos.position, startPos.rotation);   

            camera.transform.SetParent(g.transform);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TrickSnow") && isDie == false)
        {
            SoundManager.Instance.Play(SoundType.SFX, "Damage");
            isDie = true;
            animator.SetFloat(ANIMA_SPEED_NAME, 0);
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            
            GameObject g = Instantiate(player, startPos.position, startPos.rotation);
            gameObject.tag = "Untagged";
            camera.transform.SetParent(g.transform);
            Destroy(gameObject.GetComponent<BoxCollider2D>());
        }
    }

    private void Update()
    {
        if (gameObject.transform.position.y < -10.5f)
        {
            isDie = true;
            GameObject g = Instantiate(player, startPos.position, startPos.rotation);
            camera.transform.SetParent(g.transform);
            Destroy(gameObject);
        }
    }
}
