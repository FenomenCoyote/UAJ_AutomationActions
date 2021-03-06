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
    /// Este test verifica que cuando el jugador 1 lanza la habilidad de invertir controles, la velocidad del jugador 2 es negativa
    /// y sus teclas de saltar y rodar estan cambiadas
    /// </summary>
    public class InvertControlsTest
    {
        [SetUp]
        public void SetUp()
        {
            //Empezamos en la escena del menu
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Menu.unity", new LoadSceneParameters(LoadSceneMode.Single));
        }
      
        [UnityTest]
        public IEnumerator InvertControlsTestWithEnumeratorPasses()
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

            //Guardamos las teclas de saltar y rodar antes de aplicar la habilidad
            KeyCode jumpKey = contJug2.getTeclaSaltar();
            KeyCode rollKey = contJug2.getTeclaRodar();

            //Aplicamos la habilidad de invertir controles al jugador 2
            player1.GetComponent<PoderesManager>().ActivaInvierteControles();

            yield return null;

            //Guardamos las tecla de saltar y rodar cambiadas
            KeyCode changedJumpKey = contJug2.getTeclaSaltar();
            KeyCode changedRollKey = contJug2.getTeclaRodar();

            //Comprobamos que la velocidad del jugador 2 esta invertida (es negativa)
            Assert.Negative(contJug2.getVelocidadX());
            //Comprobamos que la tecla de saltar es la de rodar
            Assert.AreEqual(changedRollKey, jumpKey);
            //Comprobamos que la tecla de rodar es la de saltar
            Assert.AreEqual(changedJumpKey, rollKey);
        }
    }
}
