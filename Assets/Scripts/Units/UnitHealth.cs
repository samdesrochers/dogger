using UnityEngine;
using System.Collections;

public class UnitHealth : MonoBehaviour
{
	public float health = 100f;

	private bool canDropLoot;
	private GameManager gameManager;

	// To add keyvohn,
	// private anim deathAnim;

	void Start () {
		this.gameManager = (GameManager)GameObject.Find ("GameManager").GetComponent<GameManager>();
		canDropLoot = true;
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
		//Drop loot on the ground
		this.dropLootIfNeeded ();

		//Tell the game manager that a unit died
		this.gameManager.unitDied (this.gameObject);
	}

	private void dropLootIfNeeded()
	{
		if (!canDropLoot)
		{

		}
	}
}