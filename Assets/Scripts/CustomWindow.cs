using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomWindow : EditorWindow
{
    public List<GameObject> kaijus = new List<GameObject>();
    private Vector2 scrollPosition;
    GameObject focusedKaiju;
    string searchFilter = ""; 

    [MenuItem("Tool/KaijuEditor")]
    public static void Create()
    {
        CustomWindow win =
            EditorWindow.GetWindow(typeof(CustomWindow)) as
                CustomWindow;
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Find Kaiju"))
        {
            kaijus.Clear(); // Clear the list before adding new Kaijus
            foreach (GameObject kaiju in GameObject.FindGameObjectsWithTag("Kaiju"))
            {
                kaijus.Add(kaiju);
            }
        }

        if (GUILayout.Button("Clear Kaijus"))
        {
            kaijus.Clear();
        }

        GUILayout.Label("Kaijus List", EditorStyles.boldLabel);
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.MaxHeight(200));
        
        searchFilter = EditorGUILayout.TextField("Search", searchFilter);
        foreach (GameObject kaiju in kaijus)
        {
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
        if (kaijus.Count == 0)
        {
            GUILayout.Label("No Kaijus found. Click 'Find Kaiju' to search.");
        }
        else
        {
            GUILayout.Label("Kaijus found successfully!");
        }

        if (GUILayout.Button("Add Kaiju"))
        {
            GameObject kaiju = new GameObject("kaiju");
            kaiju.tag = "Kaiju";
            focusedKaiju = kaiju;
            kaijus.Add(kaiju);
            kaiju.AddComponent<KaijuStats>();
        }

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
