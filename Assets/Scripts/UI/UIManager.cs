using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
	public GameManager GameManager;

	// Hero Attributes
	public Text[] PlayerStats;
	public Text[] Debug;

	UIHelper ui;

	void Start()
	{
		Initialize();
	}
	
	private void Initialize()
	{

	}
	
	void Update()
	{
		ui = GameManager.GetUIFrame ();

		PlayerStats [0].text = ui.HealthCurrent + "/" + ui.HealthTotal;
		Debug [0].text = ui.posX + ", " + ui.posY;

	}
}
