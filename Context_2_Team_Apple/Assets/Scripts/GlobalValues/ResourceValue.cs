using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceValue
{
    public Resource resource;
    public int value;

    public ResourceValue(Resource _resource, int _value)
    {
        resource = _resource;
        value = _value;
    }
}
