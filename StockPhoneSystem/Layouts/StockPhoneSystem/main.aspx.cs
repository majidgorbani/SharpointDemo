using System;

using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;
using System.Data;

namespace StockPhoneSystem.Layouts.StockPhoneSystem
{
    public partial class main : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

            }
            
        }

        protected void btnPopulate_Click(object sender, EventArgs e)
        {
            //SPSecurity.RunWithElevatedPrivileges(delegate()
           // {
           
                    try
                    {
                        using (SPSite site=new SPSite("http://sps"))
                        {
                            using (SPWeb web=site.OpenWeb())
                            {
                                DataTable dt = new DataTable();
                                dt.Columns.Add("Description", typeof(string));
                                dt.Columns.Add("Color", typeof(string));
                                dt.Columns.Add("PhoneID", typeof(string));
                                dt.Columns.Add("BrandName", typeof(string));
                                dt.Columns.Add("Memory", typeof(string));
                                dt.Columns.Add("ModelCode", typeof(string));

                                dt.Rows.Add(new object[] { "iphone", "Black", "1", "iphone 3S", "32", "1PB661-5657" });
                                dt.Rows.Add(new object[] { "iphone", "Black", "2", "iphone 4G", "32", "1PZM661-4780" });
                                dt.Rows.Add(new object[] { "iphone", "Black", "3", "iphone 3", "16", "1PZM661-4782" });
                                dt.Rows.Add(new object[] { "iphone", "Black", "4", "iphone 5", "13", "1PZM661-4783" });
                                dt.Rows.Add(new object[] { "iphone", "White", "5", "iphone 3g", "64", "1PB661-5253	" });
                                dt.Rows.Add(new object[] { "iphone", "White", "6", "iphone GS", "64", "1PB661-5257" });
                                dt.Rows.Add(new object[] { "iphone", "White", "7", "iphone S4", "64", "1PB661-5659" });
                                dt.Rows.Add(new object[] { "iphone", "Black", "8", "iphone T", "64", "B661-6254" });
                                dt.Rows.Add(new object[] { "iphone", "White", "9", "iphone F", "64", "B661-6234" });
                                dt.Rows.Add(new object[] { "iphone", "Black", "10", "iphone 4S", "32", "B661-6250" });

                                SPList list = web.Lists["Phone"];
                                foreach (DataRow dr in dt.Rows)
                                {
                                    SPListItem newItem = list.Items.Add();
                                    newItem["Description"] = dr["Description"];
                                    newItem["Color"] = dr["Color"];
                                    newItem["PhoneID"] = dr["PhoneID"];
                                    newItem["BrandName"] = dr["BrandName"];
                                    newItem["Memory"] = dr["Memory"];
                                    newItem["ModelCode"] = dr["ModelCode"];
                                    newItem.Update();
                            
                                }


                        
                            }
                    
                        }

                    }
                    catch (Exception)
                    {
                
                        throw;
                    }
                    lblMessagge.Text = "hellorworld";
            //});
        }
         

    }
}
