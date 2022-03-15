using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private float playerSpeed = 2.5f;
    private Rigidbody2D rbPlayer;
    private Animator animPlayer;

    private BoxCollider2D collider;

    private Vector2 center { get { return collider.bounds.center; } }

    // Use this for initialization
    void Start () {
        collider = GetComponent<BoxCollider2D>();
        rbPlayer = this.GetComponent<Rigidbody2D>();
        animPlayer = this.GetComponent<Animator>();
	}

    void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        MovePlayer();
	}

    void MovePlayer()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        rbPlayer.velocity = new Vector2(hor * playerSpeed, ver * playerSpeed);

        animPlayer.SetFloat("horizontalSpeed", hor);
        animPlayer.SetFloat("verticalSpeed", ver);

        if(hor == 0 && ver == 0)
        {
            return;
        }

        if (Mathf.Abs(hor) > Mathf.Abs(ver))
        {
            if (hor < 0)
            {
                animPlayer.SetInteger("Direction", 4);
            }
            else
            {
                animPlayer.SetInteger("Direction", 2);
            }
        }
        else
        {
            if (ver < 0)
            {
                animPlayer.SetInteger("Direction", 1);
            }
            else
            {
                animPlayer.SetInteger("Direction", 3);
            }
        }
    }

}
