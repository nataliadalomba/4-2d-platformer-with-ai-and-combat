using UnityEngine;

public class Character : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;

    [SerializeField] protected float damageInterval = 2f; //in seconds
    protected float currentDamageInterval;

    //private SpriteRenderer sr;
    public Rigidbody2D rb;

    void Start() {
        //sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public virtual void Update() {
        //subtract 1 real life second
        if (currentDamageInterval > 0)
            currentDamageInterval -= Time.deltaTime;
    }

    public bool CanTakeDamage() {
        return (currentDamageInterval < 0);
    }

    public virtual void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        int yDamageVelocity = 15;
        rb.velocity = new Vector2(rb.velocity.x, yDamageVelocity);

        //set currentDamageInterval to start a new cooldown/delay period (2 seconds)
        currentDamageInterval = damageInterval;
    }
}
