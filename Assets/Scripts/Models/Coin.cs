using UnityEngine;

namespace RollABall.Models
{
	public class Coin
	{
		public float x { get; set; }
		public float y = 0.5f;
		public float z { get; set; }
		
		public Coin(float posX=0, float posZ=0)
		{
			x = posX;
			z = posZ;
		}
		
		public Vector3 GetPosition()
		{
			return new Vector3(x, y, z);
		}
		
		public void SetNewPosition(float posX, float posZ)
		{
			x = posX;
			z = posZ;
		}
	}
}

