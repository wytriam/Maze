﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WytriamSTD
{

    public class DisableOnStart : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}