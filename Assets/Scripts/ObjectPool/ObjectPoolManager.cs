using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

[Serializable]
public class PoolObject
{
    [NonSerialized] public int id = 0;
    public string name = string.Empty;
    public GameObject poolObject = null;
}

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager instance;

    public static ObjectPoolManager Instance
    {
        get => instance;

        set => instance = value;
    }

    [Header("나열된 순서 대로 id가 매겨집니다.")]
    [Header("처음 등록한 오브젝트부터 id(0)")]
    [SerializeField] private List<PoolObject> poolObjects = new List<PoolObject>();

    private List<Queue<GameObject>> pools = new();

    private int GetId(string name)
    {
        var obj = poolObjects.FirstOrDefault(x => x.name == name);
        if (obj == null)
        {
            Debug.LogError($"Object Pool Manager : '{name} 이름의 오브젝트가 존재하지 않습니다.");
            return -1;
        }

        return obj.id;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < poolObjects.Count; i++)
        {
            poolObjects[i].id = i;
            pools.Add(new Queue<GameObject>());

            for (int j = 0; j < 3; j++)
            {
                var obj = Instantiate(poolObjects[i].poolObject, transform);
                obj.SetActive(false);
                pools[i].Enqueue(obj);
            }
        }
    }

    /// <summary>
    /// 이름을 통해 오브젝트를 가져옴
    /// </summary>
    /// <param name="name">오브젝트 이름</param>
    /// <returns></returns>
    public GameObject GetObject(string name)
    {
        int id = GetId(name);
        if (id < 0)
            return null;

        GameObject obj = null;
        if (pools[id].Count <= 0)
        {
            obj = Instantiate(poolObjects[id].poolObject, transform);
        }
        else
        {
            obj = pools[id].Dequeue();
        }
        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// id를 통해 오브젝트를 가져옴
    /// </summary>
    /// <param name="id">오브젝트 번호</param>
    /// <returns></returns>
    public GameObject GetObject(int id)
    {
        if (id < 0)
        {
            Debug.LogError($"Object Pool Manager : '{id}'는 잘못된 id입니다.");
            return null;
        }


        GameObject obj = null;
        if (pools[id].Count <= 0)
        {
            obj = Instantiate(poolObjects[id].poolObject, transform);
        }
        else
        {
            obj = pools[id].Dequeue();
        }
        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// 사용이 다한 오브젝트를 풀링에 넣음
    /// </summary>
    /// <param name="obj">사용이 끝난 오브젝트</param>
    public void ReturnObject(GameObject obj, int id)
    {
        if (id < 0)
        {
            Debug.LogError($"Object Pool Manager : '{id}'는 잘못된 id입니다.");
            return;
        }

        if (!pools[id].Contains(obj))
        {
            obj.SetActive(false);
            pools[id].Enqueue(obj);
        }
    }

    public void ReturnObject(GameObject obj, string name)
    {
        int id = GetId(name);
        if (id < 0)
            return;

        if (!pools[id].Contains(obj))
        {
            obj.SetActive(false);
            pools[id].Enqueue(obj);
        }
    }
}
