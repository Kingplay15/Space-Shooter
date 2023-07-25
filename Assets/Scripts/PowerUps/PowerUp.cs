using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] private float droppingSpeed = 5f;
    [SerializeField] private float lifeTime = 10f;
    protected Player player;
    private ScoreKeeper scoreKepper;
    [SerializeField] private int score = 100;

    private void Awake()
    {
        scoreKepper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        StartCoroutine(GetDestroyedAfterLifetime());
    }

    private IEnumerator GetDestroyedAfterLifetime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
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
    }

    protected abstract bool CheckPowerExist();

    protected abstract void GrantPower(Player player);

}
