using System;
using UnityEngine;

[Serializable]
public struct HandProperty
{
    [SerializeField] int m_maxHp;
    [SerializeField] int m_hpRegenPerTick;
    [SerializeField] float m_maxLaunchPower;
    [Range(0.1f, 3f)]
    [SerializeField] float m_regenTimeInterval;
    [Range(0.1f, 3f)]
    [SerializeField] float m_burnTimeInterval;
    public int MaxHp => m_maxHp;
    public int HpRegenPerTick => m_hpRegenPerTick;
    public float MaxLaunchPower => m_maxLaunchPower;
    public float RegenTimeInterval => m_regenTimeInterval;
    public float BurnTimeInterval => m_burnTimeInterval;

    public HandProperty(int maxHp, int hpRegenPerTick, float maxLaunchPower, float regenTimeInterval, float burnTimeInterval)
    {
        m_maxHp = maxHp;
        m_hpRegenPerTick = hpRegenPerTick;
        m_maxLaunchPower = maxLaunchPower;
        m_regenTimeInterval = regenTimeInterval;
        m_burnTimeInterval = burnTimeInterval;
    }
    public HandProperty(HandProperty toCopy)
    {
        m_maxHp = toCopy.m_maxHp;
        m_hpRegenPerTick = toCopy.m_hpRegenPerTick;
        m_maxLaunchPower = toCopy.m_maxLaunchPower;
        m_regenTimeInterval = toCopy.m_regenTimeInterval;
        m_burnTimeInterval = toCopy.m_burnTimeInterval;
    }
}
