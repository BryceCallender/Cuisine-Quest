using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    private Animator anim;
    private bool playerMoving;
    private Vector2 lastMove;
    private float H_Axis;
    private float V_Axis;


    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        H_Axis = Input.GetAxisRaw("Horizontal");
        V_Axis = Input.GetAxisRaw("Vertical");
        playerMoving = false;

		if(H_Axis > 0.5f || H_Axis < -0.5f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            playerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }

        if (V_Axis > 0.5f || V_Axis < -0.5f)
        {
            transform.Translate(new Vector3( 0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            playerMoving = true;
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }
        anim.SetFloat("MoveX", H_Axis);
        anim.SetFloat("MoveY", V_Axis);
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }
}
