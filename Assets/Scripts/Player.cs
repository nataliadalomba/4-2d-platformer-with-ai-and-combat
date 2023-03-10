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
        if (currentDamageInterval > 0) {
            //sr.color = Color.white;
            currentDamageInterval -= Time.deltaTime;
        }
        //if the current number in the currentDamageInterval is <= 0 or in other words if
        //2 seconds delay has passed, take damage
        if (currentDamageInterval <= 0) {
            if (Physics2D.OverlapCircle(groundCheck.position, 0.5f, hazardLayer)) {
                TakeDamage(1);
            }
        }
    }

    public bool CanTakeDamage() {
        return !(currentDamageInterval > 0f);
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        //guarantee at most, one coroutine runs at a time
        if (damageFlash != null)
            StopCoroutine(damageFlash);

        damageFlash = StartCoroutine(DamageFlashing(1f, .1f));
        int yDamageVelocity = 10;
        rb.velocity = new Vector2(rb.velocity.x, yDamageVelocity);
        //set currentDamageInterval to start a new cooldown/delay period (2 seconds)
        currentDamageInterval = damageInterval;
        sr.color = Color.white;
    }

    IEnumerator DamageFlashing(float duration, float intervalTime) {
        int index = 0;
        var wait = new WaitForSeconds(intervalTime);

        for (float elapsedTime = 0; elapsedTime < duration; elapsedTime += intervalTime) {
            sr.color = colors[index % 2];
            index++;
            yield return wait;
        }
        damageFlash = null;
    }
}