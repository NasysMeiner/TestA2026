using System.Collections.Generic;

public class DamageData
{
    public bool IsMiss;
    public bool IsDamageNotDodgeable;
    public int CountMoves;
    public Entity Attacker;
    public Entity Target;
    public int Damage;
    public TypeDamage TypeDamage;
    public int DamageWeapon;
    public bool IsDead;

    public TypeSkill UseMainSkill;
    public Attribute DebuffTarget;

    public List<KeyValuePair<TypeAttribute, int>> damageAttribute = new();

    public int FinalDamage
    {
        get
        {
            int final = Damage + DamageWeapon;

            foreach(KeyValuePair<TypeAttribute, int> pair in damageAttribute)
                final += pair.Value;

            return final;
        }
    }

    public void AddDamageType(TypeAttribute typeDamage, int value)
    {
        damageAttribute.Add(new KeyValuePair<TypeAttribute, int>(typeDamage, value));
    }
}
