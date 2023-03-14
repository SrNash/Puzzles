using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteTrackGenerator : MonoBehaviour
{
    [Header("Note Track")]
    [Tooltip("GameObject de la Nota de Pista")]
    [SerializeField] private GameObject _noteTrack;

    [Header("TMPro")]
    [Tooltip("TextMeshPro del texto de la Node de Pista")]
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _codeText;

    [Header("Code/Pass")]
    [Tooltip("Codigo o contrasena de la Nota de Pista")]
    [SerializeField]private string _code;
    public string code { get { return _code; } set { _code = value; } }

    [Header("Texts")]
    [Tooltip("Lista de mesajes posibles que puede contener la Keynote")]
    [SerializeField] private List<string> _textsList;
    [SerializeField] private string m_note;
    public string mNote { get { return m_note; } set { m_note = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_code != null && m_note == "")
        {
            int _random = Random.Range(0, _textsList.Count);
            m_note = _textsList[_random];
        }
    }

    public void PrintNote(string text, string code)
    {
        _text.text = text;
        _codeText.text = code;
    }
}
