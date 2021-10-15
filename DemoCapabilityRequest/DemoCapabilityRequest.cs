// <copyright file="CapabilityRequest.cs" company="Flexera Software LLC">
//     Copyright (c) 2011-2020 Flexera Software LLC.
//     All Rights Reserved.
//     This software has been provided pursuant to a License Agreement
//     containing restrictions on its use.  This software contains
//     valuable trade secrets and proprietary information of
//     Flexera Software LLC and is protected by law.
//     It may not be copied or distributed in any form or medium, disclosed
//     to third parties, reverse engineered or used in any manner not
//     provided for in said License Agreement except with the prior
//     written authorization from Flexera Software LLC.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DemoUtilities;
using FlxDotNetClient;
using IdentityData;
using System.Windows.Forms;

/****************************************************************************
    CapabilityRequest.cs

    This example program allows you to:
    1. Send a capability request via http to the server and process response,
       saving data into trusted storage.
    2. Write capability request to a file. This request can be fed into server
       to generate response.
    3. Read capability response from a file and process accordingly.
*****************************************************************************/

namespace CapabilityRequest
{
    public class DemoCapabilityRequest
    {
        private static readonly List<LicenseInfo> licenses = new List<LicenseInfo>();

        private static readonly string emptyIdentity =
@"License-enabled code requires client identity data,
which you create with pubidutil and printbin -CS.
See the User Guide for more information.";

        private static readonly string processingCapabilityResponse = "Processing capability response";
        private static readonly string acquiringLicense = "Acquiring license";
        private static readonly string attemptingAcquire = "Attempting to acquire license for feature '{0}' version '{1}'";
        private static readonly string licenseAcquired = "License acquisition for feature '{0}' version '{1}' successful";
        private static readonly string attemptingReturn = "Attempting to return license for feature '{0}' version '{1}'";
        private static readonly string licenseReturned = "License for feature '{0}' version '{1}' successfully returned";
        //private static readonly string surveyFeature = "survey";
      //  private static readonly string version = "1.0";

        private static readonly string dictionaryKey1 = "StringKey";
        private static readonly string dictionaryKey2 = "Integer Key";

        private static ILicensing licensing;
        private static RequestType requestType;
        private static string fileName = String.Empty;
        private static string serverUrl = String.Empty;

        private static string currentFeature = String.Empty;

        private enum RequestType
        {
            generateCapabilityRequest,
            processCapabilityResponse,
            sendCapabilityRequest,
        }

