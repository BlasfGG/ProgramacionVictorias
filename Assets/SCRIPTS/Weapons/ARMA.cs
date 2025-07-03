
using System.Collections;
using UnityEngine;

public class Arma : MonoBehaviour
{
    [SerializeField] float velocidadBala;
    [SerializeField] GameObject balaPrefab;
    [SerializeField] Transform puntoTiro;
    [SerializeField] float tiempoEntreDisparos = 0.5f;
    [SerializeField] private AudioSource disparo;


    private bool puedeDisparar = true;

    private void Start()
    {
    }

    void Update()
    {
        AccionarArma();
    }

    public void AccionarArma()
    {
        if (JalaGatillo() && puedeDisparar)
        {
            StartCoroutine(DispararConRetraso());
            disparo.Play();
        }
    }

    bool JalaGatillo()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }

    private IEnumerator DispararConRetraso()
    {
        puedeDisparar = false;
        Disparar();
        yield return new WaitForSeconds(tiempoEntreDisparos);
        puedeDisparar = true;
    }

    public void Disparar()
    {
        GameObject clone = Instantiate(balaPrefab, puntoTiro.position, puntoTiro.rotation);
        Rigidbody rb = clone.GetComponent<Rigidbody>();
        rb.AddForce(puntoTiro.forward * velocidadBala, ForceMode.Impulse);
        Destroy(clone, 3);
    }

}


