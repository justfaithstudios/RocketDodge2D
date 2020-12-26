using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;



public class EFE_PanelTransition : MonoBehaviour {
	
	public TransitionType transitionInType;
	public enum TransitionType {None,SlideInFromLeft,SlideInFromRight,SlideInFromTop,SlideInFromBottom,TransitionFade};
	public TransitionTypeOut transitionOutType;
	public enum TransitionTypeOut {None,SlideOutToLeft,SlideOutToRight,SlideOutToTop,SlideOutToBottom,TransitionFade};
	public EaseType easeType;
	
	[Tooltip("Transition time in seconds")]
	public float transitionSpeed ;
	
	//ease types
	public enum EaseType 
	{Linear,
		InBack,
		InBounce,
		InCirc,
		InCubic,
		InElastic,
		InExpo,
		InOutBack,
		InOutBounce,
		InOutCirc,
		InOutCubic,
		InOutExpo,
		InOutFlash,
		InOutQuad,
		InOutQuart,
		InOutQuint,
		InOutSine,
		InQuad,
		InQuart,
		InSine,
		OutBack,
		OutBounce,
		OutCirc,
		OutCubic,
		OutElastic,
		OutExpo,
		OutQuad,
		OutQuart,
		OutQuint,
		OutSine
	};
	
	
	
	
	[Tooltip("OPTIONAL -An additional panel which will be positioned underneath the panel you are transitioning to. Commonly used for fading out of any panels or images underneath an overlay (or popup) panel. This can be set to nothing in most cases.")]
	public GameObject backgroundFadePanel ;//add one of these to create a faded background (good for popups)
	
	public GameObject transitionFadePanel ;//gets assigned here by the efebase at game start
	public GameObject efe_base;//a reference to the efe base GO
	public EFE_Base efe_base_comp;//a reference to the efe base GO
	
	private RectTransform myRect;
	private RectTransform rootCanvasRect;
	
	void Awake () 
	{
		
		myRect = this.gameObject.GetComponent<RectTransform>();
		rootCanvasRect = GameObject.Find("EFE_Canvas").GetComponent<RectTransform>();

	}
	
	void Update () 
	{
		
	}
	

	public void DoTransitionIn()
	{
		efe_base_comp.panelIsTransitioning=true;
		
		SetEasingType();
		
		//print("fade -doing transition IN");
		//if previous panel was a fade out we should auto switch this to transition type non.
		if(efe_base_comp.doingTransitionFade)
		{
			//return;
		
		}
		
		switch (transitionInType)
		{
		case TransitionType.None:
			{

				break;
			}
		case TransitionType.TransitionFade:
			{
				//transition fade
				PositionTransitionFadePanel();
				break;
			}
		case TransitionType.SlideInFromLeft:
			{
				Vector3 myposition = this.gameObject.transform.localPosition;
				myposition.x = myposition.x - myRect.rect.width;
				this.gameObject.transform.localPosition	=myposition;
				transform.DOMove(rootCanvasRect.position, transitionSpeed).OnComplete(TransitionComplete);;
				break;
			}
		case TransitionType.SlideInFromRight:
			{
				
				Vector3 myposition = this.gameObject.transform.localPosition;
				myposition.x = myposition.x + myRect.rect.width;
				this.gameObject.transform.localPosition	=myposition;
				transform.DOMove(rootCanvasRect.position, transitionSpeed).OnComplete(TransitionComplete);;
				break;
			}
		case TransitionType.SlideInFromTop:
			{
				Vector3 myposition = this.gameObject.transform.localPosition;
				myposition.y = myposition.y + myRect.rect.height;
				this.gameObject.transform.localPosition	=myposition;
				transform.DOMove(rootCanvasRect.position, transitionSpeed).OnComplete(TransitionComplete);;
				break;
			}
		case TransitionType.SlideInFromBottom:
			{
				
				Vector3 myposition = this.gameObject.transform.localPosition;
				myposition.y = myposition.y - myRect.rect.height;
				this.gameObject.transform.localPosition	=myposition;
				transform.DOMove(rootCanvasRect.position, transitionSpeed).OnComplete(TransitionComplete);;;
				break;
			}
			
		}
	}
	
	
	public void DoTransitionOut()
	{
		
		//take on the transition speed of the panel we are moving to
		float transitionSpeed = efe_base_comp.currentPanel.GetComponent<EFE_PanelTransition>().transitionSpeed;
		
		SetEasingType();
		
		if(efe_base_comp.doingTransitionFade)
		{
			//return;
			
		}
		
		switch (transitionOutType)
		{
		case TransitionTypeOut.None:
			{
				//TransitionComplete();
				break;
			}
		case TransitionTypeOut.TransitionFade:
			{
				//transition fade
				PositionTransitionFadePanel();
				break;
			}
		case TransitionTypeOut.SlideOutToLeft:
			{
				Vector3 newPosition = rootCanvasRect.anchoredPosition;
				newPosition.x =-rootCanvasRect.anchoredPosition.x;// - myRect.rect.width;
				transform.DOMove(newPosition, transitionSpeed).OnComplete(TransitionComplete);
				break;
			}
		case TransitionTypeOut.SlideOutToRight:
			{
				Vector3 newPosition = rootCanvasRect.anchoredPosition;
				newPosition.x =rootCanvasRect.anchoredPosition.x*3;// + myRect.rect.width;
				transform.DOMove(newPosition, transitionSpeed).OnComplete(TransitionComplete);
				break;
			}
		case TransitionTypeOut.SlideOutToTop:
			{
				Vector3 newPosition = rootCanvasRect.anchoredPosition;
				newPosition.y =rootCanvasRect.anchoredPosition.y *3;//+ myRect.rect.height;
				transform.DOMove(newPosition, transitionSpeed).OnComplete(TransitionComplete);
				break;
			}
		case TransitionTypeOut.SlideOutToBottom:
			{
				Vector3 newPosition = rootCanvasRect.anchoredPosition;
				newPosition.y =-rootCanvasRect.anchoredPosition.y ;//- myRect.rect.height;
				transform.DOMove(newPosition, transitionSpeed).OnComplete(TransitionComplete);
				break;
			}
		}
	}
	
