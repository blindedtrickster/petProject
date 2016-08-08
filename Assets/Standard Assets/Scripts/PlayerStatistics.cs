using UnityEngine;
using System.Collections;

public class PlayerStatistics : MonoBehaviour {
	
	//Public
	public CharacterInfo localPlayerData = new CharacterInfo();

	/*
	public int Level;
	public int Strength;
	public int Dexterity;
	public int Vitality;
	public int Intelligence;
	public int Luck;
	public int MaxHealth;
	public int CurHealth;
	public int nextLevel;
	public int XP;
	*/

	//Private
	public int characterID = 0;	//Each character gets their own number. it's effectively an ID.

	// Use this for initialization
	void Start () {
		/*
		Level = GameManager.Instance.Level;
		Strength = GameManager.Instance.Strength;
		Dexterity = GameManager.Instance.Dexterity;
		Vitality = GameManager.Instance.Vitality;
		Intelligence = GameManager.Instance.Intelligence;
		Luck = GameManager.Instance.Luck;
		MaxHealth = GameManager.Instance.MaxHealth;
		CurHealth = GameManager.Instance.CurHealth;
		nextLevel = GameManager.Instance.nextLevel;
		XP = GameManager.Instance.XP;
		*/

		localPlayerData = GameManager.Instance.Player1;
		Debug.Log(localPlayerData.CurHealth);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D col) {
		Debug.Log("Hit something");
		if(col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			//CurHealth -= 1;
		}

		if(col.gameObject.tag == "Door")
		{
			Vector3 newLoc = col.gameObject.GetComponent<Doorway>().Player1newLoc;
			//GameManager.Instance.savePlayer(this, newLoc);
			GameManager.Instance.savePlayer(localPlayerData, characterID, newLoc);
		}
	}

	public void savePlayer(CharacterInfo player) {
		//GameManager.Instance.savePlayer(player, characterID, newLoc);
	}

	public void takeDamage(int incomingDamage) {
		localPlayerData.CurHealth -= incomingDamage;
		Debug.Log("Took " + incomingDamage + " damage!");
	}
}
