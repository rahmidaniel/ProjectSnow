using System;
using _Scripts.Utility;
using FMOD.Studio;
using UnityEngine;

namespace _Scripts.Units.Capabilities
{
    public enum PlayerAnimationState
    {
        Idle,
        Run,
        Jump,
        Hit,
        Fall,
        Death
    }

    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        public static PlayerAnimator Instance;
        private static bool _animatorValid;
        private Animator _animator;
        private EventInstance _deathInstance;
        private EventInstance _footstepsInstance;
        private EventInstance _frozenInstance;
        private EventInstance _hitInstance;

        private EventInstance _jumpInstance;

        // move audio here
        public static PlayerAnimationState CurrentState { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            if (Instance != null) return;
            Instance = this;
            _animatorValid = true;
        }

        private void Start()
        {
            _footstepsInstance = SoundManager.Instance.CreateEventInstance(FMODEvents.Instance.Running);
            _jumpInstance = SoundManager.Instance.CreateEventInstance(FMODEvents.Instance.Jump);
            _deathInstance = SoundManager.Instance.CreateEventInstance(FMODEvents.Instance.Hit); // todo death
            _hitInstance = SoundManager.Instance.CreateEventInstance(FMODEvents.Instance.Hit);
            _frozenInstance = SoundManager.Instance.CreateEventInstance(FMODEvents.Instance.FrozenBreath);
        }

        private static string GetName(PlayerAnimationState value)
        {
            return Enum.GetName(typeof(PlayerAnimationState), value);
        }

        public void ChangeAnimation(PlayerAnimationState newState)
        {
            if (CurrentState == newState && _animatorValid) return;
            _animator.Play(GetName(newState));

            CurrentState = newState;
        }

        public float GetAnimationLength()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).length * _animator.GetCurrentAnimatorStateInfo(0).speed;
        }

        public bool IsAnimationFinished(PlayerAnimationState newState)
        {
            if (CurrentState == newState && _animatorValid)
            {
                var time = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                return Math.Abs(time - (long) time - 0.1f) < 0.01f;
            }

            return true;
        }

        public void StepSound()
        {
            _footstepsInstance.getPlaybackState(out var playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) _footstepsInstance.start();
        }

        public void JumpSound(float jump)
        {
            _jumpInstance.stop(STOP_MODE.ALLOWFADEOUT);
            _jumpInstance.setParameterByName("jump", jump);
            _jumpInstance.start();
        }

        public void DeathSound()
        {
            _deathInstance.start();
        }

        public void HitSound()
        {
            _hitInstance.start();
        }

        public void FrozenBreathSound(bool play)
        {
            if (!play)
            {
                _frozenInstance.stop(STOP_MODE.ALLOWFADEOUT);
                return;
            }

            _frozenInstance.getPlaybackState(out var stat);
            if (stat == PLAYBACK_STATE.STOPPED)
                _frozenInstance.start();
        }
    }
}