	public void TransitionComplete()
	{
		//print("fade -transition completeed!!");
		efe_base_comp.panelIsTransitioning=false;
		
	}
	
	public void SetEasingType()
	{
		
		//if(easeType==EaseType.Flash){DOTween.defaultEaseType = Ease.Flash;return;}
		if(easeType==EaseType.InBack){DOTween.defaultEaseType = Ease.InBack;return;}
		if(easeType==EaseType.InBounce){DOTween.defaultEaseType = Ease.InBounce;return;}
		if(easeType==EaseType.InCirc){DOTween.defaultEaseType = Ease.InCirc;return;}
		if(easeType==EaseType.InCubic){DOTween.defaultEaseType = Ease.InCubic;return;}
		if(easeType==EaseType.InElastic){DOTween.defaultEaseType = Ease.InElastic;return;}
		if(easeType==EaseType.InExpo){DOTween.defaultEaseType = Ease.InExpo;return;}
		//if(easeType==EaseType.InFlash){DOTween.defaultEaseType = Ease.InFlash;return;}
		if(easeType==EaseType.InOutBack){DOTween.defaultEaseType = Ease.InOutBack;return;}
		if(easeType==EaseType.InOutBounce){DOTween.defaultEaseType = Ease.InOutBounce;return;}
		if(easeType==EaseType.InOutCirc){DOTween.defaultEaseType = Ease.InOutCirc;return;}
		if(easeType==EaseType.InOutCubic){DOTween.defaultEaseType = Ease.InOutCubic;return;}
		if(easeType==EaseType.InOutCubic){DOTween.defaultEaseType = Ease.InOutElastic;return;}
		if(easeType==EaseType.InOutExpo){DOTween.defaultEaseType = Ease.InOutExpo;return;}
		if(easeType==EaseType.InOutFlash){DOTween.defaultEaseType = Ease.InOutFlash;return;}
		if(easeType==EaseType.InOutQuad){DOTween.defaultEaseType = Ease.InOutQuad;return;}
		if(easeType==EaseType.InOutQuart){DOTween.defaultEaseType = Ease.InOutQuart;return;}
		if(easeType==EaseType.InOutQuint){DOTween.defaultEaseType = Ease.InOutQuint;return;}
		if(easeType==EaseType.InOutSine){DOTween.defaultEaseType = Ease.InOutSine;return;}
		if(easeType==EaseType.InQuad){DOTween.defaultEaseType = Ease.InQuad;return;}
		if(easeType==EaseType.InQuart){DOTween.defaultEaseType = Ease.InQuart;return;}
		if(easeType==EaseType.InQuart){DOTween.defaultEaseType = Ease.InQuint;return;}
		if(easeType==EaseType.InSine){DOTween.defaultEaseType = Ease.InSine;return;}
		if(easeType==EaseType.Linear){DOTween.defaultEaseType = Ease.Linear;return;}
		if(easeType==EaseType.OutBack){DOTween.defaultEaseType = Ease.OutBack;return;}
		if(easeType==EaseType.OutBounce){DOTween.defaultEaseType = Ease.OutBounce;return;}
		if(easeType==EaseType.OutCirc){DOTween.defaultEaseType = Ease.OutCirc;return;}
		if(easeType==EaseType.OutCubic){DOTween.defaultEaseType = Ease.OutCubic;return;}
		if(easeType==EaseType.OutElastic){DOTween.defaultEaseType = Ease.OutElastic;return;}
		if(easeType==EaseType.OutExpo){DOTween.defaultEaseType = Ease.OutExpo;return;}
		//if(easeType==EaseType.OutFlash){DOTween.defaultEaseType = Ease.OutFlash;return;}
		if(easeType==EaseType.OutQuad){DOTween.defaultEaseType = Ease.OutQuad;return;}
		if(easeType==EaseType.OutQuart){DOTween.defaultEaseType = Ease.OutQuart;return;}
		if(easeType==EaseType.OutQuint){DOTween.defaultEaseType = Ease.OutQuint;return;}
		if(easeType==EaseType.OutSine){DOTween.defaultEaseType = Ease.OutSine;return;}
		
	
	}
	
