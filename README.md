## IP Tracker - .NET Framework + Angular WebApp

This project is a web application built with .NET Framework and Angular that provides information about a given IP address. It utilizes Docker for containerization and Redis for caching.

## Project Overview

IP Tracker is a web application that takes an IP address as input and returns data about that IP. It provides endpoints for fetching IP information, retrieving statistics about the most requested IP data, and calculating the average distance to Buenos Aires based on these statistics.

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
