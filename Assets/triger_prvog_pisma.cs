using UnityEngine;
using UnityEngine.UI;

public class PismoInterakcija : MonoBehaviour
{
    public GameObject tekstPrompt;      // UI tekst koji pokazuje "Pritisni USE"
    public GameObject slikaPisma;       // UI slika pisma
    public string useButtonName = "Use"; // Ime input buttona u Input Manageru
    public GameObject igrac;            // Referenca na igrača (za kontrolu kretanja)

    private bool uTriggerZoni = false;  // Prati je li igrač u zoni triggera
    private bool slikaAktivna = false;  // Prati je li slika trenutno prikazana

    void Start()
    {
        // Osiguraj da su tekst i slika isključeni na početku
        if (tekstPrompt != null)
            tekstPrompt.SetActive(false);

        if (slikaPisma != null)
            slikaPisma.SetActive(false);
    }

    void Update()
    {
        // Provjeri je li igrač u trigger zoni i je li pritisnuo USE button
        if (uTriggerZoni && Input.GetButtonDown(useButtonName))
        {
            // Ako slika nije aktivna, prikaži je i zaustavi igrača
            if (!slikaAktivna)
            {
                PrikaziSliku();
            }
            // Ako je slika već aktivna, sakrij je i omogući kretanje
            else
            {
                SakrijSliku();
            }
        }
    }

    void PrikaziSliku()
    {
        slikaPisma.SetActive(true);
        slikaAktivna = true;

        // Onemogući kretanje igrača dok čita pismo
        if (igrac != null)
        {
            // Ako koristiš CharacterController
            CharacterController controller = igrac.GetComponent<CharacterController>();
            if (controller != null)
                controller.enabled = false;

            // Ako koristiš Rigidbody
            Rigidbody rb = igrac.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true;
        }
    }

    void SakrijSliku()
    {
        slikaPisma.SetActive(false);
        slikaAktivna = false;

        // Ponovno omogući kretanje igrača
        if (igrac != null)
        {
            // Ako koristiš CharacterController
            CharacterController controller = igrac.GetComponent<CharacterController>();
            if (controller != null)
                controller.enabled = true;

            // Ako koristiš Rigidbody
            Rigidbody rb = igrac.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = false;
        }
    }

    // Kad igrač uđe u trigger zonu
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uTriggerZoni = true;
            tekstPrompt.SetActive(true);
        }
    }

    // Kad igrač izađe iz trigger zone
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uTriggerZoni = false;
            tekstPrompt.SetActive(false);

            // Ako je slika aktivna kad igrač izađe iz zone, sakrij je
            if (slikaAktivna)
            {
                SakrijSliku();
            }
        }
    }
}
