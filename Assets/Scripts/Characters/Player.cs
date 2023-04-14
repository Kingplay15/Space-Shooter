using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Vector2 rawInput;

    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    Vector2 minBound;
    Vector2 maxBound;

    Shooter shooter;
    public Shooter.Weapon equippedWeapon { get; set; } = Shooter.Weapon.Normal;

    public bool haveShield { get; set; } = false;

    [SerializeField] Shield shield;
    public Shield GetShield()
    {
        return shield;
    }

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitBounds();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void InitBounds()
    {
        Camera mainCam = Camera.main;
        minBound = mainCam.ViewportToWorldPoint(new Vector2(0, 0));
        maxBound = mainCam.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Move()
    {
        Vector2 delta = rawInput * speed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBound.x + paddingLeft, maxBound.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBound.y + paddingBottom, maxBound.y - paddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
            shooter.isFiring = value.isPressed;
    }
}
