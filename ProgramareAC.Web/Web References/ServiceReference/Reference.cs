﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace ProgramareAC.Web.ServiceReference {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WSO2_package_017ACSOAP11Binding", Namespace="http://ws.wso2.org/dataservice")]
    public partial class WSO2_package_017AC : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback proc_testOperationCompleted;
        
        private System.Threading.SendOrPostCallback get_FreeTimeOperationCompleted;
        
        private System.Threading.SendOrPostCallback set_TimeOperationCompleted;
        
        private System.Threading.SendOrPostCallback get_RNOperationCompleted;
        
        private System.Threading.SendOrPostCallback get_ServOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WSO2_package_017AC() {
            this.Url = global::ProgramareAC.Web.Properties.Settings.Default.ProgramareAC_Web_ServiceReference_WSO2_package_017AC;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event proc_testCompletedEventHandler proc_testCompleted;
        
        /// <remarks/>
        public event get_FreeTimeCompletedEventHandler get_FreeTimeCompleted;
        
        /// <remarks/>
        public event set_TimeCompletedEventHandler set_TimeCompleted;
        
        /// <remarks/>
        public event get_RNCompletedEventHandler get_RNCompleted;
        
        /// <remarks/>
        public event get_ServCompletedEventHandler get_ServCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:proc_test", RequestNamespace="http://ws.wso2.org/dataservice", ResponseElementName="Group_proc_test", ResponseNamespace="http://ws.wso2.org/dataservice", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("p1", IsNullable=true)]
        public string proc_test() {
            object[] results = this.Invoke("proc_test", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void proc_testAsync() {
            this.proc_testAsync(null);
        }
        
        /// <remarks/>
        public void proc_testAsync(object userState) {
            if ((this.proc_testOperationCompleted == null)) {
                this.proc_testOperationCompleted = new System.Threading.SendOrPostCallback(this.Onproc_testOperationCompleted);
            }
            this.InvokeAsync("proc_test", new object[0], this.proc_testOperationCompleted, userState);
        }
        
        private void Onproc_testOperationCompleted(object arg) {
            if ((this.proc_testCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.proc_testCompleted(this, new proc_testCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:get_FreeTime", RequestNamespace="http://ws.wso2.org/dataservice", ResponseElementName="Group_get_FreeTime", ResponseNamespace="http://ws.wso2.org/dataservice", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("Row")]
        public Row[] get_FreeTime([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string Branch_code, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] System.Nullable<decimal> Serv_rf) {
            object[] results = this.Invoke("get_FreeTime", new object[] {
                        Branch_code,
                        Serv_rf});
            return ((Row[])(results[0]));
        }
        
        /// <remarks/>
        public void get_FreeTimeAsync(string Branch_code, System.Nullable<decimal> Serv_rf) {
            this.get_FreeTimeAsync(Branch_code, Serv_rf, null);
        }
        
        /// <remarks/>
        public void get_FreeTimeAsync(string Branch_code, System.Nullable<decimal> Serv_rf, object userState) {
            if ((this.get_FreeTimeOperationCompleted == null)) {
                this.get_FreeTimeOperationCompleted = new System.Threading.SendOrPostCallback(this.Onget_FreeTimeOperationCompleted);
            }
            this.InvokeAsync("get_FreeTime", new object[] {
                        Branch_code,
                        Serv_rf}, this.get_FreeTimeOperationCompleted, userState);
        }
        
        private void Onget_FreeTimeOperationCompleted(object arg) {
            if ((this.get_FreeTimeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.get_FreeTimeCompleted(this, new get_FreeTimeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:set_Time", RequestNamespace="http://ws.wso2.org/dataservice", ResponseElementName="Rez", ResponseNamespace="http://ws.wso2.org/dataservice", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("Row")]
        public Row2[] set_Time([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string IDNP, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string LastName, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string FirstName, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string BirthDate, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] System.Nullable<decimal> Orar_ID, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] System.Nullable<decimal> Serv_rf, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] System.Nullable<decimal> RN, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string pEmail, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string pTel, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string pTema) {
            object[] results = this.Invoke("set_Time", new object[] {
                        IDNP,
                        LastName,
                        FirstName,
                        BirthDate,
                        Orar_ID,
                        Serv_rf,
                        RN,
                        pEmail,
                        pTel,
                        pTema});
            return ((Row2[])(results[0]));
        }
        
        /// <remarks/>
        public void set_TimeAsync(string IDNP, string LastName, string FirstName, string BirthDate, System.Nullable<decimal> Orar_ID, System.Nullable<decimal> Serv_rf, System.Nullable<decimal> RN, string pEmail, string pTel, string pTema) {
            this.set_TimeAsync(IDNP, LastName, FirstName, BirthDate, Orar_ID, Serv_rf, RN, pEmail, pTel, pTema, null);
        }
        
        /// <remarks/>
        public void set_TimeAsync(string IDNP, string LastName, string FirstName, string BirthDate, System.Nullable<decimal> Orar_ID, System.Nullable<decimal> Serv_rf, System.Nullable<decimal> RN, string pEmail, string pTel, string pTema, object userState) {
            if ((this.set_TimeOperationCompleted == null)) {
                this.set_TimeOperationCompleted = new System.Threading.SendOrPostCallback(this.Onset_TimeOperationCompleted);
            }
            this.InvokeAsync("set_Time", new object[] {
                        IDNP,
                        LastName,
                        FirstName,
                        BirthDate,
                        Orar_ID,
                        Serv_rf,
                        RN,
                        pEmail,
                        pTel,
                        pTema}, this.set_TimeOperationCompleted, userState);
        }
        
        private void Onset_TimeOperationCompleted(object arg) {
            if ((this.set_TimeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.set_TimeCompleted(this, new set_TimeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:get_RN", RequestNamespace="http://ws.wso2.org/dataservice", ResponseElementName="Group_get_RN", ResponseNamespace="http://ws.wso2.org/dataservice", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("Row")]
        public Row3[] get_RN() {
            object[] results = this.Invoke("get_RN", new object[0]);
            return ((Row3[])(results[0]));
        }
        
        /// <remarks/>
        public void get_RNAsync() {
            this.get_RNAsync(null);
        }
        
        /// <remarks/>
        public void get_RNAsync(object userState) {
            if ((this.get_RNOperationCompleted == null)) {
                this.get_RNOperationCompleted = new System.Threading.SendOrPostCallback(this.Onget_RNOperationCompleted);
            }
            this.InvokeAsync("get_RN", new object[0], this.get_RNOperationCompleted, userState);
        }
        
        private void Onget_RNOperationCompleted(object arg) {
            if ((this.get_RNCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.get_RNCompleted(this, new get_RNCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:get_Serv", RequestNamespace="http://ws.wso2.org/dataservice", ResponseElementName="Group_get_Serv", ResponseNamespace="http://ws.wso2.org/dataservice", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("Row")]
        public Row4[] get_Serv([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] System.Nullable<decimal> RN) {
            object[] results = this.Invoke("get_Serv", new object[] {
                        RN});
            return ((Row4[])(results[0]));
        }
        
        /// <remarks/>
        public void get_ServAsync(System.Nullable<decimal> RN) {
            this.get_ServAsync(RN, null);
        }
        
        /// <remarks/>
        public void get_ServAsync(System.Nullable<decimal> RN, object userState) {
            if ((this.get_ServOperationCompleted == null)) {
                this.get_ServOperationCompleted = new System.Threading.SendOrPostCallback(this.Onget_ServOperationCompleted);
            }
            this.InvokeAsync("get_Serv", new object[] {
                        RN}, this.get_ServOperationCompleted, userState);
        }
        
        private void Onget_ServOperationCompleted(object arg) {
            if ((this.get_ServCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.get_ServCompleted(this, new get_ServCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ws.wso2.org/dataservice")]
    public partial class Row {
        
        private string orar_idField;
        
        private string orar_dayField;
        
        private string orar_timeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", IsNullable=true)]
        public string orar_id {
            get {
                return this.orar_idField;
            }
            set {
                this.orar_idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string orar_day {
            get {
                return this.orar_dayField;
            }
            set {
                this.orar_dayField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string orar_time {
            get {
                return this.orar_timeField;
            }
            set {
                this.orar_timeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ws.wso2.org/dataservice")]
    public partial class Row4 {
        
        private string sERV_IDField;
        
        private string sERV_NAME_ROField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string SERV_ID {
            get {
                return this.sERV_IDField;
            }
            set {
                this.sERV_IDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string SERV_NAME_RO {
            get {
                return this.sERV_NAME_ROField;
            }
            set {
                this.sERV_NAME_ROField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ws.wso2.org/dataservice")]
    public partial class Row3 {
        
        private System.Nullable<decimal> rnField;
        
        private string nAMERField;
        
        private string aDRESField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> RN {
            get {
                return this.rnField;
            }
            set {
                this.rnField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string NAMER {
            get {
                return this.nAMERField;
            }
            set {
                this.nAMERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string ADRES {
            get {
                return this.aDRESField;
            }
            set {
                this.aDRESField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ws.wso2.org/dataservice")]
    public partial class Row2 {
        
        private string rezult_TextField;
        
        private System.Nullable<decimal> errorField;
        
        private string pCERERE_IDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Rezult_Text {
            get {
                return this.rezult_TextField;
            }
            set {
                this.rezult_TextField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> Error {
            get {
                return this.errorField;
            }
            set {
                this.errorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", IsNullable=true)]
        public string pCERERE_ID {
            get {
                return this.pCERERE_IDField;
            }
            set {
                this.pCERERE_IDField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void proc_testCompletedEventHandler(object sender, proc_testCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class proc_testCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal proc_testCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void get_FreeTimeCompletedEventHandler(object sender, get_FreeTimeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class get_FreeTimeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal get_FreeTimeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Row[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Row[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void set_TimeCompletedEventHandler(object sender, set_TimeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class set_TimeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal set_TimeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Row2[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Row2[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void get_RNCompletedEventHandler(object sender, get_RNCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class get_RNCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal get_RNCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Row3[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Row3[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void get_ServCompletedEventHandler(object sender, get_ServCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class get_ServCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal get_ServCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Row4[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Row4[])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591