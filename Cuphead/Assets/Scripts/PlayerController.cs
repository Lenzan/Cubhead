using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _moveSpeed;
    public float _jumpSpeed;
    public float _parrySpeed;
    public Transform _groundCheck;
    public LayerMask _whatIsGround;
    public LayerMask _whatIsParry;


    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool _facingRight = true;
    private bool _isGround = false;
    private bool _isParry = false;
    private float _groundRadius = 0.2f;
    private float _parryRadius = 0.3f;



    // Use this for initialization
    void Awake ()
	{
	    _rigidbody = GetComponent<Rigidbody2D>();
	    _animator = GetComponent<Animator>();

	}

    void FixedUpdate()
    {
        _isGround = Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _whatIsGround);
        _isParry = Physics2D.OverlapCircle(_groundCheck.position, _parryRadius, _whatIsParry);
        _animator.SetBool("IsGround" , _isGround);

        float move = Input.GetAxis("Horizontal");

        _animator.SetFloat("Speed" , Mathf.Abs(move));

        _rigidbody.velocity = new Vector2(move * _moveSpeed , _rigidbody.velocity.y);

        if (move > 0 && !_facingRight)
            Flip();
        else if(move < 0 && _facingRight)
            Flip();
    }

    void Update()
    {
        if (_isGround && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if ((!_isGround || _isParry) && Input.GetMouseButtonDown(0))
        {
            Parry();
        }
    }

    /// <summary>
    /// 转头
    /// </summary>
    private void Flip()
    {
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

    void OnParry()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _parrySpeed);
    }
}
