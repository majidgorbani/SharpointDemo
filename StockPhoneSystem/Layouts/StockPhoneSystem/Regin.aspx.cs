using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using Utilities;

namespace StockPhoneSystem.Layouts.StockPhoneSystem
{
    public partial class Regin : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                //populateGridview();

            }          

        }
        protected override void OnPreRenderComplete(EventArgs e)
        {
            populateGridview();
        }

        public void populateGridview()
        {
            // IMEI, RegDate, regUser, Status, Location, ModelCode
            
            using (SPSite site=new SPSite("http://sps"))
            {
                using (SPWeb web = site.RootWeb)
                {
                    SPList mylist = web.Lists["PhoneReg"];
                    SPListItemCollection items = mylist.Items;

                    DataTable table;
                    table = new DataTable();
                    table.Columns.Add("Model Code", typeof(string));
                    table.Columns.Add("IMEI Number", typeof(string));
                    table.Columns.Add("Supplier", typeof(string));
                    table.Columns.Add("Status", typeof(string));
                    table.Columns.Add("Location Code", typeof(string));
                    DataRow row;
                    foreach (SPListItem item in items)
                    {
                        row = table.Rows.Add();

                        row["Model Code"] = item["ModelCode"].ToString();
                        row["IMEI Number"] = item["IMEI"].ToString();
                        row["Supplier"] = item["Supplier"].ToString();
                        row["Status"] = item["Status"].ToString();
                        row["Location Code"] = item["Location"].ToString();

                    }

                    gvMyView.DataSource = table.DefaultView;
                    gvMyView.DataBind();
                }
            }
        }
        protected void insert_Click(object sender, EventArgs e)
        {            
            if (Request.Form["txtCode"] != "" && Request.Form["txtImei"] != "")
            {
                bool found = false;
                string strEmei = Request.Form["txtImei"];
                string strCode = Request.Form["txtCode"];
                if(ValidateIMEI(strEmei))
                {
                    using (SPSite site = new SPSite("http://sps"))
                    {
                        using (SPWeb web = site.RootWeb)
                        {                            
                            SPList listPhoneReg = web.Lists.TryGetList("PhoneReg");
                            SPList listPhone = web.Lists.TryGetList("Phone");
                            
                            if (modelCodeExist(listPhone, strCode)) // see if there exist phone registret by Modelcode
                            {
                                if (!is_item_in_registered(listPhoneReg, strEmei))
                                {
                                    try
                                    {
                                        SPListItemCollection phone = listPhone.Items;
                                        SPListItemCollection phonereg = listPhoneReg.Items;

                                        string luFname = "ModelCode";
                                        if (!listPhoneReg.Fields.ContainsField(luFname))
                                            return;

                                        SPField lookupFld = listPhoneReg.Fields.GetField(luFname);
                                        foreach (SPListItem ph in phone)
                                        {
                                            if (ph["ModelCode"].ToString() == strCode)
                                            {
                                                SPListItem prItem = phonereg.Add();

                                                SPFieldLookupValue value = new SPFieldLookupValue(ph.ID, ph.ID.ToString());
                                                prItem["IMEI"] = strEmei.ToString();
                                                prItem["ModelCode"] = value.ToString();
                                                prItem["PhoneID"] = "";
                                                prItem["Supplier"] = "tre";
                                                prItem["RegDate"] = DateTime.Now;
                                                prItem["regUser"] = Context.User.Identity.Name;
                                                prItem["Status"] = "in";
                                                prItem["Location"] = "vasteras";
                                                prItem.Update();
                                                return;
                                            }                                            
                                        }
                                    }
                                    catch (Exception eex)
                                    {
                                        lblMessage.Text = "Error: " + eex.Message;
                                    }
                                    finally
                                    {
                                        lblMessage.Text = "the new phone has been added!";
                                    }                                    
                                }
                                else
                                {
                                    lblMessage.Text = "Emei is alread registrated";
                                }                                
                            }
                            else
                            {
                                lblMessage.Text = "no Model code";

                            }
                        }
                    }
                }
                else
                {
                    lblMessage.Text = "Emei error";
                }                
                //lblMessage.Text = Request.Form["txtCode"];
            }
            else
            {
                lblMessage.Text = "cant be empty";
            }
        }

        private bool modelCodeExist(SPList listPhone, string strCode)
        {
            foreach (SPListItem item in listPhone.Items)
            {
                if (item["ModelCode"].ToString() == strCode)
                {
                    return true;
                }
            }
            return false;
        }

     //012021007168406

        private bool is_item_in_registered(SPList l, string strEmei)
        {
            string s = "";
            SPList list = l;
            foreach (SPListItem item in list.Items)
            {
                s = item["IMEI"].ToString();
                if (strEmei==s)
                {
                    return true;                    
                }                
            }
            return false;
        }
        private Boolean ValidateIMEI(string IMEI)
        {
            if (IMEI.Length != 15)
                return false;
            else
            {
                Int32[] PosIMEI = new Int32[15];
                for (int innlop = 0; innlop < 15; innlop++)
                {
                    PosIMEI[innlop] = Convert.ToInt32(IMEI.Substring(innlop, 1));
                    if (innlop % 2 != 0) PosIMEI[innlop] = PosIMEI[innlop] * 2;
                    while (PosIMEI[innlop] > 9) PosIMEI[innlop] = (PosIMEI[innlop] % 10) + (PosIMEI[innlop] / 10);
                }

                Int32 Totalval = 0;
                foreach (Int32 v in PosIMEI) Totalval += v;
                if (0 == Totalval % 10)
                    return true;
                else
                    return false;
            }

        }
    }
}
