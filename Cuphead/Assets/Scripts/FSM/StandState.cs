using UnityEngine;
using FSM;

public class StandState : State<Player>
{
    private static StandState _instance;
    private float _fireTimer;
    private bool _isFire = true;
    private bool _isGround = false;

    private StandState()
    {
        if (_instance != null)
            return;
        _instance = this;
    }

    public static StandState Instance
    {
        get
        {
            if (_instance == null)
                new StandState();
            return _instance;
        }
    }

    public override void EnterState(Player owner)
    {
        Debug.Log(Tools.GetColorFont(Color.Green, "Standing") + " --> enter stand state");
        owner._animator.SetBool("IsStart" , true);
    }

    public override void ExitState(Player owner)
    {
        Debug.Log(Tools.GetColorFont(Color.Green, "Standing") + " --> exiting stand state");
    }

    public override void UpdateState(Player owner)
    {
        _fireTimer += Time.deltaTime;
        if (_fireTimer > owner._rate)
        {
            _fireTimer = 0;
            _isFire = true;
        }

        float move = Input.GetAxis("Horizontal");

        if (Mathf.Abs(move) > 0)
        {
            owner._stateMachine.SwitchState(MoveState.Instance);
        }
        _isGround = Physics2D.OverlapCircle(owner._groundCheck.position, owner._groundRadius, owner._whatIsGround);
    }

    public override void HandleInput(Player owner)
    {
        if (Input.GetKey(KeyCode.F) && _isFire)
        {
            owner.DoStartFire();
            _isFire = false;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            owner.DoEndFire();
        }

        if (Input.GetKeyDown(KeyCode.Space) && owner._isGround)
        {
            owner._stateMachine.SwitchState(JumpState.Instance);
        }
    }
}
