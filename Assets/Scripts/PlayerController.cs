using System;
using Unity.VisualScripting;
using UnityEngine;

/*
This class is responsible for managing the player object and its movement
*/
public class PlayerController : MonoBehaviour {

    [SerializeField] private GameManager gameManager;
    [SerializeField] private CollisionHandler collisionHandler;
    
    private Rigidbody playerRb;
    private float moveSpeed;

    public GameObject plane;
    [NonSerialized] public float planeLeft;
    [NonSerialized] public float planeRight;
    [NonSerialized] public float planeTop;
    [NonSerialized] public float planeBottom;

    private UnityEngine.Vector3 startPosition = new UnityEngine.Vector3(0, 0.5f, 0);

    public void Start() {
        UnityEngine.Vector3 planeSize = plane.transform.localScale * 10;
        UnityEngine.Vector3 planeCenter = plane.transform.position;
        planeLeft = planeCenter.x - (planeSize.x / 2);
        planeRight = planeCenter.x + (planeSize.x / 2);
        planeTop = planeCenter.z + (planeSize.z / 2);
        planeBottom = planeCenter.z - (planeSize.z / 2);

        playerRb = GetComponent<Rigidbody>();

        moveSpeed = gameManager.playerSpeed;

    }


    private void Update() {
        float wrappedX = Mathf.Repeat(playerRb.position.x - planeLeft, planeRight - planeLeft) + planeLeft;
        float wrappedZ = Mathf.Repeat(playerRb.position.z - planeBottom, planeTop - planeBottom) + planeBottom;

        playerRb.position = new UnityEngine.Vector3(wrappedX, playerRb.position.y, wrappedZ);
    }



    private void FixedUpdate() {
        if (gameManager.gameEnabled) {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            UnityEngine.Vector3 movement = new UnityEngine.Vector3(moveX, 0, moveZ) * moveSpeed * Time.fixedDeltaTime;

            playerRb.MovePosition(playerRb.position + movement);
        }
    }

    public void ResetPosition() {
        if (playerRb != null) {
            playerRb.linearVelocity = UnityEngine.Vector3.zero;
            playerRb.angularVelocity = UnityEngine.Vector3.zero;
            playerRb.position = startPosition;
        } else {
            transform.position = startPosition;
        }
    }
}