//-----------------------------------------------------------------------
// <copyright file="EmailBuilderFull.cs" company="Genesys Source">
//      Copyright (c) 2016 Genesys Source. All rights reserved.
// 
//      Licensed to the Apache Software Foundation (ASF) under one or more 
//      contributor license agreements.  See the NOTICE file distributed with 
//      this work for additional information regarding copyright ownership.
//      The ASF licenses this file to You under the Apache License, Version 2.0 
//      (the 'License'); you may not use this file except in compliance with 
//      the License.  You may obtain a copy of the License at 
//       
//        http://www.apache.org/licenses/LICENSE-2.0 
//       
//       Unless required by applicable law or agreed to in writing, software  
//       distributed under the License is distributed on an 'AS IS' BASIS, 
//       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  
//       See the License for the specific language governing permissions and  
//       limitations under the License. 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using Genesys.Extensions;
using Genesys.Extras.Text;

namespace Genesys.Extras.Net
{
    /// <summary>
    /// Forms and sends emails. Supports HTML templates.
    /// </summary>
    [CLSCompliant(true)]
    public class EmailBuilder
    {
        /// <summary>
        /// Legal footer required on every email
        /// </summary>
        public class FooterLegal
        {
            /// <summary>
            /// ApplicationID
            /// </summary>
            public Guid ApplicationID { get; set; } = TypeExtension.DefaultGuid;
            /// <summary>
            /// PublishFooter
            /// </summary>
            public bool PublishFooter { get; set; } = TypeExtension.DefaultBoolean;
            /// <summary>
            /// ToEmailAddress
            /// </summary>
            public string ToEmailAddress { get; set; } = TypeExtension.DefaultString; // 0
            /// <summary>
            /// CompanyFriendlyName
            /// </summary>
            public string CompanyFriendlyName { get; set; } = TypeExtension.DefaultString; // 1
            /// <summary>
            /// UnsubscribeURL
            /// </summary>
            public string UnsubscribeURL { get; set; } = TypeExtension.DefaultString; // 2
            /// <summary>
            /// CompanyLegalName
            /// </summary>
            public string CompanyLegalName { get; set; } = TypeExtension.DefaultString; // 3
            /// <summary>
            /// Address
            /// </summary>
            public string Address { get; set; } = TypeExtension.DefaultString; // 4
            
            /// <summary>
            /// Constructor
            /// </summary>
            public FooterLegal() : base() { }

            /// <summary>
            /// Constructor
            /// </summary>
            public FooterLegal(string toEmailAddress, string companyFriendlyName, string unsubscribeURL, string companyLegalName, string address, Guid applicationID)
            {
                this.PublishFooter = true;
                this.ToEmailAddress = toEmailAddress;
                this.CompanyFriendlyName = companyFriendlyName;
                this.UnsubscribeURL = unsubscribeURL;
                this.CompanyLegalName = companyLegalName;
                this.Address = address;
                this.ApplicationID = applicationID;
            }
            
            /// <summary>
            /// Returns as array that fills data in the footer
            /// </summary>
            /// <returns>List of strings with fully formed email Html</returns>
            public List<String> ToList()
            {
                List<String> returnValue = new List<String>();
                this.PublishFooter = true;
                returnValue.Add(this.ToEmailAddress);
                returnValue.Add(this.CompanyFriendlyName);
                returnValue.Add(this.UnsubscribeURL);
                returnValue.Add(this.CompanyLegalName);
                returnValue.Add(this.Address);
                return returnValue;
            }

            /// <summary>
            /// Returns HTML footer with all data
            /// </summary>
            /// <returns>Footer portion of email</returns>
            public string ToFooter()
            {
                string returnValue = TypeExtension.DefaultString;
                TemplateBuilder builder = new TemplateBuilder(Genesys.Extras.Properties.Resources.LegalFooter, this.ToList());
                this.PublishFooter = true;
                returnValue = builder.ToString();
                return returnValue;
            }
            
        }
        
        /// <summary>
        /// SendCompletedCallbackDelegate
        /// </summary>
        /// <param name="sender">Sender information</param>
        /// <param name="e">Event argument data</param>
        public delegate void SendCompletedCallbackDelegate(object sender, AsyncCompletedEventArgs e);
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation. 
            string token = Convert.ToString(e.UserState);

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public EmailBuilder() : base()
        {
        }
        
        /// <summary>
        /// Sends a mail based on off SMTP settings in .config
        /// </summary>
        /// <param name="mailToAddresses"></param>
        /// <param name="title"></param>
        /// <param name="contents"></param>
        /// <param name="legal"></param>
        /// <param name="addressSeperator"></param>
        /// <returns></returns>
        public KeyValuePair<string, bool> Send(string mailToAddresses, string title, string contents, FooterLegal legal, char addressSeperator = ';')
        {
            
            KeyValuePair<string, bool> returnValue = new KeyValuePair<string, bool>();
            System.Collections.Generic.List<string> mailTo = new System.Collections.Generic.List<string>();

            // The Mail To Addresses will be separated by ;
            mailTo = new List<string>(mailToAddresses.Trim().Split(new char[] { addressSeperator }));
            returnValue = this.Send(mailTo, title, contents, legal, SendCompletedCallback, TypeExtension.DefaultString);

            // Return success/failure
            return returnValue;
        }

        /// <summary>
        /// Sends a mail based on off SMTP settings in .config
        /// </summary>
        /// <param name="mailToAddresses"></param>
        /// <param name="title"></param>
        /// <param name="contents"></param>
        /// <param name="legal"></param>
        /// <param name="callback"></param>
        /// <param name="callbackData"></param>
        /// <param name="addressSeperator"></param>
        /// <returns>Email addresses and their send status (true or false)</returns>
        public KeyValuePair<String, Boolean> Send(System.Collections.Generic.List<String> mailToAddresses, string title, string contents, FooterLegal legal, 
            SendCompletedCallbackDelegate callback, string callbackData, char addressSeperator = ';')
        {
            KeyValuePair<String, bool> returnValue = new KeyValuePair<String, bool>();
            SmtpClient client = new SmtpClient();
            string footer = TypeExtension.DefaultString;
            List<String> toAddresses = new List<String>();

            try
            {
                // Never batch send for legal reasons. Have to put email in the footer of every email
                foreach (string emailAddress in mailToAddresses)
                {
                    if (emailAddress.IsEmail(false) == true)
                    {
                        // Will get 'from, etc' from .config file
                        MailMessage OutgoingMail = new MailMessage();
                        OutgoingMail.To.Add(new MailAddress(emailAddress));
                        OutgoingMail.Subject = title;
                        OutgoingMail.Body = contents + (legal.PublishFooter ? legal.ToFooter() : TypeExtension.DefaultString);
                        OutgoingMail.IsBodyHtml = true;
                        OutgoingMail.Priority = MailPriority.Normal;
                        client.SendCompleted += SendCompletedCallback;
                        client.Send(OutgoingMail);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Dispose();
            }

            // Return success/failre
            return returnValue;
        }
        
        /// <summary>
        /// Sends an email one at a time, with variable data for each message body
        /// Uses String.Format to handle a template.
        ///    Dim Template As string = "Confirmation email for {0}. " + _
        ///          "Date: {1}. Weeks: {2}\n" + _
        ///          "Name: {3}\n" + _
        ///          "{4}"
        ///    String.Format(Template, "Spot1", Date.UtcNow, "Spot3", "Spot4")
        /// </summary>
        /// <param name="mailToAddressesAndData">email address and all the data for that email message</param>
        /// <param name="title"></param>
        /// <param name="contents"></param>
        /// <param name="legal"></param>
        /// <param name="callback"></param>
        /// <param name="callbackData"></param>
        /// <param name="addressSeperator"></param>
        /// <returns></returns>
        public Dictionary<String, Boolean> SendTemplate(List<KeyValuePair<String, List<String>>> mailToAddressesAndData, string title, string contents,
                                            FooterLegal legal, SendCompletedCallbackDelegate callback, string callbackData, char addressSeperator = ';')
        {
            Dictionary<String, Boolean> returnValue = new Dictionary<String, Boolean>();
            KeyValuePair<String, Boolean> mailResult = new KeyValuePair<String, Boolean>();
            System.Collections.Generic.List<String> mailTo = new System.Collections.Generic.List<String>();
            string titleFilled = TypeExtension.DefaultString;

            foreach (KeyValuePair<string, List<String>> Item_loopVariable in mailToAddressesAndData)
            {
                mailTo.Clear();
                mailTo.Add(Item_loopVariable.Key);
                contents = new TemplateBuilder(contents, Item_loopVariable.Value).ToString();
                mailResult = this.Send(mailTo, title, contents, legal, callback, callbackData);
                returnValue.Add(mailResult.Key, mailResult.Value);
            }

            return returnValue;
        }
    }
}
