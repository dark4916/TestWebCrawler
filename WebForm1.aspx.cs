using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Http;
using AngleSharp;
using AngleSharp.Dom;


namespace TestWebCrawler
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        String orgUrl = "http://www.3375.com.tw";

        protected void Page_Load(object sender, EventArgs e)
        {
            String Url = "http://www.3375.com.tw/Menu/FoodMenu";
           

            IDocument doc = null;
            if (GetHttpDocument(Url, ref doc))
            {
                PostGridView(doc);
            }
        }

        public Boolean GetHttpDocument(String Url,ref IDocument doc)
        {
            HttpClient httpClient = new HttpClient();

            //發送請求並取得回應內容
            var responMessage = httpClient.GetAsync(Url).Result;

            //檢查回應的伺服器狀態StatusCode是否是200 OK
            if (responMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //讀取Content內容
                string responseResult = responMessage.Content.ReadAsStringAsync().Result;
                //Console.WriteLine(responResult);

                //使用AngleSharp時的前置設定
                var config = Configuration.Default;
                var context = BrowsingContext.New(config);

                //傳入response資料
                //將我們用httpclient拿到的資料放入res.Content中()
                doc = context.OpenAsync(res => res.Content(responseResult)).Result;
                return true;
            }
            else
                return false;
        }

        public void PostGridView(IDocument doc) 
        {
            DataTable FoodList = new DataTable();
            FoodList.Columns.Add("ItemName");
            FoodList.Columns.Add("ItemFacts");
            FoodList.Columns.Add("ItemPrice");
            FoodList.Columns.Add("ItemPicture");

            var ItemName = doc.QuerySelectorAll("div.pro_text-wrap > h3 ");
            var ItemFacts = doc.QuerySelectorAll("div.pro_text-wrap > p");
            var ItemPrice = doc.QuerySelectorAll("sub.prize");
            var ItemPicture = doc.QuerySelectorAll("div.pro-img");

            for (int i = 0; i < ItemName.Length; i++)
            {
                //var Name = ItemName[i].TextContent.Replace("\n","");
                var Name = ItemName[i].TextContent.Replace(ItemName[i].GetElementsByClassName("prize")[0].TextContent, "");
                var Facts = ItemFacts[i].TextContent.Replace("\n", "");
                var Price = ItemPrice[i].TextContent.Replace("\n", "");
                var UrlPicture = ItemPicture[i].GetElementsByClassName("img-responsive")[0].GetAttribute("src");

                DataRow NewData = FoodList.NewRow();
                NewData["ItemName"] = Name;
                NewData["ItemFacts"] = Facts;
                NewData["ItemPrice"] = Price;
                NewData["ItemPicture"] = orgUrl + UrlPicture.Trim();
                FoodList.Rows.Add(NewData);
            }

            GridView1.DataSource = FoodList;
            GridView1.DataBind();
            
        }



    }

}