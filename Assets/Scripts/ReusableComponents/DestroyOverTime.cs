using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DestroyOverTime : MonoBehaviour
{

    [Header("Lifetime")]
    [SerializeField] private float lifetime;

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //This was being triggered by the Room polygon collider
        //leading to the projectiles beign destroyed immediately.
        //Checking the Player tag solves this thankfully!
        //Mister Taft found this in episode 60+
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
