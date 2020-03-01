using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class EmailAccountBusiness
    {
        public static bool SaveEmailAccount(int userId, string account, string password, string emailServer)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    EmailAccount email = EmailAccount.FindOne(CK.K["UserId"] == userId && CK.K["IsDelete"] == false);
                    if (email == null)
                    {
                        email = new EmailAccount();
                    }
                    email.UserId = userId;
                    email.Account = account;
                    email.Password = password;
                    email.EmailServer = emailServer;
                    email.IsDelete = false;
                    email.Save();
                });
            }
            catch { return false; }
            return true;
        }

        public static EmailAccount GetEmailAccount(int userId)
        {
            try
            {
                EmailAccount email = EmailAccount.FindOne(CK.K["UserId"] == userId && CK.K["IsDelete"] == false);
                return email;
            }
            catch { return null; }
        }

    }
}
