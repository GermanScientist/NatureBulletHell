using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Actor {    

    private Vector3 mousePosition;
    private Vector3 mouseDirection;

    private Text ammoText;
    private Slider healthbar;

    private Animator animator;
    private CameraShake cameraShake;

    private AudioSource playerHitSound;

    public Vector3 MouseDirection { get { return mouseDirection; } }

    protected override void Start() {
        base.Start();

        firingSound = GameObject.Find("mothFire").GetComponent<AudioSource>();
        playerHitSound = GameObject.Find("playerHit").GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.Confined;
        
        ammoText = GameObject.Find("AmmoCount").GetComponent<Text>();
        ammoText.text = weapon.CurrentAmmo.ToString();

        healthbar = GameObject.Find("Healthbar").GetComponent<Slider>();
        healthbar.value = (float)currentHitpoints / (float)maxHitpoints;

        animator = GetComponent<Animator>();
        cameraShake = Camera.main.gameObject.GetComponent<CameraShake>();
    }

    private void Update() {
        UpdatePlayerDirection();
        UpdateAimDirection();
        CheckToFire();
    }

    private void FixedUpdate() {
        MoveTowards(movementSpeed, moveDirection);
        animator.SetInteger("Vertical", (int)(currentSpeed.y * 100));
        if (currentSpeed == Vector2.zero) animator.speed = 0;
        else animator.speed = .5f;
    }

    private void UpdateAimDirection() {
        mousePosition = Input.mousePosition;
        mousePosition.z = 0;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        mouseDirection = (mousePosition - projectileSpawn.position).normalized;
    }

    private void UpdatePlayerDirection() {
        float horizontalDirection = Input.GetAxisRaw("Horizontal"); //-1 is left, 1 is right
        float verticalDirection = Input.GetAxisRaw("Vertical"); //-1 is down, 1 is up

        moveDirection = new Vector2(horizontalDirection, verticalDirection);
    }

    private void CheckToFire() {
        if (weapon.CurrentAmmo <= 0) return;
        if (!Input.GetMouseButtonDown(0)) return;

        Fire(); //Only fire a projectile when the player has ammo and the mousebutton has been pressed
    }

    private void Fire() {
        firingSound.Play();
        weapon.FireFriendlyProjectile(mouseDirection, projectileSpawn.position); 
        ammoText.text = weapon.CurrentAmmo.ToString();
        StartCoroutine(cameraShake.Shake(.1f, .15f));
    }

    public override void Damage(int _damageAmount) {
        base.Damage(_damageAmount);
        healthbar.value = (float)currentHitpoints / (float)maxHitpoints;
        StartCoroutine(cameraShake.Shake(.30f, .20f));
        playerHitSound.Play();
    }

    public void Heal(int _healAmount) {
        currentHitpoints += _healAmount;
        currentHitpoints = Mathf.Clamp(currentHitpoints, 0, maxHitpoints);
        healthbar.value = (float)currentHitpoints / (float)maxHitpoints;
    }

    protected override void Die() {
        SceneManager.LoadScene("MainMenu");
    }
}
