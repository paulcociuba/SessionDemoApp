//base libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

//added imports
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;

namespace Utilities
{
    public class SessionContentManager
    {
        //private variables
        private const string sessionKeyPrefix = "sessionContent_";
        private HttpSessionState _session;

        //constructor
        public SessionContentManager(HttpSessionState session)
        {
            //save the session off to a private variable
            _session = session;
        }


        //method implementations
        public string AddContents(string content)
        {
            //get the number of current items in the session
            int itemCount = _session.Count;

            //compose a new key comprised of the word "sessionContent_" and appending the item count + 1
            string newSessionKey = sessionKeyPrefix + itemCount;

            //append information on threading
            content = PrefixContent(content);

            //add the new content to the session
            _session.Add(newSessionKey, content);

            //dump out the contents back
            return RetrieveSessionContent();
        }


        //method to add the app domain and the thread id to the content
        private string PrefixContent(string content)
        {
            //append the new string with information to existing content
            return DateTime.Now.ToShortTimeString() + " : " + " AppDomainID : " + AppDomain.CurrentDomain.Id +
            " ThreadID : " + System.Threading.Thread.CurrentThread.ManagedThreadId + " : " + content;
        }

        public string RetrieveSessionContent()
        {
            //iterate through all items in the session
            StringBuilder sBuilder = new StringBuilder();

            //check if the session is empty
            if(_session.Count == 0)
            {
                return "... Session has no content ...";
            }

            for(int i = 0; i < _session.Count; i++)
            {
                sBuilder = sBuilder.Append(_session[i].ToString() + "\r\n");
            }

            //return the newly built string to display
            return sBuilder.ToString();
        }


        public void AddRedirect(string redirectUrl)
        {
            //add the url to the session
            _session.Add("_redirectUrl", redirectUrl);
        }


        public int DamageSession()
        {
            //get the container which stores the dictionary of session items
            var _container = GetInstanceField(_session.GetType(), _session, "_container");

            //retrieve the dictionary of items
            var _sessionItems = (System.Collections.Specialized.NameObjectCollectionBase)GetInstanceField(_container.GetType(), _container, "_sessionItems");

            //get the entries array list inside the dictionary
            var _entriesArray = GetInstanceField(typeof(System.Collections.Specialized.NameObjectCollectionBase), _sessionItems, "_entriesArray");

            //get the object[] array backing up the array list
            var _items = (Object[])GetInstanceField(_entriesArray.GetType(), _entriesArray, "_items");

            //should there be no items in the sesison return -1;
            if (_items == null || _items.Length == 0)
                return -1;

            //pick a random number from 0 to the count of the _items array list
            Random r = new Random();
            int killerIndex = r.Next(0, _items.Length - 1);

            //kill the targetted item
            _items[killerIndex] = null;

            //return the index of the killed item
            return killerIndex;
        }


        public void DamageSessionAsync()
        {
            //create a task so we can run this on another thread and also, configure an await false,
            //so we can create a fire and forget solution           
            Task damageSessionTask = 
                Task.Run(() => 
                {
                    //induce a wait of 10 seconds
                    Thread.Sleep(10000);

                    //damage the session
                    DamageSession();

                    //now try and read back the contents of the session
                    string content = RetrieveSessionContent();
                });

            //fire the task away and forget about it
            damageSessionTask.ConfigureAwait(false);

        }


        public void DamageSessionAndDie()
        {
            Thread backgroundThread = new Thread(() =>
            {
                //induce a wait of 10 seconds
                Thread.Sleep(10000);

                //damage the session in an async manner
                int damagedIndex = DamageSession();

                //now try and read back the contents of the session
                string content = RetrieveSessionContent();
            })
            {
                //mark the thread as a background thread
                IsBackground = true
            };
            backgroundThread.Start();
        }


        internal static object GetInstanceField(Type type, object instance, string fieldName)
        {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindFlags);
            return field.GetValue(instance);
        }
    }
}