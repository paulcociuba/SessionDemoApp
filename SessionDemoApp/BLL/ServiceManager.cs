//base libararies
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

//added imports
using SessionDemoApp.LongRestService;

namespace Utilities
{
    public class ServiceManager
    {
        //class variables
        DelayServiceSoapClient serviceClient;

        public ServiceManager()
        {
            //initialize the service client
            serviceClient = new DelayServiceSoapClient();
        }


        //methods
        public string CallService(uint delayResponse)
        {
            //attempt to perform a synchronous call of the defined time
            return serviceClient.DelayResponse((int)delayResponse);
        }

        public async Task<string> CallServiceAsync(uint delayResponse)
        {
            //attempt to perform the same operation in an async fashion
            var response = await serviceClient.DelayResponseAsync((int)delayResponse);

            //induce a further 20 second delay here
            System.Threading.Thread.Sleep(20000);

            //return the body of the response
            return response.Body.DelayResponseResult;
        }
    }
}