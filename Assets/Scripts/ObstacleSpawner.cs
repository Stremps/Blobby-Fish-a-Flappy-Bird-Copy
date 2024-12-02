using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private float cooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var gameManager = GameManager.Instance; // To simplify the call of attributes
        cooldown -= Time.deltaTime;

        // Ignore if game is over
        if(gameManager.IsGameOver()){
            return; 
        }

        if(cooldown <= 0f){
            cooldown = gameManager.obstacleInterval;

            // Get the object
            int prefabIndex = Random.Range(0, gameManager.obstaclePrefabs.Count);
            GameObject prefab = gameManager.obstaclePrefabs[prefabIndex];

            // Get the transform attributes
            float x = gameManager.obstacleOffsetX;
            float y = Random.Range(gameManager.obstacleOffsetY.x, gameManager.obstacleOffsetY.y);
            float z = 0;
            Vector3 position = new Vector3(x, y, z);
            Quaternion rotation = prefab.transform.rotation;

            // Spawn the object
            Instantiate(prefab, position, rotation);

        }
    }
}
