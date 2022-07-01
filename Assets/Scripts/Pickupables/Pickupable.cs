using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour {
    private float startingY;
    [SerializeField] private float amplitude;
    [SerializeField] private float speed;
 
    // Start is called before the first frame update
    private void Start() {
        startingY = transform.position.y;
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
            Player player = _other.gameObject.GetComponent<Player>();
            if (gameObject.tag == "Health") {
                player.Heal(7);
            } else if (gameObject.tag == "Ammo") {
                player.Weapon.AddAmmo(45);
            }
            Destroy(gameObject);
        }
    }
}
