using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class SquatState : State<AI>
{
    private static SquatState _instance;

    private SquatState()
    {
        if (_instance != null)
            return;
        _instance = this;
    }

    public static SquatState Instance
    {
        get
        {
            if (_instance == null)
                new SquatState();
            return _instance;
        }
    }

    public override void EnterState(AI owner)
    {
        Debug.Log("--> enter squat state");
    }

    public override void ExitState(AI owner)
    {
        Debug.Log("--> exiting squat state");
    }

    public override void HandleInput(AI owner)
    {

    }

    public override void UpdateState(AI owner)
    {
        
    }
}