﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is intended to be inherited by every Scene_Manager class I use (not to be confused with SceneManger)
 * It includes some standard scene manager items, like access the Constants class. 
 * Scene_Manager's that inherit from this should be called SM_SceneName
 */

namespace WytriamSTD
{

    public class Scene_Manager : MonoBehaviour
    {
        

        // Use this for initialization
        void Start()
        {
            Debug.Log("Scene_Manager::Start() - You are using the generic Scene_Manager. Please create a SM_SceneName that inherits from this class instead and override the Start() method.");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}