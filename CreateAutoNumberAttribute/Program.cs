using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Configuration;
namespace CreateAutoNumberAttribute
{
    class Program
    {
        private static string entityName = "new_autonumberdemoentity";
        static void Main(string[] args)
        {
            try
            {
                CrmServiceClient crmServiceClientObj = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CrmOnlineStringFromAppConfig"].ConnectionString);
                if (!crmServiceClientObj.IsReady)
                    Console.WriteLine("No Connection was Made.");
                Console.WriteLine("Connected");
                Console.WriteLine("Creating Auto number Attribute for Entity {0}", entityName);
                var attributeMetaData = new StringAttributeMetadata()
                {
                    //{DATETIMEUTC:yyyyMMddhhmmss} can also be used 
                    AutoNumberFormat = "SYS {RANDSTRING:4} - ORG {SEQNUM:4}",

                    //this should be unique
                    SchemaName = "new_AutoNumAtt",

                    //set it as per required
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),

                    //Lable Name 
                    DisplayName = new Microsoft.Xrm.Sdk.Label("Entity Code", 1033),

                    // On hover description
                    Description = new Microsoft.Xrm.Sdk.Label("The value will be AUTO GENERATED", 1033),

                    IsAuditEnabled = new Microsoft.Xrm.Sdk.BooleanManagedProperty(false),

                    // we need it to be searched direclty from global search.
                    IsGlobalFilterEnabled = new Microsoft.Xrm.Sdk.BooleanManagedProperty(true),
                    MaxLength = 100 //
                };

                CreateAttributeRequest req = new CreateAttributeRequest()
                {
                    EntityName = entityName,
                    Attribute = attributeMetaData
                };

                crmServiceClientObj.Execute(req);
                Console.WriteLine("Created Auto number Attribute for Entity {0}", entityName);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error here. " + e.Message);
                Console.ReadLine();
            }

        }
    }
}
