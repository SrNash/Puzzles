using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorWithLock : MonoBehaviour
{
    [Header("Player Input")]
    [Tooltip("Player Input, configuracion de inputs del Player")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Door Prefabs")]
    [Tooltip("Prefab de la puerta")]
    [SerializeField]private GameObject _door;

    [Header("Inventory")]
    [Tooltip("Llave del inventario")]
    [SerializeField]private GameObject _cardIDInv;

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
    [SerializeField] private AudioSource _lockedSound;
    [SerializeField]private AudioSource _unlockedSound;

    [Header("Reach")]
    [Tooltip("ReachTool para la interacción con la puerta")]
    [SerializeField]private bool _inReach = false;

    [Header("Unlock/Lock")]
    [SerializeField] private bool _unlocked = false;
    /*[SerializeField] private*/ public bool _locked = true;
    /*[SerializeField] private*/public  bool _hasKey;
    //public bool HasKey { get { return _hasKey; } set { _hasKey = value; } }

    [Header("Control Bool")]
    [Tooltip("Booleans de control")]
    [SerializeField] private bool _isOpens = false;
    [SerializeField] private bool _isCloses = true;

    private void Start()
    {
        _inReach = false;
        _hasKey = false;
        _unlocked = false;
        _locked = true;
    }

    private void Update()
    {
        /*if (_keyINV.activeInHierarchy)
        {
            _locked = false;
            _unlocked = true;
        }
        else
        {
            _hasKey = false;
        }*/

        if (_hasKey && _inReach && _playerInput.actions["Interaction"].WasPressedThisFrame())   //cambiar GetKeyDown -> GetButtonDown
        {
            _unlocked = true;
            DoorOpen();
        }
        else if (_isOpens && _inReach && _playerInput.actions["Interaction"].WasPressedThisFrame())
        {
            DoorClose();
        }
        else if(_locked)
        {
            Debug.Log("Cerrada con llave");
            DoorClose();
        }

        if(_locked && _inReach && _playerInput.actions["Interaction"].WasPressedThisFrame())
        {
            Debug.Log("Cerrada con llave 2");
            _lockedSound.Play();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entro");

        if (other.CompareTag("Reach"))
        {
            _inReach = true;
            _openText.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Entro");

        if (other.CompareTag("Reach"))
        {
            _inReach = false;
            _openText.SetActive(false);
        }
    }

    public void DoorOpen()
    {
        if (_unlocked)
        {
            _isOpens = true;
            _isCloses = false;

            //Seteamos las propiedades del animator
            _doorAnim.SetBool("openDoor", true);
            _doorAnim.SetBool("closeDoor", false);
            _doorOpenSound.Play();
        }
    }

    public void DoorClose()
    {
        if (_unlocked)
        {
            _isOpens = false;
            _isCloses = true;

            //Seteamos las propiedades del animator
            _doorAnim.SetBool("openDoor", false);
            _doorAnim.SetBool("closeDoor", true);
            _doorCloseSound.Play();
        }
    }
}
