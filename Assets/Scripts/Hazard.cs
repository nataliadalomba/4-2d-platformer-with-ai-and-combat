using UnityEngine;

public class Hazard : MonoBehaviour {

    [SerializeField] private int hazardDamage = 1;

    void OnTriggerStay(Collider other) {
        if (other.TryGetComponent(out Player player)) {
            if (player.CanTakeDamage()) {
                player.TakeDamage(hazardDamage);
            }
        }
    }

}
