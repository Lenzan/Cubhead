using System;

namespace FSM
{
    public class StateMachine<T>
    {
        /// <summary>
        /// 当前的状态
        /// </summary>
        public State<T> _currentState { get; private set; }

        /// <summary>
        /// 执行者
        /// </summary>
        public T _owner;

        public StateMachine(T o)
        {
            _owner = o;
            _currentState = null;
        }

        /// <summary>
        /// 转换状态
        /// </summary>
        /// <param name="newState"></param>
        public void SwitchState(State<T> newState)
        {
            if (_currentState != null)
                _currentState.ExitState(_owner);
            _currentState = newState;
            _currentState.EnterState(_owner);
        }

        /// <summary>
        /// 检测输入
        /// </summary>
        public void HandleInput()
        {
            _currentState.HandleInput(_owner);
        }

        /// <summary>
        /// 状态更新
        /// </summary>
        public void Update()
        {
            _currentState.UpdateState(_owner);
        }
    }

    public abstract class State<T>
    {
        /// <summary>
        /// 状态进入
        /// </summary>
        /// <param name="owner"></param>
        public abstract void EnterState(T owner);

        /// <summary>
        /// 状态退出
        /// </summary>
        /// <param name="owner"></param>
        public abstract void ExitState(T owner);

        /// <summary>
        /// 检测输入
        /// </summary>
        /// <param name="owner"></param>
        public abstract void HandleInput(T owner);

        /// <summary>
        /// 状态更新
        /// </summary>
        /// <param name="owner"></param>
        public abstract void UpdateState(T owner);
    }
}

