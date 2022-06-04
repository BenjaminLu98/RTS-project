using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject castlePrefab;
    public GameObject WorkerPrefab;

    Castle castle1;
    Castle castle2;
    // Start is called before the first frame update
    void Start()
    {
        var gridSystem = GridSystem.current;
        castle1 = Instantiate(castlePrefab).GetComponent<Castle>();
        castle2 = Instantiate(castlePrefab).GetComponent<Castle>();
        castle1.placeAt(25, 25);
        castle2.placeAt(75, 75);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
