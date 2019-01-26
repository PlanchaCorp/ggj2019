using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTwitching : MonoBehaviour
{
    private Light light;
    private float baseRadius;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        baseRadius = light.spotAngle;
        StartCoroutine("Flare");
    }

    // Update is called once per frame
   
        IEnumerator Flare()
    {
        yield return new WaitForSeconds(Random.Range(0.3f,0.7f));
        float radius = baseRadius + Random.Range(-2, 2);
        light.spotAngle = radius;
        StartCoroutine("Flare");
    }
       
    
}
