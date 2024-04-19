using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movimientos B�sicos
    public float velocidadMovimiento = 5f;
    public float velocidadRotacion = 100f;

    public float x, y;

    //Entrada
    public bool awake = false;


    //Animaciones
    private Animator anim;

    //C�maras
    public Camera camBack;
    public Camera camFront;

    //Salto
    public Rigidbody rb;
    public float fuerzaSalto = 8f;
    public bool puedoSaltar;
    public bool tocoSuelo;
    public bool salte;

    //Agachado
    public float velocidadInicial;
    public float velocidadAgachado;

    //Golpeo animaci�n
    public bool estoyAtacando;
    public bool avanzoSolo;
    public float impulsoGolpe;

    // Start is called before the first frame update
    void Start()
    {

        //C�maras
        //camBack.enabled = true;
        //camFront.enabled = false;

        //Salto
        puedoSaltar = true;

        //Agachado
        velocidadInicial = velocidadMovimiento;
        velocidadAgachado = velocidadMovimiento * 0.5f;

        //Golpeo Animaci�n
        estoyAtacando = false;
        avanzoSolo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!awake && Input.GetKeyDown(KeyCode.E))
        {
            awake = true;
            anim.SetBool("Awake", true);
        }
        //Lectura cursores
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        //Animaciones
        //anim.SetFloat("VelX", x);
        //anim.SetFloat("VelY", y);

        //C�maras
        //CambioCamara();

        //Salto
        Saltar();

        //Agachado
        //Agacharse();

        //Golpeo
        //Golpear();

        //Patada
        //Kickear();

    }

    //Para las fisicas
    private void FixedUpdate()
    {
        //Golpeo animaci�n
        if (!estoyAtacando)
        {
            //Movimiento
            transform.Rotate(0, x * Time.fixedDeltaTime * velocidadRotacion, 0);
            transform.Translate(0, 0, y * Time.fixedDeltaTime * velocidadMovimiento);
        }
        if (avanzoSolo)
        {
            rb.velocity = transform.forward * impulsoGolpe;
        }
    }


    private void Saltar()
    {
        if (puedoSaltar)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Animaciones
                //anim.SetBool("Salte", true);

                salte = true;
                rb.AddForce(new Vector3(0, fuerzaSalto, 0), ForceMode.Impulse);
            }

            //Animaciones
            //anim.SetBool("TocoSuelo", true);
            tocoSuelo = true;
        }
        else
        {
            EstoyCayendo();
        }
    }

    private void EstoyCayendo()
    {
        //Animaciones
        //anim.SetBool("TocoSuelo", false);
        //anim.SetBool("Salte", false);
        tocoSuelo = false;
        salte = false;

    }

    private void CambioCamara()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            camBack.enabled = false;
            camFront.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            camBack.enabled = true;
            camFront.enabled = false;
        }
    }

}
