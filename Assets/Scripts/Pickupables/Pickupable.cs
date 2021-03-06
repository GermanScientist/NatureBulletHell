using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour {
    private float startingY;
    [SerializeField] private float amplitude;
    [SerializeField] private float speed;

    [SerializeField] private int healAmount = 25;
    [SerializeField] private int ammoAmount = 45;

    private AudioSource pickupSound;
 
    // Start is called before the first frame update
    private void Start() {
        startingY = transform.position.y;
        pickupSound = GameObject.Find("pickupSound").GetComponent<AudioSource>();
    }

    private void Update() {
        Float();
    }

    private void Float() {
        float y = startingY + amplitude * Mathf.Sin(speed * Time.time);
        transform.position = new Vector2(transform.position.x, y);
    }

    public virtual void OnTriggerEnter2D(Collider2D _other) {
        if (_other.gameObject.tag == "Player") {
            pickupSound.Play();
            Player player = _other.gameObject.GetComponent<Player>();
            if (gameObject.tag == "Health") {
                player.Heal(healAmount);
            } else if (gameObject.tag == "Ammo") {
                player.Weapon.AddAmmo(ammoAmount);
            }
            Destroy(gameObject);
        }
    }
}
