using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
   public int coins = 0;

   //public TMP_Text coinText;  DELETADO


   public float moveSpeed;
   public float maxVelocity;
   public float rayDistance;
   
   public LayerMask groundLayer;
   public float jumpForce; 
   
   private GameControles _gameControles;
   private PlayerInput _playerInput;
   private Camera _mainCamera;
   private Rigidbody _rigidbody;
   
   private Vector2 _moveInput;

   private bool _isGrounded;

   private void OnEnable()
   {
      //Inicialização
      _gameControles = new GameControles();

      //Referencia dos componetes no mesmo objeto da unity  
      _playerInput = GetComponent<PlayerInput>();
      _rigidbody = GetComponent<Rigidbody>();

      //Referencia para a camera main guardada na classe camera
      _mainCamera = Camera.main;

      //Atribuindo ao delegate do actin triggerd no player input
      _playerInput.onActionTriggered += OnActionTriggerd;
   }

   private void OnDisable()
   {
      _playerInput.onActionTriggered -= OnActionTriggerd;
   }

   private void OnActionTriggerd(InputAction.CallbackContext obj)
   {
      //Comparando o nome do Action que esta chegando com o nome da action de mover
      if (obj.action.name.CompareTo(_gameControles.Gameplay.Movement.name) == 0)
      {
         _moveInput = obj.ReadValue<Vector2>();
      }

      if (obj.action.name.CompareTo(_gameControles.Gameplay.Jump.name) == 0)
      {
         if (obj.performed) Jump(); 
      }
   }


   private void Mover()
   {
      
             //pegar o vetor q aponta na direção em q a camera esta olhando e zeramos o componente Y
             Vector3 camForward = _mainCamera.transform.forward;
             camForward.y = 0;
             //Calcular o movimento no eixo da camera para o movimento frente/tras
             Vector3 moveVertical = camForward * _moveInput.y;
             
            // pega o vetor q aponta para o lado direito da camera zeramos o componente Y
            Vector3 camRight = _mainCamera.transform.right;
            camRight.y = 0;
            //Calcular o movimento no eixo da camera para o movimento esuqrdo/direito
            Vector3 moveHorizontal = _mainCamera.transform.right * _moveInput.x;
            
            //Adiciona a força mo eixo no objeto atraves do rigidody, com intensidade definida por moveSpeed
            _rigidbody.AddForce((moveVertical + moveHorizontal)* moveSpeed * Time.fixedDeltaTime);
         }

   private void FixedUpdate()
   {
      Mover();
      LimiteVeloty();
      
   }
   
   private void LimiteVeloty()
   {
      //Pegar a veloc. do player
      Vector3 velocity = _rigidbody.velocity;
      
      //checar se a veloc. esta dentro dos limites nos direfente eixos
      
       //limitando o eixo x usando ifs, abs e sing
       if (Math.Abs(velocity.x) > maxVelocity) velocity.x = Math.Sign(velocity.x) * maxVelocity ;

       //-maxVelocity < velocity.z < maxVeloty
       velocity.z = Mathf.Clamp(velocity.z, min: -maxVelocity, maxVelocity);
       //alterar a veloci. do player para ficar dentro dos limites
       _rigidbody.velocity = velocity;
   }
   
   /* como fz o jogador pular
    * 1- Checar se o jogador esta no chão
    * --a Checar a colição a parti da fisica (usando os eventos de colição)
    * -- Vantagem: Facil de implementar(add uma função q ja existe na unity - OnCollisionEnter)
    * -- Desvantagem: Não sabemos a hr exata q a unity vai chamar essa função (pode ser q o jogador toque no chão e demore alguns frames pra o jogo saber q ele esta no chão.
    * --b Atraves de raycast: o---| bolinha vai atirar um raio, o raio vai bater em algun obj e a gente recebe o resultado dessa colição
    * --podemos usar Layers para definir quais obj q o raycats deve checar colisao
    * -- Vantagem: Resposta da colisao é imediata
    * -- Desvantagem: Um pouco mais de trabalho de configurar
    * -- uma variavel bool q vai dizer para resto do codic se o jogador estara no chao (true) ou não (false)
    * 2- jogador precisa aperta o botão de pulo
    * -- Precisa configuar o botão a ser utilizado para a ação de pular no nosso Input
    * -- Na função OnActionTriggered precisaremos  comparar se a ação recebida tem o mesmo nome da ação de pulo
    * -- Precisanos dizer em ql momento do botão ser apertado queremos execultar o pulo (Started, canceled, performe)
    * 3-realizar o pulo atraves da fisica
    * -- Vamos criar uma funçao q vai realizar o pulo
    * -- se o personagem estiver no chão, iremos plicar uma cima com uma certa magnitude
    */

   private void Jump()
   {
      if (_isGrounded)
      {
         _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
      }
      
   }
   private void CheckGround()
   {
      _isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance, groundLayer);
      
   }

   private void Update()
   {
      CheckGround();
   }

   private void OnDrawGizmos()
   {
      Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.yellow);
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Coin"))
      {
         coins++;
         PlayerObserverManager.PlayerCoinsChanged(coins);
         
         //coinText.text = coins.ToString(); DELETADO
         
         Destroy((other.gameObject));
      }
   }
}

