namespace Storm.Helpers.Economy
{
    public interface ICoinsTransfer
    {
        void UserToUser(ulong sourceUserId, ulong targetUserId, ulong amount);
    }
}
