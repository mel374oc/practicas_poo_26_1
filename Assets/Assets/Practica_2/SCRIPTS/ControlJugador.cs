using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    //Movimiento
    public float velocidad = 5f; // velocidad de movimiento del jugador 
    public float gravedad = -9.8f; // fuerza aplicada a la gravedad del jugador 
    private CharacterController controller; // permite el movimiento en el juego
    private Vector3 velocidadVertical; // permite saber que tan rapido caemos 

    //Variables vista
    public Transform camara; // registra que camara funciona como los ojos de jugador 
    public float sensibilidadMouse = 200f; // que tan rapido gira el mouse
    private float rotacionXVertical = 0f; //indica cuantos grados hacia arriba o hacia abajo se puede voltear

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); // busca el componente CharacterContoller 
        Cursor.lockState = CursorLockMode.Locked; // bloquea el puntero del mouse en los limites de la pantalla
    }

    // Update is called once per frame
    void Update()
    {
        ManejadorVista();
        ManejadorMovimiento();
    }
    void ManejadorVista()
    {
        // lee el imput del mouse
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        // construye la rotacion horizontal
        transform.Rotate(Vector3.up * mouseX);

        // registro de la rotación vertical
        rotacionXVertical -= mouseY;

        // limita la rotacion vertical
        Mathf.Clamp(rotacionXVertical, -90f, 90f);

        // aplica la rotación           //EJES          X          Y  Z
        camara.localRotation = Quaternion.Euler(rotacionXVertical, 0, 0);
    }
    void ManejadorMovimiento()
    {
        // lee el imput de movimeinto (flechas de dirección o wasd)
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // crea el vector de movimiento
        Vector3 direccion = transform.right * inputX + transform.forward * inputZ;

        // Mueve al charactarController
        controller.Move(direccion * velocidad * Time.deltaTime);

        //aplica gravedad
        if (controller.isGrounded && velocidadVertical.y < 0)
        {
            velocidadVertical.y = -2f;
        }

        velocidadVertical.y += gravedad*Time.deltaTime; //aplicamos la acelercaiopn a la gravedad
        controller.Move(velocidadVertical * Time.deltaTime);   //movemos el controlador hacia abajo 
    }
}
