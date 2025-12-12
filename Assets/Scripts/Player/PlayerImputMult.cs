using UnityEngine;
using UnityEngine.InputSystem;
using UnityPlayerInput = UnityEngine.InputSystem.PlayerInput;

namespace Player
{

    [RequireComponent(typeof(UnityPlayerInput))]
    public class PlayerImputMult : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        Vector3 move;
        Rigidbody2D rb;


        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            rb.MovePosition(transform.position + move * (Time.fixedDeltaTime * speed));
        }

        public void OnSpecial(InputValue value)
        {
            Debug.Log($"Special: {value.Get<float>()}");
        }

        public void OnFire(InputValue value)
        {
            Debug.Log("Fire Mult");
        }

        public void OnMovement(InputValue value)
        {
            move = value.Get<Vector2>();
        }

        public void OnDoDisconnect()
        {
            PlayerSpawner.Disconnect(GetComponent<UnityPlayerInput>());
            Destroy(gameObject);
        }
    }

}