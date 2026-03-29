using UnityEngine;

public class Structure : MonoBehaviour
{
    private SpriteRenderer SR;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        DetermineSortingLayer();
    }
    void DetermineSortingLayer()
    {
        if(player.transform.position.y - -0.0525f > transform.position.y)
        {
            //render in front
            SR.sortingOrder = 5;
        }
        else
        {
            //render in behind
            SR.sortingOrder = -5;
        }
    }
}
