using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(EnemyPatterns))]
[ExecuteInEditMode]
public class EnemyPatternsEditor : Editor
{

    GUIStyle label = new GUIStyle();
    GUIStyle title = new GUIStyle();
    GUIStyle value = new GUIStyle();

    

    public override void OnInspectorGUI() {
        EnemyPatterns script = (EnemyPatterns)target;

        label.richText = true;
        label.alignment = TextAnchor.MiddleLeft;

        title.richText = true;
        title.alignment = TextAnchor.MiddleCenter;

        value.alignment = TextAnchor.MiddleRight;



        base.OnInspectorGUI();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);


        switch (script.mode) {
            #region TitleModes
            case EnemyPatterns.modifier.circle:
                GUILayout.Label("<b><color=RED>Circle Mode Settings</color></b>", title);
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                GUILayout.Space(10);
                break;
            case EnemyPatterns.modifier.player:
                GUILayout.Label("<b><color=RED>Player Mode Settings</color></b>", title);
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                GUILayout.Space(10);
                break;
            case EnemyPatterns.modifier.burst:
                GUILayout.Label("<b><color=RED>Burst Mode Settings</color></b>", title);
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                GUILayout.Space(10);
                break;
            case EnemyPatterns.modifier.oneTime:
                GUILayout.Label("<b><color=RED>Sigle Shot Mode Settings</color></b>", title);
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                GUILayout.Space(10);
                break;
                #endregion
        }

        #region BaseVariables
        if (script.mode != EnemyPatterns.modifier.player) {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("<b>Number of Axes   </b>", label);
                script.number = (int)GUILayout.HorizontalSlider(script.number, 1, 75, GUILayout.Width(150));
                int val1 = (int)script.number;
                string valstring = string.Format("{0:00}", val1);

                GUILayout.Box(valstring, value);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("<b>Bullet Speed           </b>", label);
            script.speed = Mathf.Round(GUILayout.HorizontalSlider(script.speed, 0.0f, 50.0f, GUILayout.Width(150)) * 10f) / 10f;
            int val1 = (int)script.speed % 100;
            float val2 = Mathf.Round((script.speed - val1) * 10);
            string valstring = string.Format("{0:00}.{1}", val1, val2);
            GUILayout.Label(valstring, value);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("<b>Bullet Acceleration   </b>", label);
            script.accelleration = Mathf.Round(GUILayout.HorizontalSlider(script.accelleration, 0.0f, 10.0f, GUILayout.Width(150)) * 10f) / 10f;
            int val1 = (int)script.accelleration % 100;
            float val2 = Mathf.Round((script.accelleration - val1) * 10);
            string valstring = string.Format("{0:00}.{1}", val1, val2);
            GUILayout.Label(valstring, value);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("<b>Wave mode</b>", label);
            script.waveMode = GUILayout.Toggle(script.waveMode, "");

            GUILayout.Label("<b>Wave Speed</b>", label);
            script.waveSpeed = EditorGUILayout.FloatField(script.waveSpeed);
        }
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("<b>Arround mode</b>", label);
            script.arroundMode = GUILayout.Toggle(script.arroundMode, "");
        }
        GUILayout.EndHorizontal();
        #endregion

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        switch (script.mode) {
            case EnemyPatterns.modifier.circle:
                #region CircleMode
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("<b>Rotation Speed   </b>", label);
                    script.rotationSpeed = (int)GUILayout.HorizontalSlider(script.rotationSpeed, -100, 100, GUILayout.Width(150));
                    int val1 = (int)script.rotationSpeed;
                    string valstring = string.Format("{0:00}", val1);

                    GUILayout.Box(valstring, value);

                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal(); {

                    if (GUILayout.Button("Reset Rotation")) {
                        script.rotationSpeed = 0;
                    }
                }
                GUILayout.EndHorizontal();

               GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("<b>Spawn Time          </b>", label);
                    script.spawnTime = Mathf.Round(GUILayout.HorizontalSlider(script.spawnTime, 0.0f, 5.0f, GUILayout.Width(150)) * 10f) / 10f;
                    int val1 = (int)script.spawnTime % 100;
                    float val2 = Mathf.Round((script.spawnTime - val1) * 10);
                    string valstring = string.Format("{0:00}.{1}", val1, val2);
                    GUILayout.Label(valstring, value);
                }
                GUILayout.EndHorizontal();
                #endregion
                break;

            case EnemyPatterns.modifier.player:
                #region PlayerMode
                GUILayout.BeginHorizontal(); {
                    GUILayout.Label("<b><color=RED>Dispersion Angle     </color></b>", label);
                    script.dispersionAngle = Mathf.Round(GUILayout.HorizontalSlider(script.dispersionAngle, 0.0f, 3.0f, GUILayout.Width(150)) * 10f) / 10f;
                    int val1 = (int)script.dispersionAngle % 100;
                    float val2 = Mathf.Round((script.dispersionAngle - val1) * 10);
                    string valstring = string.Format("{0:00}.{1}", val1, val2);
                    GUILayout.Label(valstring, value);
                }
                GUILayout.EndHorizontal();
                #endregion
                break;

            case EnemyPatterns.modifier.burst:
                #region BurstMode
                GUILayout.BeginHorizontal(); {
                    GUILayout.Label("<b>Number by Burst     </b>", label);
                    script.numberByBurst = (int)GUILayout.HorizontalSlider(script.numberByBurst, 1, 10, GUILayout.Width(150));
                    int val1 = (int)script.numberByBurst;
                    string valstring = string.Format("{0:00}", val1);

                    GUILayout.Box(valstring, value);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal(); {
                    GUILayout.Label("<b>Time between bursts </b>", label);
                    script.timeBetweenBursts = Mathf.Round(GUILayout.HorizontalSlider(script.timeBetweenBursts, 0.0f, 3.0f, GUILayout.Width(150)) * 10f) / 10f;
                    int val1 = (int)script.timeBetweenBursts % 100;
                    float val2 = Mathf.Round((script.timeBetweenBursts - val1) * 10);
                    string valstring = string.Format("{0:00}.{1}", val1, val2);
                    GUILayout.Label(valstring, value);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal(); {
                    GUILayout.Label("<b>Time between bullets</b>", label);
                    script.timeBetweenBulletsInBurst = Mathf.Round(GUILayout.HorizontalSlider(script.timeBetweenBulletsInBurst, 0.0f, 3.0f, GUILayout.Width(150)) * 10f) / 10f;
                    int val1 = (int)script.timeBetweenBulletsInBurst % 100;
                    float val2 = Mathf.Round((script.timeBetweenBulletsInBurst - val1) * 10);
                    string valstring = string.Format("{0:00}.{1}", val1, val2);
                    GUILayout.Label(valstring, value);
                }
                GUILayout.EndHorizontal();
                #endregion
                break;
        }

        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();

        if (GUI.changed) {
            EditorUtility.SetDirty(script);
            EditorSceneManager.MarkSceneDirty(script.gameObject.scene);
        }
        

    }
}
