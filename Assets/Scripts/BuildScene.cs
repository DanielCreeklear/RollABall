using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RollABall;

public class BuildScene : MonoBehaviour
{
    public GameObject prefab;
    public GameObject player;

    void Start()
    {
        CoinGenerator.GenerateCoins(player, prefab);
		
		foreach ()
		GameObject obj = Instantiate<GameObject>(prefab, coin.GetPosition(), Quaternion.identity, transform);
		obj.name = "PickUp" + i;
    }
}
