using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*[ExecuteInEditMode]*/
public class RevealObjects : MonoBehaviour
{
    [Header("Decal")]
    [Tooltip("Decal")]
    [SerializeField] private GameObject _object;

    [Header("Light")]
    [Tooltip("Luz con la que se revelara el Decal")]
    [SerializeField] private Light _light;

    // Update is called once per frame
    void Update()
    {
        _object.GetComponent<Material>().SetVector("MyLightPosition", _light.transform.position);
        _object.GetComponent<Material>().SetVector("MyLightDirection", -_light.transform.forward);
        _object.GetComponent<Material>().SetFloat("MyLightAngle", _light.spotAngle);
    }
}
