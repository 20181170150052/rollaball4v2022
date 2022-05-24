using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerController : MonoBehaviour
{
   public float moveSpeed; 
   
   private GameControles _gameControles;
   private PlayerInput _playerInput;
   private Camera _mainCamera;
   private Rigidbody _rigidbody;
   
   private Vector2 _moveInput;

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
         
         _moveInput = obj.ReadValue<Vector2>();
   }


   private void Mover()
         {
            //Calcular o movimento no eixo da camera para o movimento frente/tras
            Vector3 moveVertical = _mainCamera.transform.forward * _moveInput.y;
            
            //Calcular o movimento no eixo da camera para o movimento esuqrdo/direito
            Vector3 moveHorizontal = _mainCamera.transform.right * _moveInput.x;
            
            //Adiciona a força mo eixo no objeto atraves do rigidody, com intensidade definida por moveSpeed
            _rigidbody.AddForce((moveVertical + moveHorizontal)* moveSpeed * Time.fixedDeltaTime);
         }

   private void FixedUpdate()
   {
      Mover();
   }
}
