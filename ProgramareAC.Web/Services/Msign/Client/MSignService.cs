﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProgramareAC.Web.Services.Msign.Client
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SignRequest", Namespace="https://msign.gov.md")]
    public partial class SignRequest : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string ContentDescriptionField;
        
        private ProgramareAC.Web.Services.Msign.Client.ContentType ContentTypeField;
        
        private ProgramareAC.Web.Services.Msign.Client.SignContent[] ContentsField;
        
        private ProgramareAC.Web.Services.Msign.Client.ExpectedSigner ExpectedSignerField;
        
        private string ShortContentDescriptionField;
        
        private string SignatureReasonField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string ContentDescription
        {
            get
            {
                return this.ContentDescriptionField;
            }
            set
            {
                this.ContentDescriptionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public ProgramareAC.Web.Services.Msign.Client.ContentType ContentType
        {
            get
            {
                return this.ContentTypeField;
            }
            set
            {
                this.ContentTypeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public ProgramareAC.Web.Services.Msign.Client.SignContent[] Contents
        {
            get
            {
                return this.ContentsField;
            }
            set
            {
                this.ContentsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ProgramareAC.Web.Services.Msign.Client.ExpectedSigner ExpectedSigner
        {
            get
            {
                return this.ExpectedSignerField;
            }
            set
            {
                this.ExpectedSignerField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string ShortContentDescription
        {
            get
            {
                return this.ShortContentDescriptionField;
            }
            set
            {
                this.ShortContentDescriptionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string SignatureReason
        {
            get
            {
                return this.SignatureReasonField;
            }
            set
            {
                this.SignatureReasonField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ExpectedSigner", Namespace="https://msign.gov.md")]
    public partial class ExpectedSigner : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int DelegatedRoleIDField;
        
        private string DelegatorIDField;
        
        private ProgramareAC.Web.Services.Msign.Client.DelegatorType DelegatorTypeField;
        
        private string IDField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public int DelegatedRoleID
        {
            get
            {
                return this.DelegatedRoleIDField;
            }
            set
            {
                this.DelegatedRoleIDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string DelegatorID
        {
            get
            {
                return this.DelegatorIDField;
            }
            set
            {
                this.DelegatorIDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ProgramareAC.Web.Services.Msign.Client.DelegatorType DelegatorType
        {
            get
            {
                return this.DelegatorTypeField;
            }
            set
            {
                this.DelegatorTypeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ContentType", Namespace="https://msign.gov.md")]
    public enum ContentType : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Hash = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Pdf = 2,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SignContent", Namespace="https://msign.gov.md")]
    public partial class SignContent : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private byte[] ContentField;
        
        private string CorrelationIDField;
        
        private bool MultipleSignaturesField;
        
        private string NameField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public byte[] Content
        {
            get
            {
                return this.ContentField;
            }
            set
            {
                this.ContentField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string CorrelationID
        {
            get
            {
                return this.CorrelationIDField;
            }
            set
            {
                this.CorrelationIDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public bool MultipleSignatures
        {
            get
            {
                return this.MultipleSignaturesField;
            }
            set
            {
                this.MultipleSignaturesField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DelegatorType", Namespace="https://msign.gov.md")]
    public enum DelegatorType : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Person = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Organization = 2,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SignResponse", Namespace="https://msign.gov.md")]
    public partial class SignResponse : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string MessageField;
        
        private ProgramareAC.Web.Services.Msign.Client.SignResult[] ResultsField;
        
        private ProgramareAC.Web.Services.Msign.Client.SignStatus StatusField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                this.MessageField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public ProgramareAC.Web.Services.Msign.Client.SignResult[] Results
        {
            get
            {
                return this.ResultsField;
            }
            set
            {
                this.ResultsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public ProgramareAC.Web.Services.Msign.Client.SignStatus Status
        {
            get
            {
                return this.StatusField;
            }
            set
            {
                this.StatusField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SignResult", Namespace="https://msign.gov.md")]
    public partial class SignResult : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private byte[] CertificateField;
        
        private string CorrelationIDField;
        
        private byte[] SignatureField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public byte[] Certificate
        {
            get
            {
                return this.CertificateField;
            }
            set
            {
                this.CertificateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string CorrelationID
        {
            get
            {
                return this.CorrelationIDField;
            }
            set
            {
                this.CorrelationIDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public byte[] Signature
        {
            get
            {
                return this.SignatureField;
            }
            set
            {
                this.SignatureField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SignStatus", Namespace="https://msign.gov.md")]
    public enum SignStatus : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Pending = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Success = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Failure = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Expired = 3,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="VerificationRequest", Namespace="https://msign.gov.md")]
    public partial class VerificationRequest : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private ProgramareAC.Web.Services.Msign.Client.VerificationContent[] ContentsField;
        
        private string LanguageField;
        
        private ProgramareAC.Web.Services.Msign.Client.ContentType SignedContentTypeField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public ProgramareAC.Web.Services.Msign.Client.VerificationContent[] Contents
        {
            get
            {
                return this.ContentsField;
            }
            set
            {
                this.ContentsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Language
        {
            get
            {
                return this.LanguageField;
            }
            set
            {
                this.LanguageField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public ProgramareAC.Web.Services.Msign.Client.ContentType SignedContentType
        {
            get
            {
                return this.SignedContentTypeField;
            }
            set
            {
                this.SignedContentTypeField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="VerificationContent", Namespace="https://msign.gov.md")]
    public partial class VerificationContent : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private byte[] ContentField;
        
        private string CorrelationIDField;
        
        private byte[] SignatureField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public byte[] Content
        {
            get
            {
                return this.ContentField;
            }
            set
            {
                this.ContentField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string CorrelationID
        {
            get
            {
                return this.CorrelationIDField;
            }
            set
            {
                this.CorrelationIDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public byte[] Signature
        {
            get
            {
                return this.SignatureField;
            }
            set
            {
                this.SignatureField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="VerificationResponse", Namespace="https://msign.gov.md")]
    public partial class VerificationResponse : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private ProgramareAC.Web.Services.Msign.Client.VerificationResult[] ResultsField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public ProgramareAC.Web.Services.Msign.Client.VerificationResult[] Results
        {
            get
            {
                return this.ResultsField;
            }
            set
            {
                this.ResultsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="VerificationResult", Namespace="https://msign.gov.md")]
    public partial class VerificationResult : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private ProgramareAC.Web.Services.Msign.Client.VerificationCertificate[] CertificatesField;
        
        private string CorrelationIDField;
        
        private string MessageField;
        
        private bool SignaturesValidField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ProgramareAC.Web.Services.Msign.Client.VerificationCertificate[] Certificates
        {
            get
            {
                return this.CertificatesField;
            }
            set
            {
                this.CertificatesField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string CorrelationID
        {
            get
            {
                return this.CorrelationIDField;
            }
            set
            {
                this.CorrelationIDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                this.MessageField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public bool SignaturesValid
        {
            get
            {
                return this.SignaturesValidField;
            }
            set
            {
                this.SignaturesValidField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="VerificationCertificate", Namespace="https://msign.gov.md")]
    public partial class VerificationCertificate : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private byte[] CertificateField;
        
        private bool SignatureValidField;
        
        private System.Nullable<System.DateTime> SignedAtField;
        
        private string SubjectField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public byte[] Certificate
        {
            get
            {
                return this.CertificateField;
            }
            set
            {
                this.CertificateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public bool SignatureValid
        {
            get
            {
                return this.SignatureValidField;
            }
            set
            {
                this.SignatureValidField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public System.Nullable<System.DateTime> SignedAt
        {
            get
            {
                return this.SignedAtField;
            }
            set
            {
                this.SignedAtField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string Subject
        {
            get
            {
                return this.SubjectField;
            }
            set
            {
                this.SubjectField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="https://msign.gov.md", ConfigurationName="ProgramareAC.Web.Services.Msign.Client.IMSign")]
    public interface IMSign
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="https://msign.gov.md/IMSign/PostSignRequest", ReplyAction="https://msign.gov.md/IMSign/PostSignRequestResponse")]
        string PostSignRequest(ProgramareAC.Web.Services.Msign.Client.SignRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://msign.gov.md/IMSign/PostSignRequest", ReplyAction="https://msign.gov.md/IMSign/PostSignRequestResponse")]
        System.Threading.Tasks.Task<string> PostSignRequestAsync(ProgramareAC.Web.Services.Msign.Client.SignRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://msign.gov.md/IMSign/GetSignResponse", ReplyAction="https://msign.gov.md/IMSign/GetSignResponseResponse")]
        ProgramareAC.Web.Services.Msign.Client.SignResponse GetSignResponse(string requestID, string language);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://msign.gov.md/IMSign/GetSignResponse", ReplyAction="https://msign.gov.md/IMSign/GetSignResponseResponse")]
        System.Threading.Tasks.Task<ProgramareAC.Web.Services.Msign.Client.SignResponse> GetSignResponseAsync(string requestID, string language);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://msign.gov.md/IMSign/VerifySignatures", ReplyAction="https://msign.gov.md/IMSign/VerifySignaturesResponse")]
        ProgramareAC.Web.Services.Msign.Client.VerificationResponse VerifySignatures(ProgramareAC.Web.Services.Msign.Client.VerificationRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://msign.gov.md/IMSign/VerifySignatures", ReplyAction="https://msign.gov.md/IMSign/VerifySignaturesResponse")]
        System.Threading.Tasks.Task<ProgramareAC.Web.Services.Msign.Client.VerificationResponse> VerifySignaturesAsync(ProgramareAC.Web.Services.Msign.Client.VerificationRequest request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMSignChannel : ProgramareAC.Web.Services.Msign.Client.IMSign, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MSignClient : System.ServiceModel.ClientBase<ProgramareAC.Web.Services.Msign.Client.IMSign>, ProgramareAC.Web.Services.Msign.Client.IMSign
    {
        
        public MSignClient()
        {
        }
        
        public MSignClient(string endpointConfigurationName) : 
                base(endpointConfigurationName)
        {
        }
        
        public MSignClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public MSignClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public MSignClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public string PostSignRequest(ProgramareAC.Web.Services.Msign.Client.SignRequest request)
        {
            return base.Channel.PostSignRequest(request);
        }
        
        public System.Threading.Tasks.Task<string> PostSignRequestAsync(ProgramareAC.Web.Services.Msign.Client.SignRequest request)
        {
            return base.Channel.PostSignRequestAsync(request);
        }
        
        public ProgramareAC.Web.Services.Msign.Client.SignResponse GetSignResponse(string requestID, string language)
        {
            return base.Channel.GetSignResponse(requestID, language);
        }
        
        public System.Threading.Tasks.Task<ProgramareAC.Web.Services.Msign.Client.SignResponse> GetSignResponseAsync(string requestID, string language)
        {
            return base.Channel.GetSignResponseAsync(requestID, language);
        }
        
        public ProgramareAC.Web.Services.Msign.Client.VerificationResponse VerifySignatures(ProgramareAC.Web.Services.Msign.Client.VerificationRequest request)
        {
            return base.Channel.VerifySignatures(request);
        }
        
        public System.Threading.Tasks.Task<ProgramareAC.Web.Services.Msign.Client.VerificationResponse> VerifySignaturesAsync(ProgramareAC.Web.Services.Msign.Client.VerificationRequest request)
        {
            return base.Channel.VerifySignaturesAsync(request);
        }
    }
}
