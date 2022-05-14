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
    /// <summary>
    /// Este test verifica que desde la escena Juego vamos a la escena Menu, usando el menu de pausa
    /// </summary>
    public class GoToMenuFromGameTest
    {
        [SetUp]
        public void SetUp()
        {
            //Empezamos en la escena Menu
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Menu.unity", new LoadSceneParameters(LoadSceneMode.Single));
        }

        [UnityTest]
        public IEnumerator GoToMenuFromGameTestWithEnumeratorPasses()
        {
            //Cargamos la escena Juego, empezando en el mapa 1 por defecto y esperamos un frame
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Juego.unity", new LoadSceneParameters(LoadSceneMode.Single));
            yield return null;

            //Abrimos el menu de pausa
            GameObject canvas = GameObject.Find("CanvasUI");
            Assert.IsNotNull(canvas);
            Debug.Log("Encontrado Canvas");
            UIManager ui = canvas.GetComponent<UIManager>();
            Assert.IsNotNull(ui);
            ui.AbreMenuIngame(Player.jugador1);
            Debug.Log("Encontrado ui manager");

            //Nos aseguramos que el menu de pausa esta tras abrirlo
            GameObject config = GameObject.Find("MenuConfiguracion");
            Assert.IsNotNull(config);
            Debug.Log("Encontrado menu config");

            string currentScene1 = SceneManager.GetActiveScene().name;
            Debug.Log("Escena antes de dar al boton: " + currentScene1);

            //Ejecutamos el evento que tenga asignado el boton de volver al Menu
            GameObject menuButton = GameObject.Find("BotonSalir");
            Assert.IsNotNull(menuButton);
            EventSystem.current.SetSelectedGameObject(menuButton);
            menuButton.GetComponent<Button>().onClick.Invoke();

            yield return null;

            //Comprobamos que la escena a la que vamos es la de Menu
            string currentScene = SceneManager.GetActiveScene().name;
            Debug.Log("Escena despues de dar al boton: " + currentScene);
            Assert.AreEqual("Menu", currentScene);
        }
    }
}
