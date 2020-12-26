using UnityEngine;
using System.Collections;

public class Test01 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		//optional 
		EFE_Base instance = new EFE_Base();
		instance.OpenPanel(GameObject.Find("Panel_MainMenu"));

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
