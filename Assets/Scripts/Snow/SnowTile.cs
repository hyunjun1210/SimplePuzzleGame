using Unity.VisualScripting;
using UnityEngine;

public class SnowTile : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] GameObject minX = null;
    [SerializeField] GameObject maxX = null;

    [SerializeField] GameObject snow = null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        bool range = player.transform.position.x >= minX.transform.position.x && player.transform.position.x <= maxX.transform.position.x;
        bool isUntagged = player.tag == "Untagged";
        if (range == false || isUntagged == true)
        {
            player = null;
        }
        snow.SetActive(range && !isUntagged);
    }
}
