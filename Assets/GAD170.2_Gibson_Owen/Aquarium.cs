using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace OwenGibson
{
    public class Aquarium : MonoBehaviour
    {
        public List<Fish> fishList = new List<Fish>();

        private void Awake()
        {
            Debug.Log(fishList);
        }

    }
}
