using DAL.Core;

namespace DAL.Common
{
    public static class SqlRepositoryFactory
    {
        private static ISqlRepository _sqlRepository;

        public static ISqlRepository CreateOrGet()
        {
            if(_sqlRepository == null)
            {
                _sqlRepository = new SqlRepository();
            }
            return _sqlRepository;
        }
    }
}
