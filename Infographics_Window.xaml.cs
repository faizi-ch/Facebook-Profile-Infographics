using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Facebook;
using System.Web.Script.Serialization;
using System.Drawing.Drawing2D;
using System.Net;
using System.IO;
using System.Drawing.Imaging;
using DevExpress.Xpf.Core;


namespace Facebook_Profile_Infographics
{
    /// <summary>
    /// Interaction logic for Infographics_Window.xaml
    /// </summary>
    public partial class Infographics_Window : DXWindow
    {
        string clientToken = string.Empty;
        public Infographics_Window(string token)
        {
            InitializeComponent();

            clientToken = token;
        }

        private void GetUserDetails()
        {
            FacebookClient fb = null;
            object data = null;
            DVO.UserDvo user = null;
            try
            {
                fb = new FacebookClient(clientToken);
                data = fb.Get("/me?fields=devices,first_name,gender,installed,last_name,link,locale,location");
                user = new DVO.UserDvo();
                user = new JavaScriptSerializer().Deserialize & lt; DVO.UserDvo & gt; (data.ToString());
                profilePic.Image = DownloadImage("https://graph.facebook.com/v2.5/" + user.id + "/picture?height=100&amp;width=100&amp;type=album&amp;access_token=" + clientToken);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fb = null;
                data = null;
                user = null;
            }

        }
        private static Image DownloadImage(string url)
        {
            WebClient wc = null;
            Image img = null;
            MemoryStream ms = null;
            try
            {
                wc = new WebClient();
                byte[] bytes = wc.DownloadData(url);
                ms = new MemoryStream(bytes);
                img = System.Drawing.Image.FromStream(ms);
                return img;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                wc.Dispose();
                ms.Dispose();
            }

        }


    }
    public class UserDvo
    {
        public Cover cover { get; set; }
        public List&lt;Device&gt; devices { get; set; }
    public string first_name { get; set; }
    public string gender { get; set; }
    public bool installed { get; set; }
    public string last_name { get; set; }
    public string link { get; set; }
    public Location location { get; set; }
    public string id { get; set; }

}

public class Cover
{
    public string id { get; set; }
    public int offset_y { get; set; }
    public string source { get; set; }
}

public class Data
{
    public bool is_silhouette { get; set; }
    public string url { get; set; }
}

public class Picture
{
    public Data data { get; set; }
}
public class Device
{
    public string hardware { get; set; }
    public string os { get; set; }
}

public class Location
{
    public string id { get; set; }
    public string name { get; set; }
}
}
