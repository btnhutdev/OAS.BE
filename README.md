<a name="readme-top"></a>
<!--
*** Thanks for checking out the project. If you have a suggestion that would make this better, please fork the repo and create a pull request
*** Don't forget to give the project a star!
*** Thanks again!
-->
<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#requirements">Requirements</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#license">License</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

Project: Online Auction System (OAS)<br>
Part 1: Source Code back-end.<br>
Part 2: Source Code front-end.<br>
This is the first part of the project.

The system supports users to participate in online product auctions.<br>
Users can post products for auction.<br>
Users can also participate in product auctions.<br>
After successfully bidding on the product, users can make online payments on the system.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Built With

The project is built based on the following frameworks/libraries:

* ![C#](https://img.shields.io/badge/C%23-8A2BE2.svg?style=for-the-badge&logo=C%23)
* ![dotNET Core](https://img.shields.io/badge/.NET%20Core-purple?style=for-the-badge&logo=dotNET)
* ![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-purple?style=for-the-badge&logo=dotnet)
* ![Hangfire](https://img.shields.io/badge/Hangfire-purple?style=for-the-badge&logo=dotnet)
* ![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Sever-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
* ![Redis](https://img.shields.io/badge/Redis-black?style=for-the-badge&logo=Redis)
* ![AmazonS3](https://img.shields.io/badge/Amazon%20S3-green?style=for-the-badge&logo=Amazon%20S3)
* ![JWT](https://img.shields.io/badge/JWT-black?style=for-the-badge&logo=web%20token)
* ![RESTful API](https://img.shields.io/badge/RESTful%20API-blue?style=for-the-badge&logo=RESTful%20API)
* ![Ocelot](https://img.shields.io/badge/Ocelot-black?style=for-the-badge&logo=Ocelot)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Getting Started

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Requirements

Before you continue, ensure you meet the following requirements:
* MS SQL Server, Version = 2022
* dotNet, Version = 6.0
* Redis, Version = 3.0.504
* AWS IAM Account or Root Account
* Hangfire Core, Version = 1.8.5.0
* Gmail Account
* OS: Windows

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Installation

I am deploying the system on Docker but it is not yet completed, in the meantime you can install the system and components manually as follows:
* First you need to install dotNet 6.0 or later.
* After installation, next you install MS SQL Server version 2022 or later.
* You can install an IDE to code C# such as Visual Studio or another IDE, my project uses Visual Studio 2022.
* Install Redis version = 3.0.504 or later.
* Next, you can Clone the source code or Download the Zip file project OAS.BE.
* **Note**: This project only contains the back-end source code of the project, if you need the full project, you need to install the OAS.FE project in my github. I have a pin on my github homepage or you can refer to it [**here**](https://github.com/btnhutdev/OAS.FE)
* For the database, you have two options:
   * Using the function Import a data-tier application with the file Database.bacpac
   * Using Entity Framework Core Code First.
* You also need to customize information such as AWS account, Email Configuration, Connection Strings, JWT Token,... in the Core\appsettings.json file.
* Next install AWS CLI version 2 and log in to your AWS account with S3 access.

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- USAGE EXAMPLES -->
## Usage
* Right-click Solution in the Solution Explorer window, select Properties, select Multiple startup projects. Select action start for projects:
  * ApiGateway
  * Authen.API
  * Payment.API
  * Product.API
  * Search.API
* You can run the project in local by clicking Start in Visual Studio.
* **Note**: I have set "launchBrowser": false in projects Authen, Payment, Search, ApiGateway. So when running, it will not open a browser window. If you want to open a browser window, set "launchBrowser": true.



<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- LICENSE -->
## License
Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>
