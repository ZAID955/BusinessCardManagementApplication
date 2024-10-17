# Business Card Management Application

This is a web application for managing business card information, built with .NET Core for the backend and Angular for the frontend. The application allows users to create, view, delete,import and export business card data 

## Table of Contents
- [Technologies Used](#technologies-used)
- [Features](#features)
- [Setup](#Setup)
- [Installation](#installation)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Database Schema](#database-schema)
- [Unit Testing](#unit-testing)

## Technologies Used
- **Backend:** .NET Core 7
- **Frontend:** Angular,Bootstrap
- **Database:** MySQL - EF-Core
- **Photo Encoding:** Base64

## Features
- **Create New Business Card:** Input from the user interface.
- **View Business Cards:** List all stored business cards with options to delete.
- **Import/Export Business Cards:** Import/Export data to XML and CSV formats.
- **Optional Filtering:** Filter results by Name, Date of Birth, Phone, Gender, or Email.

## Setup
### Prerequisites
Make sure you have the following installed on your machine:
- [.NET Core SDK 7.0](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/en/download/)
- [Angular CLI](https://angular.io/cli)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) with the appropriate connection string in `appsettings.json`

## Installation
1. **Clone the repository:**
   ```bash
   git clone https://github.com/ZAID955/BusinessCardManagementApplication.git
   cd BusinessCardManagementApplication


## Usage
- Access the application by navigating to http://localhost:4200 in your browser.
- Use the provided forms to create new business cards and manage your business card collection.
- Use the filtering to refine your search results.

## API-Endpoints
**Business Card Controller**

**Create Business Card**

**POST** /api/BusinessCard/CreateBusinessCard

Request Body:
json
{
  "name": "John Doe",
  "gender": "Male",
  "dateOfBirth": "1985-01-01",
  "email": "john@example.com",
  "phone": "1234567890",
  "photo": "base64string",
  "address": "123 Street Name"
}


**Get All Business Cards**

**GET** /api/BusinessCard/GetAllBusinessCard

Response: List of all business cards in JSON format.

**Delete Business Card**

**DELETE** /api/BusinessCard/DeleteBusinessCard/{Id}

Description: Deletes the business card with the specified ID.

**Filtering**

**Get Filtered Business Cards**

**GET** /api/BusinessCard/FilterOnBusinessCard?input=Test

Parameters: string input.

**File Controller**

**Export All Business Cards To Xml File**

**POST** /api/Files/ExportBusinessCardsToXmlFile

**Export All Business Cards To Csv File**

**POST** /api/Files/ExportBusinessCardsToCsvFile

**Export A Business Card To Xml File**

**POST** /api/Files/ExportBusinessCardToXmlFile?id=1

**Export A Business Card To Csv File**

**POST** /api/Files/ExportBusinessCardToCsv?id=1

**Import Business Cards From Xml File**

**POST** /api/Files/ImportBusinessCardsFromXml

**Import Business Cards From Csv File**

**POST** /api/Files/ImportBusinessCardsFromCsv

## Database-schema

**BusinessCard Table:**

**Id** (int, primary key, auto-increment)

**Name** (string, not null)

**Gender** (enum, not null)

**DateOfBirth** (DateOnly, not null)

**Email** (string, not null)

**Phone** (string, not null)

**Photo** (string, Base64)

**Address** (string, not null)

## Unit Testing

## Overview

Unit testing is an essential part of software development that helps ensure the correctness of individual components or functions in your application. This document provides detailed information on how to run unit tests for the Business Card Management application, which is built using .NET Core and employs the xUnit testing framework.

## Getting Started

### Prerequisites

Before running the tests, ensure you have the following installed:
- [.NET SDK](https://dotnet.microsoft.com/download) (version 7.0)
- [xUnit](https://xunit.net/) (included in the test project via NuGet)
- [Moq](https://github.com/moq/moq4) (for mocking dependencies)

### Project Structure FilesControllerTests

The unit tests are located in the `BusinessCardTest` project, which includes the following key files:

- **BusinessCardServiceTests.cs**: Contains tests for the `BusinessCardService` class.
- **BusinessCardControllerTest.cs**: Contains tests for the `BusinessCardController` class.
- **FilesControllerTests.cs**  Contains tests for the `FilesController` class.

### Running the Tests

1. **Open your terminal or command prompt**.
2. Navigate to the directory containing the test project.
3. Run the following command to execute all tests:

   ```bash
   dotnet test
