using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class AgedBusiness
    {
        public static bool AddAged(int people_Id, string health, string tendance, string relationship,string phone,string type)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Aged st = new Aged();
                    st.People_Id = people_Id;
                    st.Health = health;
                    st.Tendance = tendance;
                    st.Relationship = relationship;
                    st.Phone = phone;
                    st.Type = type;
                    st.IsDelete = false;
                    st.Save();
                });
            }
            catch { return false; }
            return true;
        }
        public static bool AddAgedNull(int people_Id)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Aged st = new Aged();
                    st.People_Id = people_Id;
                    st.IsDelete = false;
                    st.Save();
                });
            }
            catch { return false; }
            return true;
        }


        public static bool UpdateAged(int ageId, string health, string tendance, string relationship, string phone,string type)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Aged st = Aged.FindById(ageId);
                    st.Health = health;
                    st.Tendance = tendance;
                    st.Relationship = relationship;
                    st.Phone = phone;
                    st.Type = type;
                    st.Save();
                });
            }
            catch { return false; }
            return true;
        }

        public static bool UpdateAgedByPeople(int people_Id, string health, string tendance, string relationship, string phone, string type)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Aged st = Aged.FindOne(CK.K["People_Id"] == people_Id && CK.K["IsDelete"] == false);
                    st.Health = health;
                    st.Tendance = tendance;
                    st.Relationship = relationship;
                    st.Phone = phone;
                    st.Type = type;
                    st.Save();
                });
            }
            catch { return false; }
            return true;
        }
        public static bool DeleteAged(int ageId)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Aged st = Aged.FindById(ageId);
                    st.IsDelete = true;
                    st.Save();
                });
            }
            catch { return false; }
            return true;
        }

    }
}
