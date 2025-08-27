using System.Collections.Generic;
using UnityEngine;

public class CompositionOrder : MonoBehaviour
{
    [SerializeField] private List<CompositeRoot> _composites;

    private void Awake()
    {
        foreach (CompositeRoot root in _composites)
            root.Compose();
    }
}