using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class RatsTest
    {
        private Vector3[] positions;

        // A Test behaves as an ordinary method
        [Test]
        public void RatsTestSimplePasses() //-5.5 3.3  //111 13 //233.4 11.3
        {
            // Use the Assert class to test conditions
        }
        [SetUp]
        public void SetUp()
        {
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Menu.unity", new LoadSceneParameters(LoadSceneMode.Single));
            positions = new[]{ new Vector3(-5.5f, 3.3f, 0f),
                               new Vector3(111f, 13f, 0f),
                               new Vector3(233.4f, 11.3f, 0f)};

        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator RatsTestWithEnumeratorPasses()
        {
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Juego.unity", new LoadSceneParameters(LoadSceneMode.Single));
            yield return new WaitForSeconds(5.0f);

            GameManager.instance.SetIndiceMapa(3);
            GameManager.instance.CargaMapaEnMundos();

            yield return new WaitForSeconds(5.0f);

            //Esperamos a la carga del mapa
            GameObject player = null;
            while (player == null)
            {
                yield return new WaitForSeconds(1.0f);
                player = GameObject.Find("Jugador1 ");
            }
            yield return new WaitForSeconds(5.0f);

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

            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
