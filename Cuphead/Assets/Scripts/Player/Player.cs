using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

public class Player : AI {

    public float _rate = 0.2f;
    public float _moveSpeed = 5;
    public float _jumpSpeed = 20;
    public float _parrySpeed = 13;
    public int _bulletDir = 1;
    public float _groundRadius = 0.2f;
    public Transform _groundCheck;
    public LayerMask _whatIsGround;
    public LayerMask _whatIsParry;
    public bool _isGround = false;
    public bool _isParry = false;
    public StateMachine<Player> _stateMachine { get; set; }
    

    public override void Awake()
    {
        base.Awake();
        _stateMachine = new StateMachine<Player>(this);
        _stateMachine.SwitchState(StandState.Instance);
    }

    public override void Update()
    {
        _stateMachine.HandleInput();
        _stateMachine.Update();
        _isGround = Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _whatIsGround);
        _isParry = Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _whatIsParry);
        _animator.SetBool("IsGround" , _isGround);
    }

    public override void DoStartFire()
    {
        Debug.Log(Tools.GetColorFont(Color.Red, "Fire") +" ：boom");
        Instantiate(_bulletPrefab, _firePos.position, Quaternion.Euler(Tools.GetRotation(_bulletDir)));
        _animator.SetBool("IsFire" , true);
    }

    public override void DoEndFire()
    {
        Debug.Log(Tools.GetColorFont(Color.Red, "Fire") + " ：ending");
        _animator.SetBool("IsFire", false);
    }
}
