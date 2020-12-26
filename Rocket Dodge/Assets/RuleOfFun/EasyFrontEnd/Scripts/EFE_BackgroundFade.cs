using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class EFE_BackgroundFade : MonoBehaviour {
	
	[Tooltip("Lower values indicate a slower fade in. Increase this value for a faster fade in.")]
	public float fadeSpeed=2f;
	
	private Image myImage;
	private Color myImageColorEnd;
	private bool fadeIn ;
	private float t;
	
	// Use this for initialization
	void Awake () {
		
		myImage = gameObject.GetComponent<Image>();
		myImageColorEnd= myImage.color;

	}
	
	// Update is called once per frame
	void Update () {
		
		t += Time.deltaTime*fadeSpeed;
		
		if(fadeIn==true)
		{
			myImage.color  = Color.Lerp(Color.clear, myImageColorEnd, t);
		}
		else//fade out
		{
			myImage.color  = Color.Lerp( myImageColorEnd,Color.clear, t);
		}
			

	}
	
	void OnEnable()
	{
		if(myImage==null){return;}
		
		myImage.color = Color.clear;
		fadeIn =true;
		t=0;

	}
	
	void BackgroundFadeOut()
	{
		myImage.color = myImageColorEnd;
		fadeIn =false;
		t=0;
	}
}


