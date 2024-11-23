using PostaAPI.Classes;

namespace PostaAPI.Helpers
{
    public static class EntityHelper
    {
        public static void SetEntryData(BaseEntity entity, int userId, string userName)
        {
            entity.EntryDate = DateTime.Now;
            entity.IdEntryUser = userId;
            entity.EntryUser = userName;
            entity.IsDeleted = false;
        }

        public static void SetUpdateData(BaseEntity entity, int userId, string userName)
        {
            entity.UpdateDate = DateTime.Now;
            entity.IdUpdateUser = userId;
            entity.UpdateUser = userName;
        }

        public static void SetDeleteData(BaseEntity entity, int userId, string userName)
        {
            entity.IsDeleted = true;
            entity.DeleteDate = DateTime.Now;
            entity.IdDeleteUser = userId;
            entity.UpdateUser = userName;
        }
    }

}
