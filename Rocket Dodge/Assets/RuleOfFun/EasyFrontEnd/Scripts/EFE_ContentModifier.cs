using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EFE_ContentModifier : MonoBehaviour {
	
	//[Header("This script should only be attached to a button object.\n \n\n")]
	
	//Texts
	public Text textToModify1;
	public string  newTextString1; 
	public Text textToModify2;
	public string  newTextString2; 
	public Text textToModify3;
	public string  newTextString3; 
	
	//Images
	public GameObject imageToModify1;
	public Sprite  newImage1; 
	public GameObject imageToModify2;
	public Sprite  newImage2; 
	public GameObject imageToModify3;
	public Sprite  newImage3; 
	
	//functionality modifiers
	[Tooltip("Drag the OTHER button object that you wish to change the function of when THIS button is clicked here.")]
	public GameObject onClickSender1;
	[Tooltip("ANY gameobject that will recieve the new event.")]
	public GameObject onClickMessageReciever1;
	[Tooltip("The function to send to the receiving object.")]
	public string onClickNewMessage1;
	[Tooltip("An optional argument that the function may require.")]
	public string onClickNewArg1;
	[Tooltip("Should this new event completly replace any existing events or execute in addition to it?")]
	public bool replaceExistingEvents1;
	
	[Tooltip("Drag the OTHER button object that you wish to change the function of when THIS button is clicked here.")]
	public GameObject onClickSender2;
	[Tooltip("ANY gameobject that will recieve the new event.")]
	public GameObject onClickMessageReciever2;
	[Tooltip("The function to send to the receiving object.")]
	public string onClickNewMessage2;
	[Tooltip("An optional argument that the function may require.")]
	public string onClickNewArg2;
	[Tooltip("Should this new event completly replace any existing events or execute in addition to it?")]
	public bool replaceExistingEvents2;
	
	[Tooltip("Drag the OTHER button object that you wish to change the function of when THIS button is clicked here.")]
	public GameObject onClickSender3;
	[Tooltip("ANY gameobject that will recieve the new event.")]
	public GameObject onClickMessageReciever3;
	[Tooltip("The function to send to the receiving object.")]
	public string onClickNewMessage3;
	[Tooltip("An optional argument that the function may require.")]
	public string onClickNewArg3;
	[Tooltip("Should this new event completly replace any existing events or execute in addition to it?")]
	public bool replaceExistingEvents3;
	
	private Button myButton;
	
	// Use this for initialization
	void Start () {
		
			
		gameObject.GetComponent<Button>().onClick.AddListener(() => { OnClick();});	
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void SendEFEMessage(GameObject reciever,string function,object value)
	{
		reciever.SendMessage(function,value);
		
	}
	
	void OnClick()
	{
		
		//Replace text
		if(newTextString1!=null&&textToModify1!=null)
		{
			newTextString1 = newTextString1.Replace("<br>","\n");
			textToModify1.text = newTextString1;
		}
		
		if(newTextString2!=null&&textToModify2!=null)
		{
			newTextString2 = newTextString2.Replace("<br>","\n");
			textToModify2.text = newTextString2;
		}
		if(newTextString3!=null&&textToModify3!=null)
		{
			newTextString3 = newTextString3.Replace("<br>","\n");
			textToModify3.text = newTextString3;
		}
		
		
		//Replace Images
		if(imageToModify1!=null)
		{
			Image r1 = imageToModify1.GetComponent<Image>() ;
			r1.sprite 	= newImage1 as Sprite;
		}
		
		if(imageToModify2!=null)
		{
			Image r2 = imageToModify2.GetComponent<Image>() ;
			r2.sprite 	= newImage2 as Sprite;
		}
		
		if(imageToModify3!=null)
		{
			Image r3 = imageToModify3.GetComponent<Image>() ;
			r3.sprite 	= newImage3 as Sprite;
		}
		
		//modifiers
		if(onClickSender1!=null)
		{
			
			if(replaceExistingEvents1)
			{
				onClickSender1.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();//removes existing event listener
			}
			onClickSender1.GetComponent<Button>().onClick.AddListener(delegate { SendEFEMessage(onClickMessageReciever1,onClickNewMessage1,onClickNewArg1); });
			
		}
		if(onClickSender2!=null)
		{
			if(replaceExistingEvents2)
			{
				onClickSender2.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
			}
			onClickSender2.GetComponent<Button>().onClick.AddListener(delegate { SendEFEMessage(onClickMessageReciever2,onClickNewMessage2,onClickNewArg2); });
		}
		if(onClickSender3!=null)
		{
			if(replaceExistingEvents3)
			{
				onClickSender3.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
			}
			onClickSender3.GetComponent<Button>().onClick.AddListener(delegate { SendEFEMessage(onClickMessageReciever3,onClickNewMessage3,onClickNewArg3); });
		}
		
	}
}
