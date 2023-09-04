using System;
using UnityEngine;

[Serializable]
public class ItemProperty 
{
    [SerializeField] string m_name = default;
    [SerializeField] int m_startingHeat = default;
    [SerializeField] int m_burnDamagePerTick = default;
    // Set to 
    int m_currentHeat;
    public string Name => m_name;
    public int StartingHeat => m_startingHeat;
    public int CurrentHeat { get => m_currentHeat; set => m_currentHeat = value; }
    public int BurnDamagePerTick { get => m_burnDamagePerTick; set => m_burnDamagePerTick = value; }

    public ItemProperty(string name, int startHeat, int burnDamage)
    {
        m_name = name;
        m_startingHeat = startHeat;
        CurrentHeat = startHeat;
        BurnDamagePerTick = burnDamage;
    }
    public ItemProperty(string name, int startHeat, int currentHeat, int burnDamage)
    {
        m_name = name;
        m_startingHeat = startHeat;
        CurrentHeat = currentHeat;
        BurnDamagePerTick = burnDamage;
    }
    public ItemProperty(ItemProperty toCopy)
    {
        m_name = toCopy.Name;
        m_startingHeat = toCopy.StartingHeat;
        CurrentHeat = toCopy.CurrentHeat;
        BurnDamagePerTick = toCopy.BurnDamagePerTick;
    }
}
