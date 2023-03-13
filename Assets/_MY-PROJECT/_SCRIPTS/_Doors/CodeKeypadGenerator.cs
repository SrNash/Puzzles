using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeKeypadGenerator : MonoBehaviour
{
    [Header("Numerics Reference")]
    [Tooltip("Referencia numerica")]
    [SerializeField] private string _characters = "0123456789";
    [SerializeField]private string m_code;

    [Header("KeypadScript")]
    [Tooltip("Referencia al script del Keypad")]
    [SerializeField] private Keypad _keypad;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            m_code += _characters[Random.Range(0, _characters.Length)];
        }
        _keypad.code = m_code;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
