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
            default:
                return "";
        }
    }
}

public enum TagEnum
{
    ENEMY,
    PLAYER
}