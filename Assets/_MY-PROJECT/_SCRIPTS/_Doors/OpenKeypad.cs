using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenKeypad : MonoBehaviour
{
    [Header("Player Input")]
    [Tooltip("Player Input, configuracion de inputs del Player")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Text Prefab")]
    [Tooltip("Prefab de indicaci�n de interacci�n")]
    [SerializeField] private GameObject _keypadText;

    [Header("Keypad")]
    [Tooltip("Referencia al GameObject del Keypad")]
    [SerializeField] private GameObject _keypadOB;

    [Header("Keypad")]
    [Tooltip("Referencia al script del Keypad")]
    [SerializeField] private Keypad _keypad;

    [Header("Reach")]
    [Tooltip("ReachTool para la interacci�n con la puerta")]
    [SerializeField] private bool _inReach = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_inReach && _playerInput.actions["Interaction"].WasPressedThisFrame())
        {
            _keypadOB.SetActive(true);
            _keypad.solving = true;
            _keypad.textOB.text = "";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            _inReach = true;
            _keypadText.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            _inReach = true;
            _keypadText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            _inReach = false;
            _keypadText.SetActive(false);
        }
    }
}
