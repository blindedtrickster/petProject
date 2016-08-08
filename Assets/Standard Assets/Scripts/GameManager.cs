using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	//Public
	public static GameManager Instance;

	//[PlayerInfo]
	public CharacterInfo Player1 = new CharacterInfo();

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
	public Vector3 Location;


	public bool paused = false;
	
	
	// Use this for initialization
	void Start () {

	}

	void Awake ()   
	{
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy (gameObject);
		}
	}

	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp (KeyCode.F2))
		{
			Object charSprite = Instantiate (Resources.Load("Sprite"),Location,Quaternion.identity);
			charSprite.name = "Sprite";
		}

		if(Input.GetKeyUp (KeyCode.Escape) && !paused)
		{
			paused = true;
		} else if (Input.GetKeyUp (KeyCode.Escape) && paused)
		{
			paused = false;
		}
	}

	//public void savePlayer(PlayerStatistics player, Vector3 newLoc)
	public void savePlayer(CharacterInfo playerInfo, int playerID, Vector3 newLoc)
	{
		/*
		Level = player.Level;
		Strength = player.Strength;
		Dexterity = player.Dexterity;
		Vitality = player.Vitality;
		Intelligence = player.Intelligence;
		Luck = player.Luck;
		MaxHealth = player.MaxHealth;
		CurHealth = player.CurHealth;
		nextLevel = player.nextLevel;
		XP = player.XP;
		Location = newLoc;
		*/
		Debug.Log("Saving player");
		GameManager.Instance.Player1 = playerInfo;
	}

	void OnLevelLoaded()
	{
		Debug.Log("Test worked!");
	}

}
