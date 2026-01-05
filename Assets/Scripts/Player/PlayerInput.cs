using Projectiles.Controllers.Data;
using Projectiles;
using Projectiles.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using World;

namespace Player.PlayerInputs
{
    public class PlayerInput : MonoBehaviour, IPausable
    {
        [SerializeField] InputActionAsset asset;
        [SerializeField] float speed = 5;

        [SerializeField] List<Collider> shipBody;
        [SerializeField] Shield shield;

        [Header("Projectiles")]
        [SerializeField] SimpleProjData projData;
        [SerializeField] RocketProjData rocketData;

        InputAction movement;
        InputAction fire;
        InputAction special;

        Vector3 move;
        Rigidbody2D rb;

        float rocketCooldown;


        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            shipBody = transform.GetChild(0).GetComponentsInChildren<Collider>().ToList();
            shield = transform.GetChild(0).GetComponent<Shield>();
            movement = asset.FindActionMap("Player").FindAction("Movement");
            fire = asset.FindActionMap("Player").FindAction("Fire");
            special = asset.FindActionMap("Player").FindAction("Special");

            rocketCooldown = 0;
        }

        void Special(InputAction.CallbackContext obj)
        {
            Debug.Log($"Special {obj.ReadValue<float>()}");
            /*if (canFireRockets && obj.ReadValue<float>() == 1)
            {
                RProjController.GetProjectile(rocketData, transform);
            }*/
        }

        public void EnableRockets(float cooldown)
        {
            rocketCooldown = cooldown;
            StartCoroutine(FireRocket());
        }

        IEnumerator FireRocket()
        {
            while (true)
            {
                yield return new PauseWaitUntil(rocketCooldown);
                RProjController.GetProjectile(rocketData, transform);
            }
        }


        void Fire(InputAction.CallbackContext context)
        {
            SProjController.GetProjectile(projData, transform);
        }

        private void OnDisable()
        {
            asset.Disable();
            fire.performed -= Fire;
            special.performed -= Special;
        }

        private void OnEnable()
        {
            asset.Enable();
            fire.performed += Fire;
            special.performed += Special;
        }


        void Update()
        {
            move = movement.ReadValue<Vector2>();
        }

        void FixedUpdate()
        {
            rb.MovePosition(transform.position + move * speed);
        }

        public void OnPause()
        {
            enabled = false;
            asset.Disable();
        }

        public void OnResume()
        {
            enabled = true;
            asset.Enable();
        }
    }

}