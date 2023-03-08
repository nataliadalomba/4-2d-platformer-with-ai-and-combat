using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;

        //on the gradient that has (left to right) red, yellow and green fixed portions,
        //all the way to the left 0f (red), and all the way to the right 1f (green)
        //we assign the entire fill of the HealthBar to this color
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health) {
        slider.value = health;

        //we assign the entire fill of the HealthBar to the normalized value (0 to 1) so
        //the 0 to 10 points of health syncs with 0 to 1 on the gradient. this determines
        //what color the fill will be depending on how much health is left
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
