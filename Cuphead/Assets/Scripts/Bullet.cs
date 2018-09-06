using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float _flySpeed;

    private Animator _animator;
    private bool isCanDestroy = false;
	// Use this for initialization
	void Awake ()
	{
	    _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    transform.Translate( transform.right * _flySpeed , Space.World);

	    AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

	    if (animatorStateInfo.IsName("boom"))
	    {
	        isCanDestroy = true;
	    }
	    if (!animatorStateInfo.IsName("boom") && isCanDestroy)
	    {
	        Destroy(gameObject);
	    }

    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player") return;
        _flySpeed = 0;
        _animator.SetBool("IsBoom" , true);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
