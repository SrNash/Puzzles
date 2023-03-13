using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableItem : MonoBehaviour
{
    [SerializeField] private Canvas _pickUpCanvas;
    public bool _wasFound = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CanvasPickUp();
    }

    public void CanvasPickUp()
    {
        if (_wasFound)
        {
            _pickUpCanvas.gameObject.SetActive(true);
        }
        else
        {
            _pickUpCanvas.gameObject.SetActive(false);
        }
    }
}