        private static bool ValidateCommandLineArgs(string[] args)
        {
            bool validCommand = false;
            bool invalidSpec = false;
            if (args.Length >= 1)
            {
                switch (args[0].ToLowerInvariant())
                {
                    case "-h":
                    case "-help":
                        break;
                    case "-generate":
                        if (!(invalidSpec = (args.Length != 2)))
                        {
                            fileName = args[1];
                            requestType = RequestType.generateCapabilityRequest;
                            validCommand = true;
                        }
                        break;
                    case "-process":
                        if (!(invalidSpec = (args.Length != 2)))
                        {
                            fileName = args[1];
                            requestType = RequestType.processCapabilityResponse;
                            validCommand = true;
                        }
                        break;
                    case "-server":
                        if (!(invalidSpec = (args.Length != 2 )))
                        {
                            serverUrl = args[1];
                            requestType = RequestType.sendCapabilityRequest;
                            validCommand = true;
                        }
                        break;
                    default:
                        Util.DisplayErrorMessage(String.Format("unknown option: {0}", args[0]));
                        break;
                }
            }
            if (!validCommand && invalidSpec)
            {
                Util.DisplayErrorMessage(String.Format("invalid specification for option: {0}", args[0]));
            }
            return validCommand;
        }
        [STAThread]
        static void Main1()
        {
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void Main()
        {
            /*  if (!ValidateCommandLineArgs(args))
              {
                  Usage(Path.GetFileName(Environment.GetCommandLineArgs()[0]));
                  return;
              }
            */

            if (IdentityClient.IdentityData == null || IdentityClient.IdentityData.Length == 0)
            {
                Console.WriteLine(emptyIdentity);
                return;
            }

            try
            {
                string strPath = Util.GetDefaultTSLocation() + Path.DirectorySeparatorChar;
                // Initialize ILicensing interface with identity data using the Windows common document 
                // respository as the trusted storage location and the hard-coded string hostid "1234567890".

                //string trialPath= Util.()
               licensing = LicensingFactory.GetLicensing(
                         IdentityClient.IdentityData,
                          strPath,
                          "84C5A66DA60A");
           //     if (licensing == null)
           //         MessageBox.Show("Cannot initialize licensing at {0} ", strPath);
                //using ()
                {
          //          Dictionary<HostIdEnum, List<String>> hostIDs = licensing.LicenseManager.HostIds;
                   // if (hostIDs.ContainsKey(HostIdEnum.FLX_HOSTID_TYPE_USER))
                   // {
                        // select only the Ethernet addresses
                   //     List<String> ethernetIDs = hostIDs[HostIdEnum.FLX_HOSTID_TYPE_USER];
                        //Chapter 6 Using the FlexNet Embedded APIs
                        // Common Steps to Prepare for Licensing
                        //FlexNet Embedded Client 2021.09.NET XT SDK User Guide FNE - 2021 - 09 - NXTSDK - UG00 Company Confidential 61
                        // use the first Ethernet address (index 0) in the list
                     //   licensing.LicenseManager.SetHostId(HostIdEnum.FLX_HOSTID_TYPE_USER, ethernetIDs[0]);
                   // }
                    // The optional host name is typically set by a user as a friendly name for the host.  
                    // The host name is not used for license enforcement.                  
          //           licensing.LicenseManager.HostName = "FNE Toolkit Test #2";
                    //    licensing.LicenseManager.SetHostId(HostIdEnum., ethernetIDs[0]);
                    // The host type is typically a name set by the implementer, and is not modifiable by the user.
                    // While optional, the host type may be used in certain scenarios by some back-office systems such as FlexNet Operations.
                    licensing.LicenseManager.HostType = "FLX_CLIENT";

                    licensing.LicenseManager.AddTrustedStorageLicenseSource();
                    licensing.LicenseManager.AddTrialLicenseSource();
                    //licensing.LicenseManager.SetHostId(HostIdEnum.FLX_HOSTID_TYPE_USER, ethernetIDs[0]);

                    // ShowTSFeatures();
                    Util.DisplayInfoMessage(requestType.ToString());
                    //MessageBox.Show("Attempting to register online with activation id ce4e-155e-f7d0-4399-aca1-6967-9688-9483", "FNE Toolkit Demo");
                    //DemoSendCapabilityRequest("ce4e-155e-f7d0-4399-aca1-6967-9688-9483", "https://eaton-fno-uat.flexnetoperations.com//flexnet//operations//deviceservices");
                    //MessageBox.Show("Generating capability request for activation id ce4e-155e-f7d0-4399-aca1-6967-9688-9483", "FNE Toolkit Demo");
                    //DemoGenerateCapabilityRequest("ce4e-155e-f7d0-4399-aca1-6967-9688-9483", "c:\\temp\\caprequest.bin");
                   // MessageBox.Show("Processing capability response for activation id ce4e-155e-f7d0-4399-aca1-6967-9688-9483", "FNE Toolkit Demo");
                   // DemoProcessCapabilityResponse("FNETestFeature", "c:\\temp\\capresponse.bin");
                    /*   switch (requestType)
                       {
                           case RequestType.generateCapabilityRequest:
                               GenerateCapabilityRequest();
                               break;
                           case RequestType.processCapabilityResponse:
                               ProcessCapabilityResponse();
                               break;
                           case RequestType.sendCapabilityRequest:
                               SendCapabilityRequest();
                               break;
                       }
                    */
                }
            }
            catch (Exception exc)
            {
                HandleException(exc);
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           // Application.Run(new Form1());
           Application.Run(new DemoUserLicense());



        }
        public bool DemoCapabilityRequestTest(string demoFileName)
        {
            string strPath = Util.GetDefaultTSLocation() + Path.DirectorySeparatorChar;

            ILicensing licensing1 = LicensingFactory.GetLicensing(
                      IdentityClient.IdentityData,
                      strPath,
                      "12345");
            licensing1.LicenseManager.HostName = "FNE Toolkit Test #2";
            licensing1.LicenseManager.HostType = "FLX_CLIENT";
            ICapabilityRequestOptions options = licensing1.LicenseManager.CreateCapabilityRequestOptions();
            options.AddRightsId("1cb5-b7bf-dc3b-421f-82ad-a159-1f06-fee8", 1);
            options.AddVendorDictionaryItem(dictionaryKey1, "Some string value");
            options.AddVendorDictionaryItem(dictionaryKey2, 123);

            // force capability response from server even if nothing has changed
         //   options.ForceResponse = false;// true;
            ICapabilityRequestData capabilityRequestData = licensing1.LicenseManager.CreateCapabilityRequest(options);
            if (Util.WriteData(demoFileName, capabilityRequestData.ToArray()))
            {
                //MessageBox.Show(String.Format("Capability request data written to: {0}", demoFileName));
            }
            return true;
        }
        public void RefreshLicensingHost(string hostId)
        {
            string strPath = Util.GetDefaultTSLocation() + Path.DirectorySeparatorChar;

            licensing = LicensingFactory.GetLicensing(
          IdentityClient.IdentityData,
          strPath,
          hostId);
            licensing.LicenseManager.HostType = "FLX_CLIENT";
        }
        //To-Do change "buffer1" -> filename
        public bool DemoLoadLicenseFile( string licFileName)
        {
         

            //licensing1.LicenseManager.HostName = "FNE Toolkit Test #2";
            
            if (!String.IsNullOrEmpty(licFileName))
            {
                Util.DisplayInfoMessage(String.Format("Reading data from {0}", licFileName));
                byte[] inputFileData = Util.ReadData(licFileName);
                licensing.LicenseManager.AddBufferLicenseSource(inputFileData, "buffer1");
            }
            IFeatureCollection bufferFeatures = licensing.LicenseManager.GetFeatureCollection("buffer1");
            foreach (IFeature feature in bufferFeatures)
            {
                licenses.Add(new LicenseInfo(feature.Name, feature.Version, 1));
                try
                {
                    ILicense acquiredLicense = licensing.LicenseManager.Acquire(feature.Name, feature.Version);
                }
                catch (Exception exc)
                {
                    HandleException(exc);
                    return false;
                }
            }
           
            return true;
        }
        public string DemoDisplayLicenses()
        {
            string ret = "";
            IFeatureCollection bufferFeatures = licensing.LicenseManager.GetFeatureCollection("buffer1");
            foreach (IFeature feature in bufferFeatures)
            {
                ret += feature.ToString();
            }
            return ret;
        }
        public bool DemoGenerateCapabilityRequest(string act_id, string act2_id, int txtCnt, string demoFileName)
        {
            // saving the capablity request to a file
            // create the capability request
            int cnt = txtCnt;
            ICapabilityRequestOptions options = licensing.LicenseManager.CreateCapabilityRequestOptions();
            options.AddRightsId(act_id, cnt);
            if (act2_id.Trim() != "")
                options.AddRightsId(act2_id, cnt);
            // Optionally add capability requeest vendor dictionary items.
            // if we don't include code below,
            //request does not parse in FNO
            //options.AddVendorDictionaryItem(dictionaryKey1, "Some string value");
            //options.AddVendorDictionaryItem(dictionaryKey2, 123);
            options.Incremental = true;
            options.ForceResponse = true;
            ICapabilityRequestData capabilityRequestData = licensing.LicenseManager.CreateCapabilityRequest(options);
            if (Util.WriteData(demoFileName, capabilityRequestData.ToArray()))
            {
                //MessageBox.Show(String.Format("Capability request data written to: {0}", demoFileName));
            }
            return true;
        }
        private static void GenerateCapabilityRequest()
        {
            // saving the capablity request to a file
            Util.DisplayInfoMessage("Creating the capability request");
            ICapabilityRequestData capabilityRequestData = licensing.LicenseManager.CreateCapabilityRequest(GenerateRequestOptions());
            if (Util.WriteData(fileName, capabilityRequestData.ToArray()))
            {
                Util.DisplayInfoMessage(String.Format("Capability request data written to: {0}", fileName));
            }
        }

        private static void ProcessCapabilityResponse()
        {
            // read the capability response from a file and process it
            Util.DisplayInfoMessage(String.Format("Reading capability response data from: {0}", fileName));
            byte[] binCapResponse = Util.ReadData(fileName);
            if (binCapResponse == null)
            {
                return;
            }
            ProcessCapabilityResponse(binCapResponse);
        }

        private static void SendCapabilityRequest()
        {
            Util.DisplayInfoMessage("Creating the capability request");

            // create the capability request
            ICapabilityRequestOptions options = GenerateRequestOptions();
            ICapabilityRequestData capabilityRequestData = licensing.LicenseManager.CreateCapabilityRequest(options);
            Util.DisplayInfoMessage(String.Format("Sending the capability request to: {0}", serverUrl));
            byte[] binCapResponse = null;

            // send the capability request to the server and receive the server response
            CommFactory.Create(serverUrl).SendBinaryMessage(capabilityRequestData.ToArray(), out binCapResponse);
            if (binCapResponse != null && binCapResponse.Length > 0)
            {
                Util.DisplayInfoMessage("Response received");
            }
            if (options.Operation != CapabilityRequestOperation.Preview)
            {
                ProcessCapabilityResponse(binCapResponse);
            }
            else
            {
                ShowPreviewResponse(binCapResponse);
            }
        }
        public bool DemoSendCapabilityFeatureRequest(string feature, int cnt, string demoServerURL)
        {
            Util.DisplayInfoMessage("Creating the capability request");

            // create the capability request
            ICapabilityRequestOptions options = licensing.LicenseManager.CreateCapabilityRequestOptions();
            options.AddDesiredFeature(new FeatureData(feature, "1.0", 1));
            //if (act2_id.Trim() != "")
            //    options.AddRightsId(act2_id, cnt);

            options.Incremental = true;
                  options.ForceResponse = true;

            ICapabilityRequestData capabilityRequestData = licensing.LicenseManager.CreateCapabilityRequest(options);

            Util.DisplayInfoMessage(String.Format("Sending the capability request to: {0}", demoServerURL));
            byte[] binCapResponse = null;

            // send the capability request to the server and receive the server response
            CommFactory.Create(demoServerURL).SendBinaryMessage(capabilityRequestData.ToArray(), out binCapResponse);
            if (binCapResponse != null && binCapResponse.Length > 0)
            {
                Util.DisplayInfoMessage("Response received");
            }
            if (options.Operation != CapabilityRequestOperation.Preview)
            {
                ProcessCapabilityResponse(binCapResponse);
            }
            else
            {
                ShowPreviewResponse(binCapResponse);
            }
            //MessageBox.Show("Registration succeeded");
            return true;
        }
        public bool DemoSendCapabilityRequest(string act_id, string act2_id, int cnt, string demoServerURL)
        {
            Util.DisplayInfoMessage("Creating the capability request");

            // create the capability request
            ICapabilityRequestOptions options = licensing.LicenseManager.CreateCapabilityRequestOptions();
            options.AddRightsId(act_id, cnt);
            if (act2_id.Trim()!="")
                options.AddRightsId(act2_id, cnt);

            options.Incremental = true;
 //         options.ForceResponse = true;

            ICapabilityRequestData capabilityRequestData = licensing.LicenseManager.CreateCapabilityRequest(options);

            Util.DisplayInfoMessage(String.Format("Sending the capability request to: {0}", demoServerURL));
            byte[] binCapResponse = null;

            // send the capability request to the server and receive the server response
            CommFactory.Create(demoServerURL).SendBinaryMessage(capabilityRequestData.ToArray(), out binCapResponse);
            if (binCapResponse != null && binCapResponse.Length > 0)
            {
                Util.DisplayInfoMessage("Response received");
            }
            if (options.Operation != CapabilityRequestOperation.Preview)
            {
                ProcessCapabilityResponse(binCapResponse);
            }
            else
            {
                ShowPreviewResponse(binCapResponse);
            }
            //MessageBox.Show("Registration succeeded");
            return true;
        }
        public bool DemoUnregister(string act_id, string act2_id, string demoServerURL)
        {
            Util.DisplayInfoMessage("Creating the capability request");
            
            // create the capability request
            ICapabilityRequestOptions options = licensing.LicenseManager.CreateCapabilityRequestOptions();
            options.AddRightsId(act_id, 0);
            if (act2_id.Trim() != "")
                options.AddRightsId(act2_id, 0);
            options.ForceResponse = true;

            ICapabilityRequestData capabilityRequestData = licensing.LicenseManager.CreateCapabilityRequest(options);

            Util.DisplayInfoMessage(String.Format("Sending the capability request to: {0}", demoServerURL));
            byte[] binCapResponse = null;

            // send the capability request to the server and receive the server response
            CommFactory.Create(demoServerURL).SendBinaryMessage(capabilityRequestData.ToArray(), out binCapResponse);
            if (binCapResponse != null && binCapResponse.Length > 0)
            {
                Util.DisplayInfoMessage("Response received");
            }
            if (options.Operation != CapabilityRequestOperation.Preview)
            {
                ProcessCapabilityResponse(binCapResponse);
            }
            else
            {
                ShowPreviewResponse(binCapResponse);
            }
            //MessageBox.Show("Registration succeeded");
            return true;
        }
        private static void ProcessCapabilityResponse(byte[] binCapResponse)
        {
            Util.DisplayInfoMessage("Processing capability response");
            ICapabilityResponse response = licensing.LicenseManager.ProcessCapabilityResponse(binCapResponse);
            Util.DisplayInfoMessage("Capability response processed");
            ShowCapabilityResponseDetails(response);
            ShowTSFeatures();
            IFeatureCollection collection = response.FeatureCollection;
            foreach (IFeature feature in collection)
            {
                AcquireReturn(feature.Name, feature.Version);
            }

            //AcquireReturn(surveyFeature, version);
            //AcquireReturn("FNETestFeature", version);
        }
        public bool DemoProcessCapabilityResponse(string actId, string actId2, string demoFileName)
        {
            // read the capability response from a file and process it
            //MessageBox.Show(String.Format("Reading capability response data from: {0}", demoFileName));
            byte[] binCapResponse = Util.ReadData(demoFileName);
            if (binCapResponse == null)
            {
                return false;
            }
            //Util.DisplayInfoMessage("Processing capability response");
            //MessageBox.Show("Processing capability response");
            ICapabilityResponse response = licensing.LicenseManager.ProcessCapabilityResponse(binCapResponse);
            //Util.DisplayInfoMessage("Capability response processed");
           // MessageBox.Show("Capability response processed");
            ShowCapabilityResponseDetails(response);
            //ShowTSFeatures();
            IFeatureCollection collection = response.FeatureCollection;
            foreach (IFeature feature in collection)
            {
                AcquireReturn(feature.Name, feature.Version);
            }

            return true;
        }
        private static void ShowPreviewResponse(byte[] binCapResponse)
        {
            Util.DisplayInfoMessage("Examining preview capability response");
            ICapabilityResponse response = licensing.LicenseManager.GetResponseDetails(binCapResponse);
            ShowCapabilityResponseDetails(response);
            ShowCapabilityResponseFeatures(response);
        }

        private static void ShowCapabilityResponseDetails(ICapabilityResponse response)
        {
            Util.DisplayInfoMessage("Obtaining capability response details");

            // get machine type from response */
            switch (response.VirtualMachineType)
            {
                case MachineTypeEnum.FLX_MACHINE_TYPE_PHYSICAL:
                    Util.DisplayInfoMessage("Machine type: PHYSICAL");
                    break;
                case MachineTypeEnum.FLX_MACHINE_TYPE_VIRTUAL:
                    Util.DisplayInfoMessage("Machine type: VIRTUAL");
                    // get virtual machine dictionary from response
                    ShowDictionary(response.VirtualMachineInfo, "virtual machine");
                    break;
                case MachineTypeEnum.FLX_MACHINE_TYPE_UNKNOWN:
                default:
                    Util.DisplayInfoMessage("Machine type: UNKNOWN");
                    break;
            }

            // get vendor dictionary from response
            ShowDictionary(response.VendorDictionary, "vendor");

            // get status information
            Util.DisplayInfoMessage(String.Format("Capability response contains {0} status item{1}", 
                response.Status.Count, response.Status.Count == 1 ? String.Empty : "s"));
            if (response.Status.Count > 0)
            {
                foreach (IResponseStatus statusItem in response.Status)
                {
                    Util.DisplayInfoMessage(String.Format("Status - category: {0}, code: {1}{2}, details: {3}",
                        statusItem.TypeDescription, (int)statusItem.Code,
                        String.IsNullOrEmpty(statusItem.CodeDescription) ? String.Empty : " (" + statusItem.CodeDescription + ")",
                        statusItem.Details));
                }
            }

            // determine whether or not a confirmation request is needed
            Util.DisplayInfoMessage(String.Format("Confirmation request is {0}needed", response.ConfirmationRequestNeeded ? String.Empty : "not "));
        }

        private static void ShowDictionary(ReadOnlyDictionary dictionary, string dictionaryType)
        {
            Util.DisplayInfoMessage(String.Format("Capability response contains {0} {1} dictionary item{2}",
                dictionary.Count, dictionaryType, dictionary.Count == 1 ? String.Empty : "s"));
            string itemType;
            foreach (KeyValuePair<string, object> item in dictionary)
            {
                if (item.Value is string)
                {
                    itemType = "string";
                }
                else if (item.Value is int)
                {
                    itemType = "integer";
                }
                else
                {
                    itemType = "unknown";
                }
                Util.DisplayInfoMessage(String.Format("{0} dictionary {1} item type: {2}={3}", 
                                        dictionaryType.Substring(0, 1).ToUpperInvariant() + dictionaryType.Substring(1),                                        
                                        itemType, item.Key, item.Value));
            }
        }

        private static void ShowTSFeatures()
        {
            // display the features found in the trusted storage
            IFeatureCollection collection = licensing.LicenseManager.GetFeatureCollection(LicenseSourceOption.TrustedStorage);
            if (collection.Count == 0)
            {
                //MessageBox.Show("You are not licensed.","FNE Toolkit Demo");
                return;
            }
            //MessageBox.Show("You are licensed.", "FNE Toolkit Demo");
            StringBuilder builder = new StringBuilder();
            builder.Append(String.Format("Features loaded from trusted storage: {0}", collection.Count));
          
            foreach (IFeature feature in collection)
            {
                builder.AppendLine(String.Empty);
                builder.Append(feature.ToString());
            }
             
            Util.DisplayInfoMessage(builder.ToString());
            //MessageBox.Show(builder.ToString());
        }
        public string CheckLicensing()
        {
            if (licensing == null)
            {
                string strPath = Util.GetDefaultTSLocation() + Path.DirectorySeparatorChar;
                // Initialize ILicensing interface with identity data using the Windows common document 
                // respository as the trusted storage location and the hard-coded string hostid "1234567890".
                licensing = LicensingFactory.GetLicensing(
                          IdentityClient.IdentityData,
                          strPath,
                          "");
            }
            IFeatureCollection collection = licensing.LicenseManager.GetFeatureCollection(LicenseSourceOption.TrustedStorage);
            if (collection.Count == 0)
            {

                return "You are not licensed.";
            }
            else
                return "You are licensed. ";
        }
        public string DisplayTrialsFeatures()
        {
            // display the features found in the trusted storage
            IFeatureCollection collection = licensing.LicenseManager.GetFeatureCollection(LicenseSourceOption.Trials);
            if (collection.Count == 0)
            {
                //MessageBox.Show("You are not licensed.", "FNE Toolkit Demo");
                return "No Features in Trials.";
            }
            //MessageBox.Show("You are licensed.", "FNE Toolkit Demo");
            StringBuilder builder = new StringBuilder();
            builder.Append(String.Format("Features loaded from trials: {0}", collection.Count));

            foreach (IFeature feature in collection)
            {
                builder.AppendLine(String.Empty);
                builder.Append(feature.ToString());

                //acquiredLicense = licensing.LicenseManager.Acquire(feature.Name, feature.Version);
                //currentFeature = acquiredLicense.Name;
            }

            Util.DisplayInfoMessage(builder.ToString());
            //MessageBox.Show(builder.ToString());
            return (builder.ToString());
        }
        public string DisplayTSFeatures()
        {
            // display the features found in the trusted storage
            IFeatureCollection collection = licensing.LicenseManager.GetFeatureCollection(LicenseSourceOption.TrustedStorage);
            if (collection.Count == 0)
            {
                //MessageBox.Show("You are not licensed.", "FNE Toolkit Demo");
                return "Trusted Storage is empty.";
            }
            //MessageBox.Show("You are licensed.", "FNE Toolkit Demo");
            StringBuilder builder = new StringBuilder();
            builder.Append(String.Format("Features loaded from trusted storage: {0}", collection.Count));

            foreach (IFeature feature in collection)
            {
                builder.AppendLine(String.Empty);
                builder.Append(feature.ToString());

              //  acquiredLicense = licensing.LicenseManager.Acquire(feature.Name, feature.Version);
              //  currentFeature = acquiredLicense.Name;
            }

            Util.DisplayInfoMessage(builder.ToString());
            //MessageBox.Show(builder.ToString());
            return (builder.ToString());
        }
        private static void ShowCapabilityResponseFeatures(ICapabilityResponse response)
        {
            // display the features found in the capability response
            Util.DisplayInfoMessage("==============================================");
            Util.DisplayInfoMessage(String.Format("Features found in {0}capability response:", response.IsPreview ? "preview " : "") +
                                    Environment.NewLine);

            IFeatureCollection collection = response.FeatureCollection;
            int index = 1;
            foreach (IFeature feature in collection)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(String.Format("{0}: {1} {2}", index, feature.Name, feature.Version));
                if (feature.IsPreview)
                {
                    builder.Append(String.Format(" TYPE=preview COUNT={0} MAXCOUNT={1}", feature.IsUncounted ? "uncounted" : feature.Count.ToString(),
                        feature.MaxCount == feature.UncountedValue ? "uncounted" : feature.MaxCount.ToString()));
                }
                else
                {
                    builder.Append(String.Format(" COUNT={0}", feature.IsUncounted ? "uncounted" : feature.Count.ToString()));
                }
                if (feature.Expiration.HasValue)
                {
                    builder.Append(String.Format(" EXPIRATION={0}", feature.IsPerpetual ? "permanent" : feature.Expiration.ToString()));
                }
                if (feature.IsMetered)
                {
                    builder.Append(String.Format(" MODEL=metered{0}{1}", feature.IsMeteredReusable ? " REUSABLE" : "",
                        feature.MeteredUndoInterval.HasValue ? " UNDO_INTERVAL=" + feature.MeteredUndoInterval.ToString() : ""));
                }
                if (!String.IsNullOrEmpty(feature.VendorString))
                {
                    builder.Append(" VENDOR_STRING=\"" + feature.VendorString + "\"");
                }
                if (!String.IsNullOrEmpty(feature.Issuer))
                {
                    builder.Append(" ISSUER=\"" + feature.Issuer + "\"");
                }
                if (feature.Issued.HasValue)
                {
                    builder.Append(" ISSUED=" + feature.Issued.ToString());
                }
                if (!String.IsNullOrEmpty(feature.Notice))
                {
                    builder.Append(" NOTICE=\"" + feature.Notice + "\"");
                }
                if (!String.IsNullOrEmpty(feature.SerialNumber))
                {
                    builder.Append(" SN=\"" + feature.SerialNumber + "\"");
                }
                if (feature.StartDate.HasValue)
                {
                    builder.Append(" START=" + feature.StartDate.ToString());
                }
                Util.DisplayInfoMessage(builder.ToString());
                index++;
            }
        }

        private static ICapabilityRequestOptions GenerateRequestOptions()
        {
            // create the capability request options object
            ICapabilityRequestOptions options = licensing.LicenseManager.CreateCapabilityRequestOptions();

            // Requesting licenses.
            //
            // Add requested license information here, such as desired features or rights IDs
            //ce4e-155e-f7d0-4399-aca1-6967-9688-9483
            //options.AddRightsId("ce4e-155e-f7d0-4399-aca1-6967-9688-9483", 0);
          options.AddRightsId("4f76-2ba4-ba76-4cb9-9e38-32f0-4781-2ffa", 0);
            // options.AddDesiredFeature(new FeatureData("FNETestFeature", "1.0", 1));
            //
            // Incremental capability requests.
            //
            // When desired features are used in conjunction with an incremental capability request
            // (see the API documentation for ICapabilityRequestOptions.Incremental), the feature count 
            // specified is a request for either an addition to (positive count value) or a subtraction 
            // from (negative count value) the current served count for the specified feature 
            // information. 
            //
            // Previewing available licenses.
            //
            // If not generating a capability request to be serviced by a back-office license server.
            // you may uncomment the following code to create a preview capability request. The license
            // server will return details for the specified features or, if options.RequestAllFeatures is
            // set to true, return details for all features that could potentially be served to this client.
            //
            // Caution: You may not specify desired features and also set 'request all features' to true
            // on the same preview capability request.
            //
            // options.Operation = CapabilityRequestOperation.Preview;
            //
            // Add specific features to preview...
            // options.AddDesiredFeature(new FeatureData(surveyFeature, version, 5));
            //
            // ... or alternatively preview all possible features.
            
           // options.RequestAllFeatures = true;
            //

            // Optionally add capability requeest vendor dictionary items.
            options.AddVendorDictionaryItem(dictionaryKey1, "Some string value");
            options.AddVendorDictionaryItem(dictionaryKey2, 123);

            // force capability response from server even if nothing has changed
            options.ForceResponse = true;

            return options;
        }

        private static void AcquireReturn(string requestedFeature, string requestedVersion)
        {
            // acquire license
            string currentFeature = requestedFeature;
            Util.DisplayInfoMessage(String.Format(attemptingAcquire, requestedFeature, requestedVersion));
            try
            {
                ILicense acquiredLicense = licensing.LicenseManager.Acquire(requestedFeature, requestedVersion);
                try
                {
                    currentFeature = acquiredLicense.Name;
                    Util.DisplayInfoMessage(String.Format(licenseAcquired, currentFeature, requestedVersion));
                    //// application logic here
                }
                finally
                {
                    // return license 
                    Util.DisplayInfoMessage(String.Format(attemptingReturn, currentFeature, requestedVersion));
                 //   acquiredLicense.ReturnLicense();
                    Util.DisplayInfoMessage(String.Format(licenseReturned, currentFeature, requestedVersion));
                }
            }
            catch (Exception exc)
            {
                HandleException(exc);
            }
        }
        ILicense acquiredLicense;
        public bool DemoAcquire(string feature, string version)
        {
            try
            {
                // attempt to obtain version 1.0 (or greater) of the requested feature
                acquiredLicense = licensing.LicenseManager.Acquire(feature, version);

                    currentFeature = acquiredLicense.Name;
                    string currentVersion = acquiredLicense.Version;
                    Util.DisplayInfoMessage(String.Format(licenseAcquired, currentFeature, currentVersion));
                    //// application logic here
 
            }
            catch (Exception exc)
            {
                HandleException(exc);
            }
            return true;
        }
        public bool DemoReturn(string feature, string version)
        {
            try
            {
                // attempt to obtain version 1.0 (or greater) of the requested feature
                //   System.Collections.ObjectModel.ReadOnlyCollection<ILicense> collection = licensing.LicenseManager.Licenses();
                acquiredLicense.ReturnLicense();

                System.Collections.ObjectModel.ReadOnlyCollection<ILicense> licenses = licensing.LicenseManager.Licenses();
                foreach (ILicense l in licenses)
                {
                    //more complex check
                    //if (l.Version==version)
                    //ILicense currentLicense =l;
                    if (l.Name==feature)
                       l.ReturnLicense();
                }


            }
            catch (Exception exc)
            {
                HandleException(exc);
            }
            return true;
        }
        private static void HandleException(Exception exc)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(String.Format("{0} encountered: ", exc.GetType()));
            PublicLicensingException flxException = exc as PublicLicensingException;
            if (flxException != null)
            {
                switch (flxException.ErrorCode)
                {
                    case ErrorCode.FLXERR_RESPONSE_STALE:
                    case ErrorCode.FLXERR_RESPONSE_EXPIRED:
                    case ErrorCode.FLXERR_CAPABILITY_RESPONSE_DATA_MISSING:
                    case ErrorCode.FLXERR_PREVIEW_RESPONSE_NOT_PROCESSED:
                        builder.Append(String.Format("{0}: {1}", processingCapabilityResponse, flxException));
                        break;
                    case ErrorCode.FLXERR_FEATURE_NOT_FOUND:
                        builder.Append(String.Format("{0} {1}: {2}", acquiringLicense, currentFeature, flxException));
                        break;
                    default:
                        builder.Append(flxException.ToString());
                        break;
                }
            }
            else
            {
                builder.Append(exc.Message);
            }
            Util.DisplayErrorMessage(builder.ToString());
        }

