﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBorder : MonoBehaviour {

    public Vector2 TransitionDirection;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            transform.parent.GetComponent<CameraController>().SceneTransition(TransitionDirection);
        }
    }
}