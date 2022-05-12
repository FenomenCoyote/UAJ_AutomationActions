using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    /// <summary>
    /// Este test verifica que cuando el jugador 1 lanza la habilidad del cubo de hielo, el estado del jugador 2 
    /// este con el del cubo de hielo 
    /// </summary>
    public class CubitoTest
    {
        [SetUp]
        public void SetUp()
        {
            //Empezamos en la escena del menu
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Menu.unity", new LoadSceneParameters(LoadSceneMode.Single));
        }

        [UnityTest]
        public IEnumerator CubitoTestWithEnumeratorPasses()
        {
            //Cargamos la escena de juego, empezando en el mapa 1 por defecto
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Juego.unity", new LoadSceneParameters(LoadSceneMode.Single));

            //Cogemos el jugador 1
            GameObject player1 = null;
            while (player1 == null)
            {
                yield return new WaitForSeconds(1.0f);
                player1 = GameObject.Find("Jugador1 ");
            }
            yield return new WaitForSeconds(5.0f);

            //Aplicamos la habilidad del cubo de hielo al jugador 2
            GameObject player2 = GameObject.Find("Jugador2");
            player1.GetComponent<PoderesManager>().AplicarCuboDeHielo();

            yield return null;

            //Comprobamos que el estado del juagdor 2 sea el del cubo de hielo
            Assert.IsTrue(player2.GetComponent<PerdidasControl>().GetEstadoActual() == PerdidaControles.enCubo);
        }
    }
}
