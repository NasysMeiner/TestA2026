using System;
using System.Collections.Generic;

[Serializable]
public struct PlayerParameters
{
    public TypeClass TypeClass;
    public EntityUi PrefabPlayer;
    public int HealthPointPerLevel;
    public TypeWeapon TypeWeapon;
    public List<SkillArrayParameters> Skills;
}
