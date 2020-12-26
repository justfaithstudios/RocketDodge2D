using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(EFE_PanelTransition))]
[CanEditMultipleObjects()]
public class EFE_PanelTransition_Editor : Editor
{
	private SerializedObject obj;
	
	private SerializedProperty transitionInType;
	private SerializedProperty transitionOutType;
	private SerializedProperty easeType;
	private SerializedProperty transitionSpeed;
	private SerializedProperty backgroundFadePanel;
	private SerializedProperty transitionFadePanel;
	
	private Texture backgroundImage ;
	
	
	public void OnEnable()
	{
		backgroundImage = Resources.Load("icon32")as Texture;
		obj = new SerializedObject(target);
		
		
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
		
		EditorGUILayout.HelpBox("This panel transition component should be attached to all panels that you want to have a transition either in, out or both." +
			"You dont need this component if you don't need any transitions, everything will still work just fine. Easy!",MessageType.Info);
		
		
		EditorGUILayout.LabelField("EFE Panel Transition Modifiers", style1, null);
		//EditorGUILayout.PropertyField(GetArrayElementAtIndex( panelList[0] ));
		
		
		EditorGUILayout.PropertyField(serializedObject.FindProperty("transitionInType"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("transitionOutType"),true);
		
		SerializedProperty sss = serializedObject.FindProperty("transitionInType");
		
		
		
		EditorGUILayout.PropertyField(serializedObject.FindProperty("easeType"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("transitionSpeed"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("backgroundFadePanel"),true);
		
		//debug to show if fade transition recognized
		//EditorGUILayout.PropertyField(serializedObject.FindProperty("transitionFadePanel"),true);
		
		//EditorGUILayout.LabelField("More coming soon..", style2, null);
		
		
		serializedObject.ApplyModifiedProperties();
		obj.ApplyModifiedProperties(); 
		
	}
	
	public void OnSceneGUI()
	{
        // Implement what you want to see in scene view here
	}
}