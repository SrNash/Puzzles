using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class ReadMessage : MonoBehaviour
{
    [Header("Player Input")]
    [Tooltip("Player Input, configuracion de inputs del Player")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Text")]
    [Tooltip("Texto que contine el INFORME")]
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private string m_title;
    [SerializeField] private TextMeshProUGUI _author;
    [SerializeField] private string m_author;
    [SerializeField] private TextMeshProUGUI _addressee;
    [SerializeField] private string m_addressee;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField]private string m_text;

    [Header("Reach")]
    [Tooltip("Variable de control de alcance")]
    [SerializeField] private bool _inReach;

    [Header("Player")]
    [Tooltip("GameObject del Player")]
    [SerializeField] private GameObject _player;

    [Header("Diaries Inventory")]
    [Tooltip("Inventario de Diarios para almacenar las notas")]
    [SerializeField]private GameObject _diariesInv;

    [Header("GameObjects")]
    [Tooltip("GameObjects que intervendrán en la interaccion con las baterias")]
    [SerializeField] private GameObject _noteUI;
    [Tooltip("HUD de la lectura de la nota")]
    [SerializeField] private GameObject _hud;
    [Tooltip("Inventario en donde guardaremos las notas")]
    [SerializeField] private GameObject _inv;
    [Tooltip("GameObject para la interaccion con las notas")]
    [SerializeField]private GameObject _pickUpText;

    [Header("Control")]
    [SerializeField] private bool _wasPicked = false;

    [Header("AudioSource")]
    [Tooltip("AudioSource al recoger las notas")]
    [SerializeField] private AudioSource _pickUpSound;


    // Start is called before the first frame update
    void Start()
    {
        _noteUI.SetActive(false);
        _hud.SetActive(true);
        _inv.SetActive(true);
        _pickUpText.SetActive(false);

        _inReach = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerInput.actions["Interaction"].WasPressedThisFrame() && _inReach && !_wasPicked)
        {
            ReadMail();
        }

        else if (_playerInput.actions["Interaction"].WasPressedThisFrame() && _wasPicked)
        {
            ExitButton();
        }
    }

    private void ReadMail()
    {
        _wasPicked = true;
        _noteUI.SetActive(true);    //No se activa
        _pickUpSound.Play();
        _hud.SetActive(false);
        _inv.SetActive(false);

        _title.text = m_title;
        _author.text = m_author;
        _addressee.text = m_addressee;
        _text.text = m_text;
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

    public void ExitButton()
    {
        _wasPicked = false;
        _noteUI.SetActive(false);
        _hud.SetActive(true);
        _inv.SetActive(true);
    }
}
