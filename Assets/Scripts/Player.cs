using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    public int maxHealth = 10;
    public int currentHealth;
    public HealthBar healthBar;

    private float currentDamageInterval;
    [SerializeField] private float damageInterval = 2f; //in seconds

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask hazardLayer;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Color[] colors = { Color.red, Color.white };
    private Coroutine damageFlash;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update() {
        //subtract 1 every real life second
        if (currentDamageInterval > 0)
            currentDamageInterval -= Time.deltaTime;
        
        //if the current number in the currentDamageInterval is <= 0 or in other words if
        //2 seconds of delay has passed, take damage
        if (CanTakeDamage())
            if (Physics2D.OverlapCircle(groundCheck.position, 0.5f, hazardLayer))
                TakeDamage(1);

        if (Input.GetKeyDown(KeyCode.H))
            TakeDamage(1);
    }

    public bool CanTakeDamage() {
        return (currentDamageInterval <= 0);
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        //guarantee at most, one coroutine runs at a time
        if (damageFlash != null)
            StopCoroutine(damageFlash);

        damageFlash = StartCoroutine(DamageFlashing(1f, .1f));
        int yDamageVelocity = 15;
        rb.velocity = new Vector2(rb.velocity.x, yDamageVelocity);
        //set currentDamageInterval to start a new cooldown/delay period (2 seconds)
        currentDamageInterval = damageInterval;
    }

    IEnumerator DamageFlashing(float duration, float interval) {
        int index = 0;
        //var is a replacement for WaitForSeconds bc it would be redundant
        var wait = new WaitForSeconds(interval);

        for (float elapsedTime = 0; elapsedTime < duration; elapsedTime += interval) {
            //divides the index by 2 and returns the remainder
            sr.color = colors[index % 2];
            index++;
            //waits the interval time and then continues the next color in the flashing duration
            yield return wait;
        }
        damageFlash = null;
    }
}