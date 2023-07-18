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

        //todo: Remove this
        //equippedWeapon = Shooter.Weapon.Machinegun;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 delta = rawInput * speed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, GeneralData.minBound.x + 
            paddingLeft, GeneralData.maxBound.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, GeneralData.minBound.y + 
            paddingBottom, GeneralData.maxBound.y - paddingTop);
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
