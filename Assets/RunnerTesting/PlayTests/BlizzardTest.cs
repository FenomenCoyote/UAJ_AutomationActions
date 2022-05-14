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
    ///  Este test verifica que cuando el jugador 1 lanza la habilidad de la ventisca, ralentiza al jugador 2, es decir, 
    ///  su velocidad es menor
    /// </summary>
    public class BlizzardTest
    {
        [SetUp]
        public void SetUp()
        {
            //Empezamos en la escena del menu
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Menu.unity", new LoadSceneParameters(LoadSceneMode.Single));
        }

        [UnityTest]
        public IEnumerator BlizzardTestWithEnumeratorPasses()
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

            //Cogemos el controlador del jugador 2
            GameObject player2 = GameObject.Find("Jugador2");
            ControladorJugador contJug2 = player2.GetComponent<ControladorJugador>();

            //Velociad del jugador 2 antes de aplicarle la ventisca
            float speed = contJug2.getVelocidadX();
            Debug.Log("Velcidad antes de aplicar Niebla: " + speed);

            //Aplicamos la habilidad de la ventisca al jugador 2
            player1.GetComponent<PoderesManager>().ActivaNeblina();

            yield return null;

            //Velocidad del jugador 2 tras aplicarle la ventisca
            float speedWithFog = contJug2.getVelocidadX();
            Debug.Log("Velocidad despues de aplicar Niebla: " + speedWithFog);

            //Comprobamos que el prefab de la ventisca existe
            GameObject fog = GameObject.Find("Neblina(Clone)");
            Assert.IsNotNull(fog);

            //Se comprueba que la velocidad del jugador 2 tras aplicarle la ventisca es menor
            //que la que tenia antes
            Assert.IsTrue(speedWithFog < speed);
        }
    }
}
