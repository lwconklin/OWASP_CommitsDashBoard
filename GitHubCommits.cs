using System.Net;
using System.Text;
using System.IO;
using System;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace OWASP_CommitsDashBoard {
    class GitHubCommits {

        public void Commits ( DashBoardData dbData ) {
            GitHubListCommits(dbData);
        }

        private static HttpWebRequest GitHubURL_ListCommits (string name) {
            String url = String.Format("https://api.github.com/repos/owasp/{0}/commits",name);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/6.0;)";
            return request;
        }

        private void GitHubListCommits ( DashBoardData dbData) {
            try {
                Stream resStream = null;
                resStream = GitHubCommits.GitHubURL_ListCommits(dbData.Repo_Name).GetResponse().GetResponseStream();
                JArray jsonVal = JArray.Parse(Request2String.ConvertRequest2String(resStream)) as JArray;
                dynamic commits = jsonVal;
                foreach (dynamic commit in commits) {
                    dbData.Commit_Author = commit.commit.committer.name;
                    dbData.Commit_LastActivityDate = commit.commit.committer.date;
                    try {
                        Program.dictionary.Add(dbData.Repo_Name, dbData);
                    } catch (Exception e) {
                        System.Console.WriteLine(e.Message);
                        Environment.Exit(-1);
                    }
                    break; // First commit in json array will be last commit made for the project
                }
            }
            catch (Exception e) {
                dbData.Commit_Author = string.Empty;
                dbData.Commit_LastActivityDate = DateTime.MinValue;
                try {
                Program.dictionary.Add(dbData.Repo_Name, dbData);
            }catch(ArgumentException ) { // we can ignore this
                   
            }
            }
        }

    }// end of class
}// end of namespace
