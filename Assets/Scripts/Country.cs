using UnityEngine;

public class Country : MonoBehaviour
{
    [SerializeField] PolygonCollider2D PC;
    [SerializeField] SpriteRenderer SR;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PC = GetComponent<PolygonCollider2D>();
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
