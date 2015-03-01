using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FOV2DEyes : MonoBehaviour
{
	public bool raysGizmosEnabled;
	//public float updateRate = 0.02f;
	public int quality = 4;
	public int fovAngle = 90;
	public float fovMaxDistance = 15;
	public LayerMask cullingMask;
	public List<RaycastHit2D> hits = new List<RaycastHit2D>();
	
	int numRays;
	float currentAngle;
	Vector3 direction;
	RaycastHit2D hit;
	
	void Update()
	{
		CastRays();
	}
	
	void Start() 
	{
	}
	
	void CastRays()
	{
		numRays = 20;

		currentAngle = fovAngle / -2;
		
		hits.Clear();
		
		for (int i = 0; i < numRays; i++)
		{
			direction = Quaternion.AngleAxis(currentAngle, transform.forward) * transform.up;
			Vector2 direction2d = new Vector2 (direction.x, direction.y);
			Vector2 origin = new Vector2(transform.position.x, transform.position.y);
			RaycastHit2D[] hits2;
			hits2 = Physics2D.RaycastAll (origin, direction2d, 10.0f); 

			foreach (RaycastHit2D hittt in hits2)
			{
				hits.Add(hittt);

			}
			currentAngle += fovAngle / numRays;
		}
	}
	
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		
		if (raysGizmosEnabled && hits.Count() > 0) 
		{
			foreach (RaycastHit2D hit in hits)
			{
				Gizmos.DrawSphere(hit.point, 1.0f);
				Gizmos.DrawLine(transform.position, hit.point);
			}
		}
	}
	
}
