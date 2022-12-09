using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    [SerializeField] List<GameObject> pools;
    Queue<GameObject> poolQueue = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject pool in pools)
        {
            poolQueue.Enqueue(pool);
            pool.SetActive(false);
        }
    }

    public GameObject activateNext()
    {
        GameObject next = poolQueue.Dequeue();
        next.SetActive(true);
        return next;
    }

    public void returnToPool(GameObject obj)
    {
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }

    public IEnumerator ReturnToPoolWithDelay(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        returnToPool(obj);
    }
}
