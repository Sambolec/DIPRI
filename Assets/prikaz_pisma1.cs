﻿using UnityEngine;
using UnityEngine.UI;

public class prikaz_pisma1 : MonoBehaviour
{
    public GameObject playerChild1;           // Povuci prvo dijete playera
    public GameObject playerChild2;           // Povuci drugo dijete playera
    public GameObject interactionText;        // Povuci UI tekst iz Canvasa
    public GameObject interactionImage;       // Povuci UI sliku iz Canvasa

    private bool playerInRange = false;
    private bool imageShown = false;

    void Start()
    {
        // Sakrij i tekst i sliku na početku
        if (interactionText != null) interactionText.SetActive(false);
        if (interactionImage != null) interactionImage.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerChild1 || other.gameObject == playerChild2)
        {
            playerInRange = true;
            // Prikaži samo tekst kad igrač uđe u trigger
            if (interactionText != null) interactionText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerChild1 || other.gameObject == playerChild2)
        {
            playerInRange = false;
            // Sakrij sve kad igrač izađe iz trigera
            if (interactionText != null) interactionText.SetActive(false);
            if (interactionImage != null) interactionImage.SetActive(false);
            imageShown = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetButtonDown("Use"))
        {
            if (!imageShown)
            {
                // Prvi klik: sakrij tekst, prikaži sliku
                if (interactionText != null) interactionText.SetActive(false);
                if (interactionImage != null) interactionImage.SetActive(true);
                imageShown = true;
            }
            else
            {
                // Drugi klik: sakrij sliku
                if (interactionImage != null) interactionImage.SetActive(false);
                imageShown = false;
            }
        }
    }
}
