using UnityEngine;

public class SnowObject : MonoBehaviour
{
    public Snow snow = null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Snow"))
        {
            ObjectPoolManager.Instance.ReturnObject(gameObject, 0);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            ObjectPoolManager.Instance.ReturnObject(gameObject, 0);
        }
    }
}
