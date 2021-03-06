﻿using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Utilities;
using System.Data;

namespace StockPhoneSystem.Layouts.StockPhoneSystem
{
    public partial class dpItem : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // test my test
            
        }
        protected override void OnPreRenderComplete(EventArgs e)
        {
            populateGridview();
        }
        public void populateGridview()
        {
            // IMEI, RegDate, regUser, Status, Location, ModelCode

            using (SPSite site = new SPSite("http://sps"))
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

                    table.Columns.Add("Reg Date", typeof(DateTime));
                    table.Columns.Add("Disp Date", typeof(DateTime));
                    table.Columns.Add("Reg User", typeof(string));
                    table.Columns.Add("Disp User", typeof(string));
                    DataRow row;
                    foreach (SPListItem item in items)
                    {
                        row = table.Rows.Add();
                        row["Model Code"] = item["ModelCode"].ToString();
                        row["IMEI Number"] = item["IMEI"].ToString();
                        row["Supplier"] = item["Supplier"].ToString();
                        row["Status"] = item["Status"].ToString();
                        row["Location Code"] = item["Location"].ToString();
                        row["Reg Date"] = item["RegDate"].ToString();

                        if (item["DisDate"] != null)
                            row["Disp Date"] = item["DisDate"].ToString();
                      

                        row["Reg User"] = item["regUser"].ToString();

                        if (item["disUser"] != null)
                            row["Disp User"] = item["disUser"].ToString();
                        
                           
                        
                    }

                    gvMyViewout.DataSource = table.DefaultView;
                    gvMyViewout.DataBind();
                }
            }
        }
        private bool ImeiExist(SPList listPhone, string imei)
        {
            foreach (SPListItem item in listPhone.Items)
            {
                if (item["IMEI"].ToString() == imei)
                {
                    return true;
                }
            }
            return false;
        }
        protected void remove_Click(object sender, EventArgs e)
        {
            if (Request.Form["txtImei"] != "")
            {
                string Imei = Request.Form["txtImei"];
                if (ValidateIMEI(Imei))
                {
                    using (SPSite site=new SPSite("http://sps"))
                    {
                        using (SPWeb web=site.RootWeb)
                        {
                            SPList listPhoneReg = web.Lists.TryGetList("PhoneReg");
                            if (ImeiExist(listPhoneReg, Imei))
                            {
                                SPListItemCollection sc = listPhoneReg.Items;
                                foreach (SPListItem phreg in sc)
                                {
                                    if (phreg["IMEI"].ToString() == Imei)
                                    {
                                        if (phreg["Status"].ToString() != "out")
                                        {
                                            lblInfo.Style.Add("color", "green");
                                            lblInfo.Text = "Found item";
                                            phreg["Status"] = "out";
                                            phreg["DisDate"] = DateTime.Now;
                                            phreg["disUser"] = Context.User.Identity.Name;
                                            //phreg["Location"] = "vasteras";
                                            phreg.Update();
                                            
                                        }
                                        else
                                        {
                                            lblInfo.Style.Add("color", "green");
                                            lblInfo.Text = "the item has already been dispatched";
                                            return;
                                        }

                                       
                                    }
                                    
                                }

                           
                            }
                            else
                            {
                                lblInfo.Style.Add("color", "green");
                                lblInfo.Text = "there is no list with the "+Imei.ToString();
                            }
                            
                            
                        }
                        
                    }
                    
                }
                else
                {
                    lblInfo.Style.Add("color", "red");
                    lblInfo.Text = "wrong Imei";
                }               
                
            }
            else
            {
                lblInfo.Style.Add("color", "red");
                lblInfo.Text = "the field should not be empty!";
                
            }
            
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
