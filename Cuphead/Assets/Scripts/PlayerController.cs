using System;
using System.Collections;
using UnityEngine;

public enum Rotation
{
    Up,
    Down,
    Right,
    Left
}

public class PlayerController : MonoBehaviour
{
    public float _moveSpeed;
    public float _jumpSpeed;
    public float _parrySpeed;
    public float rate = 0.1f;
    public Transform _groundCheck;
    public GameObject _bulletPrefab;
    public LayerMask _whatIsGround;
    public LayerMask _whatIsParry;
    public Transform _firePos;
    public Rotation rotation;


    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Animator _fireAnimator;
    private bool _facingRight = true;
    private bool _isGround = false;
    private bool _isParry = false;
    private bool _isParrying = false;
    private float _groundRadius = 0.2f;
    private float _parryRadius = 0.2f;



    // Use this for initialization
    void Awake ()
	{
	    _rigidbody = GetComponent<Rigidbody2D>();
	    _animator = GetComponent<Animator>();
	    _fireAnimator = _firePos.GetComponent<Animator>();


	}

    void FixedUpdate()
    {
        _isGround = Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _whatIsGround);
        _isParry = Physics2D.OverlapCircle(_groundCheck.position, _parryRadius, _whatIsParry);
        _animator.SetBool("IsGround" , _isGround);


        if(_isParrying)
            return;

        float move = Input.GetAxis("Horizontal");

        _animator.SetFloat("Speed" , Mathf.Abs(move));

        _rigidbody.velocity = new Vector2(move * _moveSpeed , _rigidbody.velocity.y);

        if (move > 0 && !_facingRight)
            Flip(move);
        else if(move < 0 && _facingRight)
            Flip(move);
    }

    void Update()
    {
        if (_isGround && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if ((!_isGround || _isParry) && Input.GetKeyDown(KeyCode.Space))
        {
            Parry();
        }

        if (Input.GetKey(KeyCode.F))
        {
            Fire();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
           StopFire();
        }
    }

    /// <summary>
    /// 转头
    /// </summary>
    private void Flip(float move)
    {
        rotation = move > 0 ? Rotation.Right : Rotation.Left;
        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    /// <summary>
    /// 跳跃
    /// </summary>
    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpSpeed);
        _animator.SetTrigger("IsJump");
        _fireAnimator.SetBool("Shoot", false);
        _animator.SetBool("IsFire", false);
    }

    /// <summary>
    /// Parry
    /// </summary>
    private void Parry()
    {
        _animator.SetTrigger("IsParry");
        if (_isParry)
        {
            OnParry();
        }
    }

    private float time;
    void Fire()
    {
        time += Time.deltaTime;
        if (time > rate)
        {
            time = 0;
            StartFire();
            switch (rotation)
            {
                case Rotation.Right:
                    _firePos.eulerAngles = new Vector3(0, 0, 0);
                    break;
                case Rotation.Left:
                    _firePos.eulerAngles = new Vector3(0,180,0);
                break;
            }
            Instantiate(_bulletPrefab, _firePos.position, _firePos.rotation);
        }
    }

    void OnParry()
    {
        _rigidbody.gravityScale = 0;
        _animator.speed = 0;
        _isParrying = true;
        _rigidbody.velocity = new Vector2(0, 0); 
        StartCoroutine(OnEumParry());

    }

    IEnumerator OnEumParry()
    {
        yield return new WaitForSeconds(0.1f);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _parrySpeed);
        _rigidbody.gravityScale = 5;
        _animator.speed = 1;
        _isParrying = false;
    }

    void StartFire()
    {
        if (_isGround)
        {
            _fireAnimator.SetBool("Shoot", true);
        }
        _animator.SetBool("IsFire", true);
    }

    void StopFire()
    {
        if (_isGround)
        {
            _fireAnimator.SetBool("Shoot", false);
        }
        _animator.SetBool("IsFire", false);
    }
}