	public void PositionBackgroundFadePanel()
	{
		if(backgroundFadePanel!=null)
		{
			backgroundFadePanel.SetActive(true);
			backgroundFadePanel.transform.position = rootCanvasRect.position;
			backgroundFadePanel.transform.SetAsLastSibling ();
		}
	}
	
	//transition fade : places a panel at screen position 2: fades it in 3: switches panels 4: fades out this panel.
	float fadeSpeed ;
	public void PositionTransitionFadePanel()
	{
		if(efe_base_comp.doingTransitionFade==true)
		{return;}
		
		efe_base_comp.doingTransitionFade=true;
		
		
		//set fade panel to here
		//print("fade - MOVING FADE TRANSITION INTO POSITION");
		transitionFadePanel.transform.position	=rootCanvasRect.position;
		//fade in
		Color curColor= transitionFadePanel.GetComponent<Image>().color;
		curColor.a=0.0f;
		
		if(efe_base_comp.isFirstPanel)
		{
			curColor.a=1.0f;
		}
		
		transitionFadePanel.GetComponent<Image>().color=curColor;
		
		float curPanelSpeed = efe_base_comp.currentPanel.GetComponent<EFE_PanelTransition>().transitionSpeed;
		float prevPanelSpeed;
		if(efe_base_comp.previousPanel!=null)
		{
			prevPanelSpeed = efe_base_comp.previousPanel.GetComponent<EFE_PanelTransition>().transitionSpeed;
		}
		
		
		efe_base_comp.fadeDelay = curPanelSpeed; 
			
		transitionFadePanel.GetComponent<Image>().DOFade(1,efe_base_comp.fadeDelay).OnComplete(SwitchPanelsAndFadeOut);;
		
	}
	
	
	public void  SwitchPanelsAndFadeOut()
	{
		
		//turn this off now if we used a first transition
		efe_base_comp.isFirstPanel=false;
		//print("fade - Swicth panels and fade out");
		//switch panels
		efe_base.GetComponent<EFE_Base>().currentPanel.active=true;
		//now fade out
		efe_base_comp.fadeDelay = efe_base_comp.currentPanel.GetComponent<EFE_PanelTransition>().transitionSpeed;
		
		transitionFadePanel.GetComponent<Image>().DOFade(0,efe_base_comp.fadeDelay).OnComplete(CompletedAllFades);
		
	}
	
	public void CompletedAllFades()
	{
		efe_base_comp.panelIsTransitioning=false;
		efe_base_comp.doingTransitionFade=false;
		
	}
	
	
	
}
