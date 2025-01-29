using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private int _cost;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider?.tag == "Player")
        {
            collider.GetComponent<Character>().SetCoint(_cost);
            Destroy(gameObject);
        }

    }
}
