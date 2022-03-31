using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector2 moveDir;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 10f;
    //[SerializeField] Animator animator;
    

    void FixedUpdate()
    {
        Physics.IgnoreLayerCollision(7,8);
    }

    private void Update()
    {
        ProcessInputs();

    }
    public void ProcessInputs()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(x, y).normalized;


        if(x > 0 || y > 0)
        {
            //animator.SetBool("isWalking", true);
        }
        else
        {
            //animator.SetBool("isWalking", false);
        }
    }

    public void Movement()
    {
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);



    }
}
