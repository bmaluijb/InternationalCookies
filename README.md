# InternationalCookies

![enter image description here](https://www.pluralsight.com/content/dam/pluralsight/newsroom/brand-assets/logos/pluralsight-logo-vrt-color-2.png)  

Hi! 

Welcome to the GitHub repository of the internationalcookies application.
This app is the demo app for the Pluralsight course [Building a Global App With Azure Paas](https://app.pluralsight.com/profile/author/barry-luijbregts).

You can download a copy and follow along in the course.

You can see the end result running live here: [https://www.internationalcookies.eu/](https://www.internationalcookies.eu/)

The solution consists of:

 - Website (InternationalCookies)
	 - ASP.NET Core 1.01	 
 - Database project (InternationalCookies.Data)
	 - For the database schema and seed script	 
 - ARM templates 
	 - For the creation of resources like the Web App, Azure SQL database and Redis cache for each region
		 - (InternationalCookies.Deploy-Region)
	 - For the creation of the Logic App
		 - (InternationalCookies.Deploy-Global)

Getting started
---------------

**Step 1**

Make sure that you have the following:

 - Visual Studio 2015 (or Visual Studio 2017. <a href="https://github.com/bmaluijb/InternationalCookies/wiki/Conversion-to-Visual-Studio-2017" target="_blank">See the wiki</a> for more details)
 - Azure subscription ([Try for free](https://azure.microsoft.com/en-us/free/))
 - [Azure SDK](https://azure.microsoft.com/en-us/downloads/)
 - (optional) [Azure Logic Apps Tools for Visual Studio](https://marketplace.visualstudio.com/items?itemName=VinaySinghMSFT.AzureLogicAppsToolsforVisualStudio) 

**Step 2**

Download a copy of the code and build it.

**Step 3**

Set up the resources in Azure. This application is something that runs on Azure resources and is meant to show you how Azure can help. Therefore, it depends on some Azure resources:

 3. Azure Web App
 4. Azure SQL Database
 5. Azure Redis Cache
 6. Azure Storage Account
 7. Azure Active Directory
	 - Application in Azure Active Directory
 7. **(optional)** Azure Logic App

You can choose to start with the full website (project **InternationalCookies**) or with a version of the website without authentication (project **InternationalCookiesNoAuth**).

The first one requires you to set up everything first, including Azure Active Directory. As setting up Azure Active Directory is the most difficult thing, you can start with the **InternationalCookiesNoAuth** project to get started easier. You can run the **InternationalCookiesNoAuth** project with the resources 1, 2, 3, and 4.

Numbers 1, 2, 3 and 4 can be set up by deploying the project **InternationalCookies.Deploy.Region**.

 - Simply right-click the project and click **Deploy...new**
	 - You need to connect your Azure subscription for this
 - Select or create the resource group that you want to deploy to
 - Click **OK**
 - Fill out all of the required fields and click **Save**
 - Click **OK** again to deploy
 - **Disclaimer: resources that you run in Azure can cost you money. You are responsible for these resources!**

Instructions for setting up Azure Active Directory can be found in the **Pluralsight course** in Module **5: Securing the Application and Data**.


**Step 4**

Deploy the database project **InternationalCookies.Database** to the Azure SQL database that was just deployed. This creates the database schema in the database and fills it with initial cookie stores and cookies. You can do this by doing this:

 - Right-click the project and select **Publish**
 - Select **Edit** for the Target database connection section
 - Go to the **Browse** Tab and select **Azure**
 - Your database should be listed here. Make sure that you have the right Azure sunbscription selected
 - Select the database
 - Type in the password
 - Click **Test connection**
	 - This might prompt you to add a firewall rule. If it does, add the rule	 
 - Click **OK**
 - Click **Publish**

**Step 5**

Fill the **appsettings.json** file in the **InternationalCookies** or **InternationalCookiesNoAuth** projects with the values of the the resources that you've just created:

 - "CookieDBConnection": "", 
	 - The SQL connection to the database. You can find this in the Azure Portal
		 - Navigate to your Azure SQL database in the Azure Portal
		 - Click **Show database connection string**
		 - Copy the connection string and paste it in appsettings.json
		 - Make sure to put in your username and password in the connectionstring
 - "CookieDBConnectionTemplate": "",
	 - This one is already in appsettings.json. Put your Azure SQL username and password in here
 - "RedisConnection": "",
	 - The connectionstring for Azure Redis Cache. You can find this in the Azure Portal
	 - Navigate to your Azure Redis Cache in the Azure Portal
	 - Click **show access keys**
	 - Copy the **Primary connection string** and paste it in appsettings.json
 - "AzureStorageConnection": ""
	 - The connectionstring for Azure Storage. You can find the values for it in the Azure Portal
	 - This one is already in appsettings.json
	 - Navigate to your Azure Storage Account in the Azure Portal
	 - Click Access Keys
	 - Copy the primary access key and paste it in the **AzureStorageConnection** where it says **youraccountkey**
	 - In the Azure Portal, you will find the Azure Storage Account name. Copy this and paste it in the **AzureStorageConnection** where it says **youraccountname**

Instructions for setting up Azure Active Directory can be found in the **Pluralsight course** in Module **5: Securing the Application and Data**.

