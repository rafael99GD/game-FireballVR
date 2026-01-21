using UnityEngine;
using UnityEditor;

public class AmbientAudioAssigner : EditorWindow
{
    public Transform targetFolder;    
    public AudioClip ambientClip;     

    [MenuItem("Tools/Ambient Audio Assigner")]
    public static void ShowWindow()
    {
        GetWindow<AmbientAudioAssigner>("Ambient Audio Assigner");
    }

    private void OnGUI()
    {
        GUILayout.Label("Assign Ambient Looping Audio", EditorStyles.boldLabel);

        targetFolder = (Transform)EditorGUILayout.ObjectField(
            "Target Folder", targetFolder, typeof(Transform), true);

        ambientClip = (AudioClip)EditorGUILayout.ObjectField(
            "Ambient Sound", ambientClip, typeof(AudioClip), false);

        if (GUILayout.Button("Apply AudioSource To Children"))
        {
            ApplyAudioToFolder();
        }
    }

    private void ApplyAudioToFolder()
    {
        if (targetFolder == null)
        {
            Debug.LogError("Please select a target folder (a Transform).");
            return;
        }

        if (ambientClip == null)
        {
            Debug.LogError("Please assign an AudioClip.");
            return;
        }

        int count = 0;

        foreach (Transform child in targetFolder)
        {
            AudioSource src = child.GetComponent<AudioSource>();
            if (src == null)
                src = child.gameObject.AddComponent<AudioSource>();

            src.clip = ambientClip;
            src.loop = true;
            src.playOnAwake = true;

            src.volume = 0.2f;
            src.priority = 128;
            src.pitch = 1f;
            src.spatialBlend = 1f; 

            src.dopplerLevel = 0f;
            src.spread = 0f;
            src.rolloffMode = AudioRolloffMode.Logarithmic;
            src.minDistance = 1f;
            src.maxDistance = 3f;

            count++;
        }

        Debug.Log($"Added/Updated AudioSource on {count} objects inside '{targetFolder.name}'.");
    }
}
