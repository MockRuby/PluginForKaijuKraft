using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomWindow : EditorWindow
{
   
    // List to store Kaiju GameObjects
    public List<GameObject> kaijus = new List<GameObject>();
    private Vector2 scrollPosition;
    GameObject focusedKaiju;
    string searchFilter = ""; 

    // Create a menu item to open the KaijuEditor window
    [MenuItem("Tool/KaijuEditor")]
    public static void Create()
    {
        CustomWindow win =
            EditorWindow.GetWindow(typeof(CustomWindow)) as
                CustomWindow;
    }

    // Method to draw the GUI elements of the window
    private void OnGUI()
    {
        // Button to find Kaiju GameObjects and populate the list
        if (GUILayout.Button("Find Kaiju"))
        {
            kaijus.Clear(); // Clear the list before adding new Kaijus
            foreach (GameObject kaiju in GameObject.FindGameObjectsWithTag("Kaiju"))
            {
                kaijus.Add(kaiju);
            }
        }

        // Button to clear the Kaiju list
        if (GUILayout.Button("Clear Kaijus"))
        {
            kaijus.Clear();
        }

        // Display Kaijus List label
        GUILayout.Label("Kaijus List", EditorStyles.boldLabel);
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        // Scroll view for the Kaiju list
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.MaxHeight(200));
        
        // Search filter text field
        searchFilter = EditorGUILayout.TextField("Search", searchFilter);
        foreach (GameObject kaiju in kaijus)
        {
            // Display Kaiju GameObjects that match the search filter
            if (kaiju.name.ToLower().Contains(searchFilter.ToLower()))
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(kaiju, typeof(GameObject), true, GUILayout.Width(64), GUILayout.Height(64));
                GUILayout.Label(kaiju.name); // Display the name of the GameObject next to the picture
                if (GUILayout.Button("Focus", GUILayout.Width(50)))
                {
                    focusedKaiju = kaiju;
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndScrollView();

        // Display messages based on the Kaiju list status
        if (kaijus.Count == 0)
        {
            GUILayout.Label("No Kaijus found. Click 'Find Kaiju' to search.");
        }
        else
        {
            GUILayout.Label("Kaijus found successfully!");
        }

        // Button to add a new Kaiju GameObject
        if (GUILayout.Button("Add Kaiju"))
        {
            GameObject kaiju = new GameObject("kaiju");
            kaiju.tag = "Kaiju";
            focusedKaiju = kaiju;
            kaijus.Add(kaiju);
            kaiju.AddComponent<KaijuStats>();
        }

        // Display focused Kaiju stats for editing
        if (focusedKaiju != null)
        {
            KaijuStats stats = focusedKaiju.GetComponent<KaijuStats>();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField(focusedKaiju, typeof(GameObject), true, GUILayout.Width(64), GUILayout.Height(64));
            GUILayout.Label(focusedKaiju.name); // Display the name of the GameObject next to the picture
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("Edit Kaiju Stats", EditorStyles.boldLabel);
            focusedKaiju.name = EditorGUILayout.TextField("Name", focusedKaiju.name);
            stats.health = EditorGUILayout.IntField("Health", stats.health);
            stats.attack = EditorGUILayout.IntField("Attack", stats.attack);
            stats.defence = EditorGUILayout.IntField("Defence", stats.defence);
            stats.speed = EditorGUILayout.IntField("Speed", stats.speed);
            stats.stageOfLife = (KaijuStats.StagesOfLife)EditorGUILayout.EnumPopup("Stage Of Life", stats.stageOfLife);
            stats.egg = (GameObject)EditorGUILayout.ObjectField("Egg Object", stats.egg, typeof(GameObject), true);
            stats.juv = (GameObject)EditorGUILayout.ObjectField("Juvenile Object", stats.juv, typeof(GameObject), true);
            stats.adult = (GameObject)EditorGUILayout.ObjectField("Adult Object", stats.adult, typeof(GameObject), true);
        }

    }
}
