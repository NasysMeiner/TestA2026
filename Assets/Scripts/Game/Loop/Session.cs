public class Session
{
    public GeneratedData Player { get; private set; }
    public GeneratedData Enemy { get; private set; }

    public Session(GeneratedData player)
    {
        Player = player;
    }

    public void SetDataEnemy(GeneratedData enemy)
    {
        Enemy = enemy;
    }
}
