using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightDirection : MonoBehaviour
{
    [SerializeField] private Material skyBoxMaterial;

    private void Update()
    {
        skyBoxMaterial.SetVector(name = "_MainLightDirection", transform.forward);
    }
}
