using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] float timeAlive = 10;
    float time=0;
    public Pooling pooling;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= timeAlive)
        {
            time = 0;
            pooling.returnToPool(gameObject);
        }
    }
}
