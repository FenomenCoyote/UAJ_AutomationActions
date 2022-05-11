using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tests
{
    public class MenuTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void MenuTestsSimplePasses()
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
        public IEnumerator GoToGameScene()
        {
            string currentScene1 = SceneManager.GetActiveScene().name;
            Debug.Log("Escena antes de dar al boton: "+currentScene1);
            GameObject playButton = GameObject.Find("ButtonJugar");
            Assert.IsNotNull(playButton, "Missing button " + playButton.name);

            EventSystem.current.SetSelectedGameObject(playButton);

            playButton.GetComponent<Button>().onClick.Invoke();
          
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.           
            yield return null;
                  
            string currentScene = SceneManager.GetActiveScene().name;
            Debug.Log("Escene despues de dar al boton:"+currentScene);
            Assert.AreEqual("Juego", currentScene);
        }
    }
}
