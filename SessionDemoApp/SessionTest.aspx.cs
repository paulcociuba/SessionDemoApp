//base libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//added imports


namespace SessionDemoApp
{
    public partial class SessionTest : System.Web.UI.Page
    {
        //page variables
        private List<String> applicationSessionContent;
        private List<String> applicationAppContent;

        protected void Page_Load(object sender, EventArgs e)
        {
            //show session content
            RefreshContent();
        }


        protected void cmdAddSessionContent_Click(object sender, EventArgs e)
        {
            //check if the sessionContent variable is empty, and if so create a new list
            if (applicationSessionContent == null)
            {
                applicationSessionContent = new List<String>();
            }

            //append the content from the textbox - with the date and time
            applicationSessionContent.Add(DateTime.Now.Date.ToShortDateString() + " : " +
                DateTime.Now.ToShortTimeString() + " : " + " AppDomainID : " + AppDomain.CurrentDomain.Id +
                " ThreadID : " + System.Threading.Thread.CurrentThread.ManagedThreadId + " : " + txtAddSessionContent.Text.Trim());

            //save the content to the session
            Session["appSessionContent"] = applicationSessionContent;


            //clear the textbox
            txtAddSessionContent.Text = String.Empty;

            //show session content
            RefreshContent();
        }



        protected void cmdAddApplicationContent_Click(object sender, EventArgs e)
        {
            //check if the sessionContent variable is empty, and if so create a new list
            if (applicationAppContent == null)
            {
                applicationAppContent = new List<String>();
            }

            //append the content from the textbox - with the date and time
            applicationAppContent.Add(DateTime.Now.Date.ToShortDateString() + " : " +
                DateTime.Now.ToShortTimeString() + " : " + " AppDomainID : " + AppDomain.CurrentDomain.Id +
                " ThreadID : " + System.Threading.Thread.CurrentThread.ManagedThreadId + " : " + txtAddApplicationContent.Text.Trim());

            //save the content to the session
            Application["appContent"] = applicationAppContent;

            //clear the textbox
            txtAddApplicationContent.Text = String.Empty;

            //show session content
            RefreshContent();
        }


        private void RefreshContent()
        {
            //clear any text in the textbox
            txtSessionContent.Text = String.Empty;

            //attempt to access a List<String> variable from the sesison
            if (Session["appSessionContent"] != null)
            {
                //attempt to cast the list to the page variable
                applicationSessionContent = ((List<String>)Session["appSessionContent"]);

                //line by line append the strings from the sessionContent
                //variable and insert environment returns at the end of each
                //string
                foreach (String s in applicationSessionContent)
                {
                    txtSessionContent.Text = txtSessionContent.Text + s + "\r\n";
                }
            }
            else
            {
                //show that we could not retrieve any session content
                txtSessionContent.Text = "!!! Error !!! : Session content is empty.";
            }


            //clear any text in the textbox
            txtApplicationContent.Text = String.Empty;

            //attempt to access a List<String> variable from the sesison
            if (Application["appContent"] != null)
            {
                //attempt to cast the list to the page variable
                applicationAppContent = ((List<String>)Application["appContent"]);

                //line by line append the strings from the sessionContent
                //variable and insert environment returns at the end of each
                //string
                foreach (String s in applicationAppContent)
                {
                    txtApplicationContent.Text = txtApplicationContent.Text + s + "\r\n";
                }
            }
            else
            {
                //show that we could not retrieve any session content
                txtApplicationContent.Text = "!!! Error !!! : Application content is empty.";
            }
        }


        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            //show session content
            RefreshContent();
        }
    }
}