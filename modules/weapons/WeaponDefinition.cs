using Godot;
using MonoCustomResourceRegistry;
using System;

[RegisteredType(nameof(WeaponDefinition))]
public class WeaponDefinition : Resource
{
    // meta info
    [Export] public string nameKey = "name";
    
    // shooting stats
    [Export] public float fireRange = 100.0f;
    [Export] public float rof = 0.5f; // shots fired per second
    [Export] public float damage = 1.0f;

    // animation info
    [Export] public string equipAnimation = "";
    [Export] public string unequipAnimation = "";
    [Export] public string fireAnimation = "";
    [Export] public string reloadAnimation = "";




}
