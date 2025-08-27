using System.Collections.Generic;

public abstract class Entity
{
    protected float _healPoint;
    protected int _strength;
    protected int _dexterity;
    protected int _endurance;

    protected List<ISkill> _damageSkills = new();
    protected List<ISkill> _deffSkills = new();
    protected List<IAttribute> _buffs = new();
    protected List<IAttribute> _debuffs = new();

    public void CalculateDamage(DamageData data)
    {

    }

    public void TakeDamage(DamageData data)
    {

    }
}
