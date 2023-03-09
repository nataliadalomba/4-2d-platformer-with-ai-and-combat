using UnityEngine;

public class Player : MonoBehaviour {

    public int maxHealth = 10;
    public int currentHealth;
    public HealthBar healthBar;

    //a constant that we return to for the total damage delay period length
    const float DAMAGE_DELAY = 2; //in seconds
    //the current damage delay point we are on in the delay timer
    private float damageDelay;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask hazardLayer;

    void Start() {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update() {
        //subtract 1 every real life second
        damageDelay -= Time.deltaTime;
        if (Physics2D.OverlapCircle(groundCheck.position, 0.5f, hazardLayer)) {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage) {
        //if the current number in the damageDelay is <= 0 or in other words if
        //2 seconds delay has passed, subtract 1 from the healthbar
        if (damageDelay <= 0) {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            //set damageDelay to start a new cooldown/delay period (2 seconds)
            damageDelay = DAMAGE_DELAY;
        }
    }
}
