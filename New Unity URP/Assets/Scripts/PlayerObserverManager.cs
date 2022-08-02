using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esse seria o nosso YouTube com coisas do jogador.
public static class PlayerObserverManager
{
    //Essa aqui vai ser o nosso canal para atualização da qaurtidade de coins do jogador.
    public static Action<int> OnPlayerCoinsChanged;
    
    
    // A segunda parte é cpomo o player notificar seus inscritos que as moedas mudaram.
    public static void PlayerCoinsChanged(int value)
    {
        OnPlayerCoinsChanged?.Invoke(value);
    }
    
    
    
    //Essa aqui vai ser o nosso canal para atualização da qaurtidade de coletaveis do jogador.
    public static Action<int> OnPlayerColetarChanged;
    
    
    // A segunda parte é cpomo o player notificar seus inscritos que as estrelas mudaram.
    public static void PlayerColetarChanged(int value)
    {
        OnPlayerColetarChanged?.Invoke(value);
    }
}
