using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public Animator anim = new Animator();
    private GameObject obj;
    public bool isGrounded = true;
    public float jumpSpeed = 15.0F;
    public float gravity = 15.0F;
    public float gravityForce = 3.0f;
    public float airTime = 2f;
    private Vector3 moveDirection = Vector3.zero;
    private float forceY = 0;
    private float invertGrav;

    void Start()
    {
        obj = gameObject;
        invertGrav = gravity * airTime;
    }

    // Update is called once per frame
    void Update()
    {
        MoveAndJump();
    }

    void MoveAndJump()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isRun", true);
            anim.SetBool("isMoveRight", true);
            anim.SetBool("isMoveLeft", false);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("isRun", true);
            anim.SetBool("isMoveRight", false);
            anim.SetBool("isMoveLeft", true);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("isCrouch", true);
        }
        else
        {
            anim.SetBool("isCrouch", false);
            anim.SetBool("isRun", false);
        }
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;
        if (isGrounded)
        {
            anim.SetBool("isGround", true);
            // we are grounded so forcey is 0
            forceY = 0;
            // invertgrav is also reset based on the gravity
            invertGrav = gravity * airTime;
            if (Input.GetKey(KeyCode.Space))
            {
                // we jump 
                anim.SetBool("isGround", false);
                forceY = jumpSpeed;
            }
        }
        // we are now jumping since forcey is not 0
        // we add invertgrav to our jumpforce and invertgrav is also
        // decreased so that we get a curvy jump
        if (Input.GetKey(KeyCode.Space) && forceY != 0)
        {
            anim.SetBool("isGround", false);
            invertGrav -= Time.deltaTime;
            forceY += invertGrav * Time.deltaTime;
        }
        // here we apply the gravity
        forceY -= gravity * Time.deltaTime * gravityForce;
        moveDirection.y = forceY;
        obj.transform.position += moveDirection * Time.deltaTime;
    }
}
