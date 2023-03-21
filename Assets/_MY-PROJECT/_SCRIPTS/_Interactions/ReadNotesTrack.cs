using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class ReadNotesTrack : MonoBehaviour
{
    [Header("Player Input")]
    [Tooltip("Player Input, configuracion de inputs del Player")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Reach")]
    [Tooltip("Variable de control de alcance")]
    [SerializeField] private bool _inReach;

    [Header("Text")]
    [Tooltip("Texto que contine la Nota")]
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _codeText;

    [Header("NoteTrackGenerator")]
    [Tooltip("Referencia al script del NoteTrackGenerator")]
    [SerializeField] private NoteTrackGenerator _trackGenerator;

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
            ReadNote();
        }

        else if (_playerInput.actions["Interaction"].WasPressedThisFrame() && _wasPicked)
        {
            ExitButton();
        }
    }

    private void ReadNote()
    {
        _wasPicked = true;
        _noteUI.SetActive(true);    
        _pickUpSound.Play();
        _hud.SetActive(false);
        _inv.SetActive(false);

        //Texto + código
        _text.text = _trackGenerator.mNote;
        _codeText.text = _trackGenerator.code;
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
        Debug.Log("Cerrando la nota");
        _wasPicked = false;
        _noteUI.SetActive(false);
        _hud.SetActive(true);
        _inv.SetActive(true);
    }
}
