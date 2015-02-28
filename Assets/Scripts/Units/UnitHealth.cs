using UnityEngine;
using System.Collections;

public class UnitHealth : MonoBehaviour
{
	public float health = 100f;

	private GameManager gameManager;

	// To add keyvohn,
	// private anim deathAnim;

	void Start () {
		this.gameManager = (GameManager)GameObject.Find ("GameManager").GetComponent<GameManager>();
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
		//Tell the game manager that a unit died
		this.gameManager.unitDied (this.gameObject);
	}
}