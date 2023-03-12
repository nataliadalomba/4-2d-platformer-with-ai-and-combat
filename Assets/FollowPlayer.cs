using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    [SerializeField] private Transform player;

    void Update() {
        transform.position = player.position + new Vector3(3.5f, 0, -10);
    }
}
