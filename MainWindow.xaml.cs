using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using Facebook;

namespace Facebook_Profile_Infographics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXWindow
    {
        private string access_token = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DXWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string fb =
                @"https://www.facebook.com/dialog/oauth?client_id=xxxxxxxxxxxxxxxxx&redirect_uri=https://www.facebook.com/connect/login_success.html&response_type=token";
            webBrowser.Navigate(fb);
        }
        

        private void webBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            try
            {
                // whenever the browser navigates to a new url, try parsing the url.
                // the url may be the result of OAuth 2.0 authentication.

                var fb = new FacebookClient();
                FacebookOAuthResult oauthResult;
                if (fb.TryParseOAuthCallbackUrl(e.Uri, out oauthResult))
                {
                    // The url is the result of OAuth 2.0 authentication
                    if (oauthResult.IsSuccess)
                    {
                        //Getting the AccessToken and Redirecting user to the Second form.
                        var accesstoken = oauthResult.AccessToken;
                        //Infographics_Window wall = new Infographics_Window(accesstoken.ToString());
                        //wall.Show();
                        this.Hide();
                    }
                    else
                    {
                        var errorDescription = oauthResult.ErrorDescription;
                        var errorReason = oauthResult.ErrorReason;
                    }
                }
                else
                {
                    // The url is NOT the result of OAuth 2.0 authentication.
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }

        private void webBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            //MessageBox.Show(webBrowser.Source.AbsoluteUri);
            if (webBrowser.Source.AbsoluteUri.Contains("access_token"))
            {
                string url1 = webBrowser.Source.AbsoluteUri;
                string url2 = url1.Substring(url1.IndexOf("access_token")+13);
                access_token = url2.Substring(0, url2.IndexOf("&"));
                //MessageBox.Show(access_token);

                InfographicsWindow infographicsWindow=new InfographicsWindow(access_token);
                this.Hide();
                infographicsWindow.Show();
            }
        }
    }
}
