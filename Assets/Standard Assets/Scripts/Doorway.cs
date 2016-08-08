using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Doorway : MonoBehaviour {

	//Public
	public string Destination;
	public Vector3 Player1newLoc;
	public Vector3 Player2newLoc;
	public Vector3 Player3newLoc;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			GameManager.Instance.Location = Player1newLoc;
			SceneManager.LoadScene(Destination);
		}
	}

	void OnLoadedLevel() {

	}
}
