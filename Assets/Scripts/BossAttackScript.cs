using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttackScript : MonoBehaviour
{
    private SteveScript steveScript;
    private float despawnTime = 3f;
    private float time;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        steveScript = FindObjectOfType<SteveScript>();
        player = GameObject.Find("Main Camera");
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float gval = Math.Max(1f - time * (1f/despawnTime), 0f);
        ChangeColor(gameObject.GetComponent<Renderer>().material, gval);
        time += Time.deltaTime;
        if (time > despawnTime) {
            Vector3 pLoc3D = player.transform.position;
            Vector2 playerLocation2D = new Vector2(pLoc3D.x, pLoc3D.z);
            Vector2 attackLocation2D = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
            if ((playerLocation2D - attackLocation2D).magnitude < gameObject.transform.localScale.x/2) {
                steveScript.addScore(-200);
                steveScript.PlayThatsPrettyCool();
            }
            Destroy(gameObject);
        }
    }

    void ChangeColor(Material mat, float gval)
    {
        Color newColor = Color.white;
        newColor.g = gval;
        mat.SetColor("_Color", newColor);
    }
}
