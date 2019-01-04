namespace Storm.Accounts
{
    public interface IUserAccountProvider
    {
        UserAccount GetById(ulong userId);
        void SaveByIds(params ulong[] userId);
    }
}
