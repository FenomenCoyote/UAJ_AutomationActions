using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class ControlPanelTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ControlPanelTestSimplePasses()
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
        public IEnumerator ControlPanelTestWithEnumeratorPasses()
        {
            GameObject configButton = GameObject.Find("ButtonConfig");
         
            EventSystem.current.SetSelectedGameObject(configButton);

            configButton.GetComponent<Button>().onClick.Invoke();

            //Esperamos por 1 seg para que termine el fade de la animacion
            yield return new WaitForSeconds(1.0f);

            string currentScene = SceneManager.GetActiveScene().name;

            Assert.AreEqual("Menu", currentScene);
            Debug.Log("Seguimos en la escena Menu");
            GameObject controlPanel = GameObject.Find("PanelConfig");
            Assert.IsNotNull(controlPanel);
            Debug.Log("El panel de controles esta activo");
        }
    }
}
