using UnityEngine;

public class CameraFollowMultiNoiseTerrain : MonoBehaviour
{
    public Transform cameraTransform;      // Transform de la cam�ra
    public Material terrainMaterial;       // Mat�riau utilisant le Shader Graph
    public float terrainSpeed = 0.005f;        // Vitesse de d�placement du terrain
    private float timeOffset = 0f;         // D�calage temporel

    // Variables pour stocker les param�tres du shader
    private float noise1Scale;
    private float noise1Amplitude;
    private float noise2Scale;
    private float noise2Amplitude;
    private float noise3Scale;
    private float noise3Amplitude;

    void Start()
    {
        // R�cup�re les valeurs initiales des param�tres depuis le mat�riau
        noise1Scale = terrainMaterial.GetFloat("_Noise_Scale_1");
        noise1Amplitude = terrainMaterial.GetFloat("_Amplitude_1");
        noise2Scale = terrainMaterial.GetFloat("_Noise_Scale_2");
        noise2Amplitude = terrainMaterial.GetFloat("_Amplitude_2");
        noise3Scale = terrainMaterial.GetFloat("_Noise_Scale_3");
        noise3Amplitude = terrainMaterial.GetFloat("_Amplitude_3");
    }

    void Update()
    {
        // Met � jour le d�calage temporel
        timeOffset += Time.deltaTime * terrainSpeed;

        // R�cup�re les coordonn�es actuelles de la cam�ra
        Vector3 cameraPosition = cameraTransform.position;
        Debug.Log(noise1Amplitude);

        // Calcule la hauteur combin�e des trois bruits � la position actuelle
        float terrainHeight = GetTerrainHeight(cameraPosition.x, cameraPosition.z, timeOffset);

        // Ajuste la position verticale de la cam�ra
        cameraTransform.position = new Vector3(cameraPosition.x, terrainHeight + 18f, cameraPosition.z);
    }

    float GetTerrainHeight(float x, float z, float timeOffset)
    {
        // Calcul des trois noises
        float noise1 = Mathf.PerlinNoise((x + timeOffset) * noise1Scale, (z + timeOffset) * noise1Scale) * noise1Amplitude;
        float noise2 = Mathf.PerlinNoise((x + timeOffset) * noise2Scale, (z + timeOffset) * noise2Scale) * noise2Amplitude;
        float noise3 = Mathf.PerlinNoise((x + timeOffset) * noise3Scale, (z + timeOffset) * noise3Scale) * noise3Amplitude;

        // Cumul des noises
        float totalHeight = noise1 + noise2 + noise3;

        // Debugging
        Debug.Log($"Noise1: {noise1}, Noise2: {noise2}, Noise3: {noise3}, Total Height: {totalHeight}");

        return totalHeight;
    }

}
