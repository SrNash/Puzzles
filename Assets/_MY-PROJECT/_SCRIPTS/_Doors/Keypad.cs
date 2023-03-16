using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keypad : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("Referencia al GameObject del Player")]
    [SerializeField] private GameObject _player;

    [Header("Keypad")]
    [Tooltip("Referencia al GameObject del Keypad")]
    [SerializeField] private GameObject _keypadOB;

    [Header("HUD")]
    [Tooltip("HUD del Player")]
    [SerializeField] private GameObject _hud;

    [Header("Inventory")]
    [Tooltip("Inventario del Player en caso de reuerirlo")]
    [SerializeField] private GameObject _inv;

    [Header("Animate GameObjet")]
    [Tooltip("GameObject animado")]
    [SerializeField] private GameObject _animateOB;

    [Header("Animator")]
    [Tooltip("Animator de la Puerta")]
    [SerializeField] private Animator _animator;

    [Header("Display text")]
    [Tooltip("Texto del display del Keypad")]
    [SerializeField] private TextMeshProUGUI _textOB;
    public TextMeshProUGUI textOB { get { return _textOB; } set { _textOB = value; } }

    [Header("Pass")]
    [Tooltip("Contrasena a para abrir la puerta")]
    [SerializeField] private string _code;
    public string code { get { return _code; }set { _code = value; } }

    [Header("DoorOpen")]
    [Tooltip("Referencia al script de OpenDoor")]
    [SerializeField] private OpenDoor _openDoor;

    [Header("Sound")]
    [Tooltip("Sonidos del Keypad")]
    [SerializeField] private AudioSource _buttonSound;
    [SerializeField] private AudioSource _correctSound;
    [SerializeField] private AudioSource _wrongSound;

    [SerializeField] private bool _solved;
    [SerializeField] private bool _solving;

    [Header("OpenKeypad")]
    [Tooltip("Referencia al script de OpenKeypad")]
    [SerializeField] private OpenKeypad[] _openKeypad;
    public bool solving { get { return _solving; } set { _solving = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_textOB.text == "CORRECT" && _solved)
        {
            _solving = false;
            _openDoor.enabled = true;
            _animator.SetBool("openDoor", true);
            Debug.Log("Estan abiertas");
            /*for (int i = 0; i < _openKeypad.Length; i++)
            {
                _openKeypad[i].enabled = false;
                _openKeypad[i].GetComponent<BoxCollider>().enabled = false;
            }*/
            this.enabled = false;
        }

        if (_solving)
        {
            _solved = false;
            _keypadOB.SetActive(true);
            _inv.SetActive(false);
            _hud.SetActive(false);
        }
    }

    public void Number(int number)
    {
        _textOB.text += number.ToString();
        _buttonSound.Play();
    }

    public void Confirm()
    {
        Debug.Log("Confirmando Contraseña");

        if (_textOB.text == _code)
        {
            _solved = true;
            _correctSound.Play();
            _textOB.text = "CORRECT";
        }
        else
        {
            _wrongSound.Play();
            _textOB.text = "ERROR";
        }
    }

    public void Clear()
    {
        Debug.Log("Limpiando Display");
        _textOB.text = "";
        _buttonSound.Play();
    }

    public void Exit()
    {
        Debug.Log("Saliendo del Keypad");
        _keypadOB.SetActive(false);
        _inv.SetActive(true);
        _hud.SetActive(true);
    }
}
