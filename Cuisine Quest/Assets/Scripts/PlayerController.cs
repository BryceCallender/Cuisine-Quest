using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Animator anim;
    private bool playerMoving;
    public bool playerCanMove = true;
    private Vector2 lastMove;
    private Rigidbody2D rb;
    public Vector2 DirectionFacing = new Vector2(0, -1);
    private float H_Axis;
    private float V_Axis;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerMoving = false;
        H_Axis = Input.GetAxisRaw("Horizontal");
        V_Axis = Input.GetAxisRaw("Vertical");

        if ((H_Axis > 0.5f || H_Axis < -0.5f) &&
           (V_Axis > 0.5f || V_Axis < -0.5f) && playerCanMove)
        {
            rb.velocity = new Vector3(H_Axis * moveSpeed, V_Axis * moveSpeed, 0f);
            playerMoving = true;
            lastMove = new Vector2(H_Axis, V_Axis);
        }
        else if((H_Axis > 0.5f || H_Axis < -0.5f) && playerCanMove)
        {
            rb.velocity = new Vector3(H_Axis * moveSpeed, 0f, 0f);
            playerMoving = true;
            lastMove = new Vector2(H_Axis, 0f);
            if (H_Axis > 0.5f)
            {
                DirectionFacing = new Vector2(1,0);
            }
            else
            {
                DirectionFacing = new Vector2(-1, 0);
            }
        }
        else if((V_Axis > 0.5f || V_Axis < -0.5f) && playerCanMove)
        {
            rb.velocity = new Vector3(0f, V_Axis * moveSpeed, 0f);
            playerMoving = true;
            lastMove = new Vector2(0f, V_Axis);

            if (V_Axis > 0.5f)
            {
                DirectionFacing = new Vector2(0, 1);
            }
            else
            {
                DirectionFacing = new Vector2(0, -1);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }


        anim.SetFloat("MoveX", H_Axis);
        anim.SetFloat("MoveY", V_Axis);
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }
}
