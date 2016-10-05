using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWASP_CommitsDashBoard {
    static class MediaWiki {


        public static System.IO.StreamWriter file;

        public static void TableHeader () {

            file = new System.IO.StreamWriter(@"C:\Users\xxxxxxxx\WikiDashBoard.txt");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"{| class=""wikitable""");
            sb.AppendLine("|-");
            sb.AppendLine("! Repository Owner");
            sb.AppendLine("! Repository Name");
            sb.AppendLine("! Description");
            sb.AppendLine("! Lanuage");
            sb.AppendLine("! Open Issues Count");
            sb.AppendLine("! Last Activity Date");
            sb.AppendLine("! Commit Last Date");
            sb.AppendLine("! Commit Author");
            sb.AppendLine("|-");
            file.WriteLine(sb.ToString());
        }

        public static void TableRow () {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, DashBoardData> entry in Program.dictionary) {
                sb.Append("|");
                sb.AppendLine(entry.Value.Repo_User);

                sb.Append("|");
                sb.AppendLine(entry.Value.Repo_Name);

                sb.Append("|");
                sb.AppendLine(entry.Value.Description);

                sb.Append("|");
                sb.AppendLine(entry.Value.Lanuage);

                sb.Append("|");
                sb.AppendLine(entry.Value.OpenIssuesCount.ToString());

                sb.Append("|");
                sb.AppendLine(entry.Value.Repo_LastActivityDate.ToString("g"));

                sb.Append("|");
                sb.AppendLine(entry.Value.Commit_LastActivityDate.ToString("g"));

                sb.Append("|");
                sb.AppendLine(entry.Value.Commit_Author);

 
                sb.AppendLine("|-");
                file.WriteLine(sb.ToString());
                sb.Clear();
            }
            file.WriteLine("|}");  // needed for end of table
            file.Flush();

        }

    } // end of class
}// end of namespace



