using UnityEngine;

namespace Scenes.Sctips.Checks
{
    public class Wall : MonoBehaviour
    {
        private bool _hitWall;
        private float _friction;

        private Vector2 _normal;
        private PhysicsMaterial2D _material;

        private void OnCollisionExit2D(Collision2D collision)
        {
            _hitWall = false;
            _friction = 0;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            EvaluateCollision(collision);
            RetrieveFriction(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            EvaluateCollision(collision);
            RetrieveFriction(collision);
        }

        private void EvaluateCollision(Collision2D collision)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                _normal = collision.GetContact(i).normal;
                _hitWall |= _normal.x >= 0.9f;
            }

        }

        private void RetrieveFriction(Collision2D collision)
        {
            _material = collision.rigidbody.sharedMaterial;

            _friction = 0;

            if (_material != null)
            {
                _friction = _material.friction;
            }
        }
    }
}