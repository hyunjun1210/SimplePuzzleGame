using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private Transform startPos = null;

    private bool isDie = false;

    public bool IsDie => isDie;

    private void Awake()
    {
        startPos = GameObject.FindWithTag("Spawn").transform.GetChild(0); //나중에 하드코드된 부분을 변수로 수정
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trick") && isDie == false)
        {
            isDie = true;
            gameObject.GetComponent<Rigidbody2D>().linearVelocityX = 0;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(179, 179, 179);
            GameObject g = Instantiate(player, startPos.position, startPos.rotation);
        }
    }
}
