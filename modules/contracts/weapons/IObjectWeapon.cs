namespace contracts.weapons
{
    public interface IObjectWeapon
    {
        void OnEquip();
        void OnDrop();
        void OnAttackPrimary();
        void OnAttackSecondary();
        void OnReload();
    }
}