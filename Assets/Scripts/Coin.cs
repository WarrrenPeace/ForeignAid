using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        CoinCollection.instance.AddCoinToCollection(1);
        Destroy(gameObject);
    }
}
