using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float walkSpeed = 7f;
    private float runSpeed;
    private float curSpeed;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        runSpeed = walkSpeed * 1.5f;
        curSpeed = walkSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        

        float horizontal = Input.GetAxis("Horizontal");
        float vertical =  Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(Mathf.Lerp(0, horizontal * curSpeed, 0.6f),Mathf.Lerp(0, vertical * curSpeed, 0.6f));

        rb.velocity = movement;
    }
}
