using UnityEngine;
using UnityEngine.UI;

public class spustanje_bascanske : MonoBehaviour
{
    public GameObject igrac;
    public GameObject uiTekst;
    public GameObject objektZaSpustanje;
    public float brzina = 1f;
    public float donjaGranica = 0f;

    private bool igracBlizu = false;

    void Start()
    {
        // Obavezno sakrij UI tekst na početku
        if (uiTekst != null)
        {
            uiTekst.SetActive(false);
        }
    }

    void Update()
    {
        // Provjeri je li igrač postavljen
        if (igrac == null) return;

        // Izračunaj udaljenost
        float udaljenost = Vector3.Distance(transform.position, igrac.transform.position);

        // Ako je igrač unutar 3 jedinice, prikaži tekst
        if (udaljenost < 3f)
        {
            if (!igracBlizu)
            {
                igracBlizu = true;
                if (uiTekst != null)
                {
                    uiTekst.SetActive(true);
                }
            }

            // Provjeri je li pritisnuta tipka E
            if (Input.GetKeyDown(KeyCode.E) && objektZaSpustanje != null)
            {
                // Pokreni korutinu za spuštanje objekta
                StartCoroutine(SpustiObjekt());
            }
        }
        else
        {
            if (igracBlizu)
            {
                igracBlizu = false;
                if (uiTekst != null)
                {
                    uiTekst.SetActive(false);
                }
            }
        }
    }

    System.Collections.IEnumerator SpustiObjekt()
    {
        // Sakrij UI tekst
        if (uiTekst != null)
        {
            uiTekst.SetActive(false);
        }

        // Spuštaj objekt dok ne dođe do donje granice
        while (objektZaSpustanje.transform.position.y > donjaGranica)
        {
            Vector3 pozicija = objektZaSpustanje.transform.position;
            pozicija.y -= brzina * Time.deltaTime;

            // Osiguraj da ne ide ispod granice
            if (pozicija.y < donjaGranica)
            {
                pozicija.y = donjaGranica;
            }

            objektZaSpustanje.transform.position = pozicija;
            yield return null;
        }
    }
}
