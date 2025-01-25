using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    public RectTransform creditsContainer; // Der Container für den Abspann
    public float scrollSpeed = 50f;       // Geschwindigkeit des Scrollens (Pixel pro Sekunde)
    public float endPositionY = 10000f;    // Y-Position, bei der das Scrollen stoppen soll
    public float delayBeforeScroll = 3f;  // Verzögerung vor dem Scrollen (in Sekunden)

    private Vector2 _startPosition;
    private bool _canScroll = false;

    void Start()
    {
        // Startposition speichern
        _startPosition = creditsContainer.anchoredPosition;

        // Starte den Countdown für die Verzögerung
        Invoke(nameof(EnableScrolling), delayBeforeScroll);
    }

    void Update()
    {
        // Wenn Scrollen erlaubt ist, bewege den Container nach oben
        if (_canScroll)
        {
            creditsContainer.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            // Stoppen, wenn die Endposition erreicht ist
            if (creditsContainer.anchoredPosition.y >= endPositionY)
            {
                _canScroll = false;
            }
        }
    }

    // Verzögerung
    private void EnableScrolling()
    {
        _canScroll = true;
    }
}