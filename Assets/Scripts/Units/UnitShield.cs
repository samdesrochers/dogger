using UnityEngine;
using System.Collections;

public class UnitShield : MonoBehaviour
{
	public enum States { Charged, Depleted, OnCooldown, Regenerating };
	public int State { get; private set; }

	public float ShieldCapacity = 100f;
	public float ShieldValue;
	public float CooldownTarget = 2.0f; 
	public float CooldownValue;
	public float RegenationRate = 2.0f;


	void Start() 
	{
		Charged ();
	}
	
	void Update () 
	{
		float delta = Time.deltaTime;

		switch (State) {
		case (int)States.Charged :
			// Maybe
			break;
		case (int)States.Depleted :
			Depleted(delta);
			break;
		case (int)States.OnCooldown :
			Cooldown(delta);
			break;
		case (int)States.Regenerating :
			Regenerate(delta);
			break;
		}
	}

	// Shield is taking a hit
	public void TakeHit (float amount)
	{
		ShieldValue -= amount;
		
		if (ShieldValue <= 0) {
			ShieldValue = Mathf.Max(0, ShieldValue);
			Debug.Log("Shield is out");
			State = (int)States.Depleted;
		}
	}

	private void Charged()
	{
		State = (int)States.Charged;
		ShieldValue = ShieldCapacity;
		CooldownValue = 0.0f;
	}

	private void Depleted(float dt)
	{
		State = (int)States.OnCooldown;
	}

	// Shield is not yet regenerating
	private void Cooldown(float dt)
	{
		if (CooldownValue < CooldownTarget) 
		{
			CooldownValue += dt;
		}
		Debug.Log(CooldownValue);

		if (CooldownValue >= CooldownTarget) 
		{
			State = (int)States.Regenerating;
		}
	}

	// Shield is off cooldown and regenerating
	private void Regenerate(float dt)
	{
		if (ShieldValue < ShieldCapacity) 
		{
			ShieldValue += (dt * (ShieldCapacity/RegenationRate));
			if(ShieldValue >= ShieldCapacity)
			{
				ShieldValue = Mathf.Min(ShieldValue, ShieldCapacity);
				Charged();
			}
		}
	}
}