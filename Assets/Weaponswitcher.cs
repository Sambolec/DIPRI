using UnityEngine;
using UnityEngine.InputSystem;

public class Weaponswitcher : MonoBehaviour
{
    public GameObject swordCharacter;
    public GameObject pistolCharacter;

    private GameObject activeCharacter;

    void Start()
    {
        swordCharacter.SetActive(false);
        pistolCharacter.SetActive(true);
        activeCharacter = pistolCharacter;
        
        // Ensure only gun camera is tagged MainCamera at start
        SetMainCamera(pistolCharacter);
    }

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SwitchTo(swordCharacter);
            SetMainCamera(swordCharacter);
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SwitchTo(pistolCharacter);
            SetMainCamera(pistolCharacter);
        }
    }

    void SwitchTo(GameObject newCharacter)
    {
        if (activeCharacter == newCharacter) return;

        newCharacter.transform.position = activeCharacter.transform.position;
        newCharacter.transform.rotation = activeCharacter.transform.rotation;

        activeCharacter.SetActive(false);
        newCharacter.SetActive(true);
        activeCharacter = newCharacter;
    }

    void SetMainCamera(GameObject character)
    {
        // First, untag ALL cameras in the scene
        Camera[] allCameras = FindObjectsOfType<Camera>();
        foreach (Camera cam in allCameras)
        {
            cam.tag = "Untagged";
            cam.enabled = false;
        }

        // Then, find and enable only the active character's camera
        Camera characterCamera = character.GetComponentInChildren<Camera>();
        if (characterCamera != null)
        {
            characterCamera.tag = "MainCamera";
            characterCamera.enabled = true;
            Debug.Log($"Set {characterCamera.name} as MainCamera");
        }
        else
        {
            Debug.LogError($"No camera found for {character.name}!");
        }
    }
}
