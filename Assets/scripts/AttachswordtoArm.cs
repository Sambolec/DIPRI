using UnityEngine;

public class AttachSwordToHand : MonoBehaviour
{
    public GameObject swordPrefab;
    public string handBoneName = "RightHand"; // Try "Hand_R", "mixamorig:RightHand", etc.

    void Start()
    {
        Transform hand = FindDeepChild(transform, handBoneName);
        if (hand != null)
        {
            GameObject sword = Instantiate(swordPrefab, hand);
            sword.transform.localPosition = Vector3.zero;
            sword.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogWarning("Hand bone not found!");
        }
    }

    Transform FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name.Contains(name))
                return child;
            var result = FindDeepChild(child, name);
            if (result != null)
                return result;
        }
        return null;
    }
}
