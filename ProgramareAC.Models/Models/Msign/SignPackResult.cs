using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramareAC.Services.MSign.Model
{
    public class SignPackResult
    {
        public DateTime SignDate { get; set; }

        public string SignPattern { get; set; }

        public string SignerFullName { get; set; }

        public string SingerIdnp { get; set; }

        public byte[] Sing { get; set; }

        public string OrarId { get; set; }

    }
}