using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Snow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text = null;
    [SerializeField] GameObject snow = null;
    [SerializeField] GameObject parent = null;
    DateTime date;

    [SerializeField] GameObject clear = null;

    [SerializeField] Rigidbody2D rigidbody = null;

    [SerializeField] GameObject minX = null;
    [SerializeField] GameObject maxX = null;

    Vector3 startPos;

    float speed = 2f;

    bool isClear = false;

    bool isRotate = false;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void OnEnable()
    {
        date = DateTime.Now;
        transform.position = startPos;
        StartCoroutine(nameof(CoSnow));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(CoSnow));
    }

    private void Move()
    {
        var min = minX.transform.position.x;
        var max = maxX.transform.position.x;
        //To do
        if ((min > gameObject.transform.position.x && isRotate == true))
        {
            speed = speed * -1;
            isRotate = false;
        }
        else if (max < gameObject.transform.position.x && isRotate == false)
        {
            speed = speed * -1;
            isRotate = true;
        }
        rigidbody.linearVelocityX = speed;
    }

    IEnumerator CoSnow()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            var obj = ObjectPoolManager.Instance.GetObject("Snow");
            obj.transform.position = parent.transform.position;
        }
    }

    private void Update()
    {
        if (isClear)
        {
            return;
        }
        Move();
        TimeSpan time = DateTime.Now - date;
        TimeSpan countDown = TimeSpan.FromSeconds(30) - time;

        if (countDown.TotalSeconds <= 0)
        {
            text.text = "°ÔÀÓ ½Â¸®!";
            ObjectPoolManager.Instance.ReturnObject(gameObject, 0);
            clear.SetActive(true);
            isClear = true;
            StopCoroutine(nameof(CoSnow));
            Time.timeScale = 0;
            return;
        }

        text.text = $"{countDown.Hours:00}:{countDown.Minutes:00}:{countDown.Seconds:00}";
    }
}
