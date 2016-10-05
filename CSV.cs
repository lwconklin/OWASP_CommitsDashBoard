using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OWASP_CommitsDashBoard {
    static class CSV {

    public static System.IO.StreamWriter csv;

    public static void CSVHeader () {

        csv = new System.IO.StreamWriter(@"C:\Users\*********\OWASPDashBoard.csv");
        StringBuilder sb = new StringBuilder();
        sb.Append("Repository Owner");
        sb.Append(",Repository Name");
        sb.Append(",Description");
        sb.Append(",Lanuage");
        sb.Append(",Open Issues Count");
        sb.Append(",Last Activity Date");
        sb.Append(",Commit Last Date");
        sb.Append(",Commit Author");
        csv.WriteLine(sb.ToString());
    }
        public static void CSVRow () {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, DashBoardData> entry in Program.dictionary) {
                sb.Append(entry.Value.Repo_User);
                sb.Append(",");
                sb.Append(entry.Value.Repo_Name);
                sb.Append(",");
                sb.Append(entry.Value.Description);
                sb.Append(",");
                sb.Append(entry.Value.Lanuage);
                sb.Append(",");
                sb.Append(entry.Value.OpenIssuesCount.ToString());
                sb.Append(",");
                sb.Append(entry.Value.Repo_LastActivityDate.ToString("g"));
                sb.Append(",");
                sb.Append(entry.Value.Commit_LastActivityDate.ToString("g"));
                sb.Append(",");
                sb.Append(entry.Value.Commit_Author);
                csv.WriteLine(sb.ToString());
                sb.Clear();
            }
            csv.Flush();

        }

    } //end of class
}// end of namespace
