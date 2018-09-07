using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class MoveState : State<Player>
{

    private static MoveState _instance;
    private bool _facingRight = true;
    private bool isTeleport = false;
    private float move;

    private MoveState()
    {
        if (_instance != null)
            return;
        _instance = this;
    }

    public static MoveState Instance
    {
        get
        {
            if (_instance == null)
                new MoveState();
            return _instance;
        }
    }

    public override void EnterState(Player owner)
    {
        Debug.Log(Tools.GetColorFont(Color.Green, "Move") + " --> enter move state");
    }

    public override void ExitState(Player owner)
    {
        Debug.Log(Tools.GetColorFont(Color.Green, "Move") + " --> exiting move state");
    }

    public override void HandleInput(Player owner)
    {
        move = Input.GetAxis("Horizontal");

        owner._animator.SetFloat("Speed", Mathf.Abs(move));

        owner._rigidbody2D.velocity = new Vector2(move * owner._moveSpeed, owner._rigidbody2D.velocity.y);

        if (Mathf.Abs(move) <= 0)
        {
            owner._stateMachine.SwitchState(StandState.Instance);
        }

        if (Input.GetKeyDown(KeyCode.Space) && owner._isGround)
        {
            owner._stateMachine.SwitchState(JumpState.Instance);
        }
    }

    public override void UpdateState(Player owner)
    {
        if (move > 0 && !_facingRight)
            Flip(owner);
        else if (move < 0 && _facingRight)
            Flip(owner);

        
    }

    /// <summary>
    /// 转头
    /// </summary>
    private void Flip(Player owner)
    {
        _facingRight = !_facingRight;
        Vector3 theScale = owner.transform.localScale;
        theScale.x *= -1;
        owner._bulletDir *= -1; 
        owner.transform.localScale = theScale;
        //_fastMoveSpeed *= -1;
    }
}
