using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingHUD : MonoBehaviour
{
    public Transform enemyTargeting;
    public RectTransform enemyHealth;
    Transform enemyTarget;

    public void Awake()
    {
        MonsterStats.HealthChange.AddListener(HealthChange);
    }

    public void ActivateTargeting(bool activateTargeting, Transform newTarget)
    {
        enemyTarget = newTarget;
        HealthChange();

        if (activateTargeting)
            enemyTargeting.gameObject.SetActive(true);
        else
            enemyTargeting.gameObject.SetActive(false);
    }
    public void TrackTarget(Vector3 newPosition)
    {
        enemyTargeting.position = newPosition;
    }

    void HealthChange()
    {
        if (enemyTarget != null)
            AdjustHealth(enemyTarget.GetComponent<MonsterStats>().GetHealthPercentage());
    }
    public void AdjustHealth(float newPercentage)
    {
        enemyHealth.sizeDelta = new Vector2(newPercentage, 1);
    }
}
