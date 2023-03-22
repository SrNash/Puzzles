using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenDoor : MonoBehaviour
{
    [Header("Player Input")]
    [Tooltip("Player Input, configuracion de inputs del Player")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Door Prefabs")]
    [Tooltip("Prefab de la puerta")]
    [SerializeField]private GameObject _door;

    [Header("Animator")]
    [Tooltip("Animator de la puerta")]
    [SerializeField] private Animator _doorAnim;

    [Header("Text Prefab")]
    [Tooltip("Prefab de indicación de interacción")]
    [SerializeField]private GameObject _openText;
    
    [Header("AudioSource")]
    [Tooltip("AudioSource de la puerta")]
    [SerializeField] private AudioSource _doorOpenSound;
    [SerializeField] private AudioSource _doorCloseSound;

    [Header("Reach")]
    [Tooltip("ReachTool para la interacción con la puerta")]
    [SerializeField]private bool _inReach = false;

    [Header("Control Bool")]
    [Tooltip("Booleans de control")]
    [SerializeField] private bool _isOpens = false;
    [SerializeField] private bool _isCloses = true;

    private void Start()
    {
        _inReach = false;
    }

    private void Update()
    {
        if ((_inReach && _playerInput.actions["Interaction"].WasPressedThisFrame()) && _isCloses) //cambiar GetKeyDown -> GetButtonDown
        {
            Debug.Log("Abriendo");
            DoorOpen();
        }
        else if(_inReach && _playerInput.actions["Interaction"].WasPressedThisFrame() && _isOpens) //cambiar GetKeyDown -> GetButtonDown
        {
            Debug.Log("Cerrando");
            DoorClose();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            _inReach = true;
            _openText.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            _inReach = false;
            _openText.SetActive(false);
        }
    }

    public void DoorOpen()
    {
        Debug.Log("Está Abierta");

        _isOpens = true;
        _isCloses = false;

        //Seteamos las propiedades del animator
        _doorAnim.SetBool("openDoor", true);
        _doorAnim.SetBool("closeDoor", false);
        _doorAnim.SetBool("closed", false);
        _doorOpenSound.Play();
    }

    public void DoorClose()
    {
        Debug.Log("Está Cerrada");

        _isOpens = false;
        _isCloses = true;

        //Seteamos las propiedades del animator
        _doorAnim.SetBool("openDoor", false);
        _doorAnim.SetBool("closeDoor", true);
        _doorAnim.SetBool("closed", false);
        _doorCloseSound.Play();
    }

    public void DoorStayOpen()
    {
        _doorAnim.SetBool("opened", true);
    }
}
