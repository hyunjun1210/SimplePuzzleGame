using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private Transform startPos = null;

    [SerializeField] private Camera camera;

    private bool isDie = false;

    public bool IsDie => isDie;

    private void Awake()
    {
        startPos = GameObject.FindWithTag("Spawn").transform.GetChild(0); //나중에 하드코드된 부분을 변수로 수정
        camera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trick") && isDie == false)
        {
            isDie = true;
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            GameObject g = Instantiate(player, startPos.position, startPos.rotation);

            camera.transform.SetParent(g.transform);

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
