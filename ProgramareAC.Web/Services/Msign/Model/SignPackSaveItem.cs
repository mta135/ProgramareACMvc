using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramareAC.Services.MSign.Model
{
    public class SignPackSaveItem
    {

        public byte[] XadesBytes { get; set; }

        public string SignerFullName { get; set; }
        public string SignerIDNP { get; set; }

        public DateTime SignDate { get; set; }

        public SignPackSaveItem() { }
        public SignPackSaveItem(byte[] xadesBytes, string signerIDNP, string signerFullName, DateTime signDate)
        {

            XadesBytes = xadesBytes;

            SignerIDNP = signerIDNP;
            SignerFullName = signerFullName;

            SignDate = signDate;
        }
    }
}