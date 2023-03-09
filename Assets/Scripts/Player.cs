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

    private SpriteRenderer sr;
    private Color[] colors = { Color.red, Color.white };

    void Start() {
        sr = gameObject.GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update() {
        //subtract 1 every real life second
        if (currentDamageInterval > 0) {
            //sr.color = Color.white;
            currentDamageInterval -= Time.deltaTime;
        }
        if(currentDamageInterval <= 0 ) {
            if (Physics2D.OverlapCircle(groundCheck.position, 0.5f, hazardLayer)) {
                TakeDamage(1);
            }
        }
    }

    public bool CanTakeDamage() {
        return !(currentDamageInterval > 0f);
    }

    public void TakeDamage(int damage) {
        //if the current number in the currentDamageInterval is <= 0 or in other words if
        //2 seconds delay has passed, subtract 1 from the healthbar
        if (currentDamageInterval <= 0) {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            StartCoroutine(DamageFlashing(1f, .3f));
            //set currentDamageInterval to start a new cooldown/delay period (2 seconds)
            currentDamageInterval = damageInterval;
        }
        sr.color = Color.white;
    }

    IEnumerator DamageFlashing(float time, float intervalTime) {
        float elapsedTime = 0f;
        int index = 0;

        while (elapsedTime < time) {
            sr.color = colors[index % 2];

            elapsedTime += Time.deltaTime;
            index++;
            yield return new WaitForSeconds(intervalTime);
        }
    }
}
