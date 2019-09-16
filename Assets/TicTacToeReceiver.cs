﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adding OnTouch3D here forces us to implement the 
// OnTouch function, but also allows us to reference this
// object through the OnTouch3D class.
public class TicTacToeReceiver : MonoBehaviour, OnTouch3D
{
    public GameManager gameManager;
    public int x;
    public int y;
    public int id;
    private int player = -1;

    void Start()
    {
    }

    public bool IsUnclaimed()
    {
        return player < 0;
    }

    public void OnComputerTouch()
    {
        this.gameObject.transform.Translate(new Vector3(0, 0.2f, 0));
        player = 0;

        if (gameManager) gameManager.OnMove(x, y, id, player);
    }

    public void OnTouch()
    {
        if (IsUnclaimed())
        {
            // Move the object up by 10cm and reset the wait counter.
            this.gameObject.transform.Translate(new Vector3(0, 0.1f, 0));
            player = 1;

            if (gameManager) gameManager.OnMove(x, y, id, player);
        }
    }
}
