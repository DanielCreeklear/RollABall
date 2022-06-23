using System.Collections;
using System.Collections.Generic;
using RollABall.Models;
using UnityEngine;

namespace RollABall
{
	public class CoinGenerator
	{
		public static List<Coin> GenerateCoins(GameObject player, GameObject prefab)
		{
			List<Vector3> occupiedPositions = new List<Vector3>
			{
				player.transform.position
			};

			int numPickups = 16;
			for (int i = 0; i < numPickups; i++)
			{
				var coin = new Coin();
				bool occupied;
				
				do
				{
					coin.SetNewPosition(Random.Range(-8f, 8f), Random.Range(-8f, 8f));
					occupied = false;

					foreach(Vector3 otherPosition in occupiedPositions)
					{
						if (Vector3.Distance(coin.GetPosition(), otherPosition) < 2f)
							occupied = true;
					}
				}
				while (occupied);

				occupiedPositions.Add(coin.GetPosition());
			}
			return occupiedPositions;
		}
	}
}