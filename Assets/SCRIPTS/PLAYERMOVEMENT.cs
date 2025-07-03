using UnityEngine;

    public class PlayerMovement : MonoBehaviour
    {

        public float speed = 0f;
        bool estaCorriendo;
        public float velocidadCorrer = 5f;
        private float velocidadNormal = 1f;

        //Saltar
        public float gravity = -9.81f;
        public float boxRadio = 0.3f;
        public float jump = 3f;
        public LayerMask groundMask;
        private bool isgrounded;
        public Transform groundCheck;

        public CharacterController controller;
        Vector3 velocity;

        void Update()
        {

            isgrounded = Physics.CheckSphere(groundCheck.position, boxRadio, groundMask);
            if (isgrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }


            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime * velocidadNormal);

            JumpCheck();
            RunCheck();

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

        }
        public void JumpCheck()
        {
            if (Input.GetKeyDown(KeyCode.Space) && isgrounded)
            {
                velocity.y = Mathf.Sqrt(jump * -2 * gravity);
            }
        }
        public void RunCheck()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                estaCorriendo = !estaCorriendo; //Si es true = lo apaga||, ||Si es false = lo prende
            }
            if (estaCorriendo == true)
            {
                velocidadNormal = velocidadCorrer;
            }
            else
            {
                velocidadNormal = 1f;
            }
        }
        void OnDrawGizmos()
        {
            // Establecer el color del Gizmo
            Gizmos.color = Color.red;
            // Dibujar una esfera de alambre en la posición y radio especificados
            Gizmos.DrawWireSphere(transform.position, boxRadio);
        }
    }


