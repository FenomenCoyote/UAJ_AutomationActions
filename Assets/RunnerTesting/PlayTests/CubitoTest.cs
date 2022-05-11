using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class CubitoTest
    {
        [SetUp]
        public void SetUp()
        {
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Menu.unity", new LoadSceneParameters(LoadSceneMode.Single));
        }

        // A Test behaves as an ordinary method
        [Test]
        public void CubitoTestSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator CubitoTestWithEnumeratorPasses()
        {
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Juego.unity", new LoadSceneParameters(LoadSceneMode.Single));

            GameObject jugador1 = GameObject.Find("Jugador1 ");
            while (jugador1 == null)
            {
                yield return new WaitForSeconds(1.0f);
                jugador1 = GameObject.Find("Jugador1 ");
            }
            yield return new WaitForSeconds(5.0f);

            GameObject jugador2 = GameObject.Find("Jugador2");
            jugador1.GetComponent<PoderesManager>().AplicarCuboDeHielo();

            yield return null;

            Assert.IsTrue(jugador2.GetComponent<PerdidasControl>().GetEstadoActual() == PerdidaControles.enCubo);
        }
    }
}
