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
    /// Este test verifica que en la escena de Juego el menu de pausa se abra y que este activo
    /// </summary>
    public class PauseMenuTest
    {
        [SetUp]
        public void SetUp()
        {
            //Empezamos en la escena del menu
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Menu.unity", new LoadSceneParameters(LoadSceneMode.Single));
        }

        [UnityTest]
        public IEnumerator PauseMenuTestWithEnumeratorPasses()
        {
            //Cargamos la escena de juego, empezando en el mapa 1 por defecto
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Juego.unity", new LoadSceneParameters(LoadSceneMode.Single));
            yield return null;

            //Abrimos el menu de pausa
            GameObject canvas = GameObject.Find("CanvasUI");
            Debug.Log("Encontrado Canvas");
            UIManager ui = canvas.GetComponent<UIManager>();
            Assert.IsNotNull(ui);
            ui.AbreMenuIngame(Player.jugador1);
            Debug.Log("Encontrado ui manager");

            //Comprobamos que el menu de configuracion esta activo
            GameObject config = GameObject.Find("MenuConfiguracion");
            Assert.IsNotNull(config);
            Debug.Log("Encontrado menu config");

            yield return null;
        }
    }
}
