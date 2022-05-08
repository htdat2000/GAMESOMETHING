using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject[] enemyId;
    [SerializeField] private GameObject[] bossId;
    // Start is called before the first frame update
    void Start()
    {
        SpawnBoss(new Vector3(0, 0, 0));
        player = GameObject.FindGameObjectWithTag("Player");
        TimeSystem.timeSystem.on17h.AddListener(SpawnEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        if(enemyId.Length <= 0)
        {
            return;
        }
        //Debug thôi
        Debug.Log("Spawnnnnnn");
        Instantiate(enemyId[0], player.transform.position + new Vector3(2f,2f, 0), Quaternion.identity);
        //sinh như nào thì viết dô đây
    }

    public void SpawnBoss(Vector3 pos)
    {
        if(bossId.Length <= 0)
        {
            return;
        }
        if(pos.magnitude == 0)
        {
            Vector3 posSpawn = new Vector3(26f, 15f, 0);
            Instantiate(bossId[0], posSpawn, Quaternion.identity);
        }
        else
        {
            Instantiate(bossId[0], pos, Quaternion.identity);
        }   
    }
}
