using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Text counterText, fpsText;

    private float timeOffset, elapsedTime;
    public float seconds, minutes;
    public int FPS;
    public float refreshFPSDelay = 0.65f, fpsElapsedTime = 0f;
    private bool isVisible, canInteractWithTimer;
    public bool chargement;
    private int nbFrames;


    void Start()
    {
        counterText = GetComponent<Text>() as Text;
        counterText.color = new Color(counterText.color.r, counterText.color.g, counterText.color.b, 0f);
        fpsText.color = new Color(fpsText.color.r, fpsText.color.g, fpsText.color.b, 0f);
        isVisible = false;
        canInteractWithTimer = true;

        timeOffset = Time.time;

        elapsedTime = Time.time - timeOffset; // Commence à 0;

        ResetTimer();
    }

    void Update()
    {
        if(chargement) // Le timer n'est pas incrémenté pendant les chargements
        {
            timeOffset += Time.deltaTime;
        }

        elapsedTime = Time.time - timeOffset;

        minutes = (int)(elapsedTime / 60f);
        seconds = (int)(elapsedTime %  60f);

        counterText.text = minutes.ToString("00") + ":" + seconds.ToString("00"); // Incrémentation du temps 

        fpsElapsedTime += Time.deltaTime;
        nbFrames += 1;

        if (fpsElapsedTime >= refreshFPSDelay)
        {
            float nbImagesEn1Seconde = fpsElapsedTime / nbFrames;  // Calcul de la moyenne des FPS
            FPS = (int)(1 / nbImagesEn1Seconde);
            fpsText.text = FPS.ToString() + " IPS ";
            fpsElapsedTime = 0f;
            nbFrames = 0;
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.F) && canInteractWithTimer)
        {
            StartCoroutine(DelayOnTimerEnabling());

            isVisible = !isVisible;
            if (isVisible)
            {
                counterText.color = new Color(counterText.color.r, counterText.color.g, counterText.color.b, 1f);
                fpsText.color = new Color(fpsText.color.r, fpsText.color.g, fpsText.color.b, 1f);
                canInteractWithTimer = false;
            }
            else if (!isVisible)
            {
                counterText.color = new Color(counterText.color.r, counterText.color.g, counterText.color.b, 0f);
                fpsText.color = new Color(fpsText.color.r, fpsText.color.g, fpsText.color.b, 0f);
                canInteractWithTimer = false;
            }
        }
    }

    public void ResetTimer()
    {
        timeOffset = Time.time;
    }
    IEnumerator DelayOnTimerEnabling()
    {
        yield return new WaitForSeconds(0.2f);
        canInteractWithTimer = true;
    }

    static float Modulo(float a, float b)
    {
        return Mathf.Floor(((a % b) + b) % b);
    }

}
