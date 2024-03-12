using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.InputSystem.Utilities;

public class SteveScript : MonoBehaviour
{
    private int HP = 5;
    public TMP_Text HPText;
    private HashSet<GameObject> hitRays;
    private Vector3 startLocation;
    private int randomWalkRange = 10;
    private float timeUntilNewLocation;
    private float speed = 2f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        HPText.SetText("HP: " + HP);
        hitRays = new HashSet<GameObject>();
        startLocation = gameObject.transform.position + Vector3.zero;
        rb = GetComponent<Rigidbody>();
        GetNewLocation();
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilNewLocation -= Time.deltaTime;
        if (timeUntilNewLocation < 0) {
            GetNewLocation();
        }
    }

    void GetNewLocation() {
        Vector3 newLocation = startLocation +
            new Vector3(UnityEngine.Random.Range(-randomWalkRange,randomWalkRange),
            0, UnityEngine.Random.Range(-randomWalkRange,randomWalkRange));
        float distance = (newLocation - gameObject.transform.position).magnitude;
        timeUntilNewLocation = distance/speed;
        rb.velocity = (newLocation - gameObject.transform.position).normalized * speed;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ray" && !hitRays.Contains(other.gameObject)) {
            GameObject ray = other.gameObject;
            hitRays.Add(ray);
            HP -= 1;
        HPText.SetText("HP: " + HP);
        }
    }
}
