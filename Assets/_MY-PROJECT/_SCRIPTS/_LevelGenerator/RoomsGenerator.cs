using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _tiles;    //Array de prefabs de los tiles que se utilizarán para generar el pasillo

    [SerializeField] private int _width;   //Ancho de la habitación en número de tiles
    [SerializeField] private int _height;   //Alto de la habitación en número de tiles

    // Start is called before the first frame update
    void Start()
    {
        GenerateRooms();
    }
    void GenerateRooms()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                //Selecciona un tile aleatorio del array y lo instancia en la posicion correspondiente
                GameObject tile = Instantiate(_tiles[Random.Range(0, _tiles.Length)], new Vector3(x, 0, y), Quaternion.identity);
                tile.transform.parent = transform;  //Hace que el tile sea hijo del objeto que contiene este script
            }
        }
    }
}
