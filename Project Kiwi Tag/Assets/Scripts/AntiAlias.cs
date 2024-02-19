using UnityEngine;

public class AntiAlias : MonoBehaviour
{
    [SerializeField] private int antiAliasingLevel = 2;
    [SerializeField] private int textureQualityLevel = 1;

    void Start()
    {
        QualitySettings.antiAliasing = antiAliasingLevel;

        QualitySettings.masterTextureLimit = textureQualityLevel;

        if (transform.rotation.eulerAngles.x > 70 && transform.rotation.eulerAngles.x < 90)
        {
            foreach (var renderer in FindObjectsOfType<Renderer>())
            {
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }
        }
    }
}