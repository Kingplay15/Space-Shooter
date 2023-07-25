using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Vector2 rawInput;

    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;


    private Shooter shooter;
    public Shooter.Weapon equippedWeapon { get; set; } = Shooter.Weapon.Normal;

    public bool haveShield { get; set; } = false;

    [SerializeField] private Shield shield;
    public Shield GetShield() => shield;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 delta = rawInput * speed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, GeneralData.minBound.x + 
            paddingLeft, GeneralData.maxBound.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, GeneralData.minBound.y + 
            paddingBottom, GeneralData.maxBound.y - paddingTop);
        transform.position = newPos;
    }

    private void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    private void OnFire(InputValue value)
    {
        if (shooter != null)
            shooter.isFiring = value.isPressed;
    }
}
