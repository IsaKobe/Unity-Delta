using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform spawnPoint;

    [SerializeField] InputAction joinAction;

#pragma warning disable UDR0001 // Domain Reload Analyzer
    static List<InputDevice> devices;
#pragma warning restore UDR0001 // Domain Reload Analyzer

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        joinAction.Enable();
        joinAction.performed += (c) => Join(c.control.device);
        devices = new List<InputDevice>();
        Join(Keyboard.current);
    }

    void Join(InputDevice device)
    {
        if (!devices.Contains(device))
        {
            PlayerInput p = PlayerInput.Instantiate(
                    playerPrefab,
                    pairWithDevice: device);
            p.transform.position = spawnPoint.position;
            devices.Add(device);
        }
    }

    public static void Disconnect(PlayerInput playerInput)
    {
        devices.Remove(playerInput.devices[0]);
    }
}
