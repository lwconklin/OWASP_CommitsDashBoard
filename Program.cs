using System;
using System.Collections.Generic;


namespace OWASP_CommitsDashBoard {
    class Program {
        public static Dictionary<string, DashBoardData> dictionary;

        static void Main ( string[] args ) {
           dictionary = new Dictionary<string, DashBoardData>();
            GitHubRepositories githubRepositories = new GitHubRepositories();
            try {

                githubRepositories.GitHubListRepositories();

                MediaWiki.TableHeader();
                MediaWiki.TableRow();

                CSV.CSVHeader();
                CSV.CSVRow();

            }
            catch(Exception e) {
                System.Console.Out.WriteLine(e.Message);
                Environment.Exit(-1);
            }
        }


    } // end of class
} // end of namespace
