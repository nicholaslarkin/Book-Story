using UnityEngine;

public class AdjustLight : MonoBehaviour
{
    [SerializeField] private float minLightIntensity;
    [SerializeField] private float maxLightIntensity;

    private Light spotLight;

    void Start()
    {
        spotLight = GetComponent<Light>();
    }

    void Update()
    {
        ChangeLight();
    }

    private void ChangeLight()
    {
        int RandomNumber = Random.Range(0, 25);

        if (RandomNumber != 3)
            return;

        spotLight.intensity = Random.Range(minLightIntensity, maxLightIntensity);
    }
}
