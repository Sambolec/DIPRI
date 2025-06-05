using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class DualPistolFire : MonoBehaviour
{
    private Animator animator;
    private InputSystem_Actions inputActions;

    public float clickFireTime = 0.15f; // Fire rate, npr. 0.15 sekundi za burst
    public Transform MuzzlePointL;
    public Transform MuzzlePointR;
    public float fireRange = 100f;
    public GameObject muzzleFlashPrefab; 
    public float damage = 25f;

    public LayerMask whatToHit;

    // --- Novo za burst ---
    private bool isShooting = false;
    private Coroutine shootingCoroutine;

    void Awake()
    {
        animator = GetComponent<Animator>();
        inputActions = new InputSystem_Actions();

        inputActions.Player.LeftGun.performed += ctx => OnFirePressed();
        inputActions.Player.LeftGun.canceled += ctx => OnFireReleased();
    }

    void OnEnable() { inputActions.Enable(); }
    void OnDisable() { inputActions.Disable(); }

    private void OnFirePressed()
    {
        isShooting = true;
        if (shootingCoroutine == null)
            shootingCoroutine = StartCoroutine(BurstFire());
    }

    private void OnFireReleased()
    {
        isShooting = false;
        animator.SetBool("isFiring", false);
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }

    private IEnumerator BurstFire()
    {
        while (isShooting)
        {
            animator.SetBool("isFiring", true);
            FireFromMuzzle(MuzzlePointL);
            FireFromMuzzle(MuzzlePointR);
            yield return new WaitForSeconds(clickFireTime); // pauza između burstova
        }
        animator.SetBool("isFiring", false);
    }

    private void FireFromMuzzle(Transform muzzle)
    {
        if (muzzle == null) return;

        // Ako želiš muzzle flash, otkomentiraj:
        //if (muzzleFlashPrefab != null)
        //    Instantiate(muzzleFlashPrefab, muzzle.position, muzzle.rotation, muzzle);

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, fireRange, whatToHit))
        {
            Debug.Log($"🎯 Pogođen: {hit.collider.name}");

            var enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log($"Enemyu {hit.collider.name} skinut {damage} HP!");
            }
        }
    }
}
