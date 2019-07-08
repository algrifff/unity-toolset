using UnityEngine;
using UnityEditor;

public class ProjectStart : EditorWindow
{
    public string[] folders = {"Scenes", "Scripts", "Meshes", "Textures", "Materials", "Prefabs", "Shaders", "Animation", "Audio"};

    [MenuItem("Window//Alex/Create Folders")]
    static void CreateFolderWindow()
    {
        EditorWindow.GetWindow(typeof(ProjectStart));
    }

    void OnGUI(){
    
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("folders");
 
        EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
        so.ApplyModifiedProperties(); // Remember to apply modified properties

        if(GUILayout.Button("Create Folders")){
            for(int i = 0; i < folders.Length; i++){
                string guid = AssetDatabase.CreateFolder("Assets", folders[i]);
                string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
                
            }
        }
    }
}
