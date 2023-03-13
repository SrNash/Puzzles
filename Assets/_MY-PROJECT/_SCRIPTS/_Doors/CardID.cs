using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardID : MonoBehaviour
{
    [Header("Player Input")]
    [Tooltip("Player Input, configuracion de inputs del Player")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Inventory")]
    [Tooltip("Inventario del Player")]
    [SerializeField] private GameObject _inventory;

    [Header("Components")]
    [Tooltip("Componentes a desactivar")]
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private BoxCollider _collider;


    [Header("Doors")]
    [Tooltip("Puertas en las que se puede usar la tarjeta de identificacion")]
    [SerializeField] private GameObject[] _doors;

    [Header("Text Prefab")]
    [Tooltip("Prefab de indicación de interacción")]
    [SerializeField] private GameObject _openText;

    [Header("Reach")]
    [Tooltip("ReachTool para la interacción con la puerta")]
    [SerializeField] private bool _inReach = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerInput.actions["Interaction"].WasPressedThisFrame() && _inReach)
        {
            PickingCardID();
        }
    }

    private void PickingCardID()
    {
        transform.parent = _inventory.transform;
        transform.position = _inventory.transform.position;
        _mesh.enabled = false;
        _collider.enabled = false;

        for (int i = 0; i < _doors.Length; i++)
        {
            _doors[i].GetComponent<DoorWithLock>()._hasKey = true;
            _doors[i].GetComponent<DoorWithLock>()._locked = false;
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
}
