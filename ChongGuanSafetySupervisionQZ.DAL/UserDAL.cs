using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChongGuanDotNetUtils.Extensions;
using ChongGuanDotNetUtils.Helpers;
using ChongGuanSafetySupervisionQZ.Model;

namespace ChongGuanSafetySupervisionQZ.DAL
{
    public class UserDAL
    {
        public ResultData<QZ_User> Login(QZ_User qZ_User)
        {
            string message = "登录失败，请检查用户名密码是否正确";

            var query = from u in ModelQZ.DatabaseContext.QZ_User
                        where (u.LoginName == qZ_User.LoginName && u.LoginPwd == qZ_User.LoginPwd && u.IsDeleteId != 1 && u.IsForbidden != 1)
                        select u;

            ResultData<QZ_User> result = new ResultData<QZ_User> { IsSuccessed = query.Count() == 1, Message = message };

            if (result.IsSuccessed)
            {
                message = "success";
                result.Data = query.FirstOrDefault();
            }

            return result;
        }

        public async Task<ResultData<QZ_User>> ModifyPassword(QZ_User qZ_User, string oldPassword, string newPassword)
        {
            string message = "密码修改失败，不存在的用户！";

            var query = from u in ModelQZ.DatabaseContext.QZ_User
                        where (u.LoginName == qZ_User.LoginName && u.LoginPwd == oldPassword.Md5())
                        select u;

            var data = query.FirstOrDefault();
            if (data != null)
            {
                ReflectionHelper.CopyProperties<QZ_User>(qZ_User, data, new String[] { "UserId", "LoginPwd" });

                data.ModifyTime = DateTime.Now.ToString();
                data.LoginPwd = newPassword.Md5();
                ModelQZ.DatabaseContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                await ModelQZ.DatabaseContext.SaveChangesAsync();

                message = string.Empty;
            }

            ResultData<QZ_User> result = new ResultData<QZ_User> { IsSuccessed = data != null, Message = message, Data = data };

            return result;
        }

        public async Task<ResultData<QZ_User>> Update(QZ_User qZ_User)
        {
            string message = "用户不存在";

            var query = from u in ModelQZ.DatabaseContext.QZ_User
                        where (u.UserId == qZ_User.UserId)
                        select u;

            QZ_User data = query.FirstOrDefault();

            if (data != null)
            {
                ReflectionHelper.CopyProperties<QZ_User>(qZ_User, data, new String[] { "UserId", "LoginPwd" });

                data.ModifyTime = DateTime.Now.ToString();
                ModelQZ.DatabaseContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                await ModelQZ.DatabaseContext.SaveChangesAsync();

                message = string.Empty;
            }

            ResultData<QZ_User> result = new ResultData<QZ_User> { IsSuccessed = data != null, Message = message, Data = data };

            return result;
        }

        public async Task<ResultData<QZ_User>> Add(QZ_User qZ_User)
        {
            string message = "用户已经存在";

            var query = from u in ModelQZ.DatabaseContext.QZ_User
                        where (u.LoginName == qZ_User.LoginName)
                        select u;

            QZ_User data = query.FirstOrDefault();

            if (data == null)
            {
                qZ_User.LoginPwd = qZ_User.LoginPwd.Md5();

                qZ_User.CreateTime = DateTime.Now.ToString();
                qZ_User.ModifyTime = DateTime.Now.ToString();
                //ModelQZ.DatabaseContext.Entry(qZ_User).State = System.Data.Entity.EntityState.Added;

                data = ModelQZ.DatabaseContext.QZ_User.Add(qZ_User);
                await ModelQZ.DatabaseContext.SaveChangesAsync();

                message = string.Empty;
            }

            ResultData<QZ_User> result = new ResultData<QZ_User> { IsSuccessed = data == null, Message = message, Data = data };

            return result;
        }

        public async Task<ResultData<QZ_User>> Delete(QZ_User qZ_User)
        {
            string message = "用户不存在";

            var query = from u in ModelQZ.DatabaseContext.QZ_User
                        where (u.UserId == qZ_User.UserId)
                        select u;

            QZ_User data = query.FirstOrDefault();

            if (data != null)
            {
                ReflectionHelper.CopyProperties<QZ_User>(qZ_User, data, new String[] { "UserId", "LoginPwd" });

                data.ModifyTime = DateTime.Now.ToString();
                data.IsDeleteId = 1;

                ModelQZ.DatabaseContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                await ModelQZ.DatabaseContext.SaveChangesAsync();

                message = string.Empty;
            }

            ResultData<QZ_User> result = new ResultData<QZ_User> { IsSuccessed = data != null, Message = message, Data = data };

            return result;
        }

        public ResultData<IEnumerable<QZ_User>> QueryByAreaCode(string areaCode)
        {

            var query = from u in ModelQZ.DatabaseContext.QZ_User
                        where (u.AreaCode == areaCode)
                        select u;

            IEnumerable<QZ_User> data = query.ToList();

            ResultData<IEnumerable<QZ_User>> result = new ResultData<IEnumerable<QZ_User>> { IsSuccessed = true, Message = string.Empty, Data = data };

            return result;
        }
    }
}
