using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class LightConeCollider : MonoBehaviour
{
    [Min(1)] public int steps = 10;
    public AudioClip trap_talkie_sfx;
    public AudioClip player_in_zone_sfx;
    public AudioSource zone_AudioSource;
    
    [Range(0.0f, 3.0f)] public float volume = 1.0f;
    [Range(0.0f, 3.0f)] public float volume2 = 0.3f;
    public AudioSource AudioSource;
    private bool hasPlayedSound = false;

    private bool playerHere = false;
    public event Action<GameObject> OnDetectPlayer;
    public event Action OnPlayerExit;
    
    private Light2D light2DComponent;
    private Color originalColor;
    
    float flickerSpeed = 5f; // Speed of the flickering effect
    float flickerIntensity = 0.2f; // Intensity of the flickering effect

    private float originalSpeed;

    void Start(){
        // Get the Light2D component attached to the same GameObject
        light2DComponent = GetComponent<Light2D>();
        // Store the original color of the Light2D component
        originalColor = light2DComponent.color;
    }

    private void Update(){
        // Apply flickering effect to the light's intensity using a sine wave
        float flickerValue = Mathf.Sin(Time.time * flickerSpeed) * flickerIntensity;
        light2DComponent.intensity = 1f + flickerValue; // Set the intensity of the light with flickering effect
    }

    public void PlaySound(){
        if (trap_talkie_sfx != null && !AudioSource.isPlaying){
            AudioSource.PlayOneShot(trap_talkie_sfx);
        }
    }


    public void PlaySoundDetected(){
        if (trap_talkie_sfx != null && !zone_AudioSource.isPlaying){
            zone_AudioSource.PlayOneShot(player_in_zone_sfx, volume2);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player") && !hasPlayedSound){
            PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
            OnDetectPlayer?.Invoke(other.gameObject);
            PlaySound();
            hasPlayedSound = true;
        }
        else{
            hasPlayedSound = false;
        }
    }


    void FixedUpdate(){
        var start = transform.parent.position;
        bool newPlayerHere = false;
        foreach (var ray in GetVectors().ToList()){
            RaycastHit2D hit = Physics2D.Raycast(start, ray - start, Vector3.Distance(start, ray),
                LayerMask.GetMask("Player"));
            
            if (hit.collider != null){
                newPlayerHere = true;
                OnDetectPlayer?.Invoke(hit.collider.gameObject);
                PlaySoundDetected();
                break;
            }

        }

        if (!newPlayerHere && playerHere){
            OnPlayerExit?.Invoke();
        }

        playerHere = newPlayerHere;
        light2DComponent.color = playerHere ? Color.red : originalColor;
    }

    private IEnumerable<Vector3> GetVectors(){
        var light2D = GetComponent<Light2D>();

        Vector3 start = transform.parent.position;
        float base_rotation = (transform.parent.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad;
        float dist = light2D.pointLightOuterRadius;
        float diff_angle = light2D.pointLightOuterAngle * Mathf.Deg2Rad / steps;

        for (int i = -steps / 2; i < steps / 2; i++){
            float x = start.x + dist * MathF.Cos(base_rotation + diff_angle * i);
            float y = start.y + dist * MathF.Sin(base_rotation + diff_angle * i);
            var end = new Vector3(x, y, start.z);
            RaycastHit2D hit = Physics2D.Raycast(start, end - start, dist, LayerMask.GetMask("Collision"));
            yield return (hit.collider == null ? end : hit.point);
        }
    }

    private void OnDrawGizmosSelected(){
        foreach (var v in GetVectors()){
            Debug.DrawLine(transform.parent.position, v, Color.red);
        }
    }
}