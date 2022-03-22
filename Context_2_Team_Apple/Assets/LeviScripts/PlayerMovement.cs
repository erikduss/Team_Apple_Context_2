using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector2 moveDir;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 10f;

    void FixedUpdate()
    {
        
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

    }

    public void Movement()
    {
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);



    }
}
