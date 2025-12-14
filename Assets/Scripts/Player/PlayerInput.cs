using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.PlayerInput
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] InputActionAsset asset;
        [SerializeField] float speed = 5;

        [SerializeField] Projectile projectilePrefab;
        [SerializeField] List<Projectile> availableProjectiles;
        [SerializeField] int projectileCount = 5;

        [SerializeField] Transform projectileParent;


        [SerializeField] List<Collider> shipBody;
        [SerializeField] Shield shield;


        InputAction movement;
        InputAction fire;
        InputAction special;

        Vector3 move;
        Rigidbody2D rb;


        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            shipBody = transform.GetChild(0).GetComponentsInChildren<Collider>().ToList();
            shield = transform.GetChild(0).GetComponent<Shield>();
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            asset.Enable();
            movement = asset.FindActionMap("Player").FindAction("Movement");
            fire = asset.FindActionMap("Player").FindAction("Fire");
            special = asset.FindActionMap("Player").FindAction("Special");

            fire.performed += Fire;
            special.performed += Special;


            availableProjectiles = new();
            for (int i = 0; i < projectileCount; i++)
                availableProjectiles.Add(CreateProjectile());
        }

        void Special(InputAction.CallbackContext obj)
        {
            Debug.Log($"Special {obj.ReadValue<float>()}");
        }

        void Fire(InputAction.CallbackContext context)
        {
            Projectile projectile;
            if (availableProjectiles.Count > 0)
            {
                projectile = availableProjectiles[0];
                availableProjectiles.RemoveAt(0);
            }
            else
                projectile = CreateProjectile();

            projectile.transform.position = transform.position;
            projectile.gameObject.SetActive(true);
        }

        Projectile CreateProjectile()
        {
            Projectile projectile = Instantiate(
                projectilePrefab, transform.position, Quaternion.identity, projectileParent);
            projectile.gameObject.SetActive(false);
            projectile.onEnd = (projectile) =>
            {
                availableProjectiles.Add(projectile);
                projectile.gameObject.SetActive(false);
            };
            return projectile;
        }

        void Update()
        {
            move = movement.ReadValue<Vector2>();
        }

        void FixedUpdate()
        {
            rb.MovePosition(transform.position + move * speed);
        }
    }

}