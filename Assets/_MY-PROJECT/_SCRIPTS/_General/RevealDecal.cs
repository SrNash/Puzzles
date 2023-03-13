using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[ExecuteInEditMode]
public class RevealDecal : MonoBehaviour
{
    [Header("Decal")]
    [Tooltip("Decal")]
    [SerializeField] private DecalProjector _decal;

    [Header("Material")]
    [Tooltip("Material del Decal")]
    [SerializeField] private Material _mat;

    [Header("Light")]
    [Tooltip("Luz con la que se revelara el Decal")]
    [SerializeField] private Light _light;

    // Update is called once per frame
    void Update()
    {
        _mat.SetVector("MyLightPosition", _light.transform.position);
        _mat.SetVector("MyLightDirection", -_light.transform.forward);
        _mat.SetFloat("MyLightAngle", _light.spotAngle);
        
        _decal.material.SetVector("MyLightPosition", _light.transform.position);
        _decal.material.SetVector("MyLightDirection", -_light.transform.forward);
        _decal.material.SetFloat("MyLightAngle", _light.spotAngle);
    }
}
