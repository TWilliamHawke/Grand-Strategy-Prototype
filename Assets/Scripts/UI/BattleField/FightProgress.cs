using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battlefield
{

    public class FightProgress : MonoBehaviour
    {
        [SerializeField] Image _progressBar;
        CameraController _camera;

        void Awake()
        {
            _camera = FindObjectOfType<CameraController>();
        }

        void Update()
        {
            transform.rotation = _camera.transform.rotation;
        }

        public void UpdateProgress(float progress)
        {
            _progressBar.fillAmount = progress;
        }
    }

}