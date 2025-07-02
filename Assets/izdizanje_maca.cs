using UnityEngine;
using System.Collections;

public class izdizanje_maca : MonoBehaviour
{
    [Header("Player Detection")]
    public GameObject playerChild1; // Povuci prvo dijete playera
    public GameObject playerChild2; // Povuci drugo dijete playera

    [Header("Mac Movement")]
    public GameObject sword;
    public float swordMoveAmount = 0.3f;
    public float swordMoveSpeed = 2f;
    public float bottomY = 0f;

    [Header("Click Settings")]
    public int requiredClicks = 5;
    public bool resetClicksOnExit = true;

    [Header("UI Elements")]
    public GameObject interactionText;
    public GameObject rewardCanvas;
    public float canvasDisplayDuration = 3f;

    [Header("Vrijeme za povratak dolje")]
    public float resetDelay = 1.5f;

    [Header("Vrata")]
    public GameObject doorToOpen;
    public float doorRotationAmount = 90f;
    public float doorRotationSpeed = 2f;

    // Privatne varijable
    private bool playerInRange = false;
    private int currentClicks = 0;
    private bool completed = false;
    private float lastUseTime = 0f;
    private bool isSwordUp = false;
    private float originalY;

    void Start()
    {
        InitializeComponents();
    }

    void InitializeComponents()
    {
        if (interactionText) interactionText.SetActive(false);
        if (rewardCanvas) rewardCanvas.SetActive(false);
        if (sword) originalY = sword.transform.position.y;
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject == playerChild1 || other.gameObject == playerChild2) && !completed)
        {
            playerInRange = true;
            ToggleInteractionText(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerChild1 || other.gameObject == playerChild2)
        {
            playerInRange = false;
            ToggleInteractionText(false);
            if (resetClicksOnExit && !completed) currentClicks = 0;
        }
    }

    void Update()
    {
        HandleInput();
        HandleSwordReset();
    }

    void HandleInput()
    {
        if (playerInRange && !completed && Input.GetButtonDown("Use"))
        {
            currentClicks++;
            lastUseTime = Time.time;
            StartCoroutine(MoveSword(swordMoveAmount));
            isSwordUp = true;

            if (currentClicks >= requiredClicks)
            {
                completed = true;
                ToggleUIElements();
                StartCoroutine(HideCanvasOpenDoorAndHideSword()); // UMJESTO stare korutine
            }
        }
    }

    void HandleSwordReset()
    {
        if (isSwordUp && (Time.time - lastUseTime) > resetDelay && !completed)
        {
            StartCoroutine(MoveSwordToY(bottomY));
            isSwordUp = false;
        }
    }

    void ToggleInteractionText(bool state)
    {
        if (interactionText) interactionText.SetActive(state);
    }

    void ToggleUIElements()
    {
        ToggleInteractionText(false);
        if (rewardCanvas) rewardCanvas.SetActive(true);
        // NE gasimo mač ovdje!
    }

    // NOVA KORUTINA: prvo sakrije canvas, otvori vrata, TEK ONDA gasi mač
    IEnumerator HideCanvasOpenDoorAndHideSword()
    {
        yield return new WaitForSeconds(canvasDisplayDuration);
        if (rewardCanvas) rewardCanvas.SetActive(false);
        if (doorToOpen) yield return StartCoroutine(RotateDoor());
        if (sword) sword.SetActive(false); // Mač nestaje TEK SAD, kad je sve gotovo
    }

    IEnumerator MoveSword(float amount)
    {
        Vector3 startPos = sword.transform.position;
        Vector3 targetPos = startPos + Vector3.up * amount;
        float elapsedTime = 0f;
        float duration = 1f / swordMoveSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            sword.transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
            yield return null;
        }
        sword.transform.position = targetPos;
    }

    IEnumerator MoveSwordToY(float targetY)
    {
        Vector3 startPos = sword.transform.position;
        Vector3 targetPos = new Vector3(startPos.x, targetY, startPos.z);
        float elapsedTime = 0f;
        float duration = 1f / swordMoveSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            sword.transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
            yield return null;
        }
        sword.transform.position = targetPos;
    }

    IEnumerator RotateDoor()
    {
        Quaternion startRot = doorToOpen.transform.rotation;
        Quaternion targetRot = startRot * Quaternion.Euler(0, 0, doorRotationAmount);
        float elapsedTime = 0f;
        float duration = 1f / doorRotationSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            doorToOpen.transform.rotation = Quaternion.Lerp(startRot, targetRot, elapsedTime / duration);
            yield return null;
        }

        doorToOpen.transform.rotation = targetRot;
    }
}
