using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Utils.OAuth.Provider;
using DevExpress.Xpf.Core;
using Facebook;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Facebook_Profile_Infographics
{
    /// <summary>
    /// Interaction logic for InfographicsWindow.xaml
    /// </summary>
    public partial class InfographicsWindow : DXWindow
    {
        private string access_token;
        public InfographicsWindow(string at)
        {
            InitializeComponent();

            access_token = at;

            getData();}

        void getData()
        {
            var facebookClient = new FacebookClient(access_token);
            dynamic friends = facebookClient.Get("/me/albums?fields=count,name");

            //JObject friendListJson = JObject.Parse(friends.ToString());
            //dynamic friendListJson = JsonConvert.DeserializeObject(friends.ToString());
            //ListBoxEdit.Items.Add(friendListJson["first_name"]);

            /*foreach (var item in list)
            {
                //ListBoxEdit.Items.Add(f["count"].ToString());
                //ListBoxEdit.Items.Add(f.count+f.name+f.id);
                //Console.WriteLine("{0} \n", item.count);
                //MessageBox.Show(f["first_name"].ToString());
            }*/



            /*foreach (var data in friends.data)
                {
                    //var name = data.name;
                    var c = data.count;
                    var n = data.name;
                    var id = data.id;
                    ListBoxEdit.Items.Add(c.ToString());
                    Console.WriteLine("{0} {1} {2} \n", c.ToString(), n.ToString(),id.ToString());


                }*/
            dynamic resultt = facebookClient.Get("/me/permissions");
            MessageBox.Show(resultt.ToString());
            var result = (JsonObject)facebookClient.Get("me/friends");
            var model = new List<Friend>();

            foreach (var friend in (JsonArray) result["data"])
            {
                model.Add(new Friend()
                {
                    Id = (string)(((JsonObject)friend)["id"]),
                    Name = (string)(((JsonObject)friend)["name"])
                });
                }
            foreach (var s in model)
            {
                ListBoxEdit.Items.Add(s.Name);
            }
        }
        public class Friend
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
        public class FBitem
        {
            public List<MyItem> data { get; set; }
        }
        public class MyItem
        {
            public string id;
            public string name;
            public string count;
        }
    }
}

