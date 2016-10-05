using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OWASP_CommitsDashBoard {
    class Request2String {

        public static string ConvertRequest2String (Stream resStream) {
            const int maxReadCharacters = 5125; //5k
            StringBuilder sb = new StringBuilder();
            StreamReader reader = new StreamReader(resStream, Encoding.UTF8);
            Char[] read = new Char[maxReadCharacters];
            int count = reader.Read(read, 0, maxReadCharacters);
            while (count > 0) {
                sb.Append(read, 0, count);
                count = reader.Read(read, 0, maxReadCharacters);
            }
            return sb.ToString();
        }
    } // end of class
}// end of namespace
