using TechnicalTest.Data.Model;

namespace TechnicalTest.API.Models.DateAndUser
{
    public static class DateAndUserInfoUpdate
    {
        public static BaseModel<T> UpdateCreateInfo<T>(BaseModelCreate<IdType> obj, int userId) 
        {
            obj.CreateDate =  DateTime.Now;
            obj.CreatedByUserID  = userId;
            return obj;
        }

        public static BaseModel<T> UpdateUpdateInfo<T>(BaseModel<T> obj, int userId) 
        {
            obj.UpdateDate = DateTime.Now;
            obj.UpdatedByUserID = userId;
            return obj;
        }
        public static BaseModel<T> UpdateDeleteInfo<T>(BaseModel<T> obj, int userId) 
        {
            obj.DeleteDate = obj.UpdateDate = DateTime.Now;
            obj.DeletedByUserID = obj.UpdatedByUserID = userId;
            return obj;
        }
    }
}
