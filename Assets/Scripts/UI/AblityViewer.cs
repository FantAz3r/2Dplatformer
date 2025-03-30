using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AblityViewer : MonoBehaviour
{
    [SerializeField] private VampireAbility _vampireAbility;

    private Image _cooldownImage;
    private Color _activeColor = Color.green;
    private Color _cooldownColor = Color.gray;

    private void Awake()
    {
        _cooldownImage = GetComponent<Image>();
        _cooldownImage.fillAmount = 0;
    }

    private void OnEnable()
    {
        _vampireAbility.AbilityEnabled += DrowActiveTime;
        _vampireAbility.CooldownStarted += DrowCooldown;
    }

    private void OnDisable()
    {
        _vampireAbility.AbilityEnabled -= DrowActiveTime;
        _vampireAbility.CooldownStarted -= DrowCooldown;
    }

    public void DrowActiveTime(float activeTime)
    {
        StartCoroutine(CooldownCoroutine(activeTime, _activeColor));
    }

    private void DrowCooldown(float cooldown)
    {
        StopCoroutine(CooldownCoroutine(cooldown, _activeColor));
        StartCoroutine(CooldownCoroutine(cooldown, _cooldownColor));
    }

    private IEnumerator CooldownCoroutine(float _timer, Color color)
    {
        float elapsed = 0f;

        _cooldownImage.gameObject.SetActive(true);
        _cooldownImage.color = color;

        while (elapsed < _timer)
        {
            elapsed += Time.deltaTime; 
            _cooldownImage.fillAmount = 1 - (elapsed / _timer); 
            yield return null; 
        }

        _cooldownImage.fillAmount = 0; 
    }
}
