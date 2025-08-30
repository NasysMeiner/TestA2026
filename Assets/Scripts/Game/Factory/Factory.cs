using UnityEngine;

public class Factory : MonoBehaviour
{
    private LocationFactory _locationFactory;
    private EntityFactory _entityFactory;

    public void Init(LocationFactory locationFactory, EntityFactory playerFactory)
    {
        _locationFactory = locationFactory;
        _entityFactory = playerFactory;
    }

    public GeneratedData GenerateEntity(TypeClass typeClass)
    {
        return _entityFactory.CreatePlayer(typeClass);
    }

    public GeneratedData GenerateEntity()
    {
        return _entityFactory.CreateEnemy();
    }

    public GeneratedLocation GenerateLocation(TypeLocation typeLocation)
    {
        return _locationFactory.CreateLocation(typeLocation);
    }
}
