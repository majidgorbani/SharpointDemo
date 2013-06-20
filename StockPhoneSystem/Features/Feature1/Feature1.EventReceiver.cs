using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Navigation;

namespace StockPhoneSystem.Features.Feature1
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("34d11ca1-e1db-443d-bcfb-11d4d285af9f")]
    public class Feature1EventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            
            Boolean foundNode = false;
            try
            {
                using (SPSite site = properties.Feature.Parent as SPSite)
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList PhoneList = web.Lists["Phone"];
                        SPList PhoneRegList = web.Lists["PhoneReg"];

                        string fieldModelCode = PhoneRegList.Fields.AddLookup("ModelCode", PhoneList.ID, false);
                        SPFieldLookup modelCodeLookup = new SPFieldLookup(PhoneRegList.Fields, fieldModelCode);
                        modelCodeLookup.AllowMultipleValues = false;
                        modelCodeLookup.LookupField = "ModelCode";
                        //modelCodeLookup.Indexed = true;
                        // modelCodeLookup.RelationshipDeleteBehavior = SPRelationshipDeleteBehavior.Restrict;
                        modelCodeLookup.Update();


                        SPNavigationNodeCollection qn = web.Navigation.QuickLaunch;
                        foreach (SPNavigationNode node in qn)
                        {
                            if (node.Title == "Stock System")
                            {
                                foundNode = true;
                                break;
                            }
                        }

                        if (foundNode == false)
                        {
                            SPNavigationNode headerNode = new SPNavigationNode("Stock System", @"_layouts/StockPhoneSystem/main.aspx?n=1", true);
                            headerNode = qn.AddAsFirst(headerNode);
                            MakeHeaderNode(headerNode);
                            SPNavigationNode node = new SPNavigationNode("Register in", @"_layouts/StockPhoneSystem/Regin.aspx", true);
                            SPNavigationNode nodeout = new SPNavigationNode("Register out", @"_layouts/StockPhoneSystem/dpItem.aspx", true);
                            headerNode.Children.AddAsLast(node);
                            headerNode.Children.AddAsLast(nodeout);

                        }

                       // SPList phoneList = web.Lists.TryGetList("Phone");
                        //SPList phoneRegLookupList = web.Lists.TryGetList("PhoneReg");
                        //if (phoneRegLookupList == null)
                       // {

                       // }
                        
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            Boolean foundNode = false;
            try
            {

                using (SPSite site = properties.Feature.Parent as SPSite)
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                       SPNavigationNodeCollection ql = web.Navigation.QuickLaunch;
                        foreach (SPNavigationNode node in ql)
                        {
                            if (node.Title == "Stock System")
                            {
                                node.Delete();
                                foundNode = true;
                                break;
                            }
                        }
                        if (foundNode) // found the node delete it!
                        {

                        }

                        SPList customListPhone = web.Lists.TryGetList("Phone");
                        if (customListPhone != null)
                        {
                            customListPhone.OnQuickLaunch = false;
                            customListPhone.Delete();
                        }
                        SPList customListPhoneReg = web.Lists.TryGetList("PhoneReg");
                        if (customListPhoneReg != null)
                        {
                            customListPhoneReg.OnQuickLaunch = false;
                            customListPhoneReg.Delete();

                        }
                        web.Update();

                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
        

        }


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}

        public static void MakeHeaderNode(SPNavigationNode node)
        {

            node.Properties["URL"] = @"_layouts/StockSystem/Regin.aspx?h=1";
            node.Properties["LastModifiedDate"] = DateTime.Now;
            node.Properties["Target"] = "";
            node.Properties["vti_navsequencechild"] = "true";
            node.Properties["UrlQueryString"] = "";
            node.Properties["CreatedDate"] = DateTime.Now;
            node.Properties["Description"] = "";
            node.Properties["UrlFragment"] = "";
            node.Properties["NodeType"] = "Heading";
            node.Properties["Audience"] = "";
            node.Update();
        }
    }
}
