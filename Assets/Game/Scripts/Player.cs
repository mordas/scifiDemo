﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField] private float _speed = 2f;
    private float _gravity = 9.81f;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float vericalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, vericalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;
        _controller.Move(velocity * Time.deltaTime);
    }
}