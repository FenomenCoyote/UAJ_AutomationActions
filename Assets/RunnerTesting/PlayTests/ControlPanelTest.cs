﻿using System.Collections;
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
    /// Este test se encarga verificar de que al darle al boton de ajustes se active el menu de configuracion
    /// y de que seguimos en la escena de Menu
    /// </summary>
    public class ControlPanelTest
    {       
        [SetUp]
        public void SetUp()
        {
            //Empezamos en la escena del Menu
            EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/Menu.unity", new LoadSceneParameters(LoadSceneMode.Single));
        }

        [UnityTest]
        public IEnumerator ControlPanelTestWithEnumeratorPasses()
        {
            //Ejecutamos el evento del boton de configuracion
            GameObject configButton = GameObject.Find("ButtonConfig");         
            EventSystem.current.SetSelectedGameObject(configButton);
            configButton.GetComponent<Button>().onClick.Invoke();

            //Esperamos por 1 seg para que termine el fade de la animacion
            yield return new WaitForSeconds(1.0f);

            //Verificamos que seguimos en la escena del Menu
            string currentScene = SceneManager.GetActiveScene().name;
            Assert.AreEqual("Menu", currentScene);
            Debug.Log("Seguimos en la escena Menu");

            //Verifcamos que el menu de configuracion esta activo
            GameObject controlPanel = GameObject.Find("PanelConfig");
            Assert.IsNotNull(controlPanel);
            Debug.Log("El panel de controles esta activo");
        }
    }
}
