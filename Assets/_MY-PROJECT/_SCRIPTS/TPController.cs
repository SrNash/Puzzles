using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPController : MonoBehaviour
{
    [Header("Player Input")]
    [Tooltip("Player Input, configuracion de inputs del Player")]
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Vector3 _move;

    [Header("Main Camera")]
    [Tooltip("Referencia a la cámara que utilizaremos en el juego")]
    [SerializeField]private Camera _gameCamera;

    [Header("Player Stats")]
    [Tooltip("Stats del Player tales como la Velocidad o la Fuerza de Salto")]
    [SerializeField]private float _playerSpeed = 2.0f;
    [SerializeField]private float _jumpForce = 1.0f;
    [SerializeField] private float _dashForce = 1.0f;
    
    [Header("Character Controller")]
    [Tooltip("Componente CharacterController")]
    [SerializeField]private CharacterController _controller;

    [Header("Animator")]
    [Tooltip("Componente Animator")]
    [SerializeField] private Animator _animator;

    [Header("Speed")]
    [Tooltip("Velocidad de desplazamiento del Player")]
    [SerializeField] private Vector3 _playerVelocity;

    [Header("On ground")]
    [Tooltip("Boolean para conocer si el Player esta sobre el suelo o no para aplicar la Fuerza de Gravedad")]
    [SerializeField] private bool _groundedPlayer;
    [SerializeField] private float _gravityValue = -9.81f;

    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        _animator = gameObject.GetComponentInChildren<Animator>();

        _playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        _groundedPlayer = _controller.isGrounded;
        
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -0.5f;
        }

        Vector2 movementInput = _playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 input = new Vector3(movementInput.x, 0f, movementInput.y);

        //trasnform input into camera space
        var forward = _gameCamera.transform.forward;
        forward.y = 0;
        forward.Normalize();
        var right = Vector3.Cross(Vector3.up, forward);
        
        _move = forward * input.z + right * input.x;
        _move.y = 0;
        
        _controller.Move(_move * Time.deltaTime * _playerSpeed);

        _animator.SetFloat("MovementX", input.x);
        _animator.SetFloat("MovementZ", input.z);

        if (input != Vector3.zero)
        {
            gameObject.transform.forward = forward;
        }

        // Changes the height position of the player..
        if (_playerInput.actions["Jump"].WasPressedThisFrame() && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpForce * -3.0f * _gravityValue);
            _animator.SetTrigger("Jump");
        }

        //Interaction with other objects
        if (_playerInput.actions["Interaction"].WasPressedThisFrame())
        {
            Debug.Log("Interactuando con otros objetos...");
        }

        if (_playerInput.actions["Dash"].WasPressedThisFrame())
        {
            Dash();
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;

        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    private void Dash()
    {
        Debug.Log("Dasheando");
        _controller.Move(_move * Time.deltaTime * _playerSpeed * _dashForce);       
    }
}
