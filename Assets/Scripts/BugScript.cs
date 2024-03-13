using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float pickupDistance = 2f;
    private SteveScript steveScript;
    private GameObject player;
    void Start()
    {
        steveScript = FindObjectOfType<SteveScript>();
        player = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if ((gameObject.transform.position - player.transform.position).magnitude < pickupDistance
                && steveScript.GetVulnerability()) {
            steveScript.BecomeVulnerable();
            Destroy(gameObject);
        }
    }
}
