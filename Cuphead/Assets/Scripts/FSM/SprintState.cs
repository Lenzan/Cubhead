using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class SprintState : State<AI>
{

    private static SprintState _instance;

    private SprintState()
    {
        if (_instance != null)
            return;
        _instance = this;
    }

    public static SprintState Instance
    {
        get
        {
            if (_instance == null)
                new SprintState();
            return _instance;
        }
    }

    public override void EnterState(AI owner)
    {
        Debug.Log("--> enter sprint state");
    }

    public override void ExitState(AI owner)
    {
        Debug.Log("--> exiting sprint state");
    }

    public override void HandleInput(AI owner)
    {

    }

    public override void UpdateState(AI owner)
    {

    }
}