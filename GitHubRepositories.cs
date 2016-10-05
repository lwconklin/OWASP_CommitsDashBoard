using System.Net;
using System.Text;
using System.IO;
using System;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace OWASP_CommitsDashBoard {

    public struct DashBoardData {
        public string Repo_User {get; set;}
        public string Repo_Name {get;  set;}
        public string Description {get; set;}
        public string Lanuage {  get; set; }
        public int OpenIssuesCount {get; set;}
        public DateTime Repo_LastActivityDate { get; set; }
        public DateTime Commit_LastActivityDate { get; set;}
        public string Commit_Author { get;set;}
    }// end of struct


    class GitHubRepositories {

        private static HttpWebRequest GitHubURL_ListRepositories() {
           
            string url = @"https://api.github.com/users/owasp/repos";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add(HttpRequestHeader.Authorization, "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("user:pwd")));
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/6.0;)";
            return request;
        }
        
        private static HttpWebRequest GitHubURL_SearchRepositories (int pageNumber) {
            String url = String.Format("https://api.github.com/search/repositories?q=owasp&page={0}", pageNumber);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add(HttpRequestHeader.Authorization, "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("user:pwd")));
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/6.0;)";
            return request;
        }

        private static HttpWebRequest GitHubURL_ARepository (string user, string repository) {
            String url = String.Format("https://api.github.com/repos/{0}/{1}", user,repository);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add(HttpRequestHeader.Authorization, "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("user:pwd")));
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/6.0;)";
            return request;
        }

        /// <summary>
        /// Find all respositories with login name of owasp
        /// This routine does not use pagination
        /// </summary>
        public void GitHubListRepositories () {
            try {
                GitHubCommits githubCommits = new GitHubCommits();
                Stream resStream = null;
                resStream = GitHubRepositories.GitHubURL_ListRepositories().GetResponse().GetResponseStream();
                JArray jsonVal = JArray.Parse(Request2String.ConvertRequest2String(resStream)) as JArray;
                dynamic repos = jsonVal;
                foreach (dynamic repo in repos) {
                    DashBoardData dbData = new DashBoardData();
                    if (repo.name == string.Empty) {
                        throw new ArgumentNullException();
                    }
                    dbData.Repo_User = "OWASP";
                    dbData.Repo_Name = repo.name;
                    dbData.Description = repo.description;
                    dbData.OpenIssuesCount = repo.open_issues_count;
                    dbData.Lanuage = repo.language;
                    dbData.Repo_LastActivityDate = repo.updated_at;
                    githubCommits.Commits(dbData);
                }
                GitHubFindRespositories();  // find repositories where the user name is not "OWASP"
            }catch(Exception e) {
                System.Console.WriteLine(e.Message);
                Environment.Exit(-1);
            }
        }

        /// <summary>
        /// Find OWASP respositories via search, repositories with login name of owasp will not be added.
        /// Pagination is in use 
        /// </summary>
        private void GitHubFindRespositories () {
            try {
                for (int pageNumber = 1; pageNumber < 32; pageNumber++) {
                    GitHubCommits githubCommits = new GitHubCommits();
                    Stream resStream = null;
                    resStream = GitHubRepositories.GitHubURL_SearchRepositories(pageNumber).GetResponse().GetResponseStream();
                    string s1 = Request2String.ConvertRequest2String(resStream);
                    JObject results = JObject.Parse(s1);
                    foreach (var result in results["items"]) {
                        Console.WriteLine("start specific repo" + "  " + (string)result["name"]);
                        string name = (string)result["name"];
                        string owner = (string)result["owner"]["login"];
                        if (!owner.ToLower().Equals("owasp")) {
                            resStream = GitHubRepositories.GitHubURL_ARepository(owner, name).GetResponse().GetResponseStream();
                            string s2 = Request2String.ConvertRequest2String(resStream);
                            JObject repo = JObject.Parse(s2);
                            DashBoardData dbData = new DashBoardData();
                            dbData.Repo_User = (string)repo["owner"]["login"];
                            dbData.Repo_Name = (string)repo["name"];
                            dbData.Description = (string)repo["description"];
                            dbData.OpenIssuesCount = (int)repo["open_issues_count"];
                            dbData.Lanuage = (string)repo["lanuage"];
                            dbData.Repo_LastActivityDate = (DateTime)repo["updated_at"];
                            System.Console.WriteLine(dbData.Repo_Name);
                            githubCommits.Commits(dbData);
                        }
                        Console.WriteLine("end specific repo");
                    }
                    Console.WriteLine("End GitHubFindRespositories()");
                }
            }
            catch (Exception e) {
                System.Console.WriteLine(e.Message);
                Environment.Exit(-1);
            }
        }
        
    } // end of class
} // end of namespace




