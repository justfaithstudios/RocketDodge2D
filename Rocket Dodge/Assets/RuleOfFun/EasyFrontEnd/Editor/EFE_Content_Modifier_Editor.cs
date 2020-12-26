using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(EFE_ContentModifier))]
[CanEditMultipleObjects()]
public class EFE_Content_Modifier_Editor : Editor
{
	private SerializedObject obj;// The EFE_Content_Modifier object
	private SerializedProperty textToModify1;
	private SerializedProperty newTextString1;
	private SerializedProperty textToModify2;
	private SerializedProperty newTextString2;
	private SerializedProperty textToModify3;
	private SerializedProperty newTextString3;
	//Images
	private SerializedProperty imageToModify1;
	private SerializedProperty newImage1;
	private SerializedProperty imageToModify2;
	private SerializedProperty newImage2;
	private SerializedProperty imageToModify3;
	private SerializedProperty newImage3;
	
	//Onclick functionality
	
	public SerializedProperty onClickSender1;
	public SerializedProperty onClickMessageReciever1;
	public SerializedProperty onClickNewMessage1;
	public SerializedProperty onClickNewArg1;
	public SerializedProperty replaceExistingEvents1;
	
	public SerializedProperty onClickSender2;
	public SerializedProperty onClickMessageReciever2;
	public SerializedProperty onClickNewMessage2;
	public SerializedProperty onClickNewArg2;
	public SerializedProperty replaceExistingEvents2;
	
	public SerializedProperty onClickSender3;
	public SerializedProperty onClickMessageReciever3;
	public SerializedProperty onClickNewMessage3;
	public SerializedProperty onClickNewArg3;
	public SerializedProperty replaceExistingEvents3;
	
	//private Color efe_color = new Color(255,0,0,0);
	
	private Texture backgroundImage ;
	
	public void OnEnable()
	{
		backgroundImage = Resources.Load("icon32")as Texture;
		obj = new SerializedObject(target);
		
		
		textToModify1 = obj.FindProperty("textToModify1");
		newTextString1 = obj.FindProperty("newTextString1");
		textToModify2 = obj.FindProperty("textToModify2");
		newTextString2 = obj.FindProperty("newTextString2");
		textToModify3 = obj.FindProperty("textToModify3");
		newTextString3 = obj.FindProperty("newTextString3");
		
		//images
		imageToModify1 = obj.FindProperty("imageToModify1");
		newImage1 = obj.FindProperty("newImage1");
		imageToModify2 = obj.FindProperty("imageToModify2");
		newImage2 = obj.FindProperty("newImage2");
		imageToModify3 = obj.FindProperty("imageToModify3");
		newImage3 = obj.FindProperty("newImage3");
		
		//OnClick
		//onClick1 = obj.FindProperty("onClick1");
		
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
		
		EditorGUILayout.HelpBox("The Content Modifier script is an easy way to modify the content of your panels at runtime. " +
			"This component should be added to a button in order to modify the content of a panel." +
			"It will modify content when the button is clicked." +
			"Be aware that the content will not automatically revert to its default content and that you must override this manually" +
			"by either using additional content modifier scripts or your own code.",MessageType.Info);
		
		
		EditorGUILayout.LabelField("Text Modifications", style1, null);
		
		EditorGUILayout.PropertyField(textToModify1);
		EditorGUILayout.PropertyField(newTextString1);
		EditorGUILayout.PropertyField(textToModify2);
		EditorGUILayout.PropertyField(newTextString2);
		EditorGUILayout.PropertyField(textToModify3);
		EditorGUILayout.PropertyField(newTextString3);
		
		//Images
		EditorGUILayout.LabelField("Image Modifications", style1, null);
		
		EditorGUILayout.PropertyField(imageToModify1);
		EditorGUILayout.PropertyField(newImage1);
		EditorGUILayout.PropertyField(imageToModify2);
		EditorGUILayout.PropertyField(newImage2);
		EditorGUILayout.PropertyField(imageToModify3);
		EditorGUILayout.PropertyField(newImage3);
		
		
		EditorGUILayout.HelpBox("When this button is clicked, it can alter the functionality of up to 3 other buttons! The other buttons can " +
			"be changed to send a message to any other game object when clicked. Each message can also have 1 optional argument." +
			"Easy! ",MessageType.Info);
		
		//OnClick
		
		EditorGUILayout.LabelField("OnClick Button 1 Modifications", style1, null);
		
		EditorGUILayout.PropertyField(serializedObject.FindProperty("onClickSender1"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("onClickMessageReciever1"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("onClickNewMessage1"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("onClickNewArg1"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("replaceExistingEvents1"),true);
		
		EditorGUILayout.LabelField("OnClick Button 2 Modifications", style1, null);
		
		EditorGUILayout.PropertyField(serializedObject.FindProperty("onClickSender2"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("onClickMessageReciever2"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("onClickNewMessage2"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("onClickNewArg2"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("replaceExistingEvents2"),true);
		
		EditorGUILayout.LabelField("OnClick Button 3 Modifications", style1, null);
		
		EditorGUILayout.PropertyField(serializedObject.FindProperty("onClickSender3"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("onClickMessageReciever3"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("onClickNewMessage3"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("onClickNewArg3"),true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("replaceExistingEvents3"),true);
		
		
		
		//EditorGUILayout.PropertyField(onClick1);
		
		EditorGUILayout.LabelField("More coming soon..", style2, null);
		
		//EditorGUILayout.PropertyField(newTextString.GetArrayElementAtIndex( 0 ));
		serializedObject.ApplyModifiedProperties();
		obj.ApplyModifiedProperties(); 
	}
	
	public void OnSceneGUI()
	{
        // Implement what you want to see in scene view here
	}
}