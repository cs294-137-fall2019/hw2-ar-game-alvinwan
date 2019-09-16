using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameManager : MonoBehaviour
{
    private List<int> AllSquares = new List<int>();
    private List<int> PlayerSquares = new List<int>();
    private List<int> ComputerSquares = new List<int>();

    // too lazy to make generalizable logic
    private List<List<int>> states = new List<List<int>>() {
        new List<int>() {1, 2, 3},
        new List<int>() {4, 5, 6},
        new List<int>() {7, 8, 9},
        new List<int>() {1, 4, 7},
        new List<int>() {2, 5, 8},
        new List<int>() {3, 6, 9},
        new List<int>() {1, 5, 9},
        new List<int>() {3, 5, 7}
    };

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        AllSquares = new List<int>();
        PlayerSquares = new List<int>();
        ComputerSquares = new List<int>();

        var receivers = GameObject.FindGameObjectsWithTag("Receiver");
        foreach (var receiver in receivers)
        {
            receiver.GetComponent<TicTacToeReceiver>().Reset();
        }
    }

    public void MakeComputerMove()
    {
        var receivers = GameObject.FindGameObjectsWithTag("Receiver");
        foreach (var receiver in receivers)
        {
            TicTacToeReceiver box = receiver.GetComponent<TicTacToeReceiver>();
            if (box.IsUnclaimed())
            {
                box.OnComputerTouch();
                break;
            }
        }
    }

    public bool CheckWinFor(List<int> squares)
    {
        foreach (var state in states)
        {
            bool win = true;
            foreach (var value in state)
            {
                win = win && squares.Contains(value);
            }

            if (win)
            {
                return true;
            }
        }
        return false;
    }

    public int CheckWin()
    {
        if (CheckWinFor(PlayerSquares))
        {
            return 1;
        }
        if (CheckWinFor(ComputerSquares))
        {
            return 0;
        }

        return -1;
    }

    public bool CheckTie()
    {
        return AllSquares.Count == 9;
    }

    public void OnMove(int x, int y, int id, int player)
    {
        if (player == 0)
        {
            ComputerSquares.Add(id);
        }
        else if (player > 0)
        {
            PlayerSquares.Add(id);
            MakeComputerMove();
        }
        AllSquares.Add(id);

        int winner = CheckWin();
        if (winner == 0)
        {
            Debug.Log("Computer won!");
        } else if (winner > 0)
        {
            Debug.Log("Player won!");
        }

        if (CheckTie())
        {
            Debug.Log("Game is TIED!");
        }
    }
}
