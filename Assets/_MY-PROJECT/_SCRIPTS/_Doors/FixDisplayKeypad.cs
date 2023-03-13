using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixDisplayKeypad : MonoBehaviour
{
    [SerializeField] private Keypad _keypad;
    private const int MaxLength = 5; // for example
    
    // Start is called before the first frame update
    void Awake()
    {
        //_keypad.code.Substring(5, MaxLength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
