using UnityEngine;

public class kamera : MonoBehaviour
{
    public Transform igrac;          // referenca na igrača
    public Vector3 offset = new Vector3(0, 5, -10); // udaljenost kamere od igrača

    void Start()
    {
        if (igrac == null)
        {
            GameObject igracObj = GameObject.Find("Igrac");
            if (igracObj != null)
            {
                igrac = igracObj.transform;
            }
            else
            {
                Debug.LogError("Objekt 'Igrac' nije pronađen!");
            }
        }
    }

    void LateUpdate()
    {
        if (igrac != null)
        {
            transform.position = igrac.position + offset;
            transform.LookAt(igrac); // da kamera uvijek gleda prema igraču
        }
    }
}