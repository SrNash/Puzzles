using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwaysGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _tiles;    //Array de prefabs de los tiles que se utilizarán para generar el pasillo

    [SerializeField] private int _length;   //Longitud del pasillo en número de tiles

    // Start is called before the first frame update
    void Start()
    {
        GenerateHallway();
    }
    void GenerateHallway()
    {
        for (int i = 0; i < _length; i++)
        {
            //Selecciona un tile aleatorio del array y lo instancia en la posicion correspondiente
            GameObject tile = Instantiate(_tiles[Random.Range(0, _tiles.Length)], new Vector3(i,0,0), Quaternion.identity);
            tile.transform.parent = transform;  //Hace que el tile sea hijo del objeto que contiene este script
        }
    }
}
