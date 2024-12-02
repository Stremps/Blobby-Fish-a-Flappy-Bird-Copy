using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody thisRigidbody;
    public float jumpPower = 7f;
    public float jumpInterval = 0.2f;
    private float jumpCooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update Cooldown
        jumpCooldown -= Time.deltaTime;
        bool isGameActive = GameManager.Instance.IsGameAtive();
        bool canJump = (isGameActive && jumpCooldown <=0); 

        if(canJump){
            bool JumpInput = Input.GetKey(KeyCode.Space);
            if(JumpInput){
                Jump();
            }
        }

        thisRigidbody.useGravity = isGameActive; // If the game is over, desactivate
    }

    private void OnCollisionEnter(Collision other) {
        OnCustomColliderEnter(other.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        OnCustomColliderEnter(other.gameObject);
    }

    private void OnCustomColliderEnter(GameObject other){
        bool isSensor = other.CompareTag("Sensor");
        var gameManager = GameManager.Instance;

        if(isSensor){
            // Increase points
            gameManager.score++;
            Debug.Log("Score: " + gameManager.score);

            if( gameManager.score < 25 && gameManager.score%5 == 0 && gameManager.score != 0){
                gameManager.obstacleSpeed++;
                gameManager.obstacleInterval-=0.25f;
            }
        } else {
            // GameOver
            GameManager.Instance.EndGame();
        }
    }

    private void Jump(){
        // Rest Cooldown
        jumpCooldown = jumpInterval;

        //Apply Force
        thisRigidbody.velocity = Vector3.zero; // Cancel gravity force
        thisRigidbody.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse); // Queremos colocar a força apontando para cima
    }
}
