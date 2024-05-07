using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movimientos Básicos
    public float velocidadMovimiento = 5f;
    public float velocidadRotacion = 100f;

    public float x, y;

    //Salto
    public Rigidbody rb;
    public float fuerzaSalto = 8f;
    public bool puedoSaltar;
    public bool tocoSuelo;
    public bool salte;

    public float velocidadInicial;
    public float sprint = 1;



    // Start is called before the first frame update
    void Start()
    {
        //Salto
        puedoSaltar = true;

        //Agachado
        velocidadInicial = velocidadMovimiento;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameLogic.playing)
        {

            //Lectura cursores
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                sprint = 1.5f;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                sprint = 1f;
            }

            //Salto
            Saltar();

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Destroy(other.gameObject);
            GameObject.Find("GameManager").GetComponent<GameLogic>().StartGame();
        }
    }

    //Para las fisicas
    private void FixedUpdate()
    {
        if (GameLogic.playing)
        {
            //Movimiento
            transform.Rotate(0, x * Time.fixedDeltaTime * velocidadRotacion, 0);
            transform.Translate(0, 0, y * Time.fixedDeltaTime * velocidadMovimiento * sprint);
        }
    }


    private void Saltar()
    {
        if (puedoSaltar)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                salte = true;
                rb.AddForce(new Vector3(0, fuerzaSalto, 0), ForceMode.Impulse);
            }


            tocoSuelo = true;
        }
        else
        {
            EstoyCayendo();
        }
    }

    private void EstoyCayendo()
    {
        tocoSuelo = false;
        salte = false;

    }


}
