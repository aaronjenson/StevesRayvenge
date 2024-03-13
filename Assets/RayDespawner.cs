using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDespawner : MonoBehaviour
{
    private float despawnTime = 2f;
    private float fullOpacityTime = 1f;
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
        float alpha = 0f;
        if (time < fullOpacityTime) {
            alpha = 1;
        } else {
            float slope = -1 / (despawnTime - fullOpacityTime);
            alpha = (time - fullOpacityTime) * slope + 1;
        }
        ChangeAlpha(gameObject.GetComponent<Renderer>().material, alpha);
    }

    void ChangeAlpha(Material mat, float alphaVal)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        mat.SetColor("_Color", newColor);
    }
}
