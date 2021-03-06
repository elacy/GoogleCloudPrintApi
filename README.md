# GoogleCloudPrintApi
A .NET wrapper for Google Cloud Print API, used for server application, currently supports only Google Cloud Print 1.0. This library is based on .NET standard 1.4, can be run on .NET Core, .NET Framework, Xamarin.iOS, Xamarin.Android & Universal Windows Platform.

### Features
* Allows printer registration to Google Cloud
* Allows printer manipulation on Google Cloud
* Allows job retrieval from Google Cloud
* Allows printer sharing to Google Accounts
* Allows subscribing to new job notification from Google Cloud (Thanks to [@Jezternz](https://github.com/Jezternz) for providing the xmpp implementation)

### Supported Platforms
* .NET Core 1.0
* .NET framework 4.6.1 or above
* Xamarin.iOS
* Xamarin.Android
* Universal Windows Platform

### How To Install
You can find the package through Nuget

	PM> Install-Package GoogleCloudPrintApi

### How To Use

* Initialization
	* [First Time Token Generation](#FirstTimeTokenGeneration)
	* [Initialize Google Cloud Print Client](#InitializeGoogleCloudPrintClient)
	* [Subscribe To Job Notification](#SubscribeToJobNotification)

* Printer Management
	* [Register Printer](#RegisterPrinter)
	* [List Printers](#ListPrinters)
	* [Get Printer Information](#GetPrinterInformation)
	* [Update Printer](#UpdatePrinter)
	* [Delete Printer](#DeletePrinter)

* Job Management
	* [Download Printed Job](#DownloadPrintedJob)

* Sharing/Unsharing
	* [Share Printer to Google User](#SharePrinter)
	* [Unshare Printer to Google User](#UnsharePrinter)

* Misc
	* [Customized Web Call Using Internal Access Token](#CustomizedCall)

<a name="FirstTimeTokenGeneration"></a>
#### First-time token generation
	var provider = new GoogleCloudPrintOAuth2Provider(clientId, clientSecret);
	var url = provider.BuildAuthorizationUrl( /* Optional redirect uri */ );
	/* Your method to retrieve authorization code from the above url */
	var token = await provider.GenerateRefreshTokenAsync(authorizationCode, /* Optional redirect uri */);
	
<a name="InitializeGoogleCloudPrintClient"></a>
#### Initialize Google Cloud Print Client
	var client = new GoogleCloudPrintClient(provider, token);

	// You can also subscribe to the OnTokenUpdated event for token update notification
	client.OnTokenUpdated += (sender, e) => 
	{
		// Do what you want with the updated token (Save the new token/ log, etc.)
	};

<a name="SubscribeToJobNotification"></a>
#### Subscribe job notification
	// You can subscibe to job notification by the following steps
	client.OnIncomingPrintJobs += (sender, e) => Console.WriteLine(e.PrinterId);
	await client.InitXmppAsync(<Your xmppJid here, e.g. 'admin' for 'admin@gmail.com'>);

	// Also, you can terminate the subscription by using the StopXmppAndCleanUp method
	client.StopXmppAndCleanUp();

<a name="RegisterPrinter"></a>
#### Register printer
	var request = new RegisterRequest
	{
		Name = name,
		Proxy = proxy,
		Capabilities = capabilities
	};
	var response = await client.RegisterPrinterAsync(request);
	
<a name="ListPrinters"></a>
#### List printers
	var request = new ListRequest { Proxy = proxy };
	var response = await client.ListPrinterAsync(request);
	
<a name="GetPrinterInformation"></a>
#### Get printer information
	var request = new PrinterRequest { PrinterId = printerId };
	var response = await client.GetPrinterAsync(request);
	
<a name="UpdatePrinter"></a>
#### Update printer
	var request = new UpdateRequest
	{
		PrinterId = printerId,
		Name = nameToUpdate
	};
	var response = await client.UpdatePrinterAsync(request);

<a name="DeletePrinter"></a>
#### Delete printer
	var request = new DeleteRequest { PrinterId = printerId };
	var response = await client.DeletePrinterAsync(request);
	
<a name="DownloadPrintedJob"></a>
#### Download printed job
	// Retrieve printed job list
	var fetchRequest = new FetchRequest { PrinterId = printerId };
	var fetchResponse = await client.FetchJobAsync(fetchRequest);
	var printJobs = fetchResponse.Jobs;
	
	// Select a printed job
	var printJob = printJobs.ElementAt(i);
	
	// Download and process ticket for the printed job

	// For printer with XPS capabilities
	var ticket = await client.GetTicketAsync(printJob.TicketUrl, proxy);

	// For printer with CDD capabilities
	var request = new TicketRequest
	{
		JobId = printJob.Id
	};
	var ticket = await client.GetCloudJobTicketAsync(request);

	/* Your method to process the ticket */
	
	// Download and save the document (in pdf format) for the printed job
	using (var documentStream = await client.GetDocumentAsync(printJob.FileUrl, proxy))
	using (var fs = File.Create(/* Your path for the document */))
	{
		documentStream.CopyTo(fs);
	}
	
	// Notify Google the printed job has completed processing
	var controlRequest = new ControlRequest
	{
		JobId = printJob.Id,
		Status = Models.Job.LegacyJobStatus.DONE
	};
	var controlResponse = await client.UpdateJobStatusAsync(controlRequest);
		
	/* Please notice that the FetchJobAsync call will behave as follows:
	
		1. If the printer does not exist in Google Cloud, throws "No print job available on specified printer." exception
		2. If there is no print job in the queue, throws "No print job available on specified printer." exception
		3. If there is job available in the queue, returns the job list.
		
	If you'd like to distinguish the difference between case 1 & 2, you'd be better off calling GetPrinterAsync before this method. It will throw you "The printer is not found" exception if the printer does not exist.
	*/

<a name="SharePrinter"></a>
#### Share printer to Google User
	var request = new ShareRequest
	{
		PrinterId = printerId,
		Scope = /* Google account you want to share to */,
		Role = Role.USER
	};
	var response = await client.SharePrinterAsync(request);

<a name="UnsharePrinter"></a>
#### Unshare printer from Google User
	var request = new UnshareRequest
	{
		PrinterId = printerId,
		Scope = /* Google account you want to unshare from */
	};
	var response = await client.UnsharePrinterAsync(request);
	
<a name="CustomizedCall"></a>
#### Customized Web Call Using the Internal Access Token
	var googClient = new GoogleCloudPrintClient(provider, token);
	using (var client = new HttpClient())
	{
		client.DefaultRequestHeaders.Add("X-CloudPrint-Proxy", proxy);
		var accessToken = (await googClient.GetTokenAsync()).AccessToken;
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
		using (var response = await client.GetAsync(/* some uri */))
		{
			/* Do what you want here for the response */		
		}
	}


### Powered by
* [Flurl](https://github.com/tmenier/Flurl) ([@tmenier](https://github.com/tmenier)) - A very nice fluent-style rest api library

### License
This library is under [MIT License](https://github.com/salmonthinlion/GoogleCloudPrintApi/blob/master/LICENSE)

### Reference
[Google Cloud Print API Reference](https://developers.google.com/cloud-print/docs/proxyinterfaces)
	
	
