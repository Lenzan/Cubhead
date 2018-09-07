using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class JumpState : State<Player>
{
    private static JumpState _instance;
    private bool _isGround = false;
 
    private bool _isParrying = false;
    private bool _isMoving = false;
    private int _parryCount = 0;

    private JumpState()
    {
        if(_instance != null)
            return;
        _instance = this;
    }

    public static JumpState Instance
    {
        get
        {
            if (_instance == null)
                new JumpState();
            return _instance;
        }
    }

    public override void EnterState(Player owner)
    {
        Debug.Log(Tools.GetColorFont(Color.Green, "Jumping") + " --> enter jump state");
        owner._animator.SetBool("IsJump" , true);
        owner._rigidbody2D.velocity = new Vector2(owner._rigidbody2D.velocity.x, owner._jumpSpeed);
        _isGround = false;
        _parryCount = 0;
    }

    public override void ExitState(Player owner)
    {
        Debug.Log(Tools.GetColorFont(Color.Green, "Jumping") + " --> exiting jump state");
        owner._animator.SetBool("IsParryOk" , false);
    }

    public override void HandleInput(Player owner)
    {
        if (Input.GetKeyDown(KeyCode.Space) && (_parryCount == 0 || owner._isParry))
        {
            Parry(owner);
        }
    }

    public override void UpdateState(Player owner)
    {
      

        if (!owner._isGround)
        {
            _isGround = true;
        }
        float move = Input.GetAxis("Horizontal");

        if (!_isParrying)
            owner._rigidbody2D.velocity = new Vector2(move * owner._moveSpeed, owner._rigidbody2D.velocity.y);

        if (owner._isGround && _isGround )
        {
            owner._stateMachine.SwitchState(StandState.Instance);
        }
    }

    private void Parry(Player owner)
    {
        owner._animator.SetTrigger("IsParry");
        _parryCount++;
        if (owner._isParry)
        {
            OnParry(owner);
        }
    }

    void OnParry(Player owner)
    {
        owner._rigidbody2D.gravityScale = 0;
        owner._animator.speed = 0;
        _isParrying = true;
        owner._rigidbody2D.velocity = new Vector2(0, 0);
        owner.StartCoroutine(OnEumParry(owner));
    }

    IEnumerator OnEumParry(Player owner)
    {
        yield return new WaitForSeconds(0.1f);
        owner._rigidbody2D.velocity = new Vector2(owner._rigidbody2D.velocity.x, owner._parrySpeed);
        owner._rigidbody2D.gravityScale = 5;
        owner._animator.speed = 1;
        _isParrying = false;
        owner._animator.SetBool("IsParryOk", true);
    }
}


