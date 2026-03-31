using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D RB;
    [SerializeField] int value = 1;
    [SerializeField] float power;
    void Start() {RB = GetComponent<Rigidbody2D>(); RB.AddForce(new Vector2(Random.Range(-5,6),Random.Range(-5,6)).normalized * power);}
    void OnTriggerEnter2D(Collider2D collision)
    {
        CoinCollection.instance.AddCoinToCollection(value);
        Destroy(gameObject);
    }
}