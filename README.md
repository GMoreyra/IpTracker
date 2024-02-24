# IP Tracker

## Interview exercise Overview

This programming exercise aims to create a tool that helps coordinate response actions to fraud by using contextual information from the detected place of origin during the process of purchasing, searching, and paying. The tool should obtain associated information from an IP address.

## Features

1- Find the country to which the IP address belongs and display:
* Name and ISO code of the country
* Official languages of the country
* Current time(s) in the country (if the country covers more than one time zone, display all)
* Estimated distance between Buenos Aires and the country, in km
* Local currency and its current exchange rate in dollars (if available)

2- Provide a mechanism to query service usage statistics with the following aggregations:
* Farthest distance from Buenos Aires from which the service has been queried
* Closest distance to Buenos Aires from which the service has been queried
* Average distance of all service executions

3- Account for possible aggressive traffic fluctuations (between 100 and 1 million requests per second).

4- Use the following public APIs to obtain information:
* IP geolocation: https://ip2country.info/
* Country information: http://restcountries.eu/
* Currency information: http://fixer.io/

5- The application can be either command-line or web-based:
* In the first case, the IP should be a parameter
* In the second case, there should be a form to input the address

6- Make rational use of APIs, avoiding unnecessary calls.

7- The application may have persistent state between invocations.

8- Pay attention to the style and quality of the source code.

9- The application should be able to run and be built within a Docker container (include a Dockerfile and instructions for running it).

## Example

    > traceip 83.44.196.93

    IP: 83.44.196.93, current date: 21/11/2016 16:01:23
    Country: Spain (Spain)
    ISO Code: es
    Languages: Spanish (es)
    Currency: EUR (1 EUR = 1.0631 U$S)
    Time: 20:01:23 (UTC) 0 21:01:23 (UTC+01:00)
    Estimated distance: 10270 kms (-34, -64) to (40, -4)

--- 

# *Solution*

## Prerequisites

    .NET Framework: https://dotnet.microsoft.com/download/dotnet-framework
    Angular: https://angular.io/guide/setup-local
    Node.js: https://nodejs.org/en/download/
    Docker: https://www.docker.com/get-started
    Redis: https://redis.io/download
    RestSharp: https://restsharp.dev/getting-started/installation.html

## Getting Started

   Clone the repository:

    git clone https://github.com/GMoreyra/IPTracker.git

   Navigate to the project directory:

    cd IPTracker

   Build the Angular frontend:

    cd ClientApp
    npm install
    ng build --prod
    cd ..

   Build the .NET Framework backend:

    dotnet build

   Run Redis using Docker:

    docker-compose build

   Run the project:

    dotnet run

## Endpoints

    /iptracker/{ip}: Returns the IP information for the provided IP address.
    /iptracker/statistic: Returns the most requested IP information, sorted by the furthest and closest invocations based on the distance to Buenos Aires.
    /iptracker/average: Returns the average distance to Buenos Aires from the maximum and minimum statistics.

## Project Structure

    IPTracker: The main .NET Framework backend project.
    ClientApp: The Angular frontend project.
    Dockerfile: Docker configuration for building and running the application.

## Contributing

Feel free to submit issues or pull requests to improve the project. All contributions are welcome!
License

This project is licensed under the MIT License.
