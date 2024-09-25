using ProgramareAC.Web.Services.Msign.Client;
using System.Collections.Generic;

namespace ProgramareAC.Services.MSign.Model
{
    public class SignPackSaveModel
    {
        public List<SignPackSaveItem> Items { get; set; }

        public SignPackSaveModel(SignResult[] msignResults)
        {
            Items = new List<SignPackSaveItem>();

            foreach (SignResult msignResult in msignResults)
            {

                XAdES XAdESInfo = new XAdES(msignResult.Signature);

                SignPackSaveItem packItem = new SignPackSaveItem(msignResult.Signature, XAdESInfo.SubjectIDNP, XAdESInfo.SubjectName, XAdESInfo.SignTimeStamp);

                Items.Add(packItem);
            }
        }
    }

}
