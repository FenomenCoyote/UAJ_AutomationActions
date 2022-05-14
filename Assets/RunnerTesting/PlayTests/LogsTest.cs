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
    /// Este test verifica que cuando el jugador 1 choca con troncos vuelve a la posicion de spawn
    /// </summary>
    public class LogsTest
    {
        private Vector3[] positions; 

        [SetUp]
        public void SetUp()
        {
            //Empezamos en la escena del menu
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Menu.unity", new LoadSceneParameters(LoadSceneMode.Single));

            //Posiciones en las que va a aparecer el jugador para que colisione con troncos
            positions = new[]{ new Vector3(80.8f, 11.3f, 0f),
                               new Vector3(206.4f, 16.6f, 0f),
                               new Vector3(388f, -6, 0f)};                             
        }

        [UnityTest]
        public IEnumerator LogsTestWithEnumeratorPasses()
        {
            //Cragamos la escena del Juego y esperamos unos segundos, empezando en el mapa 1 por defecto
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Juego.unity", new LoadSceneParameters(LoadSceneMode.Single));
            yield return new WaitForSeconds(5.0f);

            //Cargamos el mapa 3 que es el que contiene los troncos y colocamos al jugador 1
            //en el punto de spawn inicial del mapa 3
            GameManager.instance.SetIndiceMapa(3);
            GameManager.instance.CargaMapaEnMundos();
            GameManager.instance.SetPuntoInicial(new Vector3(-14.55f,0.41f), Mundos.mundoJ1);           
            GameManager.instance.ColocaJugadores();
            yield return new WaitForSeconds(5.0f);

            //Cogemos el jugador 1
            GameObject player = null;
            while (player == null)
            {
                yield return new WaitForSeconds(1.0f);
                player = GameObject.Find("Jugador1 ");
            }
            yield return new WaitForSeconds(5.0f);

            //Obtenemos el componente de que nos permite saber la posicion de respawn del jugador
            Respawn respawn = player.GetComponent<Respawn>();
            Assert.IsNotNull(respawn);

            foreach (Vector3 pos in positions)
            {
                player.transform.position = pos;
                //Esperamos 2 segundos a que el jugador caiga
                yield return new WaitForSeconds(6.0f);
                //Comprobamos si ha vuelto a la posición de spawn               
                bool assert = Vector3.Distance(respawn.GetSpawnPosition(), player.transform.position) < 3.0f;
                Assert.IsTrue(assert);
            }
            
            yield return null;
        }
    }
}