        private static void Usage(string applicationName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(String.Empty);
            builder.AppendLine(String.Format("{0} [-generate outputfile]", applicationName));
            builder.AppendLine(String.Format("{0} [-process inputfile]", applicationName));
            builder.AppendLine(String.Format("{0} [-server url]", applicationName));
            builder.AppendLine(String.Empty);
            builder.AppendLine("where:");
	        builder.AppendLine("-generate Generates capability request into a file.");
            builder.AppendLine("-process  Processes capability response from a file.");
            builder.AppendLine("-server   Sends request to a server and processes the response.");
            builder.AppendLine("          For the test back-office server, use");
            builder.AppendLine("          http://hostname:8080/request.");
            builder.AppendLine("          For FlexNet Operations, use");
            builder.AppendLine("          http://hostname:8888/flexnet/deviceservices.");
            builder.AppendLine("          For FlexNet Embedded License Server, use");
            builder.AppendLine("          http://hostname:7070/fne/bin/capability.");
            builder.AppendLine("          For Cloud License Server, use");
            builder.AppendLine("          https://<tenant>.compliance.flexnetoperations.com/instances/<instance-id>/request.");
            builder.AppendLine("          For FNO Cloud, use");
            builder.AppendLine("          https://<tenant>.compliance.flexnetoperations.com/deviceservices.");

            Util.DisplayMessage(builder.ToString(), "USAGE");
        }
    }
    public class LicenseInfo
    {
        private string name;
        private string version;
        private int count;

        public LicenseInfo(string name, string version, int count)
        {
            this.name = name;
            this.version = version;
            this.count = count;
        }

        public string Name
        {
            get { return name; }
        }

        public string Version
        {
            get { return version; }
        }

        public int Count
        {
            get { return count; }
        }
    }

}