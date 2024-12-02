using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate() // Funciona na mesma forma que o Update, mas funciona em paralelo, junto com a física simulada
    {
        var gameManager = GameManager.Instance;

        // Ignore if game is over
        if(gameManager.IsGameOver()){
            return; 
        }

        // Move object
        float x = gameManager.obstacleSpeed * Time.fixedDeltaTime; // fixedDeltaTime é para fixedUpdate
        transform.position -= new Vector3(x, 0, 0); // Só ira agir no eixo-x

        // Destroy object
        if(transform.position.x <= -gameManager.obstacleOffsetX){
            Destroy (gameObject); 
        }
    
    }
}
