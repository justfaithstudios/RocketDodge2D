using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(EFE_BackgroundFade))]
[CanEditMultipleObjects()]
public class EFE_BackgroundFade_Editor : Editor
{
	private SerializedObject obj;

	private SerializedProperty fadeSpeed;

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
		
		EditorGUILayout.HelpBox("Attach this to a panel which is used as a fade background such as a faded out black image. " +
			"This panel can be called to appear behind overlay popups as if to fade out the background. This panel will not be " +
			"affected by transitions.",MessageType.Info);
		
		
		EditorGUILayout.LabelField("EFE Overlay Background Modifiers", style1, null);
		//EditorGUILayout.PropertyField(GetArrayElementAtIndex( panelList[0] ));

		EditorGUILayout.PropertyField(serializedObject.FindProperty("fadeSpeed"),true);
		
		//EditorGUILayout.LabelField("More coming soon..", style2, null);


		serializedObject.ApplyModifiedProperties();
		obj.ApplyModifiedProperties(); 
		
	}
	
	public void OnSceneGUI()
	{
        // Implement what you want to see in scene view here
	}
}