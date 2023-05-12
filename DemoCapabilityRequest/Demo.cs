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
    Demo.cs
    
    This example program allows you to:
    1. Read trial information from a file into a trial storage.
    2. Send a request via http to the local license server.
    3. Send a capability request via http to the server and process response,
       saving data into trusted storage.
    4. Write capability request to a file. This request can be fed into server
       to generate response.
    5. Read capability response from a file and process accordingly.
    6. Send a request via http to the cloud license server.
     
*****************************************************************************/

namespace CapabilityRequest
{


    public class Demo
    {
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
        private static string currentFeature = String.Empty;

        private static readonly List<LicenseInfo> licenses = new List<LicenseInfo>();
        private static ILicensing licensing;
        private static Dictionary<HostIdEnum, List<String>> hostIDs;
        [STAThread]
        public static void Main()
        {

            if (IdentityClient.IdentityData == null || IdentityClient.IdentityData.Length == 0)
            {
                Console.WriteLine(emptyIdentity);
                return;
            }

            

            try
            {
                //string strPath = Util.GetDefaultTSLocation() + Path.DirectorySeparatorChar;
                //string strPath  = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + Path.DirectorySeparatorChar;
                //string strPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar;
                string strPath = "C:\\temp" + Path.DirectorySeparatorChar;
                // Initialize ILicensing interface with identity data using the Windows common document 
                // respository as the trusted storage location and the hard-coded string hostid "1234567890".
                licensing = LicensingFactory.GetLicensing(IdentityClient.IdentityData, strPath, "");

               // licensing.Administration.Delete(DeleteOption.TrustedStorage);
                hostIDs = licensing.LicenseManager.HostIds;
                var keys = hostIDs.Keys;
                //load trial data if not already loaded
                //    TrialEaton trialEaton = new TrialEaton();
                //    byte[] trialData = trialEaton.TrialData;
                //    licensing.LicenseManager.ProcessTrial(trialData);

                string hostID = hostIDs[HostIdEnum.FLX_HOSTID_TYPE_ETHERNET][0];
                licensing = LicensingFactory.GetLicensing(IdentityClient.IdentityData, strPath, hostID, "Server0");


                List<String> ethernetIDs = hostIDs[HostIdEnum.FLX_HOSTID_TYPE_ETHERNET];
                    //Chapter 6 Using the FlexNet Embedded APIs
                    // Common Steps to Prepare for Licensing

                    // The optional host name is typically set by a user as a friendly name for the host.  
                    // The host name is not used for license enforcement.                  
                    // The host type is typically a name set by the implementer, and is not modifiable by the user.
                    // While optional, the host type may be used in certain scenarios by some back-office systems such as FlexNet Operations.
                    licensing.LicenseManager.HostType = "FLX_CLIENT";
                   // licensing.LicenseManager.AddTrustedStorageLicenseSource();
                   // licensing.LicenseManager.AddTrialLicenseSource();
            }
            catch (Exception exc)
            {
                HandleException(exc);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DemoUserLicense());
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

        public bool ReturnTrials()
        {
            licensing.Administration.Delete(DeleteOption.Trials);
            
            return true;
        }
        public Dictionary<HostIdEnum, List<String>> AvailableHostIDs()
        {
            return hostIDs;
        }
        public string DemoSelectHost(FlxDotNetClient.HostIdEnum hostId)
        {
            if (hostIDs.ContainsKey(hostId))
                return hostIDs[hostId][0];
            else
                return "key not found";
        }
        public void ResetTS()
        {
           bool res = licensing.Administration.Delete(DeleteOption.TrustedStorage);
        }
        public void BufferLicensingHost(string hostId)
        {
            licensing = LicensingFactory.GetLicensing(IdentityClient.IdentityData, null,hostId);
            licensing.LicenseManager.HostType = "FLX_CLIENT";
 
        }
        public void DemoRefreshLicensingHost(string hostId)
        {

            //string strPath = Util.GetDefaultTSLocation() + Path.DirectorySeparatorChar;
            string strPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar;
            licensing = LicensingFactory.GetLicensing(IdentityClient.IdentityData, strPath, hostId);
            licensing.LicenseManager.HostType = "FLX_CLIENT";
        }
        //  ****To-Do: change "buffer1" -> filename
        public string DemoLoadLicenseFile(string licFileName)
        {
            if (!String.IsNullOrEmpty(licFileName))
            {
                IFeatureCollection temp = licensing.LicenseManager.GetFeatureCollection("buffer1");
                if (temp.Count == 0)
                {
                    Util.DisplayInfoMessage(String.Format("Reading data from {0}", licFileName));
                    byte[] inputFileData = Util.ReadData(licFileName);
                    licensing.LicenseManager.AddBufferLicenseSource(inputFileData, "buffer1");
                }
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
                    return exc.ToString();
                }
            }

            return "";
        }
        public string DemoProcessTrialFile(string trialFile)
        {
            // licensing.LicenseManager.ReturnAllLicenses();
            string txtRet;

            DateTime expirationDate;
            if (licensing.LicenseManager.TrialIsLoaded(trialFile, out expirationDate))
            {
                if (expirationDate.CompareTo(DateTime.Now) > 0)
                {
                    txtRet = String.Format("Trial has already been loaded and will expire on {0}", expirationDate);
                }
                else
                {
                    txtRet = "Trial has already been loaded and has expired";
                }
                // return txtRet;
            }

            // process trial into trials license source
            try
            {
                licensing.LicenseManager.ProcessTrial(trialFile);

                /**          IFeatureCollection collection = licensing.LicenseManager.GetFeatureCollection(trialFile);
                          foreach (IFeature feature in collection)
                          {
                              licenses.Add(new LicenseInfo(feature.Name, feature.Version, 1));
                              try
                              {
                                  ILicense acquiredLicense = licensing.LicenseManager.Acquire(feature.Name, feature.Version);
                              }
                              catch (Exception exc)
                              {
                                  HandleException(exc);
                                  return exc.ToString();
                              }
                          }
                **/
            }
            catch (PublicLicensingException licensingException)
            {
                switch (licensingException.ErrorCode)
                {
                    case ErrorCode.FLXERR_TRIAL_ALREADY_LOADED:
                    case ErrorCode.FLXERR_TRIAL_EXPIRED:
                        HandleException(licensingException);
                        return licensingException.ToString();
                    default:
                        throw licensingException;
                }

            }

            return "Trial File loaded";
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
        public bool DemoGenerateCapabilityRequest(string act_id, string act2_id, int cnt1, int cnt2, string demoFileName)
        {
            // saving the capablity request to a file
            // create the capability request
            ICapabilityRequestOptions options = licensing.LicenseManager.CreateCapabilityRequestOptions();
            options.AddRightsId(act_id, cnt1);
            if (act2_id.Trim() != "")
                options.AddRightsId(act2_id, cnt2);

            //options.AddVendorDictionaryItem(dictionaryKey1, "Some string value");
            //options.AddVendorDictionaryItem(dictionaryKey2, 123);
            //options.Incremental = true;
            //options.ForceResponse = true;
            ICapabilityRequestData capabilityRequestData = licensing.LicenseManager.CreateCapabilityRequest(options);
            if (File.Exists(demoFileName))
            {
                File.Delete(demoFileName);
            }
            if (Util.WriteData(demoFileName, capabilityRequestData.ToArray()))
            {
                //MessageBox.Show(String.Format("Capability request data written to: {0}", demoFileName));
            }
            return true;
        }
        public bool DemoSendCapabilityFeatureRequest(string feature, string version, int cnt, string demoServerURL)
        {
            Util.DisplayInfoMessage("Creating the capability request");

            // create the capability request
            ICapabilityRequestOptions options = licensing.LicenseManager.CreateCapabilityRequestOptions();
            options.AddDesiredFeature(new FeatureData(feature, version, cnt));
            //if (act2_id.Trim() != "")
            //    options.AddRightsId(act2_id, cnt);

            //options.Incremental = true;
            //options.ForceResponse = true;

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
        public bool DemoSendCapabilityFeatureTimedRequest(string feature, string version, int cnt, string demoServerURL)
        {
            Util.DisplayInfoMessage("Creating the capability request");

            // create the capability request
            ICapabilityRequestOptions options = licensing.LicenseManager.CreateCapabilityRequestOptions();
            DateTime todayDate = DateTime.Now;
            DateTime expiryDate = todayDate.AddDays(5);
            //DateTime expiryDate = todayDate.AddMinutes(5);

            options.AddDesiredFeature(new FeatureData(feature, version, cnt));

            //options.AddDesiredFeature(new FeatureData(feature, version, cnt,expiryDate));
            //if (act2_id.Trim() != "")
            //    options.AddRightsId(act2_id, cnt);

            //options.Incremental = true;
            //options.ForceResponse = true;
            //options.RequestAllFeatures = true;
            options.AcquisitionId = "XXX";
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
        public bool DemoSendCapabilityRequest(string act_id, string act2_id, string act3_id, int cnt1, int cnt2, int cnt3, string demoServerURL)
        {
            Util.DisplayInfoMessage("Creating the capability request");

            // create the capability request
            ICapabilityRequestOptions options = licensing.LicenseManager.CreateCapabilityRequestOptions();
            options.AddRightsId(act_id, cnt1);
            if (act2_id.Trim() != "")
                options.AddRightsId(act2_id, cnt2);
            if (act3_id.Trim() != "")
                options.AddRightsId(act3_id, cnt3);

            
            //options.Incremental = true;
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
        private string ShowPreviewResponse(byte[] binCapResponse)
        {
            Util.DisplayInfoMessage("Examining preview capability response");
            ICapabilityResponse response = licensing.LicenseManager.GetResponseDetails(binCapResponse);
            string res = "";
            ShowCapabilityResponseDetails(response);
            res+=ShowCapabilityResponseFeatures(response);

            return res;
        }
        private string ShowCapabilityResponseFeatures(ICapabilityResponse response)
        {
            // display the features found in the capability response
            Util.DisplayInfoMessage("==============================================");
            Util.DisplayInfoMessage(String.Format("Features found in {0}capability response:", response.IsPreview ? "preview " : "") +
                                    Environment.NewLine);

            IFeatureCollection collection = response.FeatureCollection;
            int index = 1;
            string str = "";
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
                str += builder.AppendLine(string.Empty);
            }

            return str;
        }
        private static void ShowTSFeatures()
        {
            // display the features found in the trusted storage
            IFeatureCollection collection = licensing.LicenseManager.GetFeatureCollection(LicenseSourceOption.TrustedStorage, true);
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
            IFeatureCollection collection = licensing.LicenseManager.GetFeatureCollection(LicenseSourceOption.TrustedStorage, true);
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
        public string Preview(string serverURL)
        {
            ICapabilityRequestOptions options = licensing.LicenseManager.CreateCapabilityRequestOptions();
            options.Operation = CapabilityRequestOperation.Preview;
            options.RequestAllFeatures = true;
            ICapabilityRequestData capabilityRequestData = licensing.LicenseManager.CreateCapabilityRequest(options);
            byte[] binCapResponse = null;
            // send the capability request to the server and receive the server response
            CommFactory.Create(serverURL).SendBinaryMessage(capabilityRequestData.ToArray(), out binCapResponse);
            if (binCapResponse != null && binCapResponse.Length > 0)
            {
                Util.DisplayInfoMessage("Response received");
            }
            string ret = ShowPreviewResponse(binCapResponse);
            return ret;
        }

    }
}
