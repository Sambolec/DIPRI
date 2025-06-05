using UnityEngine;
using UnityEngine.InputSystem;

public class Weaponswitcher : MonoBehaviour
{
    public GameObject swordCharacter;
    public GameObject pistolCharacter;

    private GameObject activeCharacter;

    void Start()
    {
        // Start with pistol character active
        swordCharacter.SetActive(false);
        pistolCharacter.SetActive(true);
        pistolCharacter.tag = "Player";
        swordCharacter.tag = "Untagged";
        SetMainCamera(pistolCharacter);

        activeCharacter = pistolCharacter;
    }

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SwitchTo(swordCharacter);
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SwitchTo(pistolCharacter);
        }
    }

    void SwitchTo(GameObject newCharacter)
    {
        if (activeCharacter == newCharacter) return;

        // Move new character to old character's position and rotation
        newCharacter.transform.position = activeCharacter.transform.position;
        newCharacter.transform.rotation = activeCharacter.transform.rotation;

        // Untag the old character and deactivate
        activeCharacter.tag = "Untagged";
        activeCharacter.SetActive(false);

        // Activate and tag the new character
        newCharacter.SetActive(true);
        newCharacter.tag = "Player";

        // Switch camera tag and enable only the new character's camera
        SetMainCamera(newCharacter);

        activeCharacter = newCharacter;
    }

    void SetMainCamera(GameObject character)
    {
        // Untag and disable all cameras in the scene
        Camera[] allCameras = FindObjectsOfType<Camera>();
        foreach (Camera cam in allCameras)
        {
            cam.tag = "Untagged";
            cam.enabled = false;
        }

        // Find and enable the camera under the new character, tag as MainCamera
        Camera characterCamera = character.GetComponentInChildren<Camera>(true);
        if (characterCamera != null)
        {
            characterCamera.tag = "MainCamera";
            characterCamera.enabled = true;
        }
        else
        {
            Debug.LogWarning($"No camera found for {character.name}");
        }
    }
}
