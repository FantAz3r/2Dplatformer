using UnityEngine;
using UnityEngine.UI;

public class AblityViewer : MonoBehaviour
{
    [SerializeField] private VampireAbility _vampireAbility;

    private Image _cooldownImage;

    private void Awake()
    {
        _cooldownImage = GetComponent<Image>();
        _cooldownImage.fillAmount = 0;
    }

    private void OnEnable()
    {
       _vampireAbility.TimePassed += DrowTime;
    }

    private void OnDisable()
    {
        _vampireAbility.TimePassed -= DrowTime;
    }

    public void DrowTime(float startTime, float currentTime)
    {
        _cooldownImage.fillAmount = 1-(currentTime/ startTime);
    }
}
