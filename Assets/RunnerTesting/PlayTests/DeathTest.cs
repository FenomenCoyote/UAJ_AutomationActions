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
    /// Este test verifica que cuando el jugador 1 choca con la dead zone (se cae al vacio) vuelve a la 
    /// posicion de spawn
    /// </summary>
    public class DeathTest
    {
        private Vector3[] positions;

        [SetUp]
        public void SetUp()
        {
            //Empezamos en la escena del menu
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Menu.unity", new LoadSceneParameters(LoadSceneMode.Single));

            //Posiciones en las que va a aparecer el jugador para que choque con la dead zone
            positions = new []{new Vector3(-442.55f, -24.43f, 0f), 
                               new Vector3(-433.9f, -24.9f, -0f),
                               new Vector3(-388f, -13f, 0f),
                               new Vector3(-229f, -6.8f, -0f)};
        }

        [UnityTest]
        public IEnumerator DeathTestWithEnumeratorPasses()
        {
            //Cargamos la escena de juego, empezando en el mapa 1 por defecto
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Juego.unity", new LoadSceneParameters(LoadSceneMode.Single));

            //Cogemos el jugador 1
            GameObject player = null;
            while(player == null)
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
                //Comprobaoms si ha vuelto a la posición de spawn               
                bool assert = Vector3.Distance(respawn.GetSpawnPosition(), player.transform.position) < 3.0f;
                Assert.IsTrue(assert);
            }

            yield return null;
        }
    }
}
