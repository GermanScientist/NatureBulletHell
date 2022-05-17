using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : Actor {    
    private PlayerMovement playerMovement;

    private Vector3 mousePosition;
    private Vector3 mouseDirection;

    protected override void Start() {
        base.Start();

        playerMovement = GetComponent<PlayerMovement>();
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update() {
        GetAimDirection();
        CheckToFire();
    }

    private void FixedUpdate() {
        playerMovement.MovePlayer(movementSpeed);
    }

    private void GetAimDirection() {
        mousePosition = Input.mousePosition;
        mousePosition.z = 0;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        mouseDirection = (mousePosition - this.transform.position).normalized;
    }

    private void CheckToFire() {
        if (weapon.CurrentAmmo <= 0) return;
        if (!Input.GetMouseButtonDown(0)) return;
       
        weapon.FireFriendlyProjectile(mouseDirection); //Only fire a projectile when the player has ammo and the mousebutton has been pressed
    }

    protected override void Die() {
        gameObject.SetActive(false);
    }
}
