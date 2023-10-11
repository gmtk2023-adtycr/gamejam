using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class LightConeCollider : MonoBehaviour
{

    [Min(1)]
    public int steps = 10;
    public AudioClip trap_talkie_sfx;
    public AudioClip player_in_zone_sfx;
    public AudioSource zone_AudioSource;
    [Range(0.0f,3.0f)] public float volume=1.0f;
    public AudioSource AudioSource;
    private bool hasPlayedSound = false;
    public event Action<GameObject> OnDetectPlayer;
    private Light2D light2DComponent;
      private Color originalColor; 

    void Start()
    {
        // Get the Light2D component attached to the same GameObject
        light2DComponent = GetComponent<Light2D>();

        // Store the original color of the Light2D component
        originalColor = light2DComponent.color;
    }
    private void PlaySound()
    {
        if (trap_talkie_sfx != null && !AudioSource.isPlaying)
        {
            AudioSource.PlayOneShot(trap_talkie_sfx);}
    }

        private void PlaySoundDetected()
    {
        if (trap_talkie_sfx != null && !AudioSource.isPlaying)
        {
            AudioSource.PlayOneShot(player_in_zone_sfx, volume);}
    }

        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasPlayedSound)
        {
            PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
            OnDetectPlayer?.Invoke(other.gameObject);
            PlaySound();
            hasPlayedSound = true;
        }
        else{
            hasPlayedSound = false;
        }
    }



    void FixedUpdate()
    {
        var start = transform.parent.position;
        bool playerDetected = false;
        foreach (var ray in GetVectors().ToList())
        {
            RaycastHit2D hit = Physics2D.Raycast(start, ray - start, Vector3.Distance(start, ray), LayerMask.GetMask("Player"));
            
            if (hit.collider != null && !playerDetected)
                {
                OnDetectPlayer?.Invoke(hit.collider.gameObject);
                PlaySoundDetected();
                playerDetected=true;
                light2DComponent.color = Color.red;
                break;
                }
            else{            light2DComponent.color = originalColor;}
        }
    }

    private IEnumerable<Vector3> GetVectors(){

        var light2D = GetComponent<Light2D>();

        Vector3 start = transform.parent.position;
        float base_rotation = (transform.parent.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad;
        float dist = light2D.pointLightOuterRadius;
        float diff_angle = light2D.pointLightOuterAngle * Mathf.Deg2Rad / steps;
        
        for (int i = -steps/2; i < steps/2; i++){
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
