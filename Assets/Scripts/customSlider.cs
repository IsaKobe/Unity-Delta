using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class customSlider : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Slider healthSlider;
    [SerializeField] UIDocument doc;
    ProgressBar healthSlider2;


    float maxHealth = 100;
    float healthVal;

    Rigidbody rb;

    private void Awake()
    {
        healthVal = maxHealth;
        healthSlider.maxValue = healthVal;
        healthSlider.value = healthVal;
        
        healthSlider2 = doc.rootVisualElement.Q<ProgressBar>();
        healthSlider2.highValue = healthVal;
        healthSlider2.value = healthVal;
        healthSlider2.title = $"{healthVal}/{maxHealth}";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            healthVal -= collision.gameObject.GetComponent<Wall>().damage;
            healthSlider.value = healthVal;

            healthSlider2.value = healthVal;
            healthSlider2.title = $"{healthVal}/{maxHealth}";
        }
    }
}
