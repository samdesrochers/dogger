using UnityEngine;
using System.Collections;

public class UnitHealth : MonoBehaviour
{
	public float health = 100f;

	// To add keyvohn,
	// private anim deathAnim;

	void Update ()
	{
	}
	
	public void TakeDamage (float amount)
	{
		health -= amount;

		if (health <= 0) {
			Debug.Log("Da unit is dead");
			// To do Keyvohn, death animation
			this.unitDied();
		}
	}

	private void unitDied()
	{
		Destroy (this.gameObject);
		//To do keyvohn, start die animation
	}
}