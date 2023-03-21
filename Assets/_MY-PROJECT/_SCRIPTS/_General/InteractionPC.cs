using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class InteractionPC : MonoBehaviour
{
    [Header("Player Input")]
    [Tooltip("Player Input, configuracion de inputs del PLAYER")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Text")]
    [Tooltip("Texto que contine el DIALOGO")]
    [SerializeField] private TextMeshProUGUI _message;

    [Header("Dialogues")]
    [Tooltip("Dialogos de la AI que asistirá al PLAYER")]
    [SerializeField] private int _dialoguesIndex;
    [Tooltip("Fases de la AI que asistirá al PLAYER")]
    [SerializeField] private string[] _dialogues;
    [SerializeField] private int _dialoguesCount;

    [Header("Reach")]
    [Tooltip("Variable de control de alcance")]
    [SerializeField] private bool _inReach;

    [Header("GameObjects")]
    [Tooltip("GameObject para la interaccion con la AI")]
    [SerializeField] private GameObject _talk;
    [Tooltip("GameObject para siguiente mensaje del DIALOGO")]
    [SerializeField] private GameObject _next;
    [Tooltip("GameObject para salir del DIALOGO")]
    [SerializeField] private GameObject _exit;
    [SerializeField] private GameObject _dialogueText;

    [Header("AudioSource")]
    [Tooltip("AudioSource al recoger las notas")]
    [SerializeField] private AudioSource _pcAISound;
    [SerializeField] private AudioSource _dialogueAISOund;

    [Header("Control")]
    [Tooltip("Variables de control de cuando esta en DIALOGO o no")]
    [SerializeField] private bool _inDialogue = false;
    [SerializeField] private bool _isPrinting = false;

    // Update is called once per frame
    void Update()
    {
        if (_playerInput.actions["Interaction"].WasPressedThisFrame() && _inReach && !_inDialogue)
        {
            TalkWithAI();
        }
        else if (_playerInput.actions["Interaction"].WasPressedThisFrame() && _inDialogue && (_dialoguesCount != _dialogues.Length))
        {
            NextMessage();
        }
        else if (_playerInput.actions["Interaction"].WasPressedThisFrame() && _inDialogue)
        {
            ExitDialogue();
        }
    }

    private void TalkWithAI()
    {
        _dialoguesCount = _dialoguesIndex;
        _inDialogue = true;
        _dialogueText.SetActive(true);
        _message.text = _dialogues[_dialoguesCount];
    }

    private void NextMessage()
    {
        if (_dialoguesCount >= _dialogues.Length - 1)
        {
            _dialoguesCount = _dialogues.Length - 1;
            //_message.text = _dialogues[_dialoguesCount];
            ShowExit();
            HideNext();
        }
        else if (_dialoguesCount < _dialogues.Length - 1)
        {
            _dialoguesCount += 1;
            _message.text = _dialogues[_dialoguesCount];
            ShowNext();
        }
    }

    private void ExitDialogue()
    {
        _inDialogue = false;
        _dialogueText.SetActive(false);
        ShowExit();
    }

    /// <summary>
    /// Mostramos y ocultamos las opciones de siguiente o salir del DIALOGO
    /// </summary>
    private void ShowNext()
    {
        _next.SetActive(true);
    }

    private void HideNext()
    {
        _next.SetActive(false);
    }
    private void ShowExit()
    {
        _exit.SetActive(true);
        _next.SetActive(false);
    }
    private void HideExit()
    {
        _exit.SetActive(false);
        _next.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach") && !_inDialogue)
        {
            _inReach = true;
            _talk.SetActive(true);
        }
        else if (_inDialogue)
        {
            _talk.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            _inReach = false;
            _talk.SetActive(false);
        }
    }
}