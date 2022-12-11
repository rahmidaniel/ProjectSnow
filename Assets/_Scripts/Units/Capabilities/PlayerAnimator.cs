using System;
using _Scripts.Utility;
using FMOD.Studio;
using Unity.VisualScripting;
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
        // move audio here
        public static PlayerAnimationState CurrentState { get; private set; }
        public static PlayerAnimator Instance;
        public Animator _animator;
        private static bool _animatorValid;
        private EventInstance _footstepsInstance;
        private EventInstance _jumpInstance;
        private EventInstance _deathInstance;
        private EventInstance _hitInstance;
        private EventInstance _frozenInstance;

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
            _hitInstance = SoundManager.Instance.CreateEventInstance(FMODEvents.Instance.Hit); // todo death
            _frozenInstance = SoundManager.Instance.CreateEventInstance(FMODEvents.Instance.FrozenBreath);
        }

        private static string GetName(PlayerAnimationState value) => Enum.GetName(typeof(PlayerAnimationState), value);

        public void ChangeAnimation(PlayerAnimationState newState)
        {
            if(CurrentState == newState && _animatorValid) return;
            _animator.Play(GetName(newState));

            CurrentState = newState;
        }
        public bool IsAnimationFinished(PlayerAnimationState newState)
        {
            if (CurrentState == newState && _animatorValid)
            {
                return Math.Abs(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime - 1f) < 0.1f;
            }

            return true;
        }

        public void StepSound()
        {
            _footstepsInstance.getPlaybackState(out var playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                _footstepsInstance.start();
            }
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

        public void FrozenBreathSound()
        {
            _frozenInstance.start();
        }
    }
    
    
}