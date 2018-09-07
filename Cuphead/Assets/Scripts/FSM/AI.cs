using UnityEngine;
using System.Collections;

namespace FSM
{
    public class AI : MonoBehaviour
    {
        [HideInInspector]
        public Animator _animator;
        [HideInInspector]
        public Rigidbody2D _rigidbody2D;

        public Transform _firePos;
        public GameObject _bulletPrefab;

        public virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public virtual void Start(){}

        public virtual void FixedUpdate(){}

        public virtual void Update(){}

        /// <summary>
        /// 开火
        /// </summary>
        public virtual void DoStartFire(){}
        
        /// <summary>
        /// 停止开火
        /// </summary>
        public virtual void DoEndFire(){}
    }


}

