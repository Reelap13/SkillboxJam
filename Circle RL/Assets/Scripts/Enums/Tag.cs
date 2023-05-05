public static class Tag
{
    public static string getTag(TagEnum tag)
    {
        switch (tag)
        {
            case TagEnum.ENEMY:
                return "Enemy";
            case TagEnum.PLAYER:
                return "Player";
            case TagEnum.PROJECTILE:
                return "Projectile";
            default:
                return "";
        }
    }

    public static bool IsParticipantOfTeam(string tag)
    {
        switch (tag)
        {
            case "Enemy":
            case "Player":
                return true;
            default:
                return false;
        }
    }
}

public enum TagEnum
{
    ENEMY,
    PLAYER,
    PROJECTILE
}