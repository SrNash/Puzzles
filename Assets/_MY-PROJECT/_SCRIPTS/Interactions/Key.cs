using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [Header("Reach")]
    [Tooltip("Variable de control de alcance")]
    [SerializeField] private bool _inReach;

    [Header("Inventory")]
    [SerializeField] private GameObject _keyOB;
    //[SerializeField] private GameObject _invOB;
    

    [Header("GameObjects")]
    [Tooltip("Inventario en donde guardaremos las notas")]
    [SerializeField] private GameObject _inv;
    [Tooltip("GameObject para la interaccion con las notas")]
    [SerializeField] private GameObject _pickUpText;

    [Header("AudioSource")]
    [Tooltip("AudioSource al recoger las notas")]
    [SerializeField] private AudioSource _keyPickUpSounds;
    // Start is called before the first frame update
    void Start()
    {
        _inReach = false;
        _pickUpText.SetActive(false);
        //_invOB.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f") && _inReach)
        {
            WasPickUp();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            _inReach = true;
            _pickUpText.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            _inReach = true;
            _pickUpText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            _inReach = false;
            _pickUpText.SetActive(false);
        }
    }

    public void WasPickUp()
    {
        _keyOB.SetActive(false);
        _keyPickUpSounds.Play();
        _inReach = false;
        //_invOB.SetActive(true);
        _pickUpText.SetActive(false);
        Destroy(this.gameObject);
    }
}
