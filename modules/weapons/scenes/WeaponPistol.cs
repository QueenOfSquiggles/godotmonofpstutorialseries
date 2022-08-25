using contracts.weapons;
using Godot;
using System;

public class WeaponPistol : RigidBody, IObjectWeapon
{

    private uint ogPhysicsLayer = 0;
    private uint ogPhysicsMask = 0;

    public override void _Ready()
    {
        ogPhysicsLayer = this.CollisionLayer;
        ogPhysicsMask = this.CollisionMask;
    }

    public void OnAttackPrimary()
    {
        GD.Print("WeaponPistol->OnAttackPrimary");
    }

    public void OnAttackSecondary()
    {
        GD.Print("WeaponPistol->OnAttackSecondary");
    }

    public void OnDrop()
    {
        GD.Print("WeaponPistol->OnDrop");
        CollisionLayer = ogPhysicsLayer;
        CollisionMask = ogPhysicsMask;
    }

    public void OnEquip()
    {
        GD.Print("WeaponPistol->OnEquip");
        Transform = Transform.Identity;

        CollisionLayer = 0;
        CollisionMask = 0;

    }

    public void OnReload()
    {
        GD.Print("WeaponPistol->OnReload");
    }
}
