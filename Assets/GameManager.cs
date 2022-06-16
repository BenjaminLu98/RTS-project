using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject castlePrefab;
    public GameObject workerPrefab;
    public GameObject goldMinePrefab;

    Castle castle1;
    Castle castle2;
    GoldMine goldMine1;
    GoldMine goldMine2;
    // Start is called before the first frame update
    void Start()
    {
        var gridSystem = GridSystem.current;
        castle1 = Instantiate(castlePrefab).GetComponent<Castle>();
        castle2 = Instantiate(castlePrefab).GetComponent<Castle>();
        castle1.placeAt(25, 25);
        castle2.placeAt(75, 75);
        goldMine1 = Instantiate(goldMinePrefab).GetComponent<GoldMine>();
        goldMine2 = Instantiate(goldMinePrefab).GetComponent<GoldMine>();
        goldMine1.placeAt(15, 15);
        goldMine2.placeAt(80, 80);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
