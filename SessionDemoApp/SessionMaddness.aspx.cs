//base libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//added imports
using Utilities;

namespace SessionDemoApp
{
    public partial class SessionMaddness : System.Web.UI.Page
    {
        //page level variables
        private SessionContentManager manager;
        private ServiceManager serviceManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            //display the current session Identifier
            lblSessionID.Text = Session.SessionID;

            //also load the PID of the worker process
            lblProcessID.Text = System.Diagnostics.Process.GetCurrentProcess().Id.ToString();

            //should the page be posting back, create an instance of Session Manager
            if (Page.IsPostBack)
            {
                manager = new SessionContentManager(Session);
                serviceManager = new ServiceManager();
            }
            else
            {
                //check the session if it has content anyways - if there is contents in the session
                if (Session.Contents.Count != 0)
                {
                    manager = new SessionContentManager(Session);
                    //display any content inside the textbox
                    txtSessionContents.Text = manager.RetrieveSessionContent();
                }
            }
        }

        #region Session Content Add / Display handling code

        protected void cmdAddContent_Click(object sender, EventArgs e)
        {
            //attempt to add the content if the session manager is not null and if the
            //content textbox is not null
            if (manager != null && !String.IsNullOrWhiteSpace(txtSessionAdd.Text))
            {
                string resultingContent = manager.AddContents(txtSessionAdd.Text.Trim());

                //clear the add textbox
                txtSessionAdd.Text = String.Empty;

                //display the session's content
                txtSessionContents.Text = resultingContent;
            }
        }

        protected void cmdRefreshContent_Click(object sender, EventArgs e)
        {
            //check to see if the manager is not null and retrieve session contents
            if (manager != null)
            {
                txtSessionContents.Text = manager.RetrieveSessionContent();

                //set the last refresh time
                lblLastRefreshTime.Text = "Last refresh: " + DateTime.Now.ToLongTimeString();
            }
        }

        #endregion


        #region Web-Service Lateny inducing code

        protected void cmdInduceLatency_Click(object sender, EventArgs e)
        {
            //check if the service manager is not null and call with the requested delay
            if (serviceManager != null && !String.IsNullOrWhiteSpace(txtLatencyTime.Text))
            {
                //display the result and time in label
                lblDelayOperation.Text = "Operation completed at: " + DateTime.Now.ToLongTimeString() + " - Result was: " +
                    serviceManager.CallService(uint.Parse(txtLatencyTime.Text.Trim()));
            }
        }

        protected void cmdInduceLatencyAsync_Click(object sender, EventArgs e)
        {
            //register an async callback to the service manager
            RegisterAsyncTask(new PageAsyncTask(PerformServiceManageCallAsync));
        }


        protected void cmdBlockAsyncLatency_Click(object sender, EventArgs e)
        {
            //check if the service manager is not null and call with the requested delay
            if (serviceManager != null && !String.IsNullOrWhiteSpace(txtLatencyTime.Text))
            {
                //generate a deadlock
                lblDelayOperation.Text = "Operation completed at: " + DateTime.Now.ToLongTimeString() + " - Result was: " +
                    serviceManager.CallServiceAsync(uint.Parse(txtLatencyTime.Text.Trim())).Result;
            }
        }


        private async Task PerformServiceManageCallAsync()
        {
            //call the service manager asynchronously
            //and display the result and time in label
            lblDelayOperation.Text = "Operation completed at: " + DateTime.Now.ToLongTimeString() + " - Result was: " +
                await serviceManager.CallServiceAsync(uint.Parse(txtLatencyTime.Text.Trim()));
        }


        #endregion


        #region Date-Time refresh / auto-refresh code

        protected void cmdGetDateTime_Click(object sender, EventArgs e)
        {
            //set the date and time for start and end of execution
            SetDateTime();
        }


        protected void cmdAutoRefreshDateTime_Click(object sender, EventArgs e)
        {
            //enable the timer to tick
            tmrTimeRefresh.Enabled = true;

            //hide the button
            cmdAutoRefreshDateTime.Visible = false;
        }

        protected void tmrTimeRefresh_Tick(object sender, EventArgs e)
        {
            //set the date and time for start and end of execution
            SetDateTime();
        }

        private void SetDateTime()
        {
            //get the date time at the biginning of execution
            DateTime startTime = DateTime.Now;

            //display this time
            lblStartDateTime.Text = startTime.ToLongDateString() + " / " + startTime.ToLongTimeString();

            //now display the time at the end of execution
            DateTime endTime = DateTime.Now;

            //display this time
            lblEndDateTime.Text = endTime.ToLongDateString() + " / " + endTime.ToLongTimeString();
        }


        #endregion


        #region Session contents damaging code

        protected void cmdDamageSession_Click(object sender, EventArgs e)
        {
            //check if the service manager is not and attempt to damage the session
            if (manager != null)
            {
                int damagedIndex = manager.DamageSession();

                //check what we damaged
                if (damagedIndex == -1)
                {
                    lblDamagedSession.Text = "No damage done, session was empty";
                }
                else
                {
                    lblDamagedSession.Text = "Damanaged entry at index position: " + damagedIndex;
                }
            }
            else
            {
                lblDamagedSession.Text = "!!! Manager was null !!!";
            }
        }


        protected void cmdDamageSessionAsync_Click(object sender, EventArgs e)
        {
            //fire off an async task with no await so we damage the session on another worker thread
            if (manager != null)
            {
                manager.DamageSessionAsync();
                lblDamagedSession.Text = "Please wait for about 10 seconds for a surprise...";
            }
        }

        #endregion

        #region Redirection damaging code

        protected void cmdRedirect_Click(object sender, EventArgs e)
        {
            //place a variable in the session indicating where to redirect
            if (manager != null && !String.IsNullOrWhiteSpace(txtRedirectUrl.Text))
            {
                //try and create a url
                try
                {
                    Uri redirectUri = new Uri(txtRedirectUrl.Text.Trim());

                    manager.AddRedirect(txtRedirectUrl.Text.Trim());

                    lblRedirectEngaged.Text = "Redirect set to: " + txtRedirectUrl.Text.Trim();
                }
                catch (Exception ex)
                {
                    lblRedirectEngaged.Text = "!!! Failed to parse Uri !!! - :" + ex.Message;
                }
            }
        }

        #endregion
    }
}