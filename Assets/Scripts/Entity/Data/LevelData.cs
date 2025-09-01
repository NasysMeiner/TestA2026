public class LevelData
{
    public int CurrentLevel { get; private set; }
    public int WarriorLevel { get; private set; }
    public int BararianLevel { get; private set; }
    public int RobberLevel { get; private set; }

    public LevelData()
    {
        CurrentLevel = 0;

        WarriorLevel = 0;
        BararianLevel = 0;
        RobberLevel = 0;
    }

    public void LevelUp(TypeClass typeClass)
    {
        switch (typeClass)
        {
            case TypeClass.Warrior:
                WarriorLevel++;
                break;
            case TypeClass.Barbarian:
                BararianLevel++;
                break;
            case TypeClass.Robber:
                RobberLevel++;
                break;
        }

        CurrentLevel++;
    }
}
