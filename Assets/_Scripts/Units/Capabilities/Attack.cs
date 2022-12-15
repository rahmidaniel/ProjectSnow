using System;
using _Scripts.Environment;
using Scenes.Sctips.Checks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Units.Capabilities
{
    public class Attack : MonoBehaviour
    {
        public static bool Attacking;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float radius = 2f;
        [SerializeField] private LayerMask layerMask;

        private Ground _ground;

        private void Start()
        {
            _ground = GetComponent<Ground>();
        }

        private void OnHit(InputValue value)
        {
            if (!enabled || Attacking || !_ground.OnGround) return;
            Attacking = true;
            PlayerAnimator.Instance.ChangeAnimation(PlayerAnimationState.Hit);
            Invoke(nameof(FinishAttack), PlayerAnimator.Instance.GetAnimationLength());

            var hit = Physics2D.OverlapCircle(attackPoint.position, radius, layerMask);
            if(hit == null) return;
            if (hit.transform.gameObject.TryGetComponent(out TreeController tree))
            {
                tree.Hit();
            }
        }

        private void FinishAttack()
        {
            Attacking = false;
        }
    }
}