using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    float minX, maxX, minZ, maxZ;
    private void GetGroundSize()
    {
        GameObject ground = GameObject.FindWithTag("Ground");
        Renderer groundSize = ground.GetComponent<Renderer>();
        minX = -50;
        maxX = 50;
        minZ = -50;
        maxZ = 50;
    }
        Vector3 GetNewPosiotion()
    {
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomZ = UnityEngine.Random.Range(minZ, maxZ);
        Vector3 newPosition = new Vector3(randomX, transform.position.y, randomZ);
        return newPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SpawnNewEnemy()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
