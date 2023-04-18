using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] float droppingSpeed = 5f;
    protected Player player;
    ScoreKeeper scoreKepper;
    [SerializeField] int score = 100;

    private void Awake()
    {
        scoreKepper = FindObjectOfType<ScoreKeeper>();
    }

    public float GetDroppingSpeed()
    {
        return droppingSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Player>();

            if (CheckPowerExist() == false)
                GrantPower(player);
            else AddScore();

            Destroy(gameObject);
        }
    }

    private void AddScore() //Adding score instead of granting the power the player already had
    {
        scoreKepper.ModifyScore(score);
        Debug.Log("Add score");
    }

    protected abstract bool CheckPowerExist();

    protected abstract void GrantPower(Player player);

}
