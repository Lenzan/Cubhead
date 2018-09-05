using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _moveSpeed;
    public float _jumpSpeed;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
	// Use this for initialization
	void Awake ()
	{
	    _rigidbody = GetComponent<Rigidbody2D>();
	    _animator = GetComponent<Animator>();

	}

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Move(-1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            StopMove();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            StopMove();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }





    private void Move(int dir)
    {
        transform.localScale = new Vector3(dir , 1 , 1);
        _rigidbody.velocity = new Vector2(dir * _moveSpeed , _rigidbody.velocity.y);
        _animator.SetBool("IsWalk" , true);
    }

    private void StopMove()
    {
        _rigidbody.velocity = new Vector2(0 , _rigidbody.velocity.y);
        _animator.SetBool("IsWalk", false);
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpSpeed);
        _animator.SetTrigger("IsJump");
    }
}
