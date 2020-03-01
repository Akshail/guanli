using BusinessSupply.BusinessOper;
using DataModel.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XF.HKHJXT.Administrator
{
    public partial class SendMessage_SelectPerson : System.Web.UI.Page
    {
        private string uid
        {
            get
            {
                try
                {
                    return Session["UserID"].ToString().Trim();
                }
                catch
                {
                    return null;
                }

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //TUserInfo u = BusinessSupply.BusinessOper.UserBusiness.GetUserById(Convert.ToInt32(uid));
                //int associationid = Convert.ToInt32(u.AssociationId.ToString());
                ////DataTable dt_person = BusinessSupply.BusinessOper.PersonAssociationBusiness.GetPersonWithEmailByAssociationId(associationid);
                //DataTable dt_person = BusinessSupply.BusinessOper.PersonAssociationBusiness.GetPersonByAssociationId(associationid);
                //DataTable dt_person = BusinessSupply.BusinessOper.PersonAssociationBusiness.GetPerson();
                DataTable dt = BasicInfoBusiness.GetBasicInfosForMessage();
                this.CheckBoxList1.DataSource = dt;
                this.CheckBoxList1.DataTextField = "Name";
                this.CheckBoxList1.DataValueField = "Id";
                this.CheckBoxList1.DataBind();
                CheckCellphoneNum();
                Session["ReceiverId"] = null;
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                //TUserInfo u = BusinessSupply.BusinessOper.UserBusiness.GetUserById(Convert.ToInt32(uid));
                //int associationid = Convert.ToInt32(u.AssociationId.ToString());
                if (this.ddl_MemberJob.SelectedValue != "")
                {
                    //string job = this.ddl_MemberJob.SelectedValue;
                    //DataTable member = BusinessSupply.BusinessOper.PersonAssociationBusiness.GetPersonAssociationTableByAssociationAndJob(associationid, Convert.ToInt32(job));
                    //DataTable member = BusinessSupply.BusinessOper.PersonAssociationBusiness.GetPersonAssociationTableByJob(Convert.ToInt32(job));

                    DataTable dt = BasicInfoBusiness.GetBasicInfosForMessage();
                    this.CheckBoxList1.DataSource = dt;
                    this.CheckBoxList1.DataTextField = "Name";
                    this.CheckBoxList1.DataValueField = "Id";
                    this.CheckBoxList1.DataBind();
                    CheckCellphoneNum();
                    Session["ReceiverId"] = null;

                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);

            }
        }

        //处理PersonMemberBasicInfo中Email字段无有效邮件地址的会员
        public void CheckCellphoneNum()
        {
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                string id = this.CheckBoxList1.Items[i].Value;
                DataTable personwithemail = new DataTable();
                personwithemail = BasicInfoBusiness.GetBasicInfoForHasPhoneById(Convert.ToInt32(id));
                if (personwithemail.Rows.Count == 0)
                {
                    this.CheckBoxList1.Items[i].Enabled = false;
                }
            }
        }

        protected void btn_Next_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
                {
                    if (this.CheckBoxList1.Items[i].Selected == true && this.CheckBoxList1.Items[i].Enabled)
                    {

                        Session["ReceiverId"] += this.CheckBoxList1.Items[i].Value + ",";
                    }
                }
                string temp = Session["ReceiverId"].ToString();
                Session["ReceiverId"] = temp.Substring(0, temp.Length - 1);
                Response.Redirect("SendMessage_NewMessage.aspx");
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "alter", "alert('数据加载错误！')", true);

            }
        }

        protected void cb_SelectAll_CheckedChanged(object sender, EventArgs e)
        {
            //
            if (this.cb_SelectAll.Checked)
            {
                if (this.CheckBoxList1.Items.Count > 0)
                {
                    for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
                    {
                        if (this.CheckBoxList1.Items[i].Enabled)
                        {
                            this.CheckBoxList1.Items[i].Selected = true;
                        }
                    }
                }
            }
            else
            {
                if (this.CheckBoxList1.Items.Count > 0)
                {
                    for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
                    {
                        if (this.CheckBoxList1.Items[i].Enabled)
                        {
                            this.CheckBoxList1.Items[i].Selected = false;
                        }
                    }
                }
            }
        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cb_SelectAll.Checked)
            {
                this.cb_SelectAll.Checked = false;
            }
            CheckCheckAll();
        }
        private void CheckCheckAll()
        {
            bool IsAll = true;
            for (int i = 0; i < this.CheckBoxList1.Items.Count; ++i)
            {
                if (this.CheckBoxList1.Items[i].Enabled && !this.CheckBoxList1.Items[i].Selected)
                {
                    if (this.cb_SelectAll.Checked)
                    {
                        this.cb_SelectAll.Checked = false;
                    }
                    IsAll = false;
                    break;
                }
            }
            if (IsAll)
            {
                if (!this.cb_SelectAll.Checked)
                {
                    this.cb_SelectAll.Checked = true;
                }
            }
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SendMessage.aspx");

        }
    }
}