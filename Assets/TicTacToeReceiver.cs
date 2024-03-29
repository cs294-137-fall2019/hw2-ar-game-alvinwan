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

    // Debouncing is a term from Electrical Engineering referring to 
    // preventing multiple presses of a button due to the physical switch
    // inside the button "bouncing".
    // In CS we use it to mean any action to prevent repeated input. 
    // Here we will simply wait a specified time before letting the button
    // be pressed again.
    // We set this to a public variable so you can easily adjust this in the
    // Unity UI.
    public float debounceTime = 0.3f;
    // Stores a counter for the current remaining wait time.
    private float remainingDebounceTime;

    private Color color;


    void Start()
    {
        remainingDebounceTime = 0;
        GetComponent<Renderer>().material.color = Color.gray;

    }

    void Update()
    {
        // Time.deltaTime stores the time since the last update.
        // So all we need to do here is subtract this from the remaining
        // time at each update.
        if (remainingDebounceTime > 0)
            remainingDebounceTime -= Time.deltaTime;
    }

    public bool IsUnclaimed()
    {
        return player < 0;
    }

    public void OnComputerTouch()
    {
        color = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.red;
        player = 0;

        if (gameManager) gameManager.OnMove(x, y, id, player);
    }

    public void OnTouch()
    {
        if (remainingDebounceTime <= 0)
        {
            remainingDebounceTime = debounceTime;

            if (IsUnclaimed())
            {
                color = GetComponent<Renderer>().material.color;
                GetComponent<Renderer>().material.color = Color.blue;
                player = 1;

                if (gameManager) gameManager.OnMove(x, y, id, player);
            }
            else
            {
                Debug.Log("Move invalid!");
            }
        }
    }

    public void Reset()
    {
        player = -1;
        GetComponent<Renderer>().material.color = Color.gray;
    }
}
