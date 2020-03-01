using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class StudyBusiness
    {
        public static bool AddStudy(int people_Id, string school, string grade,string major,string protector,string relationship,string phoneNo)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Study st = new Study();
                    st.People_Id = people_Id;
                    st.School = school;
                    st.Grade = grade;
                    st.Major = major;
                    st.Protector = protector;
                    st.Relationship = relationship;
                    st.PhoneNo = phoneNo;
                    st.IsDelete = false;
                    st.Save();
                });
            }
            catch { return false; }
            return true;
        }
        public static bool AddStudyNull(int people_Id)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Study st = new Study();
                    st.People_Id = people_Id;
                    st.IsDelete = false;
                    st.Save();
                });
            }
            catch { return false; }
            return true;
        }
        public static bool UpdateStudy(int studyId, string school, string grade, string major, string protector, string relationship, string phoneNo)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Study st = Study.FindById(studyId);
                    st.School = school;
                    st.Grade = grade;
                    st.Major = major;
                    st.Protector = protector;
                    st.Relationship = relationship;
                    st.PhoneNo = phoneNo;
                    st.Save();
                });
            }
            catch { return false; }
            return true;
        }
        public static bool UpdateStudyByPeople(int people_Id, string school, string grade, string major, string protector, string relationship, string phoneNo)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Study st = Study.FindOne(CK.K["People_Id"] == people_Id && CK.K["IsDelete"] == false);
                    st.School = school;
                    st.Grade = grade;
                    st.Major = major;
                    st.Protector = protector;
                    st.Relationship = relationship;
                    st.PhoneNo = phoneNo;
                    st.Save();
                });
            }
            catch { return false; }
            return true;
        }
        public static bool DeleteStudy(int studyId)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Study st = Study.FindById(studyId);
                    st.IsDelete = true;
                    st.Save();
                });
            }
            catch { return false; }
            return true;
        }

    }
}
