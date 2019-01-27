using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassOrientation : MonoBehaviour
{

    private Transform origin;
    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        origin = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("Rift").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 translation = target.position - origin.position;
            float angle = Vector2.SignedAngle(Vector2.right, translation) - 90;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}
