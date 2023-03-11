using UnityEngine;

public class PlayerRespawn : MonoBehaviour {

    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    void OnTriggerEnter2D(Collider2D other) {
        player.transform.position = respawnPoint.localPosition;
    }
}
