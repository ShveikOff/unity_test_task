using System;
using UnityEngine;

/*
This class is responsible for managing the player object and its movement
*/
public class PlayerController : MonoBehaviour {
    
    public GameObject playerPrefab;
    private Rigidbody playerRb;
    private float moveSpeed;

    public GameObject plane;
    [NonSerialized] public float planeLeft;
    [NonSerialized] public float planeRight;
    [NonSerialized] public float planeTop;
    [NonSerialized] public float planeBottom;

    public void Start() {
        Vector3 planeSize = plane.transform.localScale * 10;
        Vector3 planeCenter = plane.transform.position;
        planeLeft = planeCenter.x - (planeSize.x / 2);
        planeRight = planeCenter.x + (planeSize.x / 2);
        planeTop = planeCenter.z + (planeSize.z / 2);
        planeBottom = planeCenter.z - (planeSize.z / 2);

        GameObject player = Instantiate(playerPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity) as GameObject;
        playerRb = player.GetComponent<Rigidbody>();

        moveSpeed = GameManager.Instance.playerSpeed;       
    }


    private void Update() {
        if (playerRb.position.x < planeLeft) {
            playerRb.position = new Vector3(planeRight, playerRb.position.y, playerRb.position.z);
        }

        if (playerRb.position.x > planeRight) {
            playerRb.position = new Vector3(planeLeft, playerRb.position.y, playerRb.position.z);
        }

        if (playerRb.position.z < planeBottom) {
            playerRb.position = new Vector3(playerRb.position.x, playerRb.position.y, planeTop);
        }

        if (playerRb.position.z > planeTop) {
            playerRb.position = new Vector3(playerRb.position.x, playerRb.position.y, planeBottom);
        }
    }

    private void FixedUpdate() {
        if (GameManager.Instance.gameEnabled) {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.fixedDeltaTime;

            playerRb.MovePosition(playerRb.position + movement);
        }
    }
}