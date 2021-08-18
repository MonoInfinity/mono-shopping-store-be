

using mono_store_be.AuthModule.Entity;

namespace mono_store_be.AuthModule.Interface
{
    public interface IReTokenRepository
    {
        public ReToken GetReTokenByReTokenId(string reTokenId);

        public bool saveReToken(ReToken reToken);
    }
}