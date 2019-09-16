using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameManager : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public void OnComputerMove(int x, int y)
    {
        Debug.Log(x + " " + y);
    }

    public void OnMove(int x, int y, int player)
    {
        Debug.Log(x + " " + y + " " + player);
    }
}
