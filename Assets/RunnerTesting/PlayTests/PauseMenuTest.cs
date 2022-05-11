using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class PauseMenuTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void PauseMenuTestSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        [SetUp]
        public void SetUp()
        {
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Menu.unity", new LoadSceneParameters(LoadSceneMode.Single));
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PauseMenuTestWithEnumeratorPasses()
        {
           
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Juego.unity", new LoadSceneParameters(LoadSceneMode.Single));
            yield return null;

            GameObject canvas = GameObject.Find("CanvasUI");
            Debug.Log("Encontrado Canvas");
            UIManager ui = canvas.GetComponent<UIManager>();
            Assert.IsNotNull(ui);
            ui.AbreMenuIngame(Player.jugador1);
            Debug.Log("Encontrado ui manager");

            GameObject config = GameObject.Find("MenuConfiguracion");
            Assert.IsNotNull(config);
            Debug.Log("Encontrado menu config");
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
