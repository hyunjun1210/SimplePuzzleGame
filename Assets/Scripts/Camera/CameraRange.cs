using Unity.VisualScripting;
using UnityEngine;

public class CameraRange : MonoBehaviour
{
    Camera camera = null;
    Bounds bounds;

    [SerializeField] SpriteRenderer sprite = null;

    float vertical = 0;
    float horizontal = 0;

    float minX = 0;
    float maxX = 0;
    float minY = 0;
    float maxY = 0;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        bounds = sprite.bounds;
    }

    private void Start()
    {
        vertical = Camera.main.orthographicSize; //세로 크기의 반(고정)
        horizontal = vertical * Screen.width / Screen.height; //가로는 종횡비(해상도 비율)에 세로 크기 반을 곱하여 기기에 따라 유동적으로 바뀜

        minX = bounds.min.x + horizontal;
        maxX = bounds.max.x - horizontal;
        minY = bounds.min.y + vertical;
        maxY = bounds.max.y - vertical;
    }

    void Update()
    {
        camera.transform.localPosition = Vector3.zero + (Vector3.forward * -10);

        Vector3 cameraPos = camera.transform.position;
        cameraPos.x = Mathf.Clamp(cameraPos.x, minX, maxX);
        cameraPos.y = Mathf.Clamp(cameraPos.y, minY, maxY);

        camera.transform.position = cameraPos;

    }
}
