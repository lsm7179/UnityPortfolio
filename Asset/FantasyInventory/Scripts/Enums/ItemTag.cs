namespace Assets.FantasyInventory.Scripts.Enums
{
    /// <summary>
    /// Item tags can be used for implementing custom logic (special cases).
    /// Use constant integer values for enums to avoid data distortion when adding/removing new values.
    /// </summary>
    public enum ItemTag
    {
        Undefined   = 0,
        NotForSale  = 1,
        OneHanded   = 2,
        TwoHanded   = 3,
        Sword       = 4,
        Axe         = 5,
        Dagger      = 6,
        Spear       = 7,
        Wand        = 8,
        Bow         = 9
    }
}