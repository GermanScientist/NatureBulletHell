using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor {    

    private Vector3 mousePosition;
    private Vector3 mouseDirection;

    protected override void Start() {
        base.Start();

        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update() {
        UpdatePlayerDirection();
        UpdateAimDirection();
        CheckToFire();
    }

    private void FixedUpdate() {
        Move(movementSpeed, moveDirection);
    }

    private void UpdateAimDirection() {
        mousePosition = Input.mousePosition;
        mousePosition.z = 0;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        mouseDirection = (mousePosition - this.transform.position).normalized;
    }

    private void UpdatePlayerDirection() {
        float horizontalDirection = Input.GetAxisRaw("Horizontal"); //-1 is left, 1 is right
        float verticalDirection = Input.GetAxisRaw("Vertical"); //-1 is down, 1 is up

        moveDirection = new Vector2(horizontalDirection, verticalDirection);
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
