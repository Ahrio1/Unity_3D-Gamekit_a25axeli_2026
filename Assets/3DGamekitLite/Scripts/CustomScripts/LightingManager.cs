using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    public static LightingManager Instance { get; private set; }

    [Header("Scene References")]
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;

    [Header("Time Settings")]
    [SerializeField, Range(0f, 24f)]
    private float TimeOfDay = 12f;

    [SerializeField] private float speed = 1f;

    public float CurrentTime => TimeOfDay;
    

    private void Awake()
    {
        if (!Application.isPlaying)
            return;

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            if (Instance == null)
                Instance = this;
        }
    }

    private void OnDisable()
    {
        if (Application.isPlaying && Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Instance = null;
        }
    }

    private void OnDestroy()
    {
        if (Application.isPlaying && Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Instance = null;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the new scene's directional light.
        DirectionalLight = RenderSettings.sun;

        if (DirectionalLight == null)
        {
            Light[] lights = FindObjectsByType<Light>(
                FindObjectsSortMode.None);

            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    break;
                }
            }
        }

        // Apply the current time to the newly loaded scene.
        UpdateLighting(TimeOfDay / 24f);
    }

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            TimeOfDay = Mathf.Repeat(
                TimeOfDay + Time.deltaTime * speed, 24f);
        }

        UpdateLighting(TimeOfDay / 24f);
    }

    private void UpdateLighting(float timePercent)
    {
        if (Preset == null)
            return;

        RenderSettings.ambientLight =
            Preset.AmbientColor.Evaluate(timePercent);

        RenderSettings.fogColor =
            Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color =
                Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.rotation =
                Quaternion.Euler(
                    timePercent * 360f - 90f,
                    170f,
                    0f);
        }
    }

    private void OnValidate()
    {
        if (Application.isPlaying || DirectionalLight != null)
            return;

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
            return;
        }

        Light[] lights = FindObjectsByType<Light>(
            FindObjectsSortMode.None);

        foreach (Light light in lights)
        {
            if (light.type == LightType.Directional)
            {
                DirectionalLight = light;
                break;
            }
        }
    }
}