using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWithLock : MonoBehaviour
{
    [Header("Door Prefabs")]
    [Tooltip("Prefab de la puerta")]
    [SerializeField]private GameObject _door;

    [Header("Inventory")]
    [Tooltip("Llave del inventario")]
    [SerializeField]private GameObject _keyINV;

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
    [SerializeField] private bool _locked = true;
    [SerializeField] private bool _hasKey;

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

        if (_hasKey && _inReach && Input.GetKeyDown("f"))   //cambiar GetKeyDown -> GetButtonDown
        {
            _unlocked = true;
            DoorOpen();
        }else
        {
            Debug.Log("Cerrada con llave");
            DoorClose();
        }

        if(_locked && _inReach && Input.GetKeyDown("f"))
        {
            Debug.Log("Cerrada con llave 2");
            _lockedSound.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {        
        if (other.CompareTag("Reach"))
        {
            _inReach = true;
            _openText.SetActive(true);
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
