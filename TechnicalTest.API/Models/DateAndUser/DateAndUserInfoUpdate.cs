using TechnicalTest.Data.Model;

namespace TechnicalTest.API.Models.DateAndUser
{
    public static class DateAndUserInfoUpdate
    {
        public static IModelCreateInfo UpdateCreateInfo(IModelCreateInfo obj, int userId) 
        {
            obj.CreateDate =  DateTime.Now;
            obj.CreatedByUserId  = userId;
            return obj;
        }

        public static IModelUpdateInfo UpdateUpdateInfo(IModelUpdateInfo obj, int userId) 
        {
            obj.UpdateDate = DateTime.Now;
            obj.UpdatedByUserId = userId;
            return obj;
        }
        public static IModelDeleteInfo UpdateDeleteInfo(IModelDeleteInfo obj, int userId) 
        {
            obj.DeleteDate = DateTime.Now;
            obj.DeletedByUserId = userId;
            return obj;
        }
    }
}
