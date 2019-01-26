using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 200f;
    [SerializeField]
    private float dashSpeed = 5000f;
    private float curSpeed;

    private bool isJumping;
    [SerializeField]
    private float jumpDuration = 0.8f;


    float horizontal;
    float vertical;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true ;
            StartCoroutine("RecoverFromJump");
        }
        if (!isJumping)
        {
            
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        
    }
    private void FixedUpdate()
    {
        Move(isJumping);
       
    }

    IEnumerator RecoverFromJump()
    {
        yield return new WaitForSeconds(jumpDuration);
        isJumping = false;
    }

    void Move(bool isJumping)
    {
        //Annulation de la diagonal
        if (horizontal != 0 && vertical != 0)
        {
            curSpeed = walkSpeed * 0.7f;
        }
        else
        {
            curSpeed = walkSpeed;
        }
        if (isJumping)
        {
            curSpeed = curSpeed * 1.5f;
        }

        Vector2 movement = new Vector2(Mathf.Lerp(0, horizontal * curSpeed, 0.8f) * Time.deltaTime, Mathf.Lerp(0, vertical * curSpeed, 0.8f) * Time.deltaTime);
        rb.velocity = movement;
    }


    public bool IsJumping()
    {
        return isJumping;
    }
}
