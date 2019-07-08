using UnityEngine;
using UnityEditor;

public class MassObjectSetUp : EditorWindow
{
    int positionShift;
    public PhysicMaterial physicsMaterial;
    public Material material;

    [MenuItem("Window/Alex/Object Set Up")]
    public static void ShowWindow()
    {
        GetWindow<MassObjectSetUp>("Update_Position");
    }
    private void OnGUI()
    {
        GUILayout.Label("Move Selected Objects", EditorStyles.boldLabel);

        positionShift = EditorGUILayout.IntField("Position Shift", positionShift);


        if(GUILayout.Button("Update Position"))
        {
            for(int i =0; i < Selection.gameObjects.Length; i++)
            {
                Selection.gameObjects[i].transform.position = new Vector3(positionShift + i, 0,0);
            }
        }
        if(GUILayout.Button("Add Rigidbody"))
        {
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                Selection.gameObjects[i].AddComponent<Rigidbody>();
            }
        }
        // presentPhysicsMaterial = Dire
        GUILayout.BeginHorizontal();
        physicsMaterial = (PhysicMaterial)EditorGUILayout.ObjectField("Physics Material ", physicsMaterial, typeof(PhysicMaterial), true);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Add Box Collider"))
        {
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                BoxCollider b = Selection.gameObjects[i].AddComponent<BoxCollider>();
                b.material = physicsMaterial;
                
            }
        }
        
        if (GUILayout.Button("Add Trigger"))
        {
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                Selection.gameObjects[i].AddComponent<BoxCollider>();
                BoxCollider b = Selection.gameObjects[i].GetComponent<BoxCollider>();
                Vector3 triggerSize = b.size;
                triggerSize.x = triggerSize.x + .1f;
                triggerSize.y = triggerSize.y + .1f;
                triggerSize.z = triggerSize.z + .1f;
                b.size = triggerSize;
                b.isTrigger = true;
            }
        }

        GUILayout.BeginHorizontal();
        material = (Material)EditorGUILayout.ObjectField("Material ", material, typeof(Material), true);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Apply Material"))
        {
            if(material)
            {
                for (int i = 0; i < Selection.gameObjects.Length; i++)
                {
                    Selection.gameObjects[i].GetComponent<Renderer>().sharedMaterial = material;

                }
            }
            else
            {
                Debug.LogErrorFormat("Need to assign material in 'Material' parameter ");
                return;
            }

        }
        if (GUILayout.Button("Apply Prefab"))
        {
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                var prefab_root = PrefabUtility.FindPrefabRoot(Selection.gameObjects[i]);
                var prefab_src = PrefabUtility.GetCorrespondingObjectFromSource(prefab_root);
                if (prefab_src != null)
                {
                    PrefabUtility.ReplacePrefab(prefab_root, prefab_src, ReplacePrefabOptions.ConnectToPrefab);
                    Debug.Log("Updating prefab : " + AssetDatabase.GetAssetPath(prefab_src));
                }
                else
                {
                    Debug.Log(Selection.gameObjects[i].name + "has no prefab");
                }
            }
        }

    }
}
