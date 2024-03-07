using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDespawner : MonoBehaviour
{
    public float despawnTime = 3.0f;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > despawnTime) {
            Destroy(gameObject);
        }
    }
}
