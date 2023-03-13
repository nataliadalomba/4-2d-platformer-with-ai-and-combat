using System.Collections;
using UnityEngine;

public class Player : Character {


    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask hazardLayer;

    //private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Color[] colors = { Color.red, Color.white };
    private Coroutine damageFlash;

    void Start() {
        //base.rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        maxHealth = 10;
    }

    public override void Update() {
        base.Update();
        //if the current number in the currentDamageInterval is <= 0 or in other words if
        //2 seconds of delay has passed, take damage
        if (CanTakeDamage())
            if (Physics2D.OverlapCircle(groundCheck.position, 0.5f, hazardLayer))
                TakeDamage(1);

        //for testing purposes
        if (Input.GetKeyDown(KeyCode.H))
            TakeDamage(1);
    }

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);

        //guarantee at most, one coroutine runs at a time
        if (damageFlash != null)
            StopCoroutine(damageFlash);
        damageFlash = StartCoroutine(DamageFlashing(1f, .1f));
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