using Physics.Water;
using UnityEngine;
using Zenject;

public class WaterPhysicsSystem : MonoBehaviour
{
    private WaveSettings _settings;
    
    private static readonly int Wave1Params = Shader.PropertyToID("_Wave1Params"); // x=len, y=amp, z=speed
    private static readonly int Wave1Dir = Shader.PropertyToID("_Wave1Dir");
    private static readonly int Wave2Params = Shader.PropertyToID("_Wave2Params");
    private static readonly int Wave2Dir = Shader.PropertyToID("_Wave2Dir");

    [Inject]
    public void Construct(WaveSettings settings)
    {
        _settings = settings;
    }

    private void Update()
    {
        Debug.Log($"Updating Shader Globals: Amp1 = {_settings.Amplitude1}");
        UpdateShaderGlobals();
    }

    private void UpdateShaderGlobals()
    {
        if (_settings == null) return;

        Shader.SetGlobalVector(Wave1Params, new Vector4(_settings.Wavelength1, _settings.Amplitude1, _settings.Speed1, 0));
        Shader.SetGlobalVector(Wave1Dir, _settings.Direction1.normalized);
        
        Shader.SetGlobalVector(Wave2Params, new Vector4(_settings.Wavelength2, _settings.Amplitude2, _settings.Speed2, 0));
        Shader.SetGlobalVector(Wave2Dir, _settings.Direction2.normalized);
    }
    
    public float GetWaterHeightAt(Vector3 worldPos)
    {
        float time = Time.time;
        
        float y = 0f;
        y += CalculateGerstnerWave(worldPos, _settings.Wavelength1, _settings.Amplitude1, _settings.Speed1, _settings.Direction1, time);
        y += CalculateGerstnerWave(worldPos, _settings.Wavelength2, _settings.Amplitude2, _settings.Speed2, _settings.Direction2, time);
        
        return y;
    }

    private float CalculateGerstnerWave(Vector3 p, float wavelength, float amp, float speed, Vector2 dir, float time)
    {
        // k = 2 * PI / wavelength
        float k = 2 * Mathf.PI / wavelength;
        // c = speed (phase speed) = sqrt(g / k) - в упрощенной модели просто speed
        float f = k * (Vector2.Dot(dir.normalized, new Vector2(p.x, p.z)) - speed * time);
        
        // Формула Герстнера для Y: A * sin(f)
        // Для более острых волн (Trochoidal) формула сложнее, но начнем с синуса,
        // так как для физики подушки важна высота, а не острый гребень.
        return amp * Mathf.Sin(f);
    }
}