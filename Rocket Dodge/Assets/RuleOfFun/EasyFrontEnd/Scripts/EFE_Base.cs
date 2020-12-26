using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class EFE_Base: MonoBehaviour {
	
	[Tooltip("The first panel that is displayed at game start")]
	public  GameObject firstPanel; //the first panel you want to display at game start
	[Tooltip("Should the first panel use its transition the first time it is displayed?")]
	public bool useFirstPanelTransition;
	[Tooltip("REQUIRED if using fade transitions -An a panel that is used for fading bettween other panels. 1:Fades in 2:Switches panels 3:Fades out. NOTE: Make sure this object has a Canvas component attached with override sorting flag checked. Ensure the sorting layer is higher than 0 so that it sits above other panels.")]
	public GameObject transitionFadePanel ;//add one of these to create a faded background (good for popups)
	public GameObject efe_base; //a reference to the efe base
	
	[Tooltip("All your panel gameobject will need to be added to this list. TIP: Your Transition Fade panel (if you want to use screen fade transitions) does not go in this section. Drag that one into the property box below.")]
	public GameObject [] panelList;
	public GameObject []messageReciever;//a list of object that will recieve message events
	public static Vector3 panelLastPosition;
	public GameObject currentPanel;
	public GameObject previousPanel;
	public static GameObject currentOverlay;
	public static Vector3 overlayLastPosition;
	public bool isFirstPanel=true;
	
	EFE_PanelTransition panelTransitionScript;
	EFE_PanelTransition prevPanelTransitionScript ;
	
	[Tooltip("Check this if you want blank backrounds between screen wipes (Camera will not draw any 3D Objects underneath")]
	public bool switchOffMainCameraDuringTransitions;//sometime the user doesnt want to see whats drawn behind during slide transitions (modifies the culling objects of the camera)
	
	public bool doingTransitionFade;//a special fade transition
	public bool panelIsTransitioning=false;//any kind of transition
	
	private Camera myMainCamera;
	private int oldCullingMask;
	
	void Awake()
	{
		efe_base=this.gameObject;
		myMainCamera =Camera.main;
		oldCullingMask = myMainCamera.cullingMask;
	}
	// Use this for initialization
	void Start () {
		
		
		DontDestroyOnLoad(gameObject);
		
		AssignFadeTransitionPanel();
		
		if(firstPanel==null)
		{
			Debug.LogError("Please assign a panel string in the EFE_Container inspector. This will be the first panel that will be opened by default.");
		}
		OpenPanel(firstPanel);
	}
	
	// Update is called once per frame
	void Update () {
		
		//first panels should always have culling off
		
		
		//print("Is First panel:"+isFirstPanel);//always falswe!
		if(switchOffMainCameraDuringTransitions)
		{
			if(doingTransitionFade==false)
			{
				if(panelIsTransitioning)
				{
					myMainCamera.cullingMask=0;//nothing
				}
				else
				{
					myMainCamera.cullingMask=oldCullingMask;//whatever it used to be ()
				}
			}
		}

		
		
	}
	
	//tell each panel about the transition fade panel
	void AssignFadeTransitionPanel()
	{
		for(int x=0;x<panelList.Length;x++)
		{
			if(panelList[x]!=null)
			{
				EFE_PanelTransition panelTransitionComponent =panelList[x].GetComponent<EFE_PanelTransition>();
				
				if(panelTransitionComponent!=null)
				{
					panelTransitionComponent.transitionFadePanel=transitionFadePanel;
					panelTransitionComponent.efe_base=efe_base;
					panelTransitionComponent.efe_base_comp=efe_base.GetComponent<EFE_Base>();
					
				}
			}
		}
	}
	
	
	
	public void OpenPanel(GameObject panel)
	{
		//if panel already openingdont allow it again
		if(panelIsTransitioning)
		{return;}
		
		panelIsTransitioning=true;
		print("EFE - Open Panel");
		
		//prevent opening panels during fades
		if(doingTransitionFade){return;}
		
		string panelName=panel.name;
		//close current panel if applicabale
		if(currentPanel)
		{
			//currentPanel.transform.position = panelLastPosition;
			previousPanel = currentPanel;
		}
		

		currentPanel = FindPanel(panelName);
		currentPanel.SetActive(true);
		panelLastPosition = currentPanel.transform.position;

		////position panel on the screen!
		currentPanel.transform.position = currentPanel.transform.parent.position;
	
		/////////////////////////////////////////////
		panelTransitionScript = currentPanel.GetComponent<EFE_PanelTransition>();
		if(previousPanel)
		{prevPanelTransitionScript = previousPanel.GetComponent<EFE_PanelTransition>();}
		
		
		
		
		
		float hidePanelWaitTime =0f;
		
		//Prevent any transitions on first panel (if required)
		if(panelName ==firstPanel.name && isFirstPanel==true)
		{
			if(useFirstPanelTransition==false)
			{panelTransitionScript=null;
				isFirstPanel=false;
			}
			
			//isFirstPanel=false;
		}
		
		//Do transition
		if(panelTransitionScript)
		{
			bool isTransitionFade=false;
			
			
			//if either the prev or current have fades then this IS a fade
			//if(doingTransitionFade)
			//if(panelTransitionScript.transitionInType==EFE_PanelTransition.TransitionType.TransitionFade)
			
			//if(panelTransitionScript.transitionInType==EFE_PanelTransition.TransitionType.TransitionFade||
			//	prevPanelTransitionScript.transitionInType==EFE_PanelTransition.TransitionType.TransitionFade||
			//	panelTransitionScript.transitionOutType==EFE_PanelTransition.TransitionTypeOut.TransitionFade||
			//	prevPanelTransitionScript.transitionOutType==EFE_PanelTransition.TransitionTypeOut.TransitionFade)
				
			if(panelTransitionScript.transitionInType==EFE_PanelTransition.TransitionType.TransitionFade)
				
			{
				isTransitionFade=true;
					
			}
			
			if(prevPanelTransitionScript)
			{
				if(prevPanelTransitionScript.transitionOutType==EFE_PanelTransition.TransitionTypeOut.TransitionFade)
				{
					isTransitionFade=true;
				}
			}
			
			//if(isTransitionFade==true)
			//{
			//	panelTransitionScript.transitionInType=EFE_PanelTransition.TransitionType.None;
			//}
			if(isTransitionFade)
			{
				IEnumerator transitionIn = DoTransitionInAFterDelay(prevPanelTransitionScript,panelTransitionScript);
				StartCoroutine(transitionIn);
			}
			else
			{
				panelTransitionScript.DoTransitionIn();
			}
			
			if(prevPanelTransitionScript){
				
				//check if this transition is a fade transition and do special..
				if(isTransitionFade)
					//if(prevPanelTransitionScript.transitionInType==EFE_PanelTransition.TransitionType.TransitionFade)
				{
					//prevPanelTransitionScript.transitionInType=EFE_PanelTransition.TransitionType.None;
					//prevPanelTransitionScript.tr
					IEnumerator transitionOut = DoTransitionOutAFterDelay(prevPanelTransitionScript,panelTransitionScript);
					StartCoroutine(transitionOut);
					
				}
				else
				{
					prevPanelTransitionScript.DoTransitionOut();
				}
			}
			
			hidePanelWaitTime = panelTransitionScript.transitionSpeed;
			//
			currentPanel.transform.SetAsLastSibling ();
			StartCoroutine(HideAllPanelsAfterDelay(hidePanelWaitTime));
			

		}
		else
		{
			hidePanelWaitTime = 0f;
			HideAllPanels();//do it now
			if(previousPanel)
			{previousPanel.transform.position = panelLastPosition;}
			panelIsTransitioning=false;
			return;
		}
		
		//////////////////////////////////////////////
		
		
		SendPanelOpenedMessages();
		
	}
	
	//TODO THIS IS ALL BROKEN!!
	
	//transitionas after fades
	public IEnumerator  DoTransitionOutAFterDelay(EFE_PanelTransition prevPanel,EFE_PanelTransition panel)
	{
		//print("fade -DoTransitionOut after delay");
		//get current panel and set its transition type to instant
		EFE_PanelTransition.TransitionType prevTransitionType_prevPanel =prevPanel.transitionInType;
		
		//temporary set the cur panel speed to equal delay time
		float prevTransSpeed = panel.transitionSpeed;
		panel.transitionSpeed =fadeDelay;
		prevPanel.PositionTransitionFadePanel();
		//print("fade - Setting Panel OFF ");
		currentPanel.active=false;
		//print("Started coroutine");
		yield return new WaitForSeconds(fadeDelay);
		currentPanel.active=true;
		//print("fade - Done trans OUT after delay of "+fadeDelay);
		prevPanel.DoTransitionOut();
		
		prevPanel.transitionInType = prevTransitionType_prevPanel;
		panel.transitionSpeed = prevTransSpeed;
		
		
	}
	
	public float fadeDelay;//set by panel transition
	public float prevTransSpeed;
	public IEnumerator  DoTransitionInAFterDelay(EFE_PanelTransition prevPanel,EFE_PanelTransition panel )
	{
		
		
		//print("fade -DoTransitionIN after delay");
		//get current panel and set its transition type to instant
		EFE_PanelTransition.TransitionType prevTransitionType_currentPanel =panel.transitionInType;
		panel.transitionInType = EFE_PanelTransition.TransitionType.None;
		
		//temporary set the cur panel speed to equal delay time
		if(prevPanel)
		{
			prevTransSpeed = prevPanel.transitionSpeed;
			prevPanel.transitionSpeed =fadeDelay;
		}
		
		
		panel.PositionTransitionFadePanel();
		//print("fade - Setting Panel OFF ");
		currentPanel.active=false;
		//print("Started coroutine");
		yield return new WaitForSeconds(fadeDelay);
		currentPanel.active=true;
		//print("End of coroutine");
		panel.DoTransitionIn();
		//print("fade - Done trans IN after delay of "+fadeDelay);
		//reset transition type and speed
		panel.transitionInType=prevTransitionType_currentPanel;
		if(prevPanel)
		{
			prevPanel.transitionSpeed = prevTransSpeed;
		}
		
		
	}
	
	
	public void SendPanelOpenedMessages()
	{
		for(int x=0;x<messageReciever.Length;x++)
			{
			messageReciever[x].SendMessage("EFE_Panel_Opened",SendMessageOptions.DontRequireReceiver);
			}
	}
	
	
	
	public void CloseCurrentOverlayPanel()
	{
		print("today - current overlay is "+currentOverlay.name);
		if(currentOverlay)
		{
			//currentOverlay.transform.position = overlayLastPosition;
			
		}
		
		panelTransitionScript = currentOverlay.GetComponent<EFE_PanelTransition>();
		float hidePanelWaitTime =0f;
		
		if(panelTransitionScript)
		{
			panelTransitionScript.DoTransitionOut();
			hidePanelWaitTime = panelTransitionScript.transitionSpeed;
			StartCoroutine(HideAllPanelsAfterDelay(hidePanelWaitTime));
		}
		else
		{
			currentOverlay.transform.position = overlayLastPosition;
		}
		
		//fade out the dark fade screen
		BroadcastMessage("BackgroundFadeOut",SendMessageOptions.DontRequireReceiver);
		
		
		print("EFE - Close Overlay");
		StartCoroutine(HideAllPanelsAfterDelay(hidePanelWaitTime));
	}
	
	
	public IEnumerator HideAllPanelsAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		panelIsTransitioning=false;
		HideAllPanels();
		
		if(previousPanel)
		{
			previousPanel.transform.position = panelLastPosition;
		}
	}
	
	private GameObject foundPanel;
	
	public GameObject FindPanel(string panelName)
	{

		for (int i=0;i<panelList.Length;i++)
		{
			if(panelList[i]!=null)
			{
				if(panelList[i].name==panelName)
				{
					foundPanel = panelList[i];
					
				}
			}
			//else
			//{
			//	Debug.LogError("It looks like you havent added your new panel ("+panelName+") to the EFE Base panel list.");
			//}
		}
		
		return (foundPanel);
	}
	
	public void HideAllPanels()//except for curent panel
	{
		for (int i=0;i<panelList.Length;i++)
		{
			if(panelList[i]!=null)
			{
				if(panelList[i].name!=currentPanel.name)
				{
					panelList[i].SetActive(false);
				}
			}
		}
	}
	
	public void HideCurrentPanel()
	{
		currentPanel.transform.position = panelLastPosition;
	}

	
	public void OpenOverlayPanel(GameObject panel)
	{
		//set this to false for overlays
		//panelIsTransitioning=false;
		
		string panelName = panel.name;
		currentOverlay = FindPanel(panelName);
		//print("Current overlay name "+currentOverlay.name);
		currentOverlay.SetActive(true);
		//get the starting position of the overlay (for resetting its position later)
		overlayLastPosition = currentOverlay.transform.position;
		//Do the Overlay
		currentOverlay.transform.position = currentPanel.transform.parent.position;
		
		currentOverlay.SendMessage("PositionBackgroundFadePanel");
		
		currentOverlay.SendMessage("DoTransitionIn");
		currentOverlay.transform.SetAsLastSibling ();

	}
	
	
	
	//Util functions
	
	public void OpenUrl(string url)
	{
		Application.OpenURL(url);
	}
	
	public void LoadScene(string sceneName)
	{
		
		Application.LoadLevel(sceneName);

	}
	
	public void ReloadCurrentScene()
	{
		
		Application.LoadLevel(Application.loadedLevel);

	}
	
	
}
