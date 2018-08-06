using UnityEditor;
using UnityEngine;
using System.IO;

namespace GridMap
{
    public class SquareGridMapEditorWindow : EditorWindow
    {
        private Map map;
        private TextAsset mapAsset;
        private int columns;
        private int rows;

        private readonly string[] tabsText = new string[] { "Fields", "Buttons" };
        private int selectedTabIndex;

        private string filePath;

        private Vector2 scrollPosition;

        private int current;

        [MenuItem("Window/GridMap/Edit Grid Map")]
        public static void OpenWindow()
        {
            EditorWindow.GetWindow<SquareGridMapEditorWindow>();
        }

        void OnGUI()
        {
            mapAsset = (TextAsset)EditorGUILayout.ObjectField(mapAsset, typeof(TextAsset), false);

            rows = EditorGUILayout.IntField("Rows", rows);
            columns = EditorGUILayout.IntField("Columns", columns);
            current = EditorGUILayout.IntField("Current", current);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Create", GUILayout.MaxWidth(60)))
            {
                map = new Map(columns, rows);
                for (int i = 0; i < map.OneDimensionalMap.Length; i++)
                {
                    map.OneDimensionalMap[i] = 1;
                }
            }

            if (GUILayout.Button("Load", GUILayout.MaxWidth(60)))
            {
                if (mapAsset != null)
                {
                    map = JsonUtility.FromJson<Map>(mapAsset.text);
                }
            }

            if (GUILayout.Button("Save As", GUILayout.MaxWidth(60)))
            {
                string directory = mapAsset != null ? AssetDatabase.GetAssetPath(mapAsset) : Directory.GetCurrentDirectory();
                filePath = EditorUtility.SaveFilePanel("Save", directory, null, "json");
                save(filePath, map);
            }

            if (GUILayout.Button("Save", GUILayout.MaxWidth(60)))
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    string directory = mapAsset != null ? AssetDatabase.GetAssetPath(mapAsset) : Directory.GetCurrentDirectory();
                    filePath = EditorUtility.SaveFilePanel("Save", directory, null, "json");
                }
                save(filePath, map);
            }

            EditorGUILayout.EndHorizontal();

            selectedTabIndex = GUILayout.Toolbar(selectedTabIndex, tabsText);

            if (map != null)
            {
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                for (int i = map.Rows - 1; i >= 0; i--)
                {
                    EditorGUILayout.BeginHorizontal();
                    for (int j = 0; j < map.Columns; j++)
                    {
                        var previousBackgroundColor = GUI.backgroundColor;

                        switch (map[j, i])
                        {
                            case 0:
                                GUI.backgroundColor = Color.black;
                                break;
                            case 4:
                                GUI.backgroundColor = Color.blue;
                                break;
                        }
                        switch (selectedTabIndex)
                        {
                            case 0:
                                map[j, i] = EditorGUILayout.IntField(map[j, i], GUILayout.MaxWidth(20));
                                break;
                            case 1:
                                if (GUILayout.Button(map[j, i].ToString(), GUILayout.MaxWidth(20)))
                                {
                                    map[j, i] = current;
                                }
                                break;
                        }
                        GUI.backgroundColor = previousBackgroundColor;
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndScrollView();
            }
        }

        private static void save(string filePath, Map map)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                string jsonData = JsonUtility.ToJson(map);
                File.WriteAllText(filePath, jsonData);
                AssetDatabase.Refresh();
            }
        }
    }
}
