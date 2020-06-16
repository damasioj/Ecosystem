﻿using System;
using System.Collections.Generic;

// TODO : could be refactored with generics ...
public class HouseStructure : BaseStructure
{
    private ResourceCollection<WoodResource> woodResources; 
    private ResourceCollection<StoneResource> stoneResources;

    public int woodRequired;
    public int stoneRequired;

    public override void Reset()
    {
        woodResources = new ResourceCollection<WoodResource>();
        stoneResources = new ResourceCollection<StoneResource>();
    }

    public override bool IsComplete
    {
        get
        {
            return woodResources.Count >= woodRequired
                && stoneResources.Count >= stoneRequired;
        }
    }

    public override IDictionary<Type, int> GetResourcesRequired()
    {
        return new Dictionary<Type, int>
        {
            [typeof(WoodResource)] = woodRequired - woodResources.Count,
            [typeof(StoneResource)] = stoneRequired - stoneResources.Count
        };
    }

    private void Awake()
    {
        woodResources = new ResourceCollection<WoodResource>();
        stoneResources = new ResourceCollection<StoneResource>();
    }

    public override void AddResource(ref BaseResource resource) // TODO : REFACTOR
    {
        if (resource is WoodResource wood)
        {
            AddResource(ref wood);
        }
        else if (resource is StoneResource stone)
        {
            AddResource(ref stone);
        }

        // ensure the source object is null to avoid duplication
        // need to find better solution
        if (resource is object)
        {
            resource = null;
        }
    }

    private void AddResource(ref WoodResource wood)
    {
        woodResources.Add(ref wood);
    }

    private void AddResource(ref StoneResource stone)
    {
        stoneResources.Add(ref stone);
    }
}