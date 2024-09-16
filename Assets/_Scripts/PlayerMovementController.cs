using System;
using System.Collections;
using Core.Input;
using UnityEngine;
using UnityEngine.Windows;

namespace Core.Player.Movement
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private BaseInput _baseInput;

        [SerializeField] private Vector2 _moveDirection;

        [SerializeField] private float _moveTime;
        [SerializeField] private float _moveTimeCounter;

        [SerializeField] private Rigidbody2D _rigidBody;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();

            _baseInput = new();
            _baseInput.Enable();
        }

        private void OnDisable()
        {
            _baseInput.Disable();
        }

        private void Update()
        {
            Timer();
            Move(ReadInput());
        }

        private Vector2 ReadInput()
        {
            _moveDirection = _baseInput.Gameplay.Movement.ReadValue<Vector2>();

            if (_moveDirection == Vector2.zero) return Vector2.zero;

            if (_moveDirection.y > 0) return Vector2.up;     // W
            if (_moveDirection.y < 0) return Vector2.down;   // S
            if (_moveDirection.x < 0) return Vector2.left;   // A
            if (_moveDirection.x > 0) return Vector2.right;  // D

            return Vector2.zero;
        }

        private void Move(Vector2 direction)
        {
            if (direction == Vector2.zero) return;
            if (_moveTimeCounter > 0) return;

            StartCoroutine(MoveOverTime(RoundPosition(_rigidBody.position), RoundPosition(_rigidBody.position) + direction, _moveTime / 2));

            _moveTimeCounter = _moveTime;
        }

        private Vector2 RoundPosition(Vector2 position)
        {
            position.y = position.y >= 0 ? Mathf.Ceil(position.y) : Mathf.Floor(position.y);
            position.x = position.x >= 0 ? Mathf.Ceil(position.x) : Mathf.Floor(position.x);

            return position;
        }

        private IEnumerator MoveOverTime(Vector3 from, Vector3 to, float duration)
        {
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                _rigidBody.MovePosition(Vector3.Lerp(from, to, elapsedTime / duration));
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            _rigidBody.position = (to);
        }

        private void Timer()
        {
            if (_moveTimeCounter <= 0) return;

            _moveTimeCounter -= Time.deltaTime;
        }
    }
}