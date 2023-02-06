using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public enum Scenes
{
    DigitalCardGeneric,
    DigitalCardMichael
}

public class CardVersionMenu : MonoBehaviour
{
    private const string PATH = "Assets/Scenes/";
    private const string SCENE_SUFFIX = ".unity";
    
    public static Scenes selectedVersion;

    // Add a menu item in the Unity menu bar
    // Select Generic Version
    [MenuItem("Card Version/Select Version/Generic")]
    static void SetGenericVersion()
    {
        selectedVersion = Scenes.DigitalCardGeneric;
        OpenScene(selectedVersion);

        Debug.Log("Generic Version has been selected.");
    }

    // Add a menu item in the Unity menu bar
    // Select Michael Version
    [MenuItem("Card Version/Select Version/Michael")]
    static void SetMichaelVersion()
    {
        selectedVersion = Scenes.DigitalCardMichael;
        OpenScene(selectedVersion);

        Debug.Log("Michael Version has been selected.");
    }

    private static void OpenScene(Scenes scene)
    {
        if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(GetScenePath(ConvertEnum(scene)), OpenSceneMode.Single);
        }
    }

    private static string ConvertEnum(Scenes scene)
    {
        switch(scene)
        {
            case Scenes.DigitalCardGeneric:
                return "DigitalCardGeneric";
            case Scenes.DigitalCardMichael:
                return "DigitalCardMichael";
            default:
                return "Error: Scene Not Found";
        }
    }

    private static string GetScenePath(string sceneName)
    {
        string scenePath = PATH + sceneName + SCENE_SUFFIX;

        return scenePath;
    }
}
