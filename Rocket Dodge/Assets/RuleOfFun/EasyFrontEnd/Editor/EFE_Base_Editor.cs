using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(EFE_Base))]
[CanEditMultipleObjects()]
public class EFE_Base_Editor : Editor
{
	private SerializedObject obj;// The EFE_Content_Modifier object
	private SerializedProperty firstPanel;
	private SerializedProperty useFirstPanelTransition;
	private SerializedProperty [] panelList;
	private SerializedProperty [] messageReciever;
	
	public SerializedProperty transitionFadePanel;
	//private SerializedProperty efe_base;
	
	private SerializedProperty currentPanel;
	private SerializedProperty previousPanel;
	
	private SerializedProperty doingTransitionFade;
	
	private Texture backgroundImage ;
	
	SerializedProperty j;
	SerializedProperty k;
	
	public void OnEnable()
	{
		backgroundImage = Resources.Load("icon32")as Texture;
		obj = new SerializedObject(target);
		//efe_base=this.gameObject;
	} 
	
	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		GUIStyle style1 = new GUIStyle();
		style1.font = EditorStyles.boldFont;
		style1.normal.textColor = new Color (0.4f,0.6f,1,1);
		
		GUIStyle style2 = new GUIStyle();
		style2.font = EditorStyles.miniFont;
		style2.normal.textColor = new Color (0.4f,0.6f,1,1);
		
		
		GUILayout.Label(backgroundImage,GUILayout.ExpandWidth(true));
		
		EditorGUILayout.HelpBox("All panel which you want to use with EFE must be placed in thie list. " +
			"If you need to create a new panel just add another element to the panel list array (Size)" +
			"and drag in you new panel object into the inpector. The ordering of the objects in this list is not important." +
			" Easy!",MessageType.Info);
		
		EditorGUILayout.LabelField("EFE First Panel To Display", style1, null);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("firstPanel"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("useFirstPanelTransition"),true);
		
		EditorGUILayout.LabelField("OPTIONS", style1, null);
		//EditorGUILayout.HelpBox("Check this if you want blank backrounds between screen wipes (Camera will not draw any 3D Objects underneath).",MessageType.Info);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("switchOffMainCameraDuringTransitions"),true);
		
		EditorGUILayout.LabelField("EFE Panel List", style1, null);
		//EditorGUILayout.PropertyField(GetArrayElementAtIndex( panelList[0] ));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("panelList"),true);
		
		EditorGUILayout.LabelField("Transition Fade Panel", style1, null);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("transitionFadePanel"),true);
		
		EditorGUILayout.LabelField("GameObjects That Recieve Panel Change Messages (EFE_Panel_Opened)", style1, null);
		EditorGUILayout.HelpBox("OPTIONAL: Add a function called 'EFE_Panel_Opened' to game object scripts and drag those game objects into this list. This function will be called whenever a panel is opened.",MessageType.Info);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("messageReciever"),true);
		
		//EditorGUILayout.LabelField("More coming soon..", style2, null);
		
		
		
		
		
		j = serializedObject.FindProperty("currentPanel");
		
		
		if(j!=null&&j.objectReferenceValue!=null)
		{
			//EditorGUILayout.PropertyField(serializedObject.FindProperty("isFirstPanel"),true);
			EditorGUILayout.LabelField("EFE Panel Debug", style1, null);
			//Debug.Log(j);
			EditorGUILayout.PropertyField(j);
			
			
			
		}
		k = serializedObject.FindProperty("previousPanel");
		
		if(k!=null&&k.objectReferenceValue!=null)
		{
			EditorGUILayout.PropertyField(k);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("doingTransitionFade"),true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("panelIsTransitioning"),true);
			
			
			
		}
		
		
		
		serializedObject.ApplyModifiedProperties();
		obj.ApplyModifiedProperties(); 
		
	}
	
	public void OnSceneGUI()
	{
        // Implement what you want to see in scene view here
	}
}