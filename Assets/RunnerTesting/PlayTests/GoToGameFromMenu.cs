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
    /// <summary>
    /// Este test se encarga de verificar que desde la escena Menu vamos a la escena Juego al darle al
    /// boton de jugar
    /// </summary>
    public class GoToGameFromMenu
    {       
        [SetUp]
        public void SetUp()
        {
            //Para hacer el test necesitamos empezar en la escena del Menu
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Menu.unity", new LoadSceneParameters(LoadSceneMode.Single));
        }

        [UnityTest]
        public IEnumerator GoToGameScene()
        {            
            string currentScene1 = SceneManager.GetActiveScene().name;
            Debug.Log("Escena antes de dar al boton: "+currentScene1);

            //Cogemos el boton del jugar de la escena Menu
            GameObject playButton = GameObject.Find("ButtonJugar");
            Assert.IsNotNull(playButton, "Missing button " + playButton.name);

            //Ejecutamos el evento que tenga asignado
            EventSystem.current.SetSelectedGameObject(playButton);
            playButton.GetComponent<Button>().onClick.Invoke();
                    
            yield return null;
            
            //Comprobamos que la escena a la que vamos es la de Juego
            string currentScene = SceneManager.GetActiveScene().name;
            Debug.Log("Escena despues de dar al boton: "+currentScene);
            Assert.AreEqual("Juego", currentScene);
        }
    }
}
