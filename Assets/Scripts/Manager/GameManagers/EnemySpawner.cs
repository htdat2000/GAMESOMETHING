using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject[] enemyId;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        TimeSystem.timeSystem.on17h.AddListener(SpawnEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        //Do somthing
        Debug.Log("Spawnnnnnn");
        Instantiate(enemyId[0], player.transform.position + new Vector3(2f,2f, 0), Quaternion.identity);
    }
}
