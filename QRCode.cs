using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Холст_для_QR
{
    class QRCode
    {
        
        public int Version { get; set; }
        public string Code { get; set; }

        public QRCode(int type, int version, string code)
        {
     
            Version = version;
            Code = code;
        }
    }
}
