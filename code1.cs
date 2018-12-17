using System;
using System.Net;
using HtmlAgilityPack;

					
public class Program
{
	public static void Main()
	{
		using (WebClient wc = new WebClient()){
			wc.Encoding = System.Text.Encoding.UTF8;			
			wc.Headers.Add(HttpRequestHeader.Cookie, "over18=1"); 
			string result = wc.DownloadString("https://www.ptt.cc/bbs/Gossiping/");
			
			HtmlDocument htmlDoc = new HtmlDocument();
		    htmlDoc.LoadHtml(result);
			
			try{
				
			for (int i=2; i<=50; i++){
				
		    var title = htmlDoc.DocumentNode.SelectNodes(@"//*[@id=""main-container""]/div[2]/div[" + i + "]/div[2]");  //Title
				if (title[0].InnerText.IndexOf("(已被", StringComparison.OrdinalIgnoreCase) >= 0 || title[0].InnerText.IndexOf("本文已被刪除", StringComparison.OrdinalIgnoreCase) >= 0 )
				{
				 //do nothing
				}
				else {
				string currenttitle = title[0].InnerHtml.Replace("\n","");
				int titlestart = currenttitle.IndexOf("html\">")+8;
				int titleend = currenttitle.IndexOf("</a>");
				int urlstart = currenttitle.IndexOf("href=")+8;
				int urlend = currenttitle.IndexOf(".html")+7;
			    string title2 = title[0].InnerHtml.Substring(titlestart, titleend - titlestart);
				string url = title[0].InnerHtml.Substring(urlstart, urlend - urlstart);
		
		        Console.WriteLine(title2 + ", " + "https://www.ptt.cc" + url);
					
				}
			}
			
			}
		    catch{
				}
			
		
		}
		
	}
}